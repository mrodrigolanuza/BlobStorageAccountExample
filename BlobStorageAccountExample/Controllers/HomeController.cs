using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlobStorageAccountExample.Models;
using BlobStorageAccountExample.Services;
using Microsoft.AspNetCore.Http;

namespace BlobStorageAccountExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMyBlobManager _blobStorage;

        public HomeController(IMyBlobManager blobStorage) {
            _blobStorage = blobStorage;
        }

        public IActionResult Index() {
            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]  //Un fichero ser envía por POST, con lo que hay que indicarlo. Si no indicamos nada, por defecto es GET.
        public async Task<IActionResult> UploadFile(IFormFile file) {
            await _blobStorage.UploadFile(file);
            return RedirectToAction("Index");
        }
    }
}
