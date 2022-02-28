using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaptcha.Model
{
    public class KaptchaResult
    {
        public string Message { get; set; }
        public int KaptchaSumResult { get; set; }
        public KaptchaFile ImageFile { get; set; }
    }
}
