using System;
using System.Net.Http;
using System.Threading.Tasks;
using newshangfire.Data;
using newshangfire.Models;
using Newtonsoft.Json;

namespace newshangfire.Jobs
{
    public class NewsApi
    {
        private ApplicationDbContext _context;

        public NewsApi(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<string> run()
        {

            var client = new HttpClient();

            var result = await client.GetAsync("https://newsapi.org/v2/top-headlines?country=gb&apiKey=a0475e2114d949738c313f5f08d5ad62");

            string v = await result.Content.ReadAsStringAsync();

            var model = JsonConvert.DeserializeObject<NewsApiResponse>(v);

            foreach (Article article in model.Articles)
            {

                await _context.AddAsync(article);

            }

            await _context.SaveChangesAsync();

            return JsonConvert.SerializeObject(model.Articles);

        }
    }
}
