using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkLoad {
    internal class Settings {
        public string FFMPEGPath { get; set; } = string.Empty;
        public string InputPath { get; set; } = string.Empty;
        public string OutputPath { get; set; } = string.Empty;
        public string ThumbnailPath { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
    }
}
