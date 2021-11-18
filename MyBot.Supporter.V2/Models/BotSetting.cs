using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MyBot.Supporter.V2.Models
{
    public class SupporterSettings
    {
        public BotSettings Bots { get; set; }
        public bool Mini { get; set; }
    }

    public class BotSettings:List<BotSetting>
    {

    }

    public class BotSetting
    {
        [JsonIgnore]
        public int? Id { get; set; }
        public bool IsEnabled { get; set; } = true;
        public string ProfileName { get; set; }
        public Emulator Emulator { get; set; }
        public string Instance { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; } = new TimeSpan(23, 59, 59);
    }

    public enum Emulator 
    {
        Bluestacks,
        MEmu,
        ITools,
        Nox,
        Bluestacks2
    }
}
