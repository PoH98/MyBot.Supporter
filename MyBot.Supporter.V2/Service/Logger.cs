using System;
using System.IO;
using System.Text;

namespace MyBot.Supporter.V2.Service
{
    public class Logger
    {
        private string FilePath { get; set; }
        private FileStream logStream { get; set; }
        private static Logger _instance { get; set; }
        private Logger()
        {
            if (!Directory.Exists("Supporter"))
            {
                Directory.CreateDirectory("Supporter");
            }
            FilePath = Path.Combine("Supporter", DateTime.Now.ToString("dd-MM-yyyyy") + ".log");
            if (!File.Exists(FilePath))
            {
                logStream = File.Create(FilePath);
            }
            else
            {
                logStream = File.OpenWrite(FilePath);
            }
        }
        /// <summary>
        /// Write Log
        /// </summary>
        /// <param name="log"></param>
        public void Write(string log)
        {
            var data = Encoding.UTF8.GetBytes(log + "\n");
            logStream.Write(data, 0, data.Length);
        }
        /// <summary>
        /// Get Log Instance
        /// </summary>
        public static Logger Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Logger();
                }
                return _instance;
            }
        }
    }
}
