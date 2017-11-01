using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Text.RegularExpressions;
using System.Web;

namespace seeker.Helpers
{
    public class HTMLParser
    {
        private HttpWebRequest httpRequest;

        public HTMLParser(string url)
        {
            httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "GET";
            httpRequest.ServerCertificateValidationCallback = (s, certificate, chain, err) => true;
        }

        public string Title { get; private set; }
        public List<string> Links { get; private set; }
        public List<string> Words { get; private set; }
        public bool ParseHTML()
        {
            Links = new List<string>();
            Words = new List<string>();
            try
            {
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                if (!httpResponse.ContentType.Contains("text/html"))
                    return false;
                using (var stream = httpResponse.GetResponseStream())
                {
                    HtmlDocument doc = new HtmlDocument();
                    doc.Load(stream);
                    Title = doc.DocumentNode.SelectSingleNode("/html[1]/head[1]/title[1]")?.InnerText??doc.DocumentNode.SelectSingleNode("/head[1]/title[1]")?.InnerText;
                    if (Title == null)
                        return false;
                    string text;
                    foreach(var node in doc.DocumentNode.DescendantsAndSelf())
                    {
                        if(!node.HasChildNodes && !node.ParentNode.Name.Equals("script") && !node.ParentNode.Name.Equals("style"))
                        {
                           if (!string.IsNullOrEmpty(node.InnerText))
                            {
                                text = Regex.Replace(node.InnerText, @"\t|\n|\r", " ");
                                text = DecodeHtmlEntities(text);
                                foreach (var word in text.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                                {
                                    if (!string.IsNullOrEmpty(word.Trim()))
                                        Words.Add(word.Trim());
                                }
                            } 
                        }
                    }

                    HtmlNodeCollection links = doc.DocumentNode.SelectNodes("//a[@href]");
                    if(links != null)
                    {
                        foreach (var link in links)
                        {
                            Links.Add(GetFullURL(link.Attributes["href"].Value));
                        }
                    }
                   
                }
                return true;
            }
            catch(Exception exp)
            {
                Console.WriteLine("Error getting "+ httpRequest.RequestUri.OriginalString +". "+ exp.Message);
                return false;
            }
        }

        private string DecodeHtmlEntities(string htmlText) {
            var matches = new Dictionary<string, string>();
            var regex = new Regex("(&[a-zA-Z]{2,7};)");
            foreach (Match match in regex.Matches(htmlText)) {
                if (!matches.ContainsKey(match.Value)) { 
                    var character = HttpUtility.HtmlDecode(match.Value);
                    if (character.Length == 1) {
                        matches.Add(match.Value, character);
                    }
                }
            }
            foreach (var match in matches) {
                htmlText = htmlText.Replace(match.Key, match.Value);
            }
        return htmlText;
    }
        private string GetFullURL(string href)
        {
            if (!href.Contains("http"))
            {
                if (href.StartsWith("/"))
                {
                    href = httpRequest.RequestUri.OriginalString.Substring(0, httpRequest.RequestUri.OriginalString.IndexOf(httpRequest.Host) + httpRequest.Host.Length) + href;
                }
                else
                {
                    href = httpRequest.RequestUri.OriginalString.Substring(0, httpRequest.RequestUri.OriginalString.LastIndexOf("/") + 1) + href;
                }
            }
            return href;
        }

    }
}
