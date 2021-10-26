using APIGenerator.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace APIGenerator.Controllers
{
    [ApiController]
    [Route("Memes")]
    public class MemessfController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };       

        private readonly ILogger<MemessfController> _logger;
        private readonly IMapper _mapper;

        public MemessfController(ILogger<MemessfController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("Image")]
        public HttpResponseMessage GetImage()
        {
            //Image image1 = Image.FromFile("c:\\sun300.JPG");

            //byte[] imagebk = ImageTextMerge(image1, "TEST", 5, 5, 15, 15, 200, 200);

            GeneratorController cont = new GeneratorController();
            //cont.PostDownloadimageFile();
            return cont.PostDownloadimageFile();

            string fileName = "new.jpg";
            //byte[] file = File.ReadAllBytes(Server.MapPath("~/Files/" + fileName));

            //MemoryStream memStream = new MemoryStream(imagebk);
           
            //BinaryFormatter binForm = new BinaryFormatter();
            //memStream.Write(imagebk, 0, imagebk.Length);
            //memStream.Seek(0, SeekOrigin.Begin);
            //Object obj = (Object)binForm.Deserialize(memStream);


            //return imagebk;
        }


        [HttpGet]
        [Route("Index")]
        public IEnumerable<ImageDTO> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new ImageDTO
            {
                //Date = DateTime.Now.AddDays(index),
                //TemperatureC = rng.Next(-20, 55),
                //Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

        }

        public static byte[] ImageTextMerge(Image imgBack, string str, Int32 x, Int32 y, Int32 w, Int32 h, Int32 width = 200, Int32 height = 200)
        {


            using (imgBack)
            {
                using (var bitmap = new Bitmap(width, height))
                {
                    using (var canvas = Graphics.FromImage(bitmap))
                    {
                        canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        canvas.DrawImage(imgBack, new Rectangle(0, 0, width, height), new Rectangle(0, 0, width, height), GraphicsUnit.Pixel);

                        // Create font and brush
                        Font drawFont = new Font("Arial", 20);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);

                        // Create rectangle for drawing. 
                        RectangleF drawRect = new RectangleF(x, y, w, h);

                        // Draw rectangle to screen.
                        Pen blackPen = new Pen(Color.Transparent);
                        canvas.DrawRectangle(blackPen, x, y, w, h);

                        // Set format of string.
                        StringFormat drawFormat = new StringFormat();
                        drawFormat.Alignment = StringAlignment.Near;

                        // Draw string to screen.
                        canvas.DrawString(str, drawFont, drawBrush, drawRect, drawFormat);
                        canvas.Save();
                    }
                    try
                    {
                        return ImageToByteArray(bitmap);
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }

        public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }



    }
}
