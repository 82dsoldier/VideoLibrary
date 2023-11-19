using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoLibrary.Data.Entities {
    public class Video {
        public int VideoId { get; set; }
        public string VideoPath { get; set; } = string.Empty;
        public string VideoName { get; set; } = string.Empty;
        public string ThumbnailPath { get; set; } = string.Empty;
        public string Keywords { get; set; } = string.Empty;
        public byte Rating { get; set; }
    }
}
