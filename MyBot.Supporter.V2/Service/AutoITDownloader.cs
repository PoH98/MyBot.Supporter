using MyBot.Supporter.V2.Helper;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MyBot.Supporter.V2.Service
{
    public class AutoITDownloader
    {
        public async Task DownloadAutoIT()
        {
            HttpClient hc = new HttpClient();
            var productValue = new ProductInfoHeaderValue("MyBot.Supporter.UpdateChecker", "1.0");
            var commentValue = new ProductInfoHeaderValue("(+https://github.com/PoH98/MyBot.Supporter/)");
            hc.DefaultRequestHeaders.UserAgent.Add(productValue);
            hc.DefaultRequestHeaders.UserAgent.Add(commentValue);
            var stream = await hc.GetStreamAsync(new Uri("https://github.com/PoH98/MyBot.Supporter/raw/v2/MyBot.Supporter.V2/AutoIT.zip"));
            using (var fileStream = File.Create("AutoIT.zip"))
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fileStream);
            }
            ZipExtract ex = new ZipExtract();
            ex.Extract("AutoIT.zip");
        }
    }
}
