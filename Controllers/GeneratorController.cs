using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace APIGenerator.Controllers
{
    public class GeneratorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //[System.Web.Http.HttpPost]
        public HttpResponseMessage PostDownloadimageFile() //([System.Web.Http.FromBody] string imagename)
        {
            try
            {
                string downloadPath = "C:\\Users\\dario.almeida\\Documents\\sun300.jpg";
                string downloadNewPath = "C:\\Users\\dario.almeida\\Documents\\sun3001.jpg";

                if (!System.IO.File.Exists(downloadPath))
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                FileStream fileStream = new FileStream(downloadPath, FileMode.Open);
                Image image = Image.FromStream(fileStream);
                MemoryStream memoryStream = new MemoryStream();
                image.Save(memoryStream, ImageFormat.Jpeg);

                HttpResponseMessage response = new HttpResponseMessage();
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new ByteArrayContent(memoryStream.ToArray());
                //response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                string contentDisposition = string.Concat("attachment; filename=", "test.jpg");
                response.Content.Headers.ContentDisposition =
                              ContentDispositionHeaderValue.Parse(contentDisposition);
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

                return response;
            }
            catch(Exception ex)
            {
                HttpResponseMessage response = new HttpResponseMessage();
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }
    }
}
