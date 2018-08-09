using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MyBot.Supporter.Main
{
    public class MyBotMOD
    {
        public static void FasterDelay(bool delay)
        {
            if (delay)
            {
                File.Delete(Environment.CurrentDirectory + "\\COCBot\\functions\\Config\\DelayTimes.au3");
                File.Move(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\DelayTimes.au3", Environment.CurrentDirectory + "\\COCBot\\functions\\Config\\DelayTimes.au3");
                Task.Delay(1000);
                File.Delete(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\DelayTimes.au3");
            }
            else
            {
                if (!Directory.Exists(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD"))
                {
                    Directory.CreateDirectory(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD");
                }
                File.Copy(Environment.CurrentDirectory + "\\COCBot\\functions\\Config\\DelayTimes.au3", Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\DelayTimes.au3");
                string[] code = File.ReadAllLines(Environment.CurrentDirectory + "\\COCBot\\functions\\Config\\DelayTimes.au3");
                int x = 0;
                foreach (var c in code)
                {
                    if (c.Contains("$DELAY") || c.Contains("$iDelay"))
                    {
                        if (!c.Contains("UPGRADE") && !c.Contains("LABORATORY") && !c.Contains("CLOCKTOWER") && !c.Contains("COLLECT") && !c.Contains("BOOST"))
                        {
                            string[] temp1 = c.Split('=');
                            if (temp1.Length < 2)
                            {
                                continue;
                            }
                            double dly = 0;
                            string[] temp = temp1[1].Split(';');
                            Double.TryParse(temp[0].Trim(), out dly);
                            if (dly > 100)
                            {
                                code[x] = temp1[0] + "= " + (dly / 4).ToString("0");
                            }
                        }
                    }
                    x++;
                }
                File.WriteAllLines(Environment.CurrentDirectory + "\\COCBot\\functions\\Config\\DelayTimes.au3", code, Encoding.Unicode);

            }
        }
        public static void DirectX(bool directX)
        {
            if (directX)
            {
                File.Delete(Environment.CurrentDirectory + "\\COCBot\\MBR Global Variables.au3"); 
                File.Delete(Environment.CurrentDirectory + "\\COCBot\\functions\\Android\\Android.au3");
                File.Move(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\MBR Global Variables(directX).au3", Environment.CurrentDirectory + "\\COCBot\\MBR Global Variables.au3");
                File.Move(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\Android(directX).au3", Environment.CurrentDirectory + "\\COCBot\\functions\\Android\\Android.au3");
            }
            else
            {
                if (!Directory.Exists(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD"))
                {
                    Directory.CreateDirectory(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD");
                }
                File.Copy(Environment.CurrentDirectory + "\\COCBot\\MBR Global Variables.au3", Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\MBR Global Variables(directX).au3");
                string[] code = File.ReadAllLines(Environment.CurrentDirectory + "\\COCBot\\MBR Global Variables.au3");
                int x = 0;
                foreach (var c in code)
                {
                    if (c.Contains("[\"MEmu\", \"MEmu\", \"MEmu \", \"[CLASS:subWin; INSTANCE:1]\", \"\",              $g_iDEFAULT_WIDTH,     $g_iDEFAULT_HEIGHT - 48,$g_iDEFAULT_WIDTH + 51,$g_iDEFAULT_HEIGHT - 12, 0, \"127.0.0.1:21503\", 2 + 4 + 8 + 16 + 32, '# ', '(Microvirt Virtual Input|User Input)', 0, 2], _; MEmu"))
                    {
                        code[x] = "[\"MEmu\", \"MEmu\", \"MEmu \", \"[CLASS:subWin; INSTANCE:1]\", \"\",              $g_iDEFAULT_WIDTH,     $g_iDEFAULT_HEIGHT - 48,$g_iDEFAULT_WIDTH + 51,$g_iDEFAULT_HEIGHT - 12, 0, \"127.0.0.1:21503\", 1 + 2 + 4 + 8 + 16 + 32, '# ', '(Microvirt Virtual Input|User Input)', 0, 2], _; MEmu";
                        break;
                    }
                    x++;
                }
                File.WriteAllLines(Environment.CurrentDirectory + "\\COCBot\\MBR Global Variables.au3", code, Encoding.Unicode);
                //Android.au3
                File.Copy(Environment.CurrentDirectory + "\\COCBot\\functions\\Android\\Android.au3", Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\Android(directX).au3");
                code = File.ReadAllLines(Environment.CurrentDirectory + "\\COCBot\\functions\\Android\\Android.au3");
                x = 0;
                foreach (var c in code)
                {
                    if (c.Contains("UpdateChkBackground"))
                    {
                        code[x] = "$g_bAndroidAdbScreenCap = false ;Edited by Supporter" + Environment.NewLine + c;
                        break;
                    }
                    x++;
                }
                File.WriteAllLines(Environment.CurrentDirectory + "\\COCBot\\functions\\Android\\Android.au3", code, Encoding.Unicode);
            }
        }
        public static void AdsLocator(bool Ads)
        {
            if (Ads)
            {
                File.Delete(Environment.CurrentDirectory + "\\COCBot\\functions\\Config\\readConfig.au3");
                File.Delete(Environment.CurrentDirectory + "\\COCBot\\functions\\Main Screen\\checkObstacles.au3");
                File.Delete(Environment.CurrentDirectory + "\\COCBot\\GUI\\MBR GUI Design Child Bot - Android.au3");
                File.Delete(Environment.CurrentDirectory + "\\COCBot\\MBR Functions.au3");
                File.Delete(Environment.CurrentDirectory + "\\COCBot\\functions\\Supporter\\AdsLocator.au3");
                File.Move(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\readConfig.au3", Environment.CurrentDirectory + "\\COCBot\\functions\\Config\\readConfig.au3");
                File.Move(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\checkObstacles.au3", Environment.CurrentDirectory + "\\COCBot\\functions\\Main Screen\\checkObstacles.au3");
                File.Move(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\MBR GUI Design Child Bot - Android.au3", Environment.CurrentDirectory + "\\COCBot\\GUI\\MBR GUI Design Child Bot - Android.au3");
                File.Move(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\MBR Functions.au3", Environment.CurrentDirectory + "\\COCBot\\MBR Functions.au3");
            }
            else
            {
                if (!Directory.Exists(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD"))
                {
                    Directory.CreateDirectory(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD");
                }
                File.Copy(Environment.CurrentDirectory + "\\COCBot\\functions\\Config\\readConfig.au3", Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\readConfig.au3");
                int x = 0;
                string[] code = File.ReadAllLines(Environment.CurrentDirectory + "\\COCBot\\functions\\Config\\readConfig.au3");
                foreach (var c in code)
                {
                    if (c.Contains("$g_sAndroidGameDistributor = IniRead($g_sProfileConfigPath,\"android\",\"game.distributor\",$g_AndroidGameDistributor)"))
                    {
                        code[x] = c + Environment.NewLine + "readADConfig() ;Edited by Supporter";
                        break;
                    }
                    x++;
                }
                File.WriteAllLines(Environment.CurrentDirectory + "\\COCBot\\functions\\Config\\readConfig.au3", code);
                //checkObstacles.au3
                File.Copy(Environment.CurrentDirectory + "\\COCBot\\functions\\Main Screen\\checkObstacles.au3", Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\checkObstacles.au3");
                x = 0;
                code = File.ReadAllLines(Environment.CurrentDirectory + "\\COCBot\\functions\\Main Screen\\checkObstacles.au3");
                foreach (var c in code)
                {
                    if (c.Contains("Local $aXButton = FindAdsxXButton()"))
                    {
                        code[x] = "CloseAd() ;Edited by Supporter" + Environment.NewLine + c;
                        break;
                    }
                    x++;
                }
                File.WriteAllLines(Environment.CurrentDirectory + "\\COCBot\\functions\\Main Screen\\checkObstacles.au3", code);
                //MBR GUI Design Child Bot - Android
                File.Copy(Environment.CurrentDirectory + "\\COCBot\\GUI\\MBR GUI Design Child Bot - Android.au3", Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\MBR GUI Design Child Bot - Android.au3");
                x = 0;
                code = File.ReadAllLines(Environment.CurrentDirectory + "\\COCBot\\GUI\\MBR GUI Design Child Bot - Android.au3");
                foreach (var c in code)
                {
                    if (c.Contains("GUICtrlCreateGroup(\"\", -99, -99, 1, 1)"))
                    {
                        code[x] = c + Environment.NewLine + "CreatLocateAdBtn($x + 210, $y) ;Edited by Supporter";
                        break;
                    }
                    x++;
                }
                File.WriteAllLines(Environment.CurrentDirectory + "\\COCBot\\GUI\\MBR GUI Design Child Bot - Android.au3", code);
                //MBR Functions.au3
                File.Copy(Environment.CurrentDirectory + "\\COCBot\\MBR Functions.au3", Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\MBR Functions.au3");
                x = 0;
                code = File.ReadAllLines(Environment.CurrentDirectory + "\\COCBot\\MBR Functions.au3");
                foreach (var c in code)
                {
                    if (c.Contains("#include \"functions\\Attack\\AttackReport.au3\""))
                    {
                        code[x] = "#include \"functions\\Supporter\\AdsLocator.au3\"" + Environment.NewLine + c;
                        break;
                    }
                    x++;
                }
                File.WriteAllLines(Environment.CurrentDirectory + "\\COCBot\\MBR Functions.au3", code);
                if (!Directory.Exists(Environment.CurrentDirectory + "\\COCBot\\functions\\Supporter"))
                {
                    Directory.CreateDirectory(Environment.CurrentDirectory + "\\COCBot\\functions\\Supporter");
                }
                File.Move("AdsLocator.au3", Environment.CurrentDirectory + "\\COCBot\\functions\\Supporter\\AdsLocator.au3");
            }
        }
    }
}
