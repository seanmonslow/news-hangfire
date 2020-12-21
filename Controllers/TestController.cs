using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using newshangfire.Data;
using newshangfire.Models;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace newshangfire.Controllers
{
    [Route("/test")]
    public class TestController : Controller
    {
        private ApplicationDbContext _context;

        public TestController(ApplicationDbContext context)
        {
            this._context = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("welcome")]
        public IActionResult Welcome()
        {
            string jobId = BackgroundJob.Enqueue(() => SendWelcomeMail());
            return Ok($"Job Id {jobId} Completed. Welcome Mail Sent!");
        }

        public async Task<string> SendWelcomeMail()
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

            //return Ok("It's done");
        }
    }
}
