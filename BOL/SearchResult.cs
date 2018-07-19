using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
   public class SearchResult
    {
        public string Path { get; set; }
        public string FileName { get; set; }

        public override string ToString()
        {
            return $"file path:{Path}, file name:{FileName}";
        }
    }
}
