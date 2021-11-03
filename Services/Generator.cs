using APIGenerator.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace APIGenerator.Services
{
    public class Generator : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public byte[] ImageTextMerge(ImageDTO imageDTO, Int32 x, Int32 y, Int32 w, Int32 h, Int32 width = 200, Int32 height = 200)
        {
            var file = imageDTO.ImageFile;
            imageDTO.ImageName = file.FileName;
            string s;
            byte[] fileBytes; 

            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
                imageDTO.Image = Convert.ToBase64String(fileBytes);
            }

            Image imgBack;
            using (var ms = new MemoryStream(fileBytes))
            {
                imgBack = Image.FromStream(ms);
            }

            using (imgBack)
            {
                using (var bitmap = new Bitmap(width, height))
                {
                    using (var canvas = Graphics.FromImage(bitmap))
                    {
                        canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        canvas.DrawImage(imgBack, new Rectangle(0, 0, width, height), new Rectangle(0, 0, width, height), GraphicsUnit.Pixel);

                        // Create font and brush
                        Font drawFont = new Font("Arial", 35);
                        SolidBrush drawBrush = new SolidBrush(Color.White);

                        // Create rectangle for drawing. 
                        RectangleF drawRect = new RectangleF(x, y, w, h);

                        // Draw rectangle to screen.
                        Pen blackPen = new Pen(Color.AliceBlue);
                        canvas.DrawRectangle(blackPen, x, y, w, h);

                        // Set format of string.
                        StringFormat drawFormat = new StringFormat();
                        drawFormat.Alignment = StringAlignment.Center;

                        // Draw string to screen.
                        canvas.DrawString(imageDTO.TextImage, drawFont, drawBrush, drawRect, drawFormat);
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
