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
        public byte[] Image { get; set; }

    }
}
