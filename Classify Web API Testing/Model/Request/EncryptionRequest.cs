using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classify_Web_API_Testing.Model.Request
{
    public class EncryptionRequest
    {
        public string data { get; set; }
        public string key { get; set; }
    }
}
