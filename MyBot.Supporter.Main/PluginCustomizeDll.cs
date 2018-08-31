using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyBot.Supporter.Plugin
{
    public interface plugin
    {
        /// <summary>
        /// function is the place that let you writes your code!
        /// </summary>
        void function();
        /// <summary>
        /// The log that will show inside Supporter after all functions are completed
        /// </summary>
        string WriteLog();
        /// <summary>
        /// The function should be run on pressing Start Botting Button only or keep running after Start Botting clicked
        /// </summary>
        /// <returns></returns>
        bool RunOnce();

    }
}
