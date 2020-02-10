using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpPost]
        public async Task<ActionResult> UploadFile(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    if (file.ContentLength > 1048576)
                    {
                        ViewData["Error"] = "File too long!";
                        return View("Index");
                    }

                    string fileEx = Path.GetExtension(file.FileName).ToUpper();
                    TransFileFormat transFileFormat;

                    switch (fileEx)
                    {
                        case ".CSV" : transFileFormat = TransFileFormat.CSV;
                            break;
                        case ".XML" : transFileFormat = TransFileFormat.XML;
                            break;
                        default:
                            {
                                ViewData["Error"] = "Unknown format";
                                return View("Index");
                            }
                    }

                    await _uploadService.UploadTransLog(file.InputStream, transFileFormat);
                    ViewBag.Message = "File Uploaded Successfully!!";
                }

                
                return View("Index");
            }
            catch (TCGException ex)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
            catch
            {
                ViewData["Error"] = "File upload failed!";
                return View("Index");
            }
        }

    }
}