using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyBot.Supporter.Main;
namespace MyBot.Supporter.Plugin
{
    /// <summary>
    /// Use this for creating customize Plugins
    /// </summary>
    public interface plugin
    {
        /// <summary>
        /// Here is the place that let you writes your code!
        /// </summary>
        void Update();
        /// <summary>
        /// The log that will show inside Supporter after all functions are completed
        /// </summary>
        string WriteLog();
        /// <summary>
        /// The function when user pressed start botting
        /// </summary>
        void Start();
        /// <summary>
        /// Telegram will send this when fixed scheduling time reached
        /// </summary>
        string FixedTelegram();
        /// <summary>
        /// Telegram will immediately send data when this string have something to return
        /// </summary>
        string InstantTelegram();
    }
    public class PLUGABLE
    {
        public static int[] CurrentGold()
        {
            return Botting.Gold;
        }
        public static int[] CurrentElixir()
        {
            return Botting.Elixir;
        }
        public static int[] CurrentDarkElixir()
        {
            return Botting.DarkElixir;
        }
    }
}
