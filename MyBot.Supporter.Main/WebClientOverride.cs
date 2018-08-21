using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace MyBot.Supporter.Main
{
    class WebClientOverride:WebClient
    {
        protected override WebRequest GetWebRequest(Uri address)
        {
            Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36");
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebRequest wr = base.GetWebRequest(address);
            wr.Timeout = 5000; // timeout in milliseconds (ms)
            return wr;
        }
    }
}
