using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace InMemoryApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMemoryCache _cache;
        public ProductController(IMemoryCache cache)
        {
            _cache = cache;
        }

        public IActionResult Index()
        {
            //1.yol
            //if (String.IsNullOrEmpty(_cache.Get<string>("zaman")))
            //{
            //    _cache.Set<string>("zaman", DateTime.Now.ToString());
            //}
            //2.yol
            if(!_cache.TryGetValue("zaman",out string zamancache))
            {
                _cache.Set<string>("zaman", DateTime.Now.ToString());
            }
            
            return View();
        }
        public IActionResult Show()
        {
            // _cache.Remove("zaman");//cache de bu key değerine sahip tüm dataları siler
            _cache.GetOrCreate<string>("zaman", entry =>
            {
                return DateTime.Now.ToString();
            });
            ViewBag.zaman = _cache.Get<string>("zaman");
            return View();
        }
    }
}
