using CKEditor5.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;
namespace CKEditor5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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

        [HttpPost]
        public async Task<JsonResult> ImageTestAsync(IFormFile Files)
        {

            var SaveDir = "D:\\HRPORTAL\\Attachment\\QAQuestion";
            FileInfo _FileInfo = new FileInfo(Files.FileName);
            var FileExt = _FileInfo.Extension;
            var newID = $"{_FileInfo.Name}";

            SaveFile(Files, SaveDir!, newID);

            //若ret=-1即上傳失敗，直接跳出迴圈
           
            return Json(200);
        }

        public static int SaveFile(IFormFile FormFile, string SaveDir, string NewFileName = "")
        {
            Stream _Stream = null!;
            try
            {
                if (FormFile.Length > 0)
                {
                    var DirPath = (NewFileName == string.Empty) ? Path.Combine(SaveDir, FormFile.FileName) : Path.Combine(SaveDir, NewFileName);

                    if (!Directory.Exists(SaveDir))
                        Directory.CreateDirectory(SaveDir);

                    using (_Stream = System.IO.File.Create(DirPath))
                        FormFile.CopyTo(_Stream);

                    return 1;
                }
                return -1;
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                if (_Stream != null)
                    _Stream.Close();
            }
        }
    }
}