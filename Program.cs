using HtmlAgilityPack;

using System;
using System.Linq.Expressions;

namespace Application
{
    class Program
    {
        static void Main(string[] args)
        {
            var results = Scrapper("Slug for PCP gun Australia", 15);
            foreach (var result in results)
            {
                Console.WriteLine(result.Title + "  ---  "+result.Url);
            }
        }
        public class Item
        {
            public string Url { get; set; }
            public string Title { get; set; }
        }
        public static List<Item> Scrapper(string query, int pages)
        {
            var list = new List<Item>();

            for (int i = 0; i <= pages; i++)
            {
                var url = "http://www.google.com/search?q=" + query + " &num=100&start=" + ((i - 1) * 10).ToString();
                HtmlWeb web = new HtmlWeb();
                //Mozilla/5.0 (X11; U; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.5110.82 Safari/537.36
                web.UserAgent = "user-agent=Mozilla/5.0" +
                " (X11; U; Linux x86_64) " +
                " AppleWebKit/537.36 (KHTML, like Gecko) " +
                " Chrome/101.0.5110.82 Safari/537.36 ";
                var htmlDoc = web.Load(url);
                try
                {
                    HtmlNodeCollection nodes = htmlDoc.DocumentNode.SelectNodes("//div[@class='yuRUbf']");
                    foreach (var tag in nodes)
                    {
                        var result = new Item();

                        result.Url = tag.Descendants("a").FirstOrDefault().Attributes["href"].Value;
                        result.Title = tag.Descendants("h3").FirstOrDefault().InnerText;
                        list.Add(result);
                    }
                }
                catch (Exception ex)
                {
                   
                }
            }

            return list;
        }
    }
}