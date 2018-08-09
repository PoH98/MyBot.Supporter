﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MyBot.Supporter.Main
{
    public class Languages
    {
        #region Form1
        public static string _NET4_5Needed;
        public static string AddNewProfile_Button;
        public static string AdsBlock_CheckBox;
        public static string AllSelected;
        public static string AutoUpdateFound;
        public static string AddTiles_Tips;
        public static string Battery;
        public static string BotNameNeeded;
        public static string Builders;
        public static string Cancel;
        public static string Close_LID_CheckBox;
        public static string CloseRunningBot_Button;
        public static string CopySettings_Button;
        public static string CSV_Writer_Button;
        public static string CSVNameNeeded;
        public static string CSV_NotFound;
        public static string CustomizeMODFoundTitle;
        public static string CustomizeMODFound;
        public static string CurrentBotCount;
        public static string CurrentBotCountFailed;
        public static string DarkElixir;
        public static string DestroyingPerformanceMode_Button;
        public static string DockBot_Button;
        public static string DropPoints_Tips;
        public static string EarnedDarkElixirPH;
        public static string EarnedDarkElixir;
        public static string EarnedElixirPH;
        public static string EarnedElixir;
        public static string EarnedGoldPH;
        public static string EarnedGold;
        public static string EarnedTrophyPH;
        public static string EarnedTrophy;
        public static string EditMyBot;
        public static string Elixir;
        public static string Error;
        public static string F1GroupBox1;
        public static string F1GroupBox2;
        public static string F1Label01;
        public static string F1Label02;
        public static string F1Label03;
        public static string F1Label04;
        public static string F1Label05;
        public static string F1Label06;
        public static string F1Label07;
        public static string F1Label08;
        public static string F1Label09;
        public static string F1Label10;
        public static string F1Label11;
        public static string F1TabPage5;
        public static string F1TabPage7;
        public static string F1TabPage8;
        public static string F1TabPage10;
        public static string F1TabPage11;
        public static string Form1;
        public static string Gold;
        public static string HideBot_CheckBox;
        public static string HideEmulator_CheckBox;
        public static string HideFromTaskBar_CheckBox;
        public static string HighPerformanceMode_Button;
        public static string Hour;
        public static string Injector_Button;
        public static string InvalidProfile;
        public static string InvalidToken;
        public static string LowPerformanceMode_Button;
        public static string MBResources;
        public static string Minute;
        public static string NoneProfileSelected;
        public static string NormalPerformanceMode_Button;
        public static string Notification;
        public static string NoNetwork;
        public static string NumbersOnly;
        public static string OnPower;
        public static string OverWriteConfirmation;
        public static string OverwriteSuccess;
        public static string OCR_Failed;
        public static string Priority_CheckBox;
        public static string ProfileNotFound1;
        public static string ProfileNotFound2;
        public static string QuotaLimit_CheckBox;
        public static string RAMCleaner_CheckBox;
        public static string Regenerate_Button;
        public static string RestartBot_CheckBox;
        public static string SameProfileSelected;
        public static string ScheduledShutdown_CheckBox;
        public static string Second;
        public static string SelectEmulatorNeeded;
        public static string Side_Tips;
        public static string ShutdownWhenLimitReached_CheckBox;
        public static string ShutdownWhenNoNetwork_CheckBox;
        public static string Start_Button;
        public static string StartSelectedMyBot_Button;
        public static string StopBotOnBattery_CheckBox;
        public static string Stop_Button;
        public static string SelectEmulator;
        public static string SupporterStarting;
        public static string TelegramFrequency;
        public static string TelegramSupporterClose;
        public static string TelegramSupporterStop;
        public static string TelegramTokenUpdate_Button;
        public static string Telegram_BotClose_CheckBox;
        public static string Telegram_BotStart_CheckBox;
        public static string Telegram_ClosingSupporter_CheckBox;
        public static string Telegram_RunningBotCount_CheckBox;
        public static string Telegram_RunningBotEarned_CheckBox;
        public static string Telegram_RunningBotStatus_CheckBox;
        public static string Telegram_StartBotting_CheckBox;
        public static string Telegram_StopBotting_CheckBox;
        public static string TelegramHelpMessage;
        public static string Telegram_BotNotInTime1;
        public static string Telegram_BotNotInTime2;
        public static string Trophy;
        public static string UpdateNow;
        #endregion
        //Injector
        public static string Form2;
        public static string Village;
        public static string AttackPlan;
        public static string Profile_ForInject;
        public static string DestructionChallenges;
        public static string DonateTroops;
        public static string DonateSpells;
        public static void LoadLanguage()
        {
            _NET4_5Needed = en_Lang._Net4_5Needed;
            AddNewProfile_Button = en_Lang.AddNewProfile_Button;
            AdsBlock_CheckBox = en_Lang.AdsBlock_CheckBox;
            AllSelected = en_Lang.AllSelected;
            AutoUpdateFound = en_Lang.AutoUpdateFound;
            AddTiles_Tips = en_Lang.AddTiles_Tips;
            Battery = en_Lang.Battery;
            BotNameNeeded = en_Lang.BotNameNeeded;
            Builders = en_Lang.Builders;
            Cancel = en_Lang.Cancel;
            Close_LID_CheckBox = en_Lang.Close_LID_CheckBox;
            CloseRunningBot_Button = en_Lang.CloseRunningBot_Button;
            CopySettings_Button = en_Lang.CopySettings_Button;
            CSV_Writer_Button = en_Lang.CSV_Writer_Button;
            CSVNameNeeded = en_Lang.CSVNameNeeded;
            CSV_NotFound = en_Lang.CSV_NotFound;
            CustomizeMODFoundTitle = en_Lang.CustomizeMODFoundTitle;
            CustomizeMODFound = en_Lang.CustomizeMODFound;
            CurrentBotCount = en_Lang.CurrentBotCount;
            CurrentBotCountFailed = en_Lang.CurrentBotCountFailed;
            DarkElixir = en_Lang.DarkElixir;
            DestroyingPerformanceMode_Button = en_Lang.DestroyingPerformanceMode_Button;
            DestructionChallenges = en_Lang.DestructionChallenge;
            DockBot_Button = en_Lang.DockBot_Button;
            DropPoints_Tips = en_Lang.DropPoints_Tips;
            EarnedDarkElixirPH = en_Lang.EarnedDarkElixirPH;
            EarnedDarkElixir = en_Lang.EarnedDarkElixir;
            EarnedElixirPH = en_Lang.EarnedElixirPH;
            EarnedElixir = en_Lang.EarnedElixir;
            EarnedGoldPH = en_Lang.EarnedGoldPH;
            EarnedGold = en_Lang.EarnedGold;
            EarnedTrophyPH = en_Lang.EarnedTrophyPH;
            EarnedTrophy = en_Lang.EarnedTrophy;
            EditMyBot = en_Lang.EditMyBot;
            Elixir = en_Lang.Elixir;
            Error = en_Lang.Error;
            F1GroupBox1 = en_Lang.F1GroupBox1;
            F1GroupBox2 = en_Lang.F1GroupBox2;
            F1Label01 = en_Lang.F1Label01;
            F1Label02 = en_Lang.F1Label02;
            F1Label03 = en_Lang.F1Label03;
            F1Label04 = en_Lang.F1Label04;
            F1Label05 = en_Lang.F1Label05;
            F1Label06 = en_Lang.F1Label06;
            F1Label07 = en_Lang.F1Label07;
            F1Label08 = en_Lang.F1Label08;
            F1Label09 = en_Lang.F1Label09;
            F1Label10 = en_Lang.F1Label10;
            F1Label11 = en_Lang.F1Label11;
            F1TabPage5 = en_Lang.F1TabPage5;
            F1TabPage7 = en_Lang.F1TabPage7;
            F1TabPage8 = en_Lang.F1TabPage8;
            F1TabPage10 = en_Lang.F1TabPage10;
            F1TabPage11 = en_Lang.F1TabPage11;
            Form1 = en_Lang.Form1;
            Gold = en_Lang.Gold;
            HideBot_CheckBox = en_Lang.HideBot_CheckBox;
            HideEmulator_CheckBox = en_Lang.HideEmulator_CheckBox;
            HideFromTaskBar_CheckBox = en_Lang.HideFromTaskBar_CheckBox;
            HighPerformanceMode_Button = en_Lang.HighPerformanceMode_Button;
            Hour = en_Lang.Hour;
            Injector_Button = en_Lang.Injector_Button;
            InvalidProfile = en_Lang.InvalidProfile;
            InvalidToken = en_Lang.InvalidToken;
            LowPerformanceMode_Button = en_Lang.LowPerformanceMode_Button;
            MBResources = en_Lang.MBResources;
            Minute = en_Lang.Minute;
            NoneProfileSelected = en_Lang.NoneProfileSelected;
            NoNetwork = en_Lang.NoNetwork;
            NormalPerformanceMode_Button = en_Lang.NormalPerformanceMode_Button;
            Notification = en_Lang.Notification;
            NumbersOnly = en_Lang.NumbersOnly;
            OnPower = en_Lang.OnPower;
            OverWriteConfirmation = en_Lang.OverWriteConfirmation;
            OverwriteSuccess = en_Lang.OverWriteSuccess;
            OCR_Failed = en_Lang.OCR_Failed;
            Priority_CheckBox = en_Lang.Priority_CheckBox;
            ProfileNotFound1 = en_Lang.ProfileNotFound1;
            ProfileNotFound2 = en_Lang.ProfileNotFound2;
            QuotaLimit_CheckBox = en_Lang.QuotaLimit_CheckBox;
            RAMCleaner_CheckBox = en_Lang.RAMCleaner_CheckBox;
            Regenerate_Button = en_Lang.Regenerate_Button;
            RestartBot_CheckBox = en_Lang.RestartBot_CheckBox;
            SameProfileSelected = en_Lang.SameProfileSelected;
            ScheduledShutdown_CheckBox = en_Lang.ScheduledShutdown_CheckBox;
            Second = en_Lang.Second;
            SelectEmulatorNeeded = en_Lang.SelectEmulatorNeeded;
            ShutdownWhenLimitReached_CheckBox = en_Lang.ShutdownWhenLimitReached_CheckBox;
            ShutdownWhenNoNetwork_CheckBox = en_Lang.ShutdownWhenNoNetwork_CheckBox;
            Start_Button = en_Lang.Start_Button;
            StartSelectedMyBot_Button = en_Lang.StartSelectedMyBot_Button;
            StopBotOnBattery_CheckBox = en_Lang.StopBotOnBattery_CheckBox;
            SupporterStarting = en_Lang.SupporterStarting;
            Side_Tips = en_Lang.Side_Tips;
            Stop_Button = en_Lang.Stop_Button;
            SelectEmulator = en_Lang.SelectEmulator;
            TelegramFrequency = en_Lang.TelegramFrequency;
            TelegramSupporterClose = en_Lang.TelegramSupporterClose;
            TelegramSupporterStop = en_Lang.TelegramSupporterStop;
            TelegramTokenUpdate_Button = en_Lang.TelegramTokenUpdate_Button;
            Telegram_BotClose_CheckBox = en_Lang.Telegram_BotClose_CheckBox;
            Telegram_BotStart_CheckBox = en_Lang.Telegram_BotStart_CheckBox;
            Telegram_ClosingSupporter_CheckBox = en_Lang.Telegram_ClosingSupporter_CheckBox;
            Telegram_RunningBotCount_CheckBox = en_Lang.Telegram_RunningBotCount_CheckBox;
            Telegram_RunningBotEarned_CheckBox = en_Lang.Telegram_RunningBotEarned_CheckBox;
            Telegram_RunningBotStatus_CheckBox = en_Lang.Telegram_RunningBotStatus_CheckBox;
            Telegram_StartBotting_CheckBox = en_Lang.Telegram_StartBotting_CheckBox;
            Telegram_StopBotting_CheckBox = en_Lang.Telegram_StopBotting_CheckBox;
            Telegram_BotNotInTime1 = en_Lang.Telegram_BotNotInTime1;
            Telegram_BotNotInTime2 = en_Lang.Telegram_BotNotInTime2;
            TelegramHelpMessage = en_Lang.TelegramHelpMessage;
            Trophy = en_Lang.Trophy;
            UpdateNow = en_Lang.UpdateNow;
            //Form2
            Form2 = en_Lang.Form2;
            Village = en_Lang.Village;
            AttackPlan = en_Lang.AttackPlan;
            Profile_ForInject = en_Lang.Profile_ForInject;
            DestructionChallenges = en_Lang.DestructionChallenge;
            DonateTroops = en_Lang.DonateTroops;
            DonateSpells = en_Lang.DonateSpells;
            if (File.Exists(Database.Location + "Text.lang"))
            {
                string[] temp = File.ReadAllLines(Database.Location + "Text.lang");
                string[] Title = { }, Value = { };
                Array.Resize(ref Title, temp.Length);
                Array.Resize(ref Value, temp.Length);
                int x = 0;
                foreach(var t in temp)
                {
                    string[] te = t.Split('=');
                    if (te.Length == 2)
                    {
                        Title[x] = te[0];
                        Value[x] = te[1];
                        x++;
                    }
                }
                x = 0;
                foreach(var t in Title)
                {
                    switch (t)
                    {
                        case "_NET4_5Needed":
                            _NET4_5Needed = Value[x];
                            break;

                    }
                    x++;
                }
            }
        }
    }
}
