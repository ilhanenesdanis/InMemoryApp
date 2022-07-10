using Microsoft.AspNetCore.Mvc;
using RedisExchageApi.Web.Services;
using StackExchange.Redis;

namespace RedisExchageApi.Web.Controllers
{
    public class StringTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase db;
        public StringTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(0);
        }
        public IActionResult Index()
        {
            db.StringSet("name", "İlhan enes daniş");
            db.StringSet("ziyaretçi", 150);

            return View();
        }
        public IActionResult Show()
        {
            var value = db.StringGet("name");
            db.StringIncrement("ziyaretçi", 1);
            if (value.HasValue)
                ViewBag.value = value;
            return View();
        }
    }
}
