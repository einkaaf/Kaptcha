using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaptcha.Model
{
    public class KaptchaFile
    {
        public KaptchaFile(byte[] fileContents, string contentType, string fileName)
        {
            FileContents = fileContents;
            ContentType = contentType;
            FileName = fileName;
        }
          public KaptchaFile()
        {
           
        }

        public byte[] FileContents { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }
}
