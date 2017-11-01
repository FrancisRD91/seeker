using System.Collections.Generic;
using System.Net.Http;
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
	public class IndexController: Controller
    {
        private MongoContext context;
        private List<IndexModel> indexes;
        private Dictionary<string,bool> indexDic;
        public IndexController(MongoContext context)
        {
            this.context = context;
        }

        [Route("~/api/indexes")]  
        [HttpGet]
        public List<IndexModel> GetAllIndexesFromDB() => context.db.GetCollection<IndexModel>("index")
            .Find(x => true).ToList();
         
        [HttpPost]
        public IndexResponse Create([FromBody] IndexModel index)
        {
            var response = new IndexResponse();
            if (string.IsNullOrEmpty(index.URL))
            {
                return response;
            }
            indexes = GetAllIndexesFromDB();
            var _indexes = new List<IndexModel>();
            indexDic = new Dictionary<string, bool>();  
            
            HTMLParser htmlParser = null;
            ProcessURL(htmlParser, index.URL, _indexes, 1);
            response.Indexes = _indexes.Count;
            response.Words = 0;
            foreach (var _index in _indexes)
            {
                context.db.GetCollection<IndexModel>("index").InsertOne(_index);
                response.Words+=_index.Words.Length;
            }
            response.Success = true;
            return response;
        }

        [HttpDelete]
        public IndexResponse Delete()
        {
            var response = new IndexResponse();
            context.db.GetCollection<IndexModel>("index").DeleteMany(new BsonDocument());
            response.Success = true;
            return response;
        }

        private bool Exist(string url, List<IndexModel> indexes)
        {
            int i = 0;
            while (i < indexes.Count && !indexes[i].URL.Equals(url))
            {
                i++;
            }
            return i != indexes.Count;
        }

        private void ProcessURL(HTMLParser htmlParser, string url, List<IndexModel> indexes, int level)
        {
            if(!Uri.IsWellFormedUriString(url,UriKind.Absolute))
                return;
            if (Exist(url, this.indexes) || indexDic.ContainsKey(url))
                return;
            htmlParser = new HTMLParser(url);

            indexDic.Add(url,htmlParser.ParseHTML());
            if (indexDic[url])
            { 
                indexes.Add(new IndexModel() { URL = url, Title = htmlParser.Title, Words = htmlParser.Words.ToArray() });
                if(level < 3)
                {
                    foreach (var link in htmlParser.Links)
                    {
                        ProcessURL(htmlParser, link, indexes, level+1);
                    }
                }
            }
        }
    }
}