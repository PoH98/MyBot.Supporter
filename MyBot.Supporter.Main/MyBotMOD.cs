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
        /*public static void ClanGameFix(bool fix)
        {
            if (fix)
            {
                File.Delete(Environment.CurrentDirectory + "\\COCBot\\functions\\Village\\Clan Games\\ClanGames.au3");
                File.Move(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\ClanGames.au3", Environment.CurrentDirectory + "\\COCBot\\functions\\Village\\Clan Games\\ClanGames.au3");
                Task.Delay(1000);
                File.Delete(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\ClanGames.au3");
            }
            else
            {
                if (!Directory.Exists(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD"))
                {
                    Directory.CreateDirectory(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD");
                }
                File.Copy(Environment.CurrentDirectory + "\\COCBot\\functions\\Village\\Clan Games\\ClanGames.au3", Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\ClanGames.au3");
                string[] code = File.ReadAllLines(Environment.CurrentDirectory + "\\COCBot\\functions\\Village\\Clan Games\\ClanGames.au3");
                int x = 0;
                foreach (var c in code)
                {
                    if (c.Contains("If Not IsClanGamesEvent() Then Return"))
                    {
                        code[x] = ";Edited By Supporter_If Not IsClanGamesEvent() Then Return";
                    }
                    x++;
                }
                File.WriteAllLines(Environment.CurrentDirectory + "\\COCBot\\functions\\Village\\Clan Games\\ClanGames.au3", code, Encoding.Unicode);
            }
        }*/
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
        public static void ExtendBar(bool extend)
        {
            if (extend)
            {
                File.Delete(Environment.CurrentDirectory + "\\COCBot\\MBR Functions.au3");
                File.Delete(Environment.CurrentDirectory + "\\COCBot\\MBR Global Variables.au3");
                File.Delete(Environment.CurrentDirectory + "\\COCBot\\functions\\Attack\\AttackCSV\\DropTroopFromINI.au3");
                File.Delete(Environment.CurrentDirectory + "\\COCBot\\functions\\Attack\\PrepareAttack.au3");
                File.Delete(Environment.CurrentDirectory + "\\COCBot\\functions\\Supporter\\ExtendedBar.au3");
                File.Delete(Environment.CurrentDirectory + "\\COCBot\\functions\\Attack\\Troops\\CheckHeroesHealth.au3");
                File.Delete(Environment.CurrentDirectory + "\\COCBot\\functions\\Attack\\Troops\\GetXPosOfArmySlot.au3");
                File.Delete(Environment.CurrentDirectory + "\\COCBot\\functions\\Image Search\\imglocAttackBar.au3");

                File.Move(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\MBR Functions.au3", Environment.CurrentDirectory + "\\COCBot\\MBR Functions.au3");
                File.Move(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\MBR Global Variables.au3", Environment.CurrentDirectory + "\\COCBot\\MBR Global Variables.au3");
                File.Move(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\DropTroopFromINI.au3", Environment.CurrentDirectory + "\\COCBot\\functions\\Attack\\AttackCSV\\DropTroopFromINI.au3");
                File.Move(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\PrepareAttack.au3", Environment.CurrentDirectory + "\\COCBot\\functions\\Attack\\PrepareAttack.au3");
                File.Move(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\CheckHeroesHealth.au3", Environment.CurrentDirectory + "\\COCBot\\functions\\Attack\\Troops\\CheckHeroesHealth.au3");
                File.Move(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\GetXPosOfArmySlot.au3", Environment.CurrentDirectory + "\\COCBot\\functions\\Attack\\Troops\\GetXPosOfArmySlot.au3");
                File.Move(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\imglocAttackBar.au3", Environment.CurrentDirectory + "\\COCBot\\functions\\Image Search\\imglocAttackBar.au3");
            }
            else
            {
                if (!Directory.Exists(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD"))
                {
                    Directory.CreateDirectory(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD");
                }
                File.Copy(Environment.CurrentDirectory + "\\COCBot\\MBR Functions.au3", Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\MBR Functions.au3");
                int x = 0;
                string[] code = File.ReadAllLines(Environment.CurrentDirectory + "\\COCBot\\MBR Functions.au3");
                foreach(var c in code)
                {
                    if (c.Contains("#include \"functions\\Attack\\AttackReport.au3\""))
                    {
                        code[x] = c + Environment.NewLine + "#include \"functions\\Supporter\\ExtendedBar.au3\"";
                        break;
                    }
                    x++;
                }
                File.WriteAllLines(Environment.CurrentDirectory + "\\COCBot\\MBR Functions.au3",code);
                File.Copy(Environment.CurrentDirectory + "\\COCBot\\MBR Global Variables.au3", Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\MBR Global Variables.au3");
                x = 0;
                code = File.ReadAllLines(Environment.CurrentDirectory + "\\COCBot\\MBR Global Variables.au3");
                foreach (var c in code)
                {
                    if (c.Contains("Global $g_bChkCollectFreeMagicItems = True"))
                    {
                        code[x] = c + Environment.NewLine + "Global $g_abChkExtendedAttackBar[2] = [True, True]" + Environment.NewLine + "Global $g_iTotalAttackSlot = 10, $g_bDraggedAttackBar = False";
                        break;
                    }
                    x++;
                }
                File.WriteAllLines(Environment.CurrentDirectory + "\\COCBot\\MBR Global Variables.au3", code);
                File.Copy(Environment.CurrentDirectory + "\\COCBot\\functions\\Attack\\AttackCSV\\DropTroopFromINI.au3", Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\DropTroopFromINI.au3");
                x = 0;
                code = File.ReadAllLines(Environment.CurrentDirectory + "\\COCBot\\functions\\Attack\\AttackCSV\\DropTroopFromINI.au3");
                foreach(var c in code)
                {
                    if (c.Contains("Local $troopPosition = -1"))
                    {
                        code[x] = "Local $troopPosition = -1, $troopSlotConst = -1";
                    }
                    else if (c.Contains("Local $troopCount = -1"))
                    {
                        code[x] = "";
                    }
                    else if (c.Contains("If $g_avAttackTroops[$i][0] = $iTroopIndex And $g_avAttackTroops[$i][1] >= $troopCount Then"))
                    {
                        code[x] = "If $g_avAttackTroops[$i][0] = $iTroopIndex And $g_avAttackTroops[$i][1] > 0 Then";
                    }
                    else if (c.Contains("$troopPosition = $i"))
                    {
                        code[x] = "$troopSlotConst = $i";
                    }
                    else if (c.Contains("$troopCount = $g_avAttackTroops[$i][1]"))
                    {
                        code[x] = "$troopPosition = $troopSlotConst" + Environment.NewLine + "ExitLoop";
                    }
                    else if (c.Contains("Local $usespell = True"))
                    {
                        code[x] = "If $troopSlotConst >= 0 And $troopSlotConst < $g_iTotalAttackSlot - 10 Then" + Environment.NewLine + "If $g_bDraggedAttackBar Then DragAttackBar($g_iTotalAttackSlot, True); return drag" + Environment.NewLine + "ElseIf $troopSlotConst > 10 Then; can only be selected when in 2nd page of troopbar" + Environment.NewLine +
"If $g_bDraggedAttackBar = False Then DragAttackBar($g_iTotalAttackSlot, False); drag forward" + Environment.NewLine + "EndIf" + Environment.NewLine + "If $g_bDraggedAttackBar And $troopPosition > -1 Then" + Environment.NewLine + "$troopPosition = $troopSlotConst - ($g_iTotalAttackSlot - 10)" + Environment.NewLine + "EndIf" + Environment.NewLine + c;
                    }
                    else if (c.Contains("dropHeroes($pixel[0], $pixel[1], $g_iKingSlot, -1, -1)"))
                    {
                        code[x] = "dropHeroes($pixel[0], $pixel[1], $troopPosition, -1, -1) ";
                    }
                    else if (c.Contains("dropHeroes($pixel[0], $pixel[1], -1, $g_iQueenSlot, -1)"))
                    {
                        code[x] = "dropHeroes($pixel[0], $pixel[1], -1, $troopPosition, -1)";
                    }
                    else if (c.Contains("dropHeroes($pixel[0], $pixel[1], -1, -1, $g_iWardenSlot)"))
                    {
                        code[x] = "dropHeroes($pixel[0], $pixel[1], -1, -1, $troopPosition)";
                    }
                    else if (c.Contains("dropCC($pixel[0], $pixel[1], $g_iClanCastleSlot)"))
                    {
                        code[x] = "dropCC($pixel[0], $pixel[1], $troopPosition)";
                    }
                    else if (c.Contains("If UBound($g_avAttackTroops) > $troopPosition And $g_avAttackTroops[$troopPosition][1] > 0 Then $g_avAttackTroops[$troopPosition][1] -= 1"))
                    {
                        code[x] = "If UBound($g_avAttackTroops) > $troopSlotConst And $g_avAttackTroops[$troopSlotConst][1] > 0 And $qty2 > 0 Then" + Environment.NewLine + "$g_avAttackTroops[$troopSlotConst][1] -= $qty2" + Environment.NewLine + "EndIf";
                        break;
                    }
                    x++;
                }
                File.WriteAllLines(Environment.CurrentDirectory + "\\COCBot\\functions\\Attack\\AttackCSV\\DropTroopFromINI.au3", code);
                File.Copy(Environment.CurrentDirectory + "\\COCBot\\functions\\Attack\\PrepareAttack.au3", Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\PrepareAttack.au3");
                x = 0;
                code = File.ReadAllLines(Environment.CurrentDirectory + "\\COCBot\\functions\\Attack\\PrepareAttack.au3");
                foreach(var c in code)
                {
                    if (c.Contains("If $g_iActivateWarden = 1 Or $g_iActivateWarden = 2 Then $g_aHeroesTimerActivation[$eHeroGrandWarden] = 0"))
                    {
                        code[x] = c + Environment.NewLine + "$g_iTotalAttackSlot = 10"+ Environment.NewLine +"$g_bDraggedAttackBar = False";
                    }
                    else if (c.Contains("If $result <> \"\" Then"))
                    {
                        code[x] = "If $pMatchMode <= $LB Then" + Environment.NewLine + "If $g_abChkExtendedAttackBar[$pMatchMode] Then" + Environment.NewLine + "ReDim $aTemp[22][3]" + Environment.NewLine + "ReDim $g_avAttackTroops[22][2]" + Environment.NewLine + "EndIf" + Environment.NewLine + "EndIf" + Environment.NewLine + c;
                        break;
                    }
                    x++;
                }
                File.WriteAllLines(Environment.CurrentDirectory + "\\COCBot\\functions\\Attack\\PrepareAttack.au3", code);
                File.Copy(Environment.CurrentDirectory + "\\COCBot\\functions\\Attack\\Troops\\CheckHeroesHealth.au3", Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\CheckHeroesHealth.au3");
                x = 0;
                code = File.ReadAllLines(Environment.CurrentDirectory + "\\COCBot\\functions\\Attack\\Troops\\CheckHeroesHealth.au3");
                foreach (var c in code)
                {
                    if (c.Contains("ForceCaptureRegion() ; ensure no screenshot caching kicks in"))
                    {
                        code[x] = c + Environment.NewLine + "Local $TempKingSlot = $g_iKingSlot" + Environment.NewLine + "Local $TempQueenSlot = $g_iQueenSlot" + Environment.NewLine + "Local $TempWardenSlot = $g_iWardenSlot" + Environment.NewLine + "If $g_iKingSlot >= 11 Or $g_iQueenSlot >= 11 Or $g_iWardenSlot >= 11 Then"
+ Environment.NewLine + "If $g_bDraggedAttackBar = False Then DragAttackBar($g_iTotalAttackSlot, False); drag forward" + Environment.NewLine + "ElseIf $g_iKingSlot >= 0 And $g_iQueenSlot >= 0 And $g_iWardenSlot >= 0 And($g_iKingSlot < $g_iTotalAttackSlot - 10 Or $g_iQueenSlot < $g_iTotalAttackSlot - 10 Or $g_iWardenSlot < $g_iTotalAttackSlot - 10) Then" + Environment.NewLine +
"If $g_bDraggedAttackBar Then DragAttackBar($g_iTotalAttackSlot, True); return drag" + Environment.NewLine + "EndIf" + Environment.NewLine + "If $g_bDraggedAttackBar Then" + Environment.NewLine + "$TempKingSlot -= $g_iTotalAttackSlot - 10" + Environment.NewLine + "$TempQueenSlot -= $g_iTotalAttackSlot - 10" + Environment.NewLine + "$TempWardenSlot -= $g_iTotalAttackSlot - 10"
+ Environment.NewLine + "EndIf";
                    }
                    else if (c.Contains("$aQueenHealthCopy[0] = GetXPosOfArmySlot($g_iQueenSlot, 68) + 3"))
                    {
                        code[x] = "$aQueenHealthCopy[0] = GetXPosOfArmySlot($TempQueenSlot, 68) + 3";
                    }
                    else if (c.Contains("SelectDropTroop($g_iQueenSlot)"))
                    {
                        code[x] = "SelectDropTroop($TempQueenSlot)";
                    }
                    else if (c.Contains("$aKingHealthCopy[0] = GetXPosOfArmySlot($g_iKingSlot, 68) + 2"))
                    {
                        code[x] = "$aKingHealthCopy[0] = GetXPosOfArmySlot($TempKingSlot, 68) + 2 ;";
                    }
                    else if (c.Contains("SelectDropTroop($g_iKingSlot)"))
                    {
                        code[x] = "SelectDropTroop($TempKingSlot)";
                    }
                    else if (c.Contains("$aWardenHealthCopy[0] = GetXPosOfArmySlot($g_iWardenSlot, 68)"))
                    {
                        code[x] = "$aWardenHealthCopy[0] = GetXPosOfArmySlot($TempWardenSlot, 68)";
                    }
                    else if (c.Contains("SelectDropTroop($g_iWardenSlot)"))
                    {
                        code[x] = "SelectDropTroop($TempWardenSlot)";
                    }
                    x++;
                }
                File.WriteAllLines(Environment.CurrentDirectory + "\\COCBot\\functions\\Attack\\Troops\\CheckHeroesHealth.au3", code);
                File.Copy(Environment.CurrentDirectory + "\\COCBot\\functions\\Attack\\Troops\\GetXPosOfArmySlot.au3", Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\GetXPosOfArmySlot.au3");
                x = 0;
                code = File.ReadAllLines(Environment.CurrentDirectory + "\\COCBot\\functions\\Attack\\Troops\\GetXPosOfArmySlot.au3");
                foreach (var c in code)
                {
                    if (c.Contains("If $slotNumber = $g_iKingSlot Or $slotNumber = $g_iQueenSlot Or $slotNumber = $g_iWardenSlot Then $xOffsetFor11Slot += 8"))
                    {
                        code[x] = c + Environment.NewLine + "If $g_bDraggedAttackBar Then Return $xOffsetFor11Slot + $SlotComp + ($slotNumber * 72) + 14";
                        break;
                    }
                    x++;
                }
                File.WriteAllLines(Environment.CurrentDirectory + "\\COCBot\\functions\\Attack\\Troops\\GetXPosOfArmySlot.au3", code);
                File.Copy(Environment.CurrentDirectory + "\\COCBot\\functions\\Image Search\\imglocAttackBar.au3", Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\imglocAttackBar.au3");
                x = 0;
                code = File.ReadAllLines(Environment.CurrentDirectory + "\\COCBot\\functions\\Image Search\\imglocAttackBar.au3");
                foreach (var c in code)
                {
                    if (c.Contains("$g_iLSpellLevel = 1"))
                    {
                        code[x] = "If $g_bDraggedAttackBar Then DragAttackBar($g_iTotalAttackSlot, True)" + Environment.NewLine + c;
                        break;
                    }
                    x++;
                }
                x = 0;
                foreach(var c in code)
                {
                    if (c.Contains("$strinToReturn &= \"|\" & TroopIndexLookup($aResult[$i][0]) & \"#\" & $aResult[$i][4] & \"#\" & $aResult[$i][3]"))
                    {
                        code[x] = "If $aResult[$i][4] <= 10 Then" + Environment.NewLine + "$strinToReturn &= \"|\" & TroopIndexLookup($aResult[$i][0]) & \"#\" & $aResult[$i][4] & \"#\" & $aResult[$i][3]" + Environment.NewLine + "EndIf";
                    }
                    else if (c.Contains("$strinToReturn = StringTrimLeft($strinToReturn, 1)"))
                    {
                        code[x] = "If $g_iMatchMode <= $LB Then" + Environment.NewLine + "If $g_abChkExtendedAttackBar[$g_iMatchMode] And $CheckSlot12 And IsArray($aResult) Then" + Environment.NewLine + "SetDebuglog(\"$strinToReturn 1st page = \" & $strinToReturn)" + Environment.NewLine + "Local $sLastTroop1stPage = $aResult[UBound($aResult) - 1][0]" + Environment.NewLine
+ "DragAttackBar()" + Environment.NewLine + "$strinToReturn &= ExtendedAttackBarCheck($sLastTroop1stPage, $Remaining)" + Environment.NewLine + "If Not $Remaining Then DragAttackBar($g_iTotalAttackSlot, True); return drag" + Environment.NewLine + "EndIf" + Environment.NewLine + "EndIf" + Environment.NewLine + c;
                    }
                    x++;
                }
                File.WriteAllLines(Environment.CurrentDirectory + "\\COCBot\\functions\\Image Search\\imglocAttackBar.au3", code);
                if (!Directory.Exists(Environment.CurrentDirectory + "\\COCBot\\functions\\Supporter"))
                {
                    Directory.CreateDirectory(Environment.CurrentDirectory + "\\COCBot\\functions\\Supporter");
                }
                File.Copy("ExtendedBar.au3", Environment.CurrentDirectory + "\\COCBot\\functions\\Supporter\\ExtendedBar.au3");
            }
        }
    }
}
