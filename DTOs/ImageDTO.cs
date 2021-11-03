using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGenerator.DTOs
{
    public class ImageDTO
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string Image { get; set; }
        public string TextImage { get; set; }
        public IFormFile ImageFile { get; set; }
        
    }
}
