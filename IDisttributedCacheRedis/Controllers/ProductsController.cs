using IDisttributedCacheRedis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

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
            _cache.SetString("namess", "İLHAN ENES DANİŞ", entryOptions);
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
        #region Complex type cache
        public IActionResult ComplexTypeCache()
        {
            _cache.Remove("product:1");
            //Json serialize edilerek cache işlemi
            List<Product> products = new List<Product>()
            {
                new Product(){Id=1,Name="Klavye",Price=150},
                new Product(){Id=2,Name="Leptop",Price=12000},
                new Product(){Id=3,Name="Mouse",Price=10},
                new Product(){Id=4,Name="Monitör",Price=1500},
            };
            //json Olarak Cache katdetme
            string jsonProduct = JsonConvert.SerializeObject(products);
            //_cache.SetString("product:1", jsonProduct);

            Byte[] bypeProduct = Encoding.UTF8.GetBytes(jsonProduct);
            _cache.Set("product:2", bypeProduct);
            return View();
        }
        public IActionResult GetComplexType()
        {
            var data = _cache.GetString("product:1");
            List<Product> p=JsonConvert.DeserializeObject<List<Product>>(data);
            ViewBag.list = p;
            return View();
        }
        #endregion
    }
}
