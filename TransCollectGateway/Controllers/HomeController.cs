using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransCollectGateway.Common;

namespace TransCollectGateway.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITransLogUploadService _uploadService;

        public HomeController(ITransLogUploadService uploadService)
        {
            _uploadService = uploadService;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}