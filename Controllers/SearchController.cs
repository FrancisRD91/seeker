using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using seeker.Models;
using seeker.Services;
using seeker.Helpers;
using HtmlAgilityPack;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System;

namespace seeker.Controllers
{
    [Produces("application/json")]
	[Route("api/[controller]")]
	public class SearchController: Controller
    {
        private MongoContext context;
        private List<SearchModel> searches;
        private SearchModel search;
        private List<IndexModel> index;
        public SearchController(MongoContext context)
        {
            this.context = context;
        }

        [Route("~/api/searches")]  
        [HttpGet]
        public List<SearchModel> GetAllSearchesFromDB() => context.db.GetCollection<SearchModel>("search")
            .Find(x => true).ToList();
         
        [HttpPost]
        public IEnumerable<SearchResponse> Search([FromBody] SearchModel search)
        {
            var _searches = new List<SearchResponse>(); 
            if (string.IsNullOrEmpty(search.Word))
            {
                return _searches;
            }

            this.searches = GetAllSearchesFromDB();

            DoSearch(search.Word, _searches);
            return _searches;
        }

        private void DoSearch(string word, List<SearchResponse> searches)
        {
            index = context.db.GetCollection<IndexModel>("index").Find(x => true).ToList();
            foreach (var item in index)
            {
                search = GetSearch(word,item.URLId);
                if(search == null)
                {
                    search = new SearchModel()
                    {
                        Word = word,
                        URLId = item.URLId
                    };
                    foreach (var _word in item.Words)
                    {
                        if(word == _word)
                            search.Count++;
                    }
                    context.db.GetCollection<SearchModel>("search").InsertOne(search);
                }
                searches.Add(new SearchResponse()
                                {
                                    Title = item.Title,
                                    Url = item.URL,
                                    Occurrences = search.Count
                                });
            }
        }

        private SearchModel GetSearch(string word, ObjectId urlId)
        {
            int i = 0;
            while (i < searches.Count && !searches[i].Word.Equals(word) && searches[i].URLId.CompareTo(urlId)!=0)
            {
                i++;
            }
            return (i < searches.Count)?searches[i]:null;
        }
    }
}