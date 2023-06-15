using Microsoft.AspNetCore.Mvc;
using MVC_Localization_SQL.Models;
using System.Diagnostics;
using System.Resources;
using System.Resources.NetStandard;


namespace MVC_Localization_SQL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly TeachContext _db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, TeachContext db, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult UpdateLang()
        {
            //string webRootPath = _webHostEnvironment.WebRootPath;
            //string contentRootPath = _webHostEnvironment.ContentRootPath;
            //string path = "";
            //path = Path.Combine(webRootPath, "~Resources\\WebResource.resx");
            //ResXResourceWriter WriterTW = new ResXResourceWriter(path);
            //WriterTW.AddResource("a", "b");
            ResXResourceWriter WriterTW = new ResXResourceWriter(@".\Resources\WebResource.resx");
            ResXResourceWriter WriterUS = new ResXResourceWriter(@".\Resources\WebResource.en-US.resx");

            var List = from m in _db.Language
                       select m;
            foreach (var item in List)
            {
                WriterTW.AddResource(item.LangKey, item.LangZhTw);
                WriterUS.AddResource(item.LangKey, item.LangEnUs);
            }
            WriterTW.Dispose();
            WriterUS.Dispose();

            //Dispose釋放資源

            //resx.AddResource("Title", "Classic American Cars");
            //resx.AddResource("HeaderString1", "Make");


            //ResourceWriter WriterUS = new ResourceWriter("~/Resources/WebResource.resx");
            //var List = from m in _db.Language
            //           select m;

            //foreach (var item in List)
            //{
            //    //WriterTW.AddResource(item.LangKey, item.LangZhTw);
            //    //WriterUS.AddResource(item.LangKey, item.LangEnUs);
            //}

            return RedirectToAction("Index", "Home");
        }
    }
}