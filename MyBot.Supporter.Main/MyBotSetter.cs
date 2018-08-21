using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Net;
using System.Net.NetworkInformation;

namespace MyBot.Supporter.Main
{
    public partial class MyBotSetter : Form
    {
        private string[] Profiles;
        private string[] Original_Settings;
        private string[] Modded_Settings;
        private string[] CSV;
        private static bool Modifying;
        private List<string> ClanGame = new List<string>();
        int x = 0;
        public MyBotSetter()
        {
            InitializeComponent();
        }
        private async void button2_Click(object sender, EventArgs e)
        {
            textBox6.Text = "Inject started";
            button2.Enabled = false;
            button3.Enabled = false;
            Thread modify = new Thread(ModifyStart);
            modify.Start();
        }

        /*private void ModifyClanGames_Config()
        {
            Modifying = true;
            x = 0;
            Modded_Settings = Original_Settings;
            foreach(var m in Modded_Settings)
            {
                Debug.WriteLine(m);
            }

            foreach (var line in Original_Settings)
            {
                string[] temp = line.Split('=');
                if (ClanGame.Contains("战利品挑战"))
                {
                    if (temp.Contains("Gold Challenge"))
                    {
                        Modded_Settings[x] = "Gold Challenge=" + numericUpDown30.Value;
                    }
                    else if (temp.Contains("Elixir Chellenge"))
                    {
                        Modded_Settings[x] = "Elixir Challenge=" + numericUpDown31.Value;
                    }
                    else if (temp.Contains("Dark Elixir Chellenge"))
                    {
                        Modded_Settings[x] = "Dark Elixir Challenge=" + numericUpDown32.Value;
                    }
                    else if (temp.Contains("Gold Grab"))
                    {
                        Modded_Settings[x] = "Gold Grab=" + numericUpDown33.Value;
                    }
                    else if (temp.Contains("Elixir Embezzlement"))
                    {
                        Modded_Settings[x] = "Elixir Embezzlement=" + numericUpDown34.Value;
                    }
                    else if (temp.Contains("Dark Elixir Heist"))
                    {
                        Modded_Settings[x] = "Dark Elixir Heist=" + numericUpDown35.Value;
                    }
                }
                if (ClanGame.Contains("战斗挑战"))
                {
                    if (temp.Contains("Star Collector"))
                    {
                        Modded_Settings[x] = "Star Collector=" + numericUpDown41.Value;
                    }
                    else if (temp.Contains("Lord of Destruction"))
                    {
                        Modded_Settings[x] = "Lord of Destruction=" + numericUpDown40.Value;
                    }
                    else if (temp.Contains("Pile Of Victories"))
                    {
                        Modded_Settings[x] = "Pile Of Victories=" + numericUpDown39.Value;
                    }
                    else if (temp.Contains("Hunt for Three Stars"))
                    {
                        Modded_Settings[x] = "Hunt for Three Stars=" + numericUpDown38.Value;
                    }
                    else if (temp.Contains("Winning Streak"))
                    {
                        Modded_Settings[x] = "Winning Streak=" + numericUpDown37.Value;
                    }
                    else if (temp.Contains("Slaying The Titans"))
                    {
                        Modded_Settings[x] = "Slaying The Titans=" + numericUpDown36.Value;
                    }
                    else if (temp.Contains("No Heroics Allowed"))
                    {
                        Modded_Settings[x] = "No Heroics Allowed=" + numericUpDown47.Value;
                    }
                    else if (temp.Contains("No-Magic Zone"))
                    {
                        Modded_Settings[x] = "No-Magic Zone=" + numericUpDown46.Value;
                    }
                    else if (temp.Contains("Attack Up"))
                    {
                        Modded_Settings[x] = "Attack Up=" + numericUpDown45.Value;
                    }
                }
                if (ClanGame.Contains("摧毁建筑挑战"))
                {
                    if (temp.Contains("Cannon Carnage"))
                    {
                        Modded_Settings[x] = "Cannon Carnage=" + numericUpDown53.Value;
                    }
                    else if (temp.Contains("Archer Tower Assault"))
                    {
                        Modded_Settings[x] = "Archer Tower Assault=" + numericUpDown52.Value;
                    }
                    else if (temp.Contains("Mortar Mauling"))
                    {
                        Modded_Settings[x] = "Mortar Mauling=" + numericUpDown51.Value;
                    }
                    else if (temp.Contains("Destroy Air Defenses"))
                    {
                        Modded_Settings[x] = "Destroy Air Defenses=" + numericUpDown50.Value;
                    }
                    else if (temp.Contains("Wizard Tower Warfare"))
                    {
                        Modded_Settings[x] = "Wizard Tower Warfare=" + numericUpDown49.Value;
                    }
                    else if (temp.Contains("Destroy Air Sweepers"))
                    {
                        Modded_Settings[x] = "Destroy Air Sweepers=" + numericUpDown48.Value;
                    }
                    else if (temp.Contains("Destroy Tesla Towers"))
                    {
                        Modded_Settings[x] = "Destroy Tesla Towers=" + numericUpDown44.Value;
                    }
                    else if (temp.Contains("Destroy Bomb Towers"))
                    {
                        Modded_Settings[x] = "Destroy Bomb Towers=" + numericUpDown43.Value;
                    }
                    else if (temp.Contains("Destroy X-Bows"))
                    {
                        Modded_Settings[x] = "Destroy X-Bows=" + numericUpDown42.Value;
                    }
                    else if (temp.Contains("Destroy Inferno Towers"))
                    {
                        Modded_Settings[x] = "Destroy Inferno Towers=" + numericUpDown58.Value;
                    }
                    else if (temp.Contains("Eagle Artillery EliDatabase.Mination"))
                    {
                        Modded_Settings[x] = "Eagle Artillery EliDatabase.Mination=" + numericUpDown57.Value;
                    }
                    else if (temp.Contains("Clan Castle Charge"))
                    {
                        Modded_Settings[x] = "Clan Castle Charge=" + numericUpDown56.Value;
                    }
                    else if (temp.Contains("Gold Storage Raid"))
                    {
                        Modded_Settings[x] = "Gold Storage Raid=" + numericUpDown55.Value;
                    }
                    else if (temp.Contains("Elixir Storage Raid"))
                    {
                        Modded_Settings[x] = "Elixir Storage Raid=" + numericUpDown54.Value;
                    }
                    else if (temp.Contains("Dark Elixir Storage Raid"))
                    {
                        Modded_Settings[x] = "Dark Elixir Storage Raid=" + numericUpDown63.Value;
                    }
                    else if (temp.Contains("Gold Database.Mine Mayhem"))
                    {
                        Modded_Settings[x] = "Gold Database.Mine Mayhem=" + numericUpDown62.Value;
                    }
                    else if (temp.Contains("Elixir Pump EliDatabase.Mination"))
                    {
                        Modded_Settings[x] = "Elixir Pump EliDatabase.Mination=" + numericUpDown61.Value;
                    }
                    else if (temp.Contains("Dark Elixir Plumbers"))
                    {
                        Modded_Settings[x] = "Dark Elixir Plumbers=" + numericUpDown60.Value;
                    }
                    else if (temp.Contains("Laboratory Strike"))
                    {
                        Modded_Settings[x] = "Laboratory Strike=" + numericUpDown59.Value;
                    }
                    else if (temp.Contains("Spell Factory Sabotage"))
                    {
                        Modded_Settings[x] = "Spell Factory Sabotage=" + numericUpDown68.Value;
                    }
                    else if (temp.Contains("Dark Spell Factory Sabotage"))
                    {
                        Modded_Settings[x] = "Dark Spell Factory Sabotage=" + numericUpDown67.Value;
                    }
                    else if (temp.Contains("Destroy Barbarian King Altars"))
                    {
                        Modded_Settings[x] = "Destroy Barbarian King Altars=" + numericUpDown66.Value;
                    }
                    else if (temp.Contains("Destroy Archer Queen Altars"))
                    {
                        Modded_Settings[x] = "Destroy Archer Queen Altars=" + numericUpDown65.Value;
                    }
                    else if (temp.Contains("Destroy Grand Warden Altars"))
                    {
                        Modded_Settings[x] = "Destroy Grand Warden Altars=" + numericUpDown64.Value;
                    }
                    else if (temp.Contains("King Level Hunter"))
                    {
                        Modded_Settings[x] = "King Level Hunter=" + numericUpDown66.Value;
                    }
                    else if (temp.Contains("Queen Level Hunter"))
                    {
                        Modded_Settings[x] = "Queen Level Hunter=" + numericUpDown65.Value;
                    }
                    else if (temp.Contains("Warden Level Hunter"))
                    {
                        Modded_Settings[x] = "Warden Level Hunter=" + numericUpDown64.Value;
                    }
                    else if (temp.Contains("Hero Level Hunter"))
                    {
                        Modded_Settings[x] = "Hero Level Hunter=" + numericUpDown69.Value;
                    }
                }
                if (temp.Contains("其他挑战"))
                {
                    if (temp.Contains("Gardening Exercise"))
                    {
                        Modded_Settings[x] = "Gardening Exercise=" + numericUpDown72.Value;
                    }
                    else if (temp.Contains("Helping Hand"))
                    {
                        Modded_Settings[x] = "Helping Hand=" + numericUpDown71.Value;
                    }
                    else if (temp.Contains("Donate Spells"))
                    {
                        Modded_Settings[x] = "Donate Spells" + numericUpDown70.Value;
                    }
                }
                x++;
            }
            Modifying = false;
        }*/ //MyBot removed this
        private void ModifyConfig(string coc)
        {
            x = 0;
            Modifying = true;
            Modded_Settings = Original_Settings;
            foreach (var line in Original_Settings)
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    GC.Collect();
                    string[] temp = line.Split('=');
                    if (coc.Length > 0)
                    {
                        switch (coc)
                        {
                            case "Google":
                            case "谷歌":
                                if (temp.Contains("game.class"))
                                {
                                    Modded_Settings[x] = "game.class=.GameApp";
                                }
                                else if (temp.Contains("game.distributor"))
                                {
                                    Modded_Settings[x] = "game.distributor=Google";
                                }
                                else if (temp.Contains("game.package"))
                                {
                                    Modded_Settings[x] = "game.package=com.supercell.clashofclans";
                                }
                                break;
                            case "Downjoy":
                            case "豌豆侠":
                                if (temp.Contains("game.class"))
                                {
                                    Modded_Settings[x] = "game.class=com.supercell.clashofclans.GameAppKunlun";
                                }
                                else if (temp.Contains("game.distributor"))
                                {
                                    Modded_Settings[x] = "game.distributor=Wandoujia/Downjoy";
                                }
                                else if (temp.Contains("game.package"))
                                {
                                    Modded_Settings[x] = "game.package=com.supercell.clashofclans.wdj";
                                }
                                break;
                            case "UC":
                            case "九游":
                                if (temp.Contains("game.class"))
                                {
                                    Modded_Settings[x] = "game.class=com.supercell.clashofclans.uc.GameApp";
                                }
                                else if (temp.Contains("game.distributor"))
                                {
                                    Modded_Settings[x] = "game.distributor=9game";
                                }
                                else if (temp.Contains("game.package"))
                                {
                                    Modded_Settings[x] = "game.package=com.supercell.clashofclans.uc";
                                }
                                break;
                            case "Baidu":
                            case "百度":
                                if (temp.Contains("game.class"))
                                {
                                    Modded_Settings[x] = "game.class=com.supercell.clashofclans.GameAppKunlun";
                                }
                                else if (temp.Contains("game.distributor"))
                                {
                                    Modded_Settings[x] = "game.distributor=Baidu";
                                }
                                else if (temp.Contains("game.package"))
                                {
                                    Modded_Settings[x] = "game.package=com.supercell.clashofclans.baidu";
                                }
                                break;
                            case "Kunlun":
                            case "昆仑":
                                if (temp.Contains("game.class"))
                                {
                                    Modded_Settings[x] = "game.class=com.supercell.clashofclans.GameAppKunlun";
                                }
                                else if (temp.Contains("game.distributor"))
                                {
                                    Modded_Settings[x] = "game.distributor=Kunlun";
                                }
                                else if (temp.Contains("game.package"))
                                {
                                    Modded_Settings[x] = "game.package=com.supercell.clashofclans.kunlun";
                                }
                                break;
                            case "Qihoo":
                            case "360":
                                if (temp.Contains("game.class"))
                                {
                                    Modded_Settings[x] = "game.class=com.supercell.clashofclans.GameAppKunlun";
                                }
                                else if (temp.Contains("game.distributor"))
                                {
                                    Modded_Settings[x] = "game.distributor=Qihoo";
                                }
                                else if (temp.Contains("game.package"))
                                {
                                    Modded_Settings[x] = "game.package=com.supercell.clashofclans.qihoo";
                                }
                                break;
                            case "Oppo":
                            case "OPPO":
                                if (temp.Contains("game.class"))
                                {
                                    Modded_Settings[x] = "game.class=com.supercell.clashofclans.GameAppKunlun";
                                }
                                else if (temp.Contains("game.distributor"))
                                {
                                    Modded_Settings[x] = "game.distributor=OPPO";
                                }
                                else if (temp.Contains("game.package"))
                                {
                                    Modded_Settings[x] = "game.package=com.supercell.clashofclans.nearme.gamecenter";
                                }
                                break;
                            case "XiaoMi":
                            case "小米":
                                if (temp.Contains("game.class"))
                                {
                                    Modded_Settings[x] = "game.class=com.supercell.clashofclans.GameAppXiaoMi";
                                }
                                else if (temp.Contains("game.distributor"))
                                {
                                    Modded_Settings[x] = "game.distributor=xiaomi";
                                }
                                else if (temp.Contains("game.package"))
                                {
                                    Modded_Settings[x] = "game.package=com.supercell.clashofclans.mi";
                                }
                                break;
                            case "AnZhi":
                            case "安智":
                                if (temp.Contains("game.class"))
                                {
                                    Modded_Settings[x] = "game.class=com.supercell.clashofclans.GameAppKunlun";
                                }
                                else if (temp.Contains("game.distributor"))
                                {
                                    Modded_Settings[x] = "game.distributor=anzhi";
                                }
                                else if (temp.Contains("game.package"))
                                {
                                    Modded_Settings[x] = "game.package=com.supercell.clashofclans.anzhi";
                                }
                                break;
                            case "GuoPan":
                            case "果盘":
                                if (temp.Contains("game.class"))
                                {
                                    Modded_Settings[x] = "game.class=com.flamingo.sdk.view.GPSplashActivity";
                                }
                                else if (temp.Contains("game.distributor"))
                                {
                                    Modded_Settings[x] = "game.distributor=guopan";
                                }
                                else if (temp.Contains("game.package"))
                                {
                                    Modded_Settings[x] = "game.package=com.supercell.clashofclans.guopan";
                                }
                                break;
                            case "Vivo":
                                if (temp.Contains("game.class"))
                                {
                                    Modded_Settings[x] = "game.class=com.supercell.clashofclans.GameAppKunlun";
                                }
                                else if (temp.Contains("game.distributor"))
                                {
                                    Modded_Settings[x] = "game.distributor=Vivo";
                                }
                                else if (temp.Contains("game.package"))
                                {
                                    Modded_Settings[x] = "com.supercell.clashofclans.vivo";
                                }
                                break;
                            case "2345":
                                if (temp.Contains("game.class"))
                                {
                                    Modded_Settings[x] = "game.class=com.supercell.clashofclans.GameAppKunlun";
                                }
                                else if (temp.Contains("game.distributor"))
                                {
                                    Modded_Settings[x] = "game.distributor=Vivo";
                                }
                                else if (temp.Contains("game.package"))
                                {
                                    Modded_Settings[x] = "com.supercell.clashofclans.ewan.sdk2345";
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    if (checkBox5.Checked)
                    {
                        if (temp.Contains("ScriptDB"))
                        {
                            if (comboBox2.SelectedItem != null)
                            {
                                Modded_Settings[x] = "ScriptDB=" + comboBox2.SelectedItem.ToString();
                            }
                        }
                        else if (temp.Contains("DBDropCC"))
                        {
                            if (checkBox8.Checked == true)
                            {
                                Modded_Settings[x] = "DBDropCC=1";
                            }
                            else
                            {
                                Modded_Settings[x] = "DBDropCC=0";
                            }
                        }
                        else if (temp.Contains("DBCloneSpell"))
                        {
                            Modded_Settings[x] = "DBCloneSpell=1";
                        }
                        else if (temp.Contains("DBEarthquakeSpell"))
                        {
                            Modded_Settings[x] = "DBEarthquakeSpell=1";
                        }
                        else if (temp.Contains("DBFreezeSpell"))
                        {
                            Modded_Settings[x] = "DBFreezeSpell=1";
                        }
                        else if (temp.Contains("DBHasteSpell"))
                        {
                            Modded_Settings[x] = "DBHasteSpell=1";
                        }
                        else if (temp.Contains("DBHealSpell"))
                        {
                            Modded_Settings[x] = "DBHealSpell=1";
                        }
                        else if (temp.Contains("DBJumpSpell"))
                        {
                            Modded_Settings[x] = "DBJumpSpell=1";
                        }
                        else if (temp.Contains("DBKingAtk"))
                        {
                            Modded_Settings[x] = "DBKingAtk=1";
                        }
                        else if (temp.Contains("DBQueenAtk"))
                        {
                            Modded_Settings[x] = "DBQueenAtk=2";
                        }
                        else if (temp.Contains("DBNotWaitHeroes"))
                        {
                            Modded_Settings[x] = "DBNotWaitHeroes=1";
                        }
                        else if (temp.Contains("DBLightSpell"))
                        {
                            Modded_Settings[x] = "DBLightSpell=1";
                        }
                        else if (temp.Contains("DBPoisonSpell"))
                        {
                            Modded_Settings[x] = "DBPoisonSpell=1";
                        }
                        else if (temp.Contains("DBRageSpell"))
                        {
                            Modded_Settings[x] = "DBRageSpell=1";
                        }
                        else if (temp.Contains("DBSkeletonSpell"))
                        {
                            Modded_Settings[x] = "DBSkeletonSpell=1";
                        }
                        else if (temp.Contains("DBWardenAtk"))
                        {
                            Modded_Settings[x] = "DBWardenAtk=4";
                        }
                        else if (temp.Contains("DBKingWait"))
                        {

                            if (checkBox10.Checked == true && numericUpDown73.Value >= 7)
                            {
                                Modded_Settings[x] = "DBKingWait=1";
                            }
                            else
                            {
                                Modded_Settings[x] = "DBKingWait=0";
                            }

                        }
                        else if (temp.Contains("DBQueenWait"))
                        {
                            this.Invoke((MethodInvoker)delegate ()
                            {
                                if (checkBox10.Checked == true && numericUpDown73.Value >= 9)
                                {
                                    Modded_Settings[x] = "DBQueenWait=1";
                                }
                                else
                                {
                                    Modded_Settings[x] = "DBQueenWait=0";
                                }
                            });
                        }
                        else if (temp.Contains("DBWardenWait"))
                        {
                            this.Invoke((MethodInvoker)delegate ()
                            {
                                if (checkBox10.Checked == true && numericUpDown73.Value >= 11)
                                {
                                    Modded_Settings[x] = "DBWardenWait=1";
                                }
                                else
                                {
                                    Modded_Settings[x] = "DBWardenWait=0";
                                }

                            });
                        }
                    }
                    if (checkBox6.Checked)
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            if (temp.Contains("ScriptAB"))
                            {
                                if (comboBox2.SelectedItem != null)
                                {
                                    Modded_Settings[x] = "ScriptAB=" + comboBox2.SelectedItem.ToString();
                                }
                            }
                            else if (temp.Contains("ABDropCC"))
                            {
                                if (checkBox2.Checked == true)
                                {
                                    Modded_Settings[x] = "ABDropCC=1";
                                }
                                else
                                {
                                    Modded_Settings[x] = "ABDropCC=0";
                                }
                            }
                            else if (temp.Contains("ABCloneSpell"))
                            {
                                Modded_Settings[x] = "ABCloneSpell=1";
                            }
                            else if (temp.Contains("ABEarthquakeSpell"))
                            {
                                Modded_Settings[x] = "ABEarthquakeSpell=1";
                            }
                            else if (temp.Contains("ABFreezeSpell"))
                            {
                                Modded_Settings[x] = "ABFreezeSpell=1";
                            }
                            else if (temp.Contains("ABHasteSpell"))
                            {
                                Modded_Settings[x] = "ABHasteSpell=1";
                            }
                            else if (temp.Contains("ABHealSpell"))
                            {
                                Modded_Settings[x] = "ABHealSpell=1";
                            }
                            else if (temp.Contains("ABJumpSpell"))
                            {
                                Modded_Settings[x] = "ABJumpSpell=1";
                            }
                            else if (temp.Contains("ABKingAtk"))
                            {
                                Modded_Settings[x] = "ABKingAtk=1";
                            }
                            else if (temp.Contains("ABQueenAtk"))
                            {
                                Modded_Settings[x] = "ABQueenAtk=2";
                            }
                            else if (temp.Contains("ABNotWaitHeroes"))
                            {
                                Modded_Settings[x] = "ABNotWaitHeroes=1";
                            }
                            else if (temp.Contains("ABLightSpell"))
                            {
                                Modded_Settings[x] = "ABLightSpell=1";
                            }
                            else if (temp.Contains("ABPoisonSpell"))
                            {
                                Modded_Settings[x] = "ABPoisonSpell=1";
                            }
                            else if (temp.Contains("ABRageSpell"))
                            {
                                Modded_Settings[x] = "ABRageSpell=1";
                            }
                            else if (temp.Contains("ABSkeletonSpell"))
                            {
                                Modded_Settings[x] = "ABSkeletonSpell=1";
                            }
                            else if (temp.Contains("ABWardenAtk"))
                            {
                                Modded_Settings[x] = "ABWardenAtk=4";
                            }
                            else if (temp.Contains("ABKingWait"))
                            {
                                if (checkBox10.Checked == true && numericUpDown73.Value >= 7)
                                {
                                    Modded_Settings[x] = "ABKingWait=1";
                                }
                                else
                                {
                                    Modded_Settings[x] = "ABKingWait=0";
                                }
                            }
                            else if (temp.Contains("ABQueenWait"))
                            {
                                if (checkBox10.Checked == true && numericUpDown73.Value >= 9)
                                {
                                    Modded_Settings[x] = "ABQueenWait=1";
                                }
                                else
                                {
                                    Modded_Settings[x] = "ABQueenWait=0";
                                }
                            }
                            else if (temp.Contains("ABWardenWait"))
                            {
                                if (checkBox10.Checked == true && numericUpDown73.Value >= 11)
                                {
                                    Modded_Settings[x] = "ABWardenWait=1";
                                }
                                else
                                {
                                    Modded_Settings[x] = "ABWardenWait=0";
                                }
                            }
                        });
                    }
                    if (checkBox13.Checked)
                    {
                        if (temp.Contains("txtBlacklist"))
                        {
                            Modded_Settings[x] = "txtBlacklist=部落战|不|自|勿";
                        }
                        else if (temp.Contains("txtDonateArchers"))
                        {
                            Modded_Settings[x] = "txtDonateArchers=need|弓|随|兵|增|守";
                        }
                        else if (temp.Contains("txtDonateBabyDragons"))
                        {
                            Modded_Settings[x] = "txtDonateBabyDragons=龙宝|小龙";
                        }
                        else if (temp.Contains("txtDonateBalloons"))
                        {
                            Modded_Settings[x] = "txtDonateBalloons=球|空";
                        }
                        else if (temp.Contains("txtDonateBarbarians"))
                        {
                            Modded_Settings[x] = "txtDonateBarbarians=野";
                        }
                        else if (temp.Contains("txtDonateBowlers"))
                        {
                            Modded_Settings[x] = "txtDonateBowlers=蓝|投";
                        }
                        else if (temp.Contains("txtDonateDragons"))
                        {
                            Modded_Settings[x] = "txtDonateDragons=龙|空";
                        }
                        else if (temp.Contains("txtDonateEarthquakeSpells"))
                        {
                            Modded_Settings[x] = "txtDonateEarthquakeSpells=震";
                        }
                        else if (temp.Contains("txtDonateFreezeSpells"))
                        {
                            Modded_Settings[x] = "txtDonateFreezeSpells=冻";
                        }
                        else if (temp.Contains("txtDonateGiants"))
                        {
                            Modded_Settings[x] = "txtDonateGiants=胖|巨";
                        }
                        else if (temp.Contains("txtDonateGoblins"))
                        {
                            Modded_Settings[x] = "txtDonateGoblins=哥布|偷";
                        }
                        else if (temp.Contains("txtDonateGolems"))
                        {
                            Modded_Settings[x] = "txtDonateGolems=石";
                        }
                        else if (temp.Contains("txtDonateHasteSpells"))
                        {
                            Modded_Settings[x] = "txtDonateHasteSpells=速";
                        }
                        else if (temp.Contains("txtDonateHealers"))
                        {
                            Modded_Settings[x] = "txtDonateHealers=天使";
                        }
                        else if (temp.Contains("txtDonateHealSpells"))
                        {
                            Modded_Settings[x] = "txtDonateHealSpells=治";
                        }
                        else if (temp.Contains("txtDonateHogRiders"))
                        {
                            Modded_Settings[x] = "txtDonateHogRiders=猪";
                        }
                        else if (temp.Contains("txtDonateJumpSpells"))
                        {
                            Modded_Settings[x] = "txtDonateJumpSpells=弹|跳";
                        }
                        else if (temp.Contains("txtDonateLavaHounds"))
                        {
                            Modded_Settings[x] = "txtDonateLavaHounds=狗|犬";
                        }
                        else if (temp.Contains("txtDonateLightningSpells"))
                        {
                            Modded_Settings[x] = "txtDonateLightningSpells=闪";
                        }
                        else if (temp.Contains("txtDonateDatabase.Miners"))
                        {
                            Modded_Settings[x] = "txtDonateDatabase.Miners=矿|遁|挖";
                        }
                        else if (temp.Contains("txtDonateDatabase.Minions"))
                        {
                            Modded_Settings[x] = "txtDonateDatabase.Minions=亡|蝇";
                        }
                        else if (temp.Contains("txtDonatePekkas"))
                        {
                            Modded_Settings[x] = "txtDonatePekkas=皮";
                        }
                        else if (temp.Contains("txtDonatePoisonSpells"))
                        {
                            Modded_Settings[x] = "txtDonatePoisonSpells=毒";
                        }
                        else if (temp.Contains("txtDonateRageSpells"))
                        {
                            Modded_Settings[x] = "txtDonateRageSpells=狂";
                        }
                        else if (temp.Contains("txtDonateSkeletonSpells"))
                        {
                            Modded_Settings[x] = "txtDonateSkeletonSpells=骷|髅";
                        }
                        else if (temp.Contains("txtDonateValkyries"))
                        {
                            Modded_Settings[x] = "txtDonateValkyries=武";
                        }
                        else if (temp.Contains("txtDonateWallBreakers"))
                        {
                            Modded_Settings[x] = "txtDonateWallBreakers=炸";
                        }
                        else if (temp.Contains("txtDonateWitches"))
                        {
                            Modded_Settings[x] = "txtDonateWitches=巫";
                        }
                        else if (temp.Contains("txtDonateWizards"))
                        {
                            Modded_Settings[x] = "txtDonateWizards=法";
                        }
                    }
                    if (checkBox4.Checked)
                    {
                        if (x < 550)
                        {
                            if (temp.Contains("CSpell"))
                            {
                                Modded_Settings[x] = "CSpell=1";
                            }
                            else if (temp.Contains("ESpell"))
                            {
                                Modded_Settings[x] = "ESpell=1";
                            }
                            else if (temp.Contains("FSpell"))
                            {
                                Modded_Settings[x] = "FSpell=1";
                            }
                            else if (temp.Contains("HaSpell"))
                            {
                                Modded_Settings[x] = "HaSpell=1";
                            }
                            else if (temp.Contains("HSpell"))
                            {
                                Modded_Settings[x] = "HSpell=1";
                            }
                            else if (temp.Contains("JSpell"))
                            {
                                Modded_Settings[x] = "JSpell=1";
                            }
                            else if (temp.Contains("LSpell"))
                            {
                                Modded_Settings[x] = "LSpell=1";
                            }
                            else if (temp.Contains("PSpell"))
                            {
                                Modded_Settings[x] = "PSpell=1";
                            }
                            else if (temp.Contains("RSpell"))
                            {
                                Modded_Settings[x] = "RSpell=1";
                            }
                            else if (temp.Contains("SkSpell"))
                            {
                                Modded_Settings[x] = "SkSpell=1";
                            }
                            else if (temp.Contains("Arch"))
                            {
                                Modded_Settings[x] = "Arch=1";
                            }
                            else if (temp.Contains("BabyD"))
                            {
                                Modded_Settings[x] = "BabyD=1";
                            }
                            else if (temp.Contains("Ball"))
                            {
                                Modded_Settings[x] = "Ball=1";
                            }
                            else if (temp.Contains("Barb"))
                            {
                                Modded_Settings[x] = "Barb=1";
                            }
                            else if (temp.Contains("Bowl"))
                            {
                                Modded_Settings[x] = "Bowl=1";
                            }
                            else if (temp.Contains("Drag"))
                            {
                                Modded_Settings[x] = "Drag=1";
                            }
                            else if (temp.Contains("Giant"))
                            {
                                Modded_Settings[x] = "Giant=1";
                            }
                            else if (temp.Contains("Gobl"))
                            {
                                Modded_Settings[x] = "Gobl=1";
                            }
                            else if (temp.Contains("Gole"))
                            {
                                Modded_Settings[x] = "Gole=1";
                            }
                            else if (temp.Contains("Heal"))
                            {
                                Modded_Settings[x] = "Heal=1";
                            }
                            else if (temp.Contains("Hogs"))
                            {
                                Modded_Settings[x] = "Hogs=1";
                            }
                            else if (temp.Contains("Lava"))
                            {
                                Modded_Settings[x] = "Lava=1";
                            }
                            else if (temp.Contains("Mine"))
                            {
                                Modded_Settings[x] = "Mine=1";
                            }
                            else if (temp.Contains("Mini"))
                            {
                                Modded_Settings[x] = "Mini=1";
                            }
                            else if (temp.Contains("Pekk"))
                            {
                                Modded_Settings[x] = "Pekk=1";
                            }
                            else if (temp.Contains("Valk"))
                            {
                                Modded_Settings[x] = "Valk=1";
                            }
                            else if (temp.Contains("Wall"))
                            {
                                Modded_Settings[x] = "Wall=1";
                            }
                            else if (temp.Contains("Witc"))
                            {
                                Modded_Settings[x] = "Witc=1";
                            }
                            else if (temp.Contains("Wiza"))
                            {
                                Modded_Settings[x] = "Wiza=1";
                            }
                            else if (temp.Contains("EDrag"))
                            {
                                Modded_Settings[x] = "EDrag=1";
                            }
                        }
                    }
                    if (checkBox7.Checked)
                    {
                        if (temp.Contains("language"))
                        {
                            if (comboBox5.SelectedIndex == 0)
                            {
                                Modded_Settings[x] = "language=English";
                            }
                            else if (comboBox5.SelectedIndex == 1)
                            {
                                Modded_Settings[x] = "language=Chinese_S";
                            }

                        }
                    }
                    if (checkBox2.Checked)
                    {
                        if (x > 700)
                        {
                            if (temp.Contains("CSpell") && numericUpDown73.Value >= 11)
                            {
                                Modded_Settings[x] = "CSpell=" + numericUpDown21.Value;
                            }
                            else if (temp.Contains("ESpell") && numericUpDown73.Value >= 8)
                            {
                                Modded_Settings[x] = "ESpell=" + numericUpDown29.Value;
                            }
                            else if (temp.Contains("FSpell") && numericUpDown73.Value >= 9)
                            {
                                Modded_Settings[x] = "FSpell=" + numericUpDown22.Value;
                            }
                            else if (temp.Contains("HaSpell") && numericUpDown73.Value >= 6)
                            {
                                Modded_Settings[x] = "HaSpell=" + numericUpDown28.Value;
                            }
                            else if (temp.Contains("HSpell") && numericUpDown73.Value >= 9)
                            {
                                Modded_Settings[x] = "HSpell=" + numericUpDown25.Value;
                            }
                            else if (temp.Contains("JSpell") && numericUpDown73.Value >= 9)
                            {
                                Modded_Settings[x] = "JSpell=" + numericUpDown23.Value;
                            }
                            else if (temp.Contains("LSpell") && numericUpDown73.Value >= 6)
                            {
                                Modded_Settings[x] = "LSpell=" + numericUpDown26.Value;
                            }
                            else if (temp.Contains("PSpell") && numericUpDown73.Value >= 8)
                            {
                                Modded_Settings[x] = "PSpell=" + numericUpDown20.Value;
                            }
                            else if (temp.Contains("RSpell") && numericUpDown73.Value >= 7)
                            {
                                Modded_Settings[x] = "RSpell=" + numericUpDown24.Value;
                            }
                            else if (temp.Contains("SkSpell") && numericUpDown73.Value >= 10)
                            {
                                Modded_Settings[x] = "SkSpell=" + numericUpDown27.Value;
                            }
                            else if (temp.Contains("Arch") && numericUpDown73.Value >= 6)
                            {
                                Modded_Settings[x] = "Arch=" + numericUpDown2.Value;
                            }
                            else if (temp.Contains("BabyD") && numericUpDown73.Value >= 9)
                            {
                                Modded_Settings[x] = "BabyD=" + numericUpDown11.Value;
                            }
                            else if (temp.Contains("Ball") && numericUpDown73.Value >= 6)
                            {
                                Modded_Settings[x] = "Ball=" + numericUpDown6.Value;
                            }
                            else if (temp.Contains("Barb") && numericUpDown73.Value >= 6)
                            {
                                Modded_Settings[x] = "Barb=" + numericUpDown1.Value;
                            }
                            else if (temp.Contains("Bowl") && numericUpDown73.Value >= 10)
                            {
                                Modded_Settings[x] = "Bowl=" + numericUpDown13.Value;
                            }
                            else if (temp.Contains("Drag") && numericUpDown73.Value >= 7)
                            {
                                Modded_Settings[x] = "Drag=" + numericUpDown9.Value;
                            }
                            else if (temp.Contains("Giant") && numericUpDown73.Value >= 6)
                            {
                                Modded_Settings[x] = "Giant=" + numericUpDown5.Value;
                            }
                            else if (temp.Contains("Gobl") && numericUpDown73.Value >= 6)
                            {
                                Modded_Settings[x] = "Gobl=" + numericUpDown3.Value;
                            }
                            else if (temp.Contains("Gole") && numericUpDown73.Value >= 8)
                            {
                                Modded_Settings[x] = "Gole=" + numericUpDown16.Value;
                            }
                            else if (temp.Contains("Heal") && numericUpDown73.Value >= 6)
                            {
                                Modded_Settings[x] = "Heal=" + numericUpDown8.Value;
                            }
                            else if (temp.Contains("Hogs") && numericUpDown73.Value >= 7)
                            {
                                Modded_Settings[x] = "Hogs=" + numericUpDown18.Value;
                            }
                            else if (temp.Contains("Lava") && numericUpDown73.Value >= 9)
                            {
                                Modded_Settings[x] = "Lava=" + numericUpDown14.Value;
                            }
                            else if (temp.Contains("Mine") && numericUpDown73.Value >= 10)
                            {
                                Modded_Settings[x] = "Mine=" + numericUpDown12.Value;
                            }
                            else if (temp.Contains("Mini") && numericUpDown73.Value >= 7)
                            {
                                Modded_Settings[x] = "Mini=" + numericUpDown19.Value;
                            }
                            else if (temp.Contains("Pekk") && numericUpDown73.Value >= 8)
                            {
                                Modded_Settings[x] = "Pekk=" + numericUpDown10.Value;
                            }
                            else if (temp.Contains("Valk") && numericUpDown73.Value >= 8)
                            {
                                Modded_Settings[x] = "Valk=" + numericUpDown17.Value;
                            }
                            else if (temp.Contains("Wall") && numericUpDown73.Value >= 6)
                            {
                                Modded_Settings[x] = "Wall=" + numericUpDown4.Value;
                            }
                            else if (temp.Contains("Witc") && numericUpDown73.Value >= 9)
                            {
                                Modded_Settings[x] = "Witc=" + numericUpDown15.Value;
                            }
                            else if (temp.Contains("Wiza") && numericUpDown73.Value >= 6)
                            {
                                Modded_Settings[x] = "Wiza=" + numericUpDown7.Value;
                            }
                            else if (temp.Contains("EDrag") && numericUpDown73.Value >= 11)
                            {
                                Modded_Settings[x] = "EDrag=" + numericUpDown74.Value;
                            }
                        }
                    }
                    if (checkBox3.Checked)
                    {
                        if (temp.Contains("ChkClanGamesAirTroop"))
                        {
                            if (ClanGame.Contains("空军挑战") || ClanGame.Contains("Air Troops Challenges"))
                            {
                                Modded_Settings[x] = "ChkClanGamesAirTroop=1";
                            }
                            else
                            {
                                Modded_Settings[x] = "ChkClanGamesAirTroop=0";
                            }
                        }
                        else if (temp.Contains("ChkClanGamesAir"))
                        {
                            if (ClanGame.Contains("空军挑战") || ClanGame.Contains("Air Troops Challenges"))
                            {
                                Modded_Settings[x] = "ChkClanGamesAir=1";
                            }
                            else
                            {
                                Modded_Settings[x] = "ChkClanGamesAir=0";
                            }
                        }
                        else if (temp.Contains("ChkClanGamesBattle"))
                        {
                            if (ClanGame.Contains("战斗挑战") || ClanGame.Contains("Battle Challenges"))
                            {
                                Modded_Settings[x] = "ChkClanGamesBattle=1";
                            }
                            else
                            {
                                Modded_Settings[x] = "ChkClanGamesBattle=0";
                            }
                        }
                        else if (temp.Contains("ChkClanGamesDestruction"))
                        {
                            if (ClanGame.Contains("摧毁建筑挑战") || ClanGame.Contains("Destruction Challenges"))
                            {
                                Modded_Settings[x] = "ChkClanGamesDestruction=1";
                            }
                            else
                            {
                                Modded_Settings[x] = "ChkClanGamesDestruction=0";
                            }
                        }
                        else if (temp.Contains("ChkClanGamesEnabled"))
                        {
                            Modded_Settings[x] = "ChkClanGamesEnabled=1";
                        }
                        else if (temp.Contains("ChkClanGamesGroundTroop"))
                        {
                            if (ClanGame.Contains("陆军挑战") || ClanGame.Contains("Ground Troops Challenges"))
                            {
                                Modded_Settings[x] = "ChkClanGamesGroundTroop=1";
                            }
                            else
                            {
                                Modded_Settings[x] = "ChkClanGamesGroundTroop=0";
                            }
                        }

                        else if (temp.Contains("ChkClanGamesLoot"))
                        {
                            if (ClanGame.Contains("战利品挑战") || ClanGame.Contains("Loot Challenges"))
                            {
                                Modded_Settings[x] = "ChkClanGamesLoot=1";
                            }
                            else
                            {
                                Modded_Settings[x] = "ChkClanGamesLoot=0";
                            }
                        }
                        else if (temp.Contains("ChkClanGamesMiscellaneous"))
                        {
                            if (ClanGame.Contains("其他挑战") || ClanGame.Contains("Miscellaneous Challenges"))
                            {
                                Modded_Settings[x] = "ChkClanGamesMiscellaneous=1";
                            }
                            else
                            {
                                Modded_Settings[x] = "ChkClanGamesMiscellaneous=0";
                            }
                        }
                        else if (temp.Contains("ChkClanGamesPurge"))
                        {
                            if (ClanGame.Contains("清理任务") || ClanGame.Contains("Purge Versus Battle"))
                            {
                                Modded_Settings[x] = "ChkClanGamesPurge=1";
                            }
                            else
                            {
                                Modded_Settings[x] = "ChkClanGamesPurge=0";
                            }
                        }
                        else if (temp.Contains("ChkClanGamesStopBeforeReachAndPurge"))
                        {
                            if (ClanGame.Contains("满分前停止继续任务切换清除任务模式") || ClanGame.Contains("Stop before completing your limit and purge"))
                            {
                                Modded_Settings[x] = "ChkClanGamesStopBeforeReachAndPurge=1";
                            }
                            else
                            {
                                Modded_Settings[x] = "ChkClanGamesStopBeforeReachAndPurge=0";
                            }
                        }
                    }
                    if (temp.Contains("chkCollect"))
                    {
                        Modded_Settings[x] = "chkCollect=1";
                    }
                    else if (temp.Contains("ChkCollectBuildersBase"))
                    {
                        Modded_Settings[x] = "ChkCollectBuildersBase=1";
                    }
                    else if (temp.Contains("ChkCollectFreeMagicItems"))
                    {
                        Modded_Settings[x] = "ChkCollectFreeMagicItems=1";
                    }
                    x++;
                });
            }

            Modifying = false;

        }
        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void MyBotSetter_Load(object sender, EventArgs e)
        {
            Language();
            FileStream fs = new System.IO.FileStream(Environment.CurrentDirectory + "\\images\\Logo.png", FileMode.Open, FileAccess.Read);
            pictureBox1.Image = Image.FromStream(fs);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            fs.Close();
            if (Directory.Exists(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD"))
            {
                if (File.Exists(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\DelayTimes.au3"))
                {
                    checkBox12.Enabled = false;
                    checkBox14.Enabled = true;
                }
                if (File.Exists(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\Android(directX).au3"))
                {
                    checkBox15.Enabled = false;
                    checkBox16.Enabled = true;
                }
                if (File.Exists(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\MBR Functions.au3") && File.Exists(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\MBR GUI Design Child Bot - Android.au3"))
                {
                    checkBox17.Enabled = false;
                    checkBox18.Enabled = true;
                }
                if(File.Exists(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\MBR Functions.au3") && File.Exists(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\DropTroopFromINI.au3"))
                {
                    checkBox17.Enabled = false;
                    checkBox18.Enabled = false;
                }
                if (File.Exists(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\Logo.patch"))
                {
                    checkBox19.Enabled = false;
                    checkBox20.Enabled = true;
                }
            }
            try
            {
                Profiles = Directory.GetDirectories(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Profiles");
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Profiles" + Path.DirectorySeparatorChar + "MyVillage");
                Profiles = Directory.GetDirectories(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Profiles");
            }
            foreach (var profile in Profiles)
            {
                var name = profile.Remove(0, profile.LastIndexOf('\\') + 1);
                Profile.Items.Add(name);
                comboBox1.Items.Add(name);
            }
            try
            {
                CSV = Directory.GetFiles(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "CSV" + Path.DirectorySeparatorChar + "Attack");
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "CSV" + Path.DirectorySeparatorChar + "Attack");
                CSV = null;
            }
            if (CSV != null)
            {
                foreach (var csv in CSV)
                {
                    var name = csv.Remove(0, csv.LastIndexOf('\\') + 1);
                    name = name.Replace(".csv", "");
                    comboBox2.Items.Add(name);
                }
            }
            Database.loadingprocess = 100;
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                groupBox4.Enabled = true;
                groupBox5.Enabled = true;
                groupBox6.Enabled = true;
            }
            else
            {
                groupBox4.Enabled = false;
                groupBox5.Enabled = false;
                groupBox6.Enabled = false;
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Profile.SelectedIndex = comboBox1.SelectedIndex;
        }
        private void Profile_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = Profile.SelectedIndex;
        }
        private void Profile_TextChanged(object sender, EventArgs e)
        {
            if (Profile.SelectedItem == null)
            {
                switch(Database.Language)
                {
                    case "English":
                    Profile.Text = "All";
                    comboBox1.SelectedItem = null;
                    comboBox1.Text = "All";
                        break;
                    case "Chinese":
                    Profile.Text = "全部";
                    comboBox1.SelectedItem = null;
                    comboBox1.Text = "全部";
                        break;
                }
            }
        }
        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                switch (Database.Language)
                {
                    case "English":
                        Profile.Text = "All";
                        comboBox1.SelectedItem = null;
                        comboBox1.Text = "All";
                        break;
                    case "Chinese":
                        Profile.Text = cn_Lang.AllSelected;
                        comboBox1.SelectedItem = null;
                        comboBox1.Text = cn_Lang.AllSelected;
                        break;
                }
            }
        }
        private void checkedListBox3_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string item = checkedListBox3.SelectedItem.ToString();
            if (e.NewValue == CheckState.Checked)
            {
                if (!ClanGame.Contains(item))
                {
                    ClanGame.Add(item);
                }
            }
            else
            {
                if (ClanGame.Contains(item))
                {
                    ClanGame.Remove(item);
                }
                
            }
        }
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                checkedListBox3.Enabled = true;
            }
            else
            {
                checkedListBox3.Enabled = false;
            }
        }
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true || checkBox6.Checked == true)
            {
                panel1.Enabled = true;
            }
            else
            {
                panel1.Enabled = false;
            }
        }
        private void Language()
        {
            if (Database.Language == "English")
            {
                Text = "MyBot Injector";
                tabPage1.Text = "Village";
                tabPage2.Text = "Attack Plan";
                label31.Text = "Profile to Inject:";
                label4.Text = "Profile to Inject:";
                checkBox1.Text = "Adb Enable switch (Inject this if\nyou met some errors while botting.\n Not confirm will fix!)" ;
                checkBox13.Visible = false;
                checkBox13.Checked = false;
                label36.Text = "Townhall Level";
                Profile.Text = "All";
                comboBox1.Text = "All";
                tabPage8.Text = "Clan Game";
                groupBox3.Text = "Clan Game Inject";
                checkBox3.Text = "Inject Clan Game";
                checkedListBox3.Items.Clear();
                checkedListBox3.Items.Add("Loot Challenges");
                checkedListBox3.Items.Add("Battle Challenges");
                checkedListBox3.Items.Add("Destruction Challenges");
                checkedListBox3.Items.Add("Air Troops Challenges");
                checkedListBox3.Items.Add("Ground Troops Challenges");
                checkedListBox3.Items.Add("Miscellaneous Challenges");
                checkedListBox3.Items.Add("Purge Versus Battle");
                checkedListBox3.Items.Add("Stop before completing your limit and purge");
                groupBox7.Text = "Deadbase";
                label32.Text = "CSV Attack";
                checkBox8.Text = "Use clan troops";
                checkBox10.Text = "Wait for heros";
                checkBox2.Text = "Inject training";
                checkBox19.Text = "MyBot Banner Changer";
                checkBox20.Text = "MyBot Original Banner";
                groupBox1.Text = "Inject training";
                checkBox5.Text = "Inject DeadBase Settings";
                checkBox6.Text = "Inject LiveBase settings";
                checkBox4.Text = "Inject MyBot troops training to show all textbox";
                groupBox4.Text = "Elixir Troops";
                groupBox5.Text = "Dark Elixir Troops";
                groupBox6.Text = "Spells";
                label1.Text = "Babarians";
                label2.Text = "Archers";
                label5.Text = "Goblins";
                label6.Text = "Wallbreakers";
                label7.Text = "Giants";
                label14.Text = "Balloons";
                label11.Text = "Wizards";
                label8.Text = "Healers";
                label9.Text = "Dragons";
                label10.Text = "Pekkas";
                label15.Text = "Baby Dragons";
                label16.Text = "Miner";
                label28.Text = "Minions";
                label27.Text = "Hog Riders";
                label26.Text = "Valkyrines";
                label25.Text = "Golems";
                label24.Text = "Witches";
                label19.Text = "Lava Hounds";
                label20.Text = "Bowlers";
                label30.Text = "Lightning";
                label29.Text = "Heal";
                label23.Text = "Rage";
                label22.Text = "Jump";
                label21.Text = "Freeze";
                label17.Text = "Clone";
                label18.Text = "Poison";
                label35.Text = "Earthquake";
                label34.Text = "Hastle";
                label33.Text = "Skeleton";
                label82.Text = "Electro Dragon";
                label3.Text = "Clash of clans Version";
                checkBox7.Text = "Change MyBot Language";
                tabPage5.Text = "MyBot MOD";
                tabPage3.Text = "Complete!";
                button2.Text = "Inject!!";
                button3.Text = "Cancel";
                textBox1.Text = "Help" + Environment.NewLine + Environment.NewLine + "* MyBot Injector is a third-party *.ini pharser, which will maybe cause MyBot not working after injection"
                    + Environment.NewLine + Environment.NewLine + "* You can inject Modded MyBot, which might not work for some injections" + Environment.NewLine + Environment.NewLine +
                    "* This injector is NOT MyBot! Any furthur settings please use MyBot itself! NOT Injector!"
                    + Environment.NewLine + Environment.NewLine + "* Injector will not responsible for MyBot crashing after injection! Use it as your own risk!" + Environment.NewLine + Environment.NewLine +
                    "* Used C# api INI Pharser, provided in CodeProject.com" + Environment.NewLine + Environment.NewLine + "Developed by PoH98";
                comboBox3.Items.Clear();
                comboBox3.Items.Add("Google");
                comboBox3.Items.Add("Downjoy");
                comboBox3.Items.Add("UC");
                comboBox3.Items.Add("Baidu");
                comboBox3.Items.Add("Kunlun");
                comboBox3.Items.Add("Qihoo");
                comboBox3.Items.Add("Oppo");
                comboBox3.Items.Add("XiaoMi");
                comboBox3.Items.Add("AnZhi");
                comboBox3.Items.Add("GuoPan");
                richTextBox1.Text = "Help:" + Environment.NewLine + Environment.NewLine + "* Injector will automatic set Troops making after CSVis selected." + Environment.NewLine + Environment.NewLine +
                    "* The troops auto set by injector will not fit to your army camp, it just shows the max amount of troops that csv supported." + Environment.NewLine + Environment.NewLine +
                    "* Injector will not confirm that the troops settings will always successfully injected into config.ini";
                groupBox9.Text = "Restore";
                checkBox15.Text = "Force DirectX usage for faster botting";
                checkBox16.Text = "Restore default adb capt";
                checkBox17.Text = "Ads locator";
                checkBox18.Text = "Remove Ads Locator";
                checkBox12.Text = "Reduce Delays";
                checkBox14.Text = "Restore Original Delays";
            }

        }
        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked == true)
            {
                comboBox5.Enabled = true;
                if (Database.Language == "English")
                {
                    comboBox5.SelectedIndex = 0;
                }
                else
                {
                    comboBox5.SelectedIndex = 1;
                }
            }
            else
            {
                comboBox5.Enabled = false;
                comboBox5.SelectedItem = null;
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = true;
            if (comboBox2.SelectedItem != null)
            {
                try
                {
                    CSVDECODER.CSVcodes = File.ReadAllLines(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "CSV" + Path.DirectorySeparatorChar + "Attack" + Path.DirectorySeparatorChar + comboBox2.SelectedItem + ".csv");
                }
                catch
                {
                    if (Database.Language == "English")
                    {
                        MessageBox.Show("CSV Not Found!!", "Error!");
                    }
                    else
                    {
                        MessageBox.Show("找不到选中的CSV!!", "错误!");
                    }
                }
                CSVDECODER.CSVDecode();
                try
                {
                    numericUpDown1.Value = CSVDECODER.SetTroops("Barb");
                }
                catch
                {
                    numericUpDown1.Value = numericUpDown1.Maximum;
                }
                try
                {
                    numericUpDown2.Value = CSVDECODER.SetTroops("Arch");
                }
                catch
                {
                    numericUpDown2.Value = numericUpDown2.Maximum;
                }
                try
                {
                    numericUpDown3.Value = CSVDECODER.SetTroops("Gobl");
                }
                catch
                {
                    numericUpDown4.Value = numericUpDown4.Maximum;
                }
                try
                {
                    numericUpDown4.Value = CSVDECODER.SetTroops("Wall");
                }
                catch
                {
                    numericUpDown4.Value = numericUpDown4.Maximum;
                }
                try
                {
                    numericUpDown5.Value = CSVDECODER.SetTroops("Giant");
                }
                catch
                {
                    numericUpDown5.Value = numericUpDown5.Maximum;
                }
                try
                {
                    numericUpDown6.Value = CSVDECODER.SetTroops("Ball");
                }
                catch
                {
                    numericUpDown6.Value = numericUpDown6.Maximum;
                }
                try
                {
                    numericUpDown7.Value = CSVDECODER.SetTroops("Wiza");
                }
                catch
                {
                    numericUpDown7.Value = numericUpDown7.Maximum;
                }
                try
                {
                    numericUpDown8.Value = CSVDECODER.SetTroops("Heal");
                }
                catch
                {
                    numericUpDown8.Value = numericUpDown8.Maximum;
                }
                try
                {
                    numericUpDown9.Value = CSVDECODER.SetTroops("Drag");
                }
                catch
                {
                    numericUpDown9.Value = numericUpDown9.Maximum;
                }
                try
                {
                    numericUpDown10.Value = CSVDECODER.SetTroops("Pekk");
                }
                catch
                {
                    numericUpDown10.Value = numericUpDown9.Maximum;
                }
                try
                {
                    numericUpDown11.Value = CSVDECODER.SetTroops("BabyDrag");
                }
                catch
                {
                    numericUpDown11.Value = numericUpDown11.Maximum;
                }
                try
                {
                    numericUpDown12.Value = CSVDECODER.SetTroops("Mine");
                }
                catch
                {
                    numericUpDown12.Value = numericUpDown12.Maximum;
                }
                try
                {
                    numericUpDown19.Value = CSVDECODER.SetTroops("Mini");
                }
                catch
                {
                    numericUpDown19.Value = numericUpDown19.Maximum;
                }
                try
                {
                    numericUpDown18.Value = CSVDECODER.SetTroops("Hogs");
                }
                catch
                {
                    numericUpDown18.Value = numericUpDown18.Maximum;
                }
                try
                {
                    numericUpDown17.Value = CSVDECODER.SetTroops("Valk");
                }
                catch
                {
                    numericUpDown17.Value = numericUpDown17.Maximum;
                }
                try
                {
                    numericUpDown16.Value = CSVDECODER.SetTroops("Gole");
                }
                catch
                {
                    numericUpDown16.Value = numericUpDown16.Maximum;
                }
                try
                {
                    numericUpDown15.Value = CSVDECODER.SetTroops("Witc");
                }
                catch
                {
                    numericUpDown15.Value = numericUpDown15.Maximum;
                }
                try
                {
                    numericUpDown14.Value = CSVDECODER.SetTroops("Lava");
                }
                catch
                {
                    numericUpDown14.Value = numericUpDown14.Maximum;
                }
                try
                {
                    numericUpDown13.Value = CSVDECODER.SetTroops("Bowl");
                }
                catch
                {
                    numericUpDown13.Value = numericUpDown13.Maximum;
                }
                try
                {
                    numericUpDown26.Value = CSVDECODER.SetTroops("LSpell");
                }
                catch
                {
                    numericUpDown26.Value = numericUpDown26.Maximum;
                }
                try
                {
                    numericUpDown25.Value = CSVDECODER.SetTroops("HSpell");
                }
                catch
                {
                    numericUpDown25.Value = numericUpDown25.Maximum;
                }
                try
                {
                    numericUpDown24.Value = CSVDECODER.SetTroops("RSpell");
                }
                catch
                {
                    numericUpDown24.Value = numericUpDown24.Maximum;
                }
                try
                {
                    numericUpDown23.Value = CSVDECODER.SetTroops("JSpell");
                }
                catch
                {
                    numericUpDown23.Value = numericUpDown23.Maximum;
                }
                try
                {
                    numericUpDown22.Value = CSVDECODER.SetTroops("FSpell");
                }
                catch
                {
                    numericUpDown22.Value = numericUpDown22.Maximum;
                }
                try
                {
                    numericUpDown21.Value = CSVDECODER.SetTroops("CSpell");
                }
                catch
                {
                    numericUpDown21.Value = numericUpDown21.Maximum;
                }
                try
                {
                    numericUpDown20.Value = CSVDECODER.SetTroops("PSpell");
                }
                catch
                {
                    numericUpDown20.Value = numericUpDown20.Maximum;
                }
                try
                {
                    numericUpDown29.Value = CSVDECODER.SetTroops("ESpell");
                }
                catch
                {
                    numericUpDown29.Value = numericUpDown29.Maximum;
                }
                try
                {
                    numericUpDown28.Value = CSVDECODER.SetTroops("HaSpell");
                }
                catch
                {
                    numericUpDown28.Value = numericUpDown28.Maximum;
                }
                try
                {
                    numericUpDown27.Value = CSVDECODER.SetTroops("SkSpell");
                }
                catch
                {
                    numericUpDown27.Value = numericUpDown27.Maximum;
                }
                Array.Clear(CSVDECODER.Troops, 0, CSVDECODER.Troops.Length);
                Array.Clear(CSVDECODER.TroopsNum, 0, CSVDECODER.TroopsNum.Length);
                Array.Clear(CSVDECODER.Vector, 0, CSVDECODER.Vector.Length);
            }
        }
        private void MyBotSetter_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Directory.Exists(Environment.CurrentDirectory + "\\Include"))
            {
                foreach (var file in Directory.GetFiles(Environment.CurrentDirectory + "\\Include"))
                {
                    File.Delete(file);
                }
                Directory.Delete(Environment.CurrentDirectory + "\\Include");
            }
            if (File.Exists("AdsLocator.au3"))
            {
                File.Delete("AdsLocator.au3");
            }
            if (File.Exists("Compiler.exe"))
            {
                File.Delete("Compiler.exe");
            }
            if (File.Exists("ExtendedBar.au3"))
            {
                File.Delete("ExtendedBar.au3");
            }
            if (File.Exists("Config.au3"))
            {
                File.Delete("Config.au3");
            }
        }
        private static bool Download_Resources()
        {
            try
            {
                Ping github = new Ping();
                var respond = github.Send("www.github.com").Status;
                if (respond == IPStatus.Success) //Github is usable
                {
                    using (WebClient wc = new WebClient())
                    {
                        string link = wc.DownloadString("https://github.com/PoH98/MyBot.Supporter/raw/master/Downloadable_Contents/ResourcesLink.txt");
                        byte[] b = wc.DownloadData(link);
                        if (b.Length > 0)
                        {
                            File.WriteAllBytes(Database.Location + "AutoIT.zip", b);
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    using (WebClient wc = new WebClient())
                    {
                        string link = wc.DownloadString("https://gitee.com/PoH98/MyBot.Supporter/raw/master/Downloadable_Contents/ResourcesLink.txt");
                        byte[] b = wc.DownloadData(link);
                        if (b.Length > 0)
                        {
                            File.WriteAllBytes(Database.Location + "AutoIT.zip", b);
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch
            {
                return false;
            }

        }
        private async void checkBox19_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox19.Checked)
            {
                textBox6.AppendText("Fetching Image from MyBot.Run Forum Database");
                if (!Directory.Exists(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD"))
                {
                    Directory.CreateDirectory(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD");
                }
                SelectImage select = new SelectImage();
                select.Show();
                while (select.Visible)
                {
                    await Task.Delay(100);
                }
                textBox6.AppendText("Fetch Completed");
                textBox6.AppendText("Applying Changes");
            }
        }
        private void ModifyStart()
        {
            try
            {
                string item = "";
                string coc = "";
                bool DirectX = false,DirectXRestore = false, Ads = false,AdsRestore = false, Delay = false,DelayRestore = false,LogoRestore = false;
                this.Invoke((MethodInvoker)delegate ()
                {
                    if(Profile.SelectedItem != null)
                        item = Profile.Text;
                    if (comboBox1.SelectedItem != null)
                        item = comboBox1.Text;
                    if (comboBox3.SelectedItem != null)
                        coc = comboBox3.SelectedText;
                    if (checkBox15.Checked)
                        DirectX = true;
                    if (checkBox16.Checked)
                        DirectXRestore = true;
                    if (checkBox17.Checked)
                        Ads = true;
                    if (checkBox18.Checked)
                        AdsRestore = true;
                    if (checkBox12.Checked)
                        Delay = true;
                    if (checkBox14.Checked)
                        DelayRestore = true;
                    if (checkBox20.Checked)
                        LogoRestore = true;
                });
                if (item.Length > 0)
                {
                    try
                    {
                        Original_Settings = File.ReadAllLines(Environment.CurrentDirectory + "\\Profiles\\" + item + Path.DirectorySeparatorChar + "config.ini", UnicodeEncoding.Unicode);
                    }
                    catch (FileNotFoundException)
                    {
                        switch (Database.Language)
                        {
                            case "English":
                                MessageBox.Show("Profile have no config.ini! Please start MyBot for the first time!");
                                break;
                        }
                        ProcessStartInfo start = new ProcessStartInfo("MyBot.run.exe");
                        start.Arguments = item + " " + "-a" + " " + "-nwd" + " " + "-nbs";
                        Process.Start(start);
                        return;
                    }
                    showlog("Injecting config.ini");
                    Modifying = true;
                    ModifyConfig(coc);
                    while (Modifying)
                    {
                        Thread.Sleep(100);
                    }
                    showlog("Inject completed");
                    using (StreamWriter w = new StreamWriter(Environment.CurrentDirectory + "\\Profiles\\" + item + Path.DirectorySeparatorChar + "config.ini", false, Encoding.Unicode))
                    {
                        foreach (var l in Modded_Settings)
                        {
                            w.WriteLine(l);
                        }
                    }
                }
                else
                {
                    Profiles = Directory.GetDirectories(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Profiles");
                    foreach (var profile in Profiles)
                    {
                        try
                        {
                            Original_Settings = File.ReadAllLines(profile + Path.DirectorySeparatorChar + "config.ini", UnicodeEncoding.Unicode);
                            Modifying = true;
                            showlog("Injecting " + profile + "'s config");
                            ModifyConfig(coc);
                            while (Modifying)
                            {
                                Thread.Sleep(100);
                            }
                            showlog("Inject completed");
                            using (StreamWriter w = new StreamWriter(profile + Path.DirectorySeparatorChar + "config.ini", false, Encoding.Unicode))
                            {
                                foreach (var l in Modded_Settings)
                                {
                                    w.WriteLine(l);
                                }
                            }
                        }

                        catch (FileNotFoundException)
                        {
                            continue;
                        }
                    }
                }
                if (DirectX || DirectXRestore|| Ads||AdsRestore||Delay||DelayRestore||LogoRestore)
                {
                    // MODED MyBot
                    bool IsExist = false;
                    if (!File.Exists(Database.Location + "AutoIT.zip"))
                    {
                       showlog("Downloading resources");
                        IsExist = Download_Resources();
                        if (IsExist)
                        {
                            showlog("Download completed");
                        }
                    }
                    else
                    {
                        IsExist = true;
                    }
                    if (IsExist)
                    {
                        ZipFile.ExtractToDirectory(Database.Location + "AutoIT.zip", Environment.CurrentDirectory);
                        if (Delay)
                        {
                            showlog("Injecting DelayTimes.au3");
                            MyBotMOD.FasterDelay(false);
                            showlog("Completed Injecting DelayTimes.au3");
                        }
                        else if (DelayRestore)
                        {
                            showlog("Injecting DelayTimes.au3");
                            MyBotMOD.FasterDelay(true);
                            showlog("Completed Injecting DelayTimes.au3");
                        }
                        if (DirectX)
                        {
                            showlog("Injecting MBR Global Variables.au3");
                            showlog("Injecting Android.au3");
                            MyBotMOD.DirectX(false);
                            showlog("Completed Injecting MBR Global Variables.au3");
                            showlog("Completed Injecting Android.au3");
                        }
                        else if (DirectXRestore)
                        {
                            showlog("Injecting MBR Global Variables.au3");
                            showlog("Injecting Android.au3");
                            MyBotMOD.DirectX(true);
                            showlog("Completed Injecting MBR Global Variables.au3");
                            showlog("Completed Injecting Android.au3");
                        }
                        if (Ads)
                        {
                            showlog("Injecting *.au3");
                            MyBotMOD.AdsLocator(false);
                            showlog("Completed Injecting *.au3");
                        }
                        else if (AdsRestore)
                        {
                            showlog("Injecting *.au3");
                            MyBotMOD.AdsLocator(true);
                            showlog("Completed Injecting *.au3");
                        }
                        if (LogoRestore)
                        {
                            showlog("Undoing Logo Patch");
                            File.Delete(Environment.CurrentDirectory + "\\images\\Logo.png");
                            File.Move(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\Logo.patch", Environment.CurrentDirectory + "\\images\\Logo.png");
                        }
                        showlog("Compiling MyBot.run.exe");
                        ProcessStartInfo compiler = new ProcessStartInfo("Compiler.exe");
                        compiler.Arguments = "/in \"" + Environment.CurrentDirectory + "\\MyBot.run.au3\" /out \"" + Environment.CurrentDirectory + "\\MyBot.run.exe\" /icon \"" + Environment.CurrentDirectory + "\\images\\MyBot.ico\"";
                        Process com = Process.Start(compiler);
                        while (!com.HasExited)
                        {
                            Thread.Sleep(100);
                        }
                        File.Delete(Environment.CurrentDirectory + "Compiler.exe");
                    }
                    else
                    {
                        MessageBox.Show(en_Lang.Error);
                        this.Invoke(new Action(() => this.Close()));
                    }
                }
                Database.loadingprocess = 100;
                this.Invoke(new Action(() => this.Close()));

            }
            catch (Exception ex)
            {
                MessageBox.Show("Inject Failed! Reason:" + ex);
                showlog("Inject Failed!");
                File.WriteAllText("error.log", ex.ToString());
                this.Invoke(new Action(() => button2.Enabled = true));
                this.Invoke(new Action(() => button3.Enabled = true));
            }
        }
        private void showlog(string log)
        {
            try
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    textBox6.AppendText("\n" + log);
                    textBox6.ScrollToCaret();
                    richTextBox2.AppendText("\n" + log);
                    richTextBox2.ScrollToCaret();
                });
            }
            catch
            {

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool DirectX = false, DirectXRestore = false, Ads = false, AdsRestore = false, Delay = false, DelayRestore = false, LogoRestore = false, IsExist = false;
            this.Invoke((MethodInvoker)delegate ()
            {
                if (checkBox15.Checked)
                    DirectX = true;
                if (checkBox16.Checked)
                    DirectXRestore = true;
                if (checkBox17.Checked)
                    Ads = true;
                if (checkBox18.Checked)
                    AdsRestore = true;
                if (checkBox12.Checked)
                    Delay = true;
                if (checkBox14.Checked)
                    DelayRestore = true;
                if (checkBox20.Checked)
                    LogoRestore = true;
            });
            if (!File.Exists(Database.Location + "AutoIT.zip"))
            {
                showlog("Downloading resources");
                IsExist = Download_Resources();
                if (IsExist)
                {
                    showlog("Download completed");
                }
            }
            else
            {
                IsExist = true;
            }
            if (IsExist)
            {
                ZipFile.ExtractToDirectory(Database.Location + "AutoIT.zip", Environment.CurrentDirectory);
                if (Delay)
                {
                    showlog("Injecting DelayTimes.au3");
                    MyBotMOD.FasterDelay(false);
                    showlog("Completed Injecting DelayTimes.au3");
                }
                else if (DelayRestore)
                {
                    showlog("Injecting DelayTimes.au3");
                    MyBotMOD.FasterDelay(true);
                    showlog("Completed Injecting DelayTimes.au3");
                }
                if (DirectX)
                {
                    showlog("Injecting MBR Global Variables.au3");
                    showlog("Injecting Android.au3");
                    MyBotMOD.DirectX(false);
                    showlog("Completed Injecting MBR Global Variables.au3");
                    showlog("Completed Injecting Android.au3");
                }
                else if (DirectXRestore)
                {
                    showlog("Injecting MBR Global Variables.au3");
                    showlog("Injecting Android.au3");
                    MyBotMOD.DirectX(true);
                    showlog("Completed Injecting MBR Global Variables.au3");
                    showlog("Completed Injecting Android.au3");
                }
                if (Ads)
                {
                    showlog("Injecting *.au3");
                    MyBotMOD.AdsLocator(false);
                    showlog("Completed Injecting *.au3");
                }
                else if (AdsRestore)
                {
                    showlog("Injecting *.au3");
                    MyBotMOD.AdsLocator(true);
                    showlog("Completed Injecting *.au3");
                }
                if (LogoRestore)
                {
                    showlog("Undoing Logo Patch");
                    File.Delete(Environment.CurrentDirectory + "\\images\\Logo.png");
                    File.Move(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\Logo.patch", Environment.CurrentDirectory + "\\images\\Logo.png");
                }
                showlog("Compiling MyBot.run.exe");
                ProcessStartInfo compiler = new ProcessStartInfo("Compiler.exe");
                compiler.Arguments = "/in \"" + Environment.CurrentDirectory + "\\MyBot.run.au3\" /out \"" + Environment.CurrentDirectory + "\\MyBot.run.exe\" /icon \"" + Environment.CurrentDirectory + "\\images\\MyBot.ico\"";
                Process com = Process.Start(compiler);
                while (!com.HasExited)
                {
                    Thread.Sleep(100);
                }
                showlog("Compile Completed");
                File.Delete(Environment.CurrentDirectory + "Compiler.exe");
                groupBox8.Enabled = false;
                groupBox9.Enabled = false;
            }
            else
            {
                showlog("Unable to compile. AutoIT fetch returned 404!");
            }
        }
    }

    public class CSVDECODER
    {
        public static string[] CSVcodes;
        public static string[] Troops;
        public static int[] Vector;
        public static int[] TroopsNum;
        public static int ArmyCamp;
        static int x;
        public static void CSVDecode()
        {
            Array.Resize(ref Troops, CSVcodes.Length);
            Array.Resize(ref Vector, CSVcodes.Length);
            Array.Resize(ref TroopsNum, CSVcodes.Length);
            x = 0;
            foreach (string code in CSVcodes)
            {
                string[] t = code.Split('|');
                List<string> temp = new List<string>();
                foreach (var word in t)
                {
                    temp.Add(word.Trim());
                }
                if (temp.Contains("DROP", StringComparer.OrdinalIgnoreCase))
                {
                    if (temp[1].Contains('-'))
                    {
                        string[] vectorsplit = temp[1].Split('-');
                        foreach (var v in vectorsplit)
                        {
                            Vector[x]++;
                        }
                    }
                    else
                    {
                        Vector[x] = 1;
                    }
                    if (string.Equals(temp[4], "barb", StringComparison.OrdinalIgnoreCase))
                    {
                        func("Barb", temp, x);
                    }
                    else if (string.Equals(temp[4], "arch", StringComparison.OrdinalIgnoreCase))
                    {
                        func("Arch", temp, x);
                    }
                    else if (string.Equals(temp[4], "gobl", StringComparison.OrdinalIgnoreCase))
                    {
                        func("Gobl", temp, x);
                    }
                    else if (string.Equals(temp[4], "giant", StringComparison.OrdinalIgnoreCase))
                    {
                        func("Giant", temp, x);
                    }
                    else if (string.Equals(temp[4], "wiza", StringComparison.OrdinalIgnoreCase))
                    {
                        func("Wiza", temp, x);
                    }
                    else if (string.Equals(temp[4], "ball", StringComparison.OrdinalIgnoreCase))
                    {
                        func("Ball", temp, x);
                    }
                    else if (string.Equals(temp[4], "wall", StringComparison.OrdinalIgnoreCase))
                    {
                        func("Wall", temp, x);
                    }
                    else if (string.Equals(temp[4], "heal", StringComparison.OrdinalIgnoreCase))
                    {
                        func("Heal", temp, x);
                    }
                    else if (string.Equals(temp[4], "drag", StringComparison.OrdinalIgnoreCase))
                    {
                        func("Drag", temp, x);
                    }
                    else if (string.Equals(temp[4], "pekk", StringComparison.OrdinalIgnoreCase))
                    {
                        func("Pekk", temp, x);
                    }
                    else if (string.Equals(temp[4], "babydrag", StringComparison.OrdinalIgnoreCase))
                    {
                        func("BabyDrag", temp, x);
                    }
                    else if (string.Equals(temp[4], "mine", StringComparison.OrdinalIgnoreCase))
                    {
                        func("Mine", temp, x);
                    }
                    else if (string.Equals(temp[4], "mini", StringComparison.OrdinalIgnoreCase))
                    {
                        func("Mini", temp, x);
                    }
                    else if (string.Equals(temp[4], "hogs", StringComparison.OrdinalIgnoreCase))
                    {
                        func("Hogs", temp, x);
                    }
                    else if (string.Equals(temp[4], "valk", StringComparison.OrdinalIgnoreCase))
                    {
                        func("Valk", temp, x);
                    }
                    else if (string.Equals(temp[4], "gole", StringComparison.OrdinalIgnoreCase))
                    {
                        func("Gole", temp, x);
                    }
                    else if (string.Equals(temp[4], "witc", StringComparison.OrdinalIgnoreCase))
                    {
                        func("Witc", temp, x);
                    }
                    else if (string.Equals(temp[4], "lava", StringComparison.OrdinalIgnoreCase))
                    {
                        func("Lava", temp, x);
                    }
                    else if (string.Equals(temp[4], "Bowl", StringComparison.OrdinalIgnoreCase))
                    {
                        func("Bowl", temp, x);
                    }
                    else if (string.Equals(temp[4], "LSpell", StringComparison.OrdinalIgnoreCase))
                    {
                        func("LSpell", temp, x);
                    }
                    else if (string.Equals(temp[4], "HSpell", StringComparison.OrdinalIgnoreCase))
                    {
                        func("HSpell", temp, x);
                    }
                    else if (string.Equals(temp[4], "RSpell", StringComparison.OrdinalIgnoreCase))
                    {
                        func("RSpell", temp, x);
                    }
                    else if (string.Equals(temp[4], "JSpell", StringComparison.OrdinalIgnoreCase))
                    {
                        func("JSpell", temp, x);
                    }
                    else if (string.Equals(temp[4], "FSpell", StringComparison.OrdinalIgnoreCase))
                    {
                        func("FSpell", temp, x);
                    }
                    else if (string.Equals(temp[4], "PSpell", StringComparison.OrdinalIgnoreCase))
                    {
                        func("PSpell", temp, x);
                    }
                    else if (string.Equals(temp[4], "ESpell", StringComparison.OrdinalIgnoreCase))
                    {
                        func("ESpell", temp, x);
                    }
                    else if (string.Equals(temp[4], "HaSpell", StringComparison.OrdinalIgnoreCase))
                    {
                        func("HaSpell", temp, x);
                    }
                    else if (string.Equals(temp[4], "CSpell", StringComparison.OrdinalIgnoreCase))
                    {
                        func("CSpell", temp, x);
                    }
                    else if (string.Equals(temp[4], "SkSpell", StringComparison.OrdinalIgnoreCase))
                    {
                        func("SkSpell", temp, x);
                    }
                    else if (string.Equals(temp[4], "EDrag", StringComparison.OrdinalIgnoreCase))
                    {
                        func("EDrag", temp, x);
                    }
                }
                x++;
            }
        }
        private static void func(string troopname, List<string> temp, int x)
        {
            int y = 0;
            if (Troops.Contains(troopname))
            {
                y = Array.IndexOf(Troops, troopname);
                if (temp[3].Contains('-'))
                {
                    string[] QTYSplit = temp[3].Split('-');
                    TroopsNum[y] = TroopsNum[y] + (Convert.ToInt32(QTYSplit[0]) * Vector[x]);
                }
                else
                {
                    try
                    {
                        TroopsNum[y] = TroopsNum[y] + (Convert.ToInt32(temp[3]) * Vector[x]);
                    }
                    catch
                    {
                        MessageBox.Show("CSV contains error!");
                    }

                }
            }
            else
            {
                Troops[x] = troopname;
                if (temp[3].Contains('-'))
                {
                    string[] QTYSplit = temp[3].Split('-');
                    TroopsNum[x] = Convert.ToInt32(QTYSplit.Last()) * Vector[x];
                }
                else
                {
                    try
                    {
                        TroopsNum[x] = Convert.ToInt32(temp[3]) * Vector[x];
                    }
                    catch
                    {
                        TroopsNum[x] = 0;
                        MessageBox.Show("CSV contains error!");
                    }
                }
            }
        }
        public static int SetTroops(string troops)
        {
            if (Troops.Contains(troops))
            {
                int y = Array.IndexOf(Troops, troops);
                return TroopsNum[y];
            }
            else
            {
                return 0;
            }
        }
    }
}
