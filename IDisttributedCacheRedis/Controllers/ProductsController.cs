using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;

namespace IDisttributedCacheRedis.Controllers
{
    public class ProductsController : Controller
    {
        private IDistributedCache _cache;
        public ProductsController(IDistributedCache cache)
        {
            _cache = cache;
        }
        public IActionResult Index()
        {
            //**************************
            /*Redis Cache Data ve datanin cache süresini ekleme işlemi*/
            DistributedCacheEntryOptions entryOptions = new DistributedCacheEntryOptions();
            entryOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(1);
            _cache.SetString("namess","İLHAN ENES DANİŞ", entryOptions);
            //**************************

            return View();
        }
        public IActionResult Show()
        {
            //**************
            //cache üzerinden data okuma işlemi
            string name = _cache.GetString("namess");
            //**************
            return View();
        }
        public IActionResult Remove()
        {
            _cache.Remove("namess");
            return View();
        }
    }
}
