using AdaTech.AIntelligence.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaTech.AIntelligence.Entities.Objects
{
    public class Image
    {
        public int Id { get; set; }
        public byte[]? ByteImage { get; set; }
        public string Path { get; set; }
        public ImageSourceType ImageSourceType { get; set; }
    }
}
