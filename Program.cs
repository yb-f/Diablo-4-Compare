using CsvHelper;
using CsvHelper.Configuration.Attributes;
using Diablo_4_Compare;
using IronOcr;
using Microsoft.VisualBasic.ApplicationServices;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using static IronOcr.OcrResult;



namespace Diablo_4_Compare
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }

    public class Calculate
    {
        //public static (double base_min, double base_max, double avg_min, double avg_max, double crit_min, double crit_max, double OP_min, double OP_Max) Calculate_Damage(Panel panel1, Panel panel2, Panel panel3, Panel panel4)
        public static void Calculate_Damage(Panel panel1, Panel panel2, Panel panel3, Panel panel4)
        {
            double additive = 1;
            double multiplicative = 1;
            double csc = 0;
            double csd = 0;
            double vuln = 1;
            double opc = 0;
            double opd = 0;
            int healthfort = 0;
            double sac_opd = 1;
            double sac_csd = 1;
            double sac_csc = 1;
            double sac_mult = 1;
            double zerk_bonus = 0;
            double zerk_mult = 1;
            double csd_mult = 1;
            double csc_mult = 1;
            double opd_mult = 1;

            TableLayoutPanel skills_table = ((TableLayoutPanel)panel1.Controls["skillsTable"]);
            TableLayoutPanel abs_table = ((TableLayoutPanel)panel2.Controls["absTable"]);
            TableLayoutPanel glyph_table = ((TableLayoutPanel)panel3.Controls["glyphTable"]);
            TableLayoutPanel mainstat_table = ((TableLayoutPanel)panel4.Controls["mainstat_Table"]);
            TableLayoutPanel weapon1_table = ((TableLayoutPanel)panel4.Controls["weapon1_Table"]);
            TableLayoutPanel weapon2_table = ((TableLayoutPanel)panel4.Controls["weapon2_Table"]);
            TableLayoutPanel minmax1_table = ((TableLayoutPanel)panel4.Controls["MinMax1_Table"]);
            TableLayoutPanel minmax2_table = ((TableLayoutPanel)panel4.Controls["MinMax2_Table"]);
            for (int i = 0; i < skills_table.Controls.Count; i++)
            { 
                if (skills_table.GetColumn(skills_table.Controls[i]) == 2)
                {
                    CheckBox cb = ((CheckBox)skills_table.Controls[i - 2]);
                    if (cb.Checked == true)
                    {
                        if (skills_table.Controls[i].Name.Contains("CriticalStrikeChance"))
                        {
                            csc = csc + (Convert.ToDouble(skills_table.Controls[i].Text) / 100);
                        }
                        if (skills_table.Controls[i].Name.Contains("CriticalStrikeDamage"))
                        {
                            csd = csd + (Convert.ToDouble(skills_table.Controls[i].Text) / 100);
                        }
                        if (skills_table.Controls[i].Name.Contains("VulnerableDamage"))
                        {
                            vuln = vuln + (Convert.ToDouble(skills_table.Controls[i].Text) / 100);
                        }
                        if (skills_table.Controls[i].Name.Contains("OverpowerChance"))
                        {
                            opc = opc + (Convert.ToDouble(skills_table.Controls[i].Text) / 100);
                        }
                        if (skills_table.Controls[i].Name.Contains("OverpowerDamage"))
                        {
                            opd = opd + (Convert.ToDouble(skills_table.Controls[i].Text) / 100);
                        }
                        if (skills_table.Controls[i].Name.Contains("BaseHealth") || skills_table.Controls[i].Name.Equals("Fortify"))
                        {
                            healthfort = healthfort + Convert.ToInt32(skills_table.Controls[i].Text);
                        }
                    }
                }
                if (skills_table.GetColumn(skills_table.Controls[i]) == 5 || skills_table.GetColumn(skills_table.Controls[i]) == 8)
                {
                    CheckBox cb = ((CheckBox)skills_table.Controls[i - 2]);
                    if (cb.Checked == true)
                    {
                        if (skills_table.Controls[i].Name.Contains("DamagewhileBerserking"))
                        {
                            zerk_bonus = zerk_bonus + (Convert.ToDouble(skills_table.Controls[i].Text) / 100) + 0.25;
                        }
                        additive = additive + (Convert.ToDouble(skills_table.Controls[i].Text) / 100);
                    }
                }
            }
            for (int i = 0; i < abs_table.Controls.Count; i++)
            {
                if (abs_table.GetColumn(abs_table.Controls[i]) ==2 || abs_table.GetColumn(abs_table.Controls[i]) == 5 || abs_table.GetColumn(abs_table.Controls[i]) == 8)
                {
                    CheckBox cb = ((CheckBox)abs_table.Controls[i - 2]);
                    if (cb.Checked == true)
                    {
                        switch (abs_table.Controls[i].Name)
                        {
                            case var _ when abs_table.Controls[i].Name.Contains("AdvancedRapidFire"):
                            case var _ when abs_table.Controls[i].Name.Contains("AvianWrath"):
                            case var _ when abs_table.Controls[i].Name.Contains("BestialRampage"):
                            case var _ when abs_table.Controls[i].Name.Contains("DevouringBlaze"):
                            case var _ when abs_table.Controls[i].Name.Contains("EnhancedDash"):
                            case var _ when abs_table.Controls[i].Name.Contains("Envenom"):
                            case var _ when abs_table.Controls[i].Name.Contains("EsusFerocityCSD"):
                            case var _ when abs_table.Controls[i].Name.Contains("Evulsion"):
                            case var _ when abs_table.Controls[i].Name.Contains("ExpertiseTwoHandedMace"):
                            case var _ when abs_table.Controls[i].Name.Contains("HeavyHanded"):
                            case var _ when abs_table.Controls[i].Name.Contains("Precision"):
                            case var _ when abs_table.Controls[i].Name.Contains("PrimalLandslide"):
                            case var _ when abs_table.Controls[i].Name.Contains("PrimalShred"):
                            case var _ when abs_table.Controls[i].Name.Contains("PrimeIronMaelstromCSD"):
                            case var _ when abs_table.Controls[i].Name.Contains("WeaponMasteryCSD"):
                                csd_mult = csd_mult * (1 + (Convert.ToDouble(abs_table.Controls[i].Text) / 100));
                                break;
                            case var _ when abs_table.Controls[i].Name.Contains("SacrificeGolemIron"):
                                sac_csd = sac_csd * (1 + (Convert.ToDouble(abs_table.Controls[i].Text) / 100));
                                break;
                            case var _ when abs_table.Controls[i].Name.Contains("EsusFerocityCSC"):
                                csc_mult = csc_mult * (1 + (Convert.ToDouble(abs_table.Controls[i].Text) / 100));
                                break;
                            case var _ when abs_table.Controls[i].Name.Contains("SacrificeWarriorsSkirmishers"):
                                sac_csc = sac_csc * (1 + (Convert.ToDouble(abs_table.Controls[i].Text) / 100));
                                break;
                            case var _ when abs_table.Controls[i].Name.Contains("BruteForce"):
                            case var _ when abs_table.Controls[i].Name.Contains("TidesofBlood"):
                                opd_mult = opd_mult * (1 + (Convert.ToDouble(abs_table.Controls[i].Text) / 100));
                                break;
                            case var _ when abs_table.Controls[i].Name.Contains("SacrificeMagesBone"):
                                sac_opd = sac_opd * (1 + (Convert.ToDouble(abs_table.Controls[i].Text) / 100));
                                break;
                            case var _ when abs_table.Controls[i].Name.Contains("MementoMori"):
                                sac_csd = sac_csd * (1 + (Convert.ToDouble(abs_table.Controls[i].Text) / 100));
                                sac_csc = sac_csc * (1 + (Convert.ToDouble(abs_table.Controls[i].Text) / 100));
                                sac_opd = sac_opd * (1 + (Convert.ToDouble(abs_table.Controls[i].Text) / 100));
                                break;
                            case var _ when abs_table.Controls[i].Name.Contains("SupremeWrathoftheBerserker"):
                                zerk_mult = zerk_mult * (1 + (Convert.ToDouble(abs_table.Controls[i].Text) / 100));
                                break;
                            case var _ when abs_table.Controls[i].Name.Contains("SacrificeMagesCold"):
                            case var _ when abs_table.Controls[i].Name.Contains("SacrificeWarriorsReapers"):
                                sac_mult = sac_mult * (1 + (Convert.ToDouble(abs_table.Controls[i].Text) / 100));
                                break;
                            default:
                                multiplicative = multiplicative * (1 + (Convert.ToDouble(abs_table.Controls[i].Text) / 100));
                                break;
                        }
                    }
                }
            }
            for (int i = 0; i < glyph_table.Controls.Count; i++)
            {
                if (glyph_table.GetColumn(glyph_table.Controls[i]) == 2)
                {
                    CheckBox cb = ((CheckBox)glyph_table.Controls[i - 2]);
                    if (cb.Checked == true)
                    {
                        switch (glyph_table.Controls[i].Name)
                        {
                            case var _ when glyph_table.Controls[i].Name.Contains("Cleaver"):
                            case var _ when glyph_table.Controls[i].Name.Contains("Essence"):
                                csd = csd * (1 + (Convert.ToDouble(glyph_table.Controls[i].Text) / 100));
                                break;
                            case var _ when glyph_table.Controls[i].Name.Contains("crusher"):
                                opd = opd * (1 + (Convert.ToDouble(glyph_table.Controls[i].Text) / 100));
                                break;
                            default:
                                multiplicative = multiplicative * (1 + (Convert.ToDouble(glyph_table.Controls[i].Text) / 100));
                                break;
                        }
                    }
                }
            }
            int mainStat = Convert.ToInt32(mainstat_table.Controls["mainStat_Text"].Text);
            double skillDamage = (Convert.ToDouble(mainstat_table.Controls["skillDamage_Text"].Text) / 100);
            int weapon1_min = Convert.ToInt32(weapon1_table.Controls["weapon1Min_Text"].Text);
            int weapon1_max = Convert.ToInt32(weapon1_table.Controls["weapon1Max_Text"].Text);
            //int weapon2_min = Convert.ToInt32(weapon2_table.Controls["weapon2Min_Text"].Text);
            //int weapon2_max = Convert.ToInt32(weapon2_table.Controls["weapon2Max_Text"].Text);
            double weapon1_dam_base_min = ((((weapon1_min + weapon1_max) / 2) * 0.9 )* skillDamage) * (1 + (mainStat * 0.001));
            double weapon1_dam_base_max = ((((weapon1_min + weapon1_max) / 2) * 1.1) * skillDamage) * (1 + (mainStat * 0.001));
            double additive1 = additive + (zerk_bonus * zerk_mult);
            double weapon1_total_base_dam_min = weapon1_dam_base_min * additive1 * multiplicative * vuln;
            double weapon1_total_base_dam_max = weapon1_dam_base_max * additive1 * multiplicative * vuln;
            minmax1_table.Controls["weapon1BaseMin_Lbl"].Text = weapon1_total_base_dam_min.ToString("N1");
            minmax1_table.Controls["weapon1BaseMax_Lbl"].Text = weapon1_total_base_dam_max.ToString("N1");
            double weapon1_crit_min = weapon1_total_base_dam_min * (1 + (csd * csd_mult));
            double weapon1_crit_max = weapon1_total_base_dam_max * (1 + (csd * csd_mult));
            minmax1_table.Controls["weapon1CritMin_Lbl"].Text = weapon1_crit_min.ToString("N1");
            minmax1_table.Controls["weapon1CritMax_Lbl"].Text = weapon1_crit_max.ToString("N1");
            double overpower1_min = ((healthfort * (additive1 * multiplicative)) * (opd * opd_mult)) + weapon1_total_base_dam_min;
            double overpower1_max = ((healthfort * (additive1 * multiplicative)) * (opd * opd_mult)) + weapon1_total_base_dam_max;
            minmax1_table.Controls["weapon1OPMin_Lbl"].Text = overpower1_min.ToString("N1");
            minmax1_table.Controls["weapon1OPMax_Lbl"].Text = overpower1_max.ToString("N1");
            double weapon1_average_min = weapon1_total_base_dam_min * (1 + ((csc * csc_mult) * (csd * csd_mult))) + (((healthfort * (additive1 * multiplicative)) * (opd * opd_mult)) * opc);
            double weapon1_average_max = weapon1_total_base_dam_max * (1 + ((csc * csc_mult) * (csd * csd_mult))) + (((healthfort * (additive1 * multiplicative)) * (opd * opd_mult)) * opc);
            minmax1_table.Controls["weapon1AvgMin_Lbl"].Text = weapon1_average_min.ToString("N1");
            minmax1_table.Controls["weapon1AvgMax_Lbl"].Text = weapon1_average_max.ToString("N1");

            string skill_name = "";
            List<Control> combo_boxes = new List<Control>();
            combo_boxes.Add(weapon1_table.Controls["weaponImplicit1_Combo"]);
            combo_boxes.Add(weapon1_table.Controls["weapon1Stat1_Combo"]);
            combo_boxes.Add(weapon1_table.Controls["weapon1Stat2_Combo"]);
            combo_boxes.Add(weapon1_table.Controls["weapon1Stat3_Combo"]);
            combo_boxes.Add(weapon1_table.Controls["weapon1Stat4_Combo"]);
            combo_boxes.Add(weapon1_table.Controls["weapon1Stat5_Combo"]);
            foreach (Control c in combo_boxes)
            {
                
                skill_name = Calculate.Get_Stat_Name(c.Text);
                if (!(skill_name == "nada"))
                {
                    string the_text = c.Name;
                    the_text = the_text.Substring(0, the_text.IndexOf("_"));
                    the_text = the_text + "_Text";
                    TextBox tb = ((TextBox)weapon1_table.Controls[the_text]);
                    CheckBox cb = ((CheckBox)skills_table.Controls[skill_name+"_CB"]);
                    if (skill_name.Equals("mainStat"))
                    {
                        mainStat = mainStat - (Convert.ToInt32(tb.Text));
                    }
                    else if (cb.Checked == true)
                    {
                        if (skill_name.Contains("CriticalStrikeDamage"))
                        {
                            csd = csd - (Convert.ToDouble(tb.Text) / 100);
                        }
                        else if (skill_name.Equals("OverpowerDamage"))
                        {
                            opd = opd - (Convert.ToDouble(tb.Text) / 100);
                        }
                        else if (skill_name.Equals("VulnerableDamage"))
                        {
                            vuln = vuln - (Convert.ToDouble(tb.Text) / 100);
                        }
                        else if (skill_name.Equals("DamagewhileBerserking"))
                        {
                            zerk_bonus = 0 + Convert.ToInt32 (skills_table.Controls["DamagewhileBerserking_Text"].Text) - Convert.ToInt32(tb.Text);
                        }
                        else
                        {
                            additive = additive - (Convert.ToDouble(tb.Text) / 100);
                        }

                    }
                }
            }
            combo_boxes.Clear();
            combo_boxes.Add(weapon2_table.Controls["weaponImplicit2_Combo"]);
            combo_boxes.Add(weapon2_table.Controls["weapon2Stat1_Combo"]);
            combo_boxes.Add(weapon2_table.Controls["weapon2Stat2_Combo"]);
            combo_boxes.Add(weapon2_table.Controls["weapon2Stat3_Combo"]);
            combo_boxes.Add(weapon2_table.Controls["weapon2Stat4_Combo"]);
            combo_boxes.Add(weapon2_table.Controls["weapon2Stat5_Combo"]);
            foreach (Control c in combo_boxes)
            {
                skill_name = Calculate.Get_Stat_Name(c.Text);
                if (!(skill_name == "nada"))
                {
                    string the_text = c.Name;
                    the_text = the_text.Substring(0, the_text.IndexOf("_"));
                    the_text = the_text + "_Text";
                    TextBox tb = ((TextBox)weapon2_table.Controls[the_text]);
                    CheckBox cb = ((CheckBox)skills_table.Controls[skill_name + "_CB"]);
                    if (skill_name.Equals("mainStat"))
                    {
                        mainStat = mainStat + (Convert.ToInt32(tb.Text));
                    }
                    else if (cb.Checked == true)
                    {
                        if (skill_name.Contains("CriticalStrikeDamage"))
                        {
                            csd = csd + (Convert.ToDouble(tb.Text) / 100);
                        }
                        else if (skill_name.Equals("OverpowerDamage"))
                        {
                            opd = opd + (Convert.ToDouble(tb.Text) / 100);
                        }
                        else if (skill_name.Equals("VulnerableDamage"))
                        {
                            vuln = vuln + (Convert.ToDouble(tb.Text) / 100);
                        }
                        else
                        {
                            additive = additive + (Convert.ToDouble(tb.Text) / 100);
                        }

                    }
                }
            }
            int weapon2_min = Convert.ToInt32(weapon2_table.Controls["weapon2Min_Text"].Text);
            int weapon2_max = Convert.ToInt32(weapon2_table.Controls["weapon2Max_Text"].Text);
            double weapon2_dam_base_min = ((((weapon2_min + weapon2_max) / 2) * 0.9) * skillDamage) * (1 + (mainStat * 0.001));
            double weapon2_dam_base_max = ((((weapon2_min + weapon2_max) / 2) * 1.1) * skillDamage) * (1 + (mainStat * 0.001));
            double additive2 = additive + (zerk_bonus * zerk_mult);
            double weapon2_total_base_dam_min = weapon2_dam_base_min * additive2 * multiplicative * vuln;
            double weapon2_total_base_dam_max = weapon2_dam_base_max * additive2 * multiplicative * vuln;
            minmax2_table.Controls["weapon2BaseMin_Lbl"].Text = weapon2_total_base_dam_min.ToString("N1");
            minmax2_table.Controls["weapon2BaseMax_Lbl"].Text = weapon2_total_base_dam_max.ToString("N1");
            double weapon2_crit_min = weapon2_total_base_dam_min * (1 + (csd * csd_mult));
            double weapon2_crit_max = weapon2_total_base_dam_max * (1 + (csd * csd_mult));
            minmax2_table.Controls["weapon2CritMin_Lbl"].Text = weapon2_crit_min.ToString("N1");
            minmax2_table.Controls["weapon2CritMax_Lbl"].Text = weapon2_crit_max.ToString("N1");
            double overpower2_min = ((healthfort * (additive * multiplicative)) * (opd * opd_mult)) + weapon2_total_base_dam_min;
            double overpower2_max = ((healthfort * (additive * multiplicative)) * (opd * opd_mult)) + weapon2_total_base_dam_max;
            minmax2_table.Controls["weapon2OPMin_Lbl"].Text = overpower2_min.ToString("N1");
            minmax2_table.Controls["weapon2OPMax_Lbl"].Text = overpower2_max.ToString("N1");
            double weapon2_average_min = weapon2_total_base_dam_min * (1 + ((csc * csc_mult) * (csd * csd_mult))) + (((healthfort * (additive2 * multiplicative)) * (opd * opd_mult)) * opc);
            double weapon2_average_max = weapon2_total_base_dam_max * (1 + ((csc * csc_mult) * (csd * csd_mult))) + (((healthfort * (additive2 * multiplicative)) * (opd * opd_mult)) * opc);
            minmax2_table.Controls["weapon2AvgMin_Lbl"].Text = weapon2_average_min.ToString("N1");
            minmax2_table.Controls["weapon2AvgMax_Lbl"].Text = weapon2_average_max.ToString("N1");
            //return (additive, additive, additive, additive, additive, additive, additive, additive);
        }

        public static string Get_Stat_Name(string combo_value)
        {
            switch (combo_value)
            {
                case "All Stats":
                case "Dexterity":
                case "Intelligence":
                case "Strength":
                case "Willpower":
                    return "mainStat";
                case "Basic Skill Damage":
                    return "DamagewithBasic";
                case "Core Skill Damage":
                    return "DamagewithCore";
                case "Critical Strike Damage":
                    return "CriticalStrikeDamage";
                case "Critical Strike Damage with Bone Skills":
                    return "CriticalStrikeDamagewithBone";
                case "Critical Strike Damage with Earth Skills":
                    return "CriticalStrikeDamagewithEarth";
                case "Critical Strike Damage with Imbued Skills":
                    return "CriticalStrikeDamagewithImbued";
                case "Critical Strike Damage with Werewolf Skills":
                    return "CriticalStrikeDamagewithWerewolf";
                case "Damage Over Time":
                    return "DamagewithShadowDoT";
                case "Damage to Bleeding Enemies":
                    return "DamageVsBleeding";
                case "Damage to Burning Enemies":
                    return "DamageVsBurning";
                case "Damage to Chilled Enemies":
                    return "DamageVsChilled";
                case "Damage to Close Enemies":
                    return "DamageVsClose";
                case "Damage to Crowd Controlled Enemies":
                    return "DamageVsCC";
                case "Damage to Dazed Enemies":
                    return "DamageVsDazed";
                case "Damage to Distant Enemies":
                    return "DamageVsDistant";
                case "Damage to Enemies Affected by Trap Skills":
                    return "DamageVsTrapped";
                case "Damage to Frozen Enemies":
                    return "DamageVsFrozen";
                case "Damage to Healthy Enemies":
                    return "DamagewhileHealthy";
                case "Damage to Injured Enemies":
                    return "DamageVsInjured";
                case "Damage to Poisoned Enemies":
                    return "DamageVsPoisoned";
                case "Damage to Slowed Enemies":
                    return "DamageVsSlowed";
                case "Damage to Stunned Enemies":
                    return "DamageVsStunned";
                case "Damage while Berserking":
                    return "DamagewhileBerserking";
                case "Lightning Critical Strike Damage":
                    return "CriticalStrikeDamagewithLightning";
                case "Overpower Damage":
                    return "OverpowerDamage";
                case "Overpower Damage with Werebear Skills":
                    return "OverpowerDamage";
                case "Vulnerable Damage":
                    return "VulnerableDamage";
                default:
                    return "nada";
            }
        }

    }

    public class Read_image
    {

        public static void populate_Stats(OcrResult ocrResult, Panel panel1)
        {
            TableLayoutPanel table = panel1.Controls["skillsTable"] as TableLayoutPanel;
            //MessageBox.Show(panel1.Controls["skillsTable"].Controls.Count.ToString());
            foreach (var line in ocrResult.Lines)
            {
                var lineString = line.ToString();
                foreach (Control c in table.Controls)
                {
                    if (c.Name.Contains("Text"))
                    {
                        if (lineString.Contains("Critical Strike Chance vs"))
                        {
                            if (c.Name.Contains("CriticalStrikeChancevs"))
                            {
                                foreach (var s in lineString.Split(" "))
                                {

                                    string st = s.Replace("%", "");
                                    //MessageBox.Show(st);
                                    if (double.TryParse(st, out var num))
                                    {
                                        c.Text = st;
                                    }
                                }
                            }
                            continue;
                        }
                        if (lineString.Contains("Damage vs Crowd"))
                        {
                            //MessageBox.Show(lineString);
                            if (c.Name.Contains("DamageVsCC"))
                            {
                                //MessageBox.Show(c.Name);
                                foreach (var s in lineString.Split(" "))
                                {

                                    string st = s.Replace("%", "");
                                    //MessageBox.Show(st);
                                    if (double.TryParse(st, out var num))
                                    {
                                        c.Text = st;
                                    }
                                }
                            }
                            continue;
                        }
                        int index = c.Name.IndexOf("_");
                        string stat = c.Name.Substring(0, index);
                        //MessageBox.Show(lineString.ToLower().Replace(" ", ""));
                        //MessageBox.Show(c.Name.ToLower());
                        if (lineString.ToLower().Replace(" ", "").Contains(stat.ToLower()))
                        {
                            //MessageBox.Show(lineString);
                            foreach (var s in lineString.Split(" "))
                            {
                                
                                string st = s.Replace("%", "");
                                //MessageBox.Show(st);
                                if (double.TryParse(st, out var num))
                                {
                                    c.Text = st;
                                }
                            }
                        }
                    }
                }
                //lineString.ToLower().Replace(" ", "");
                //MessageBox.Show(lineString);

            }
        }
        public static void populate_weapon(OcrResult ocrResult, Panel panel4, int num)
        {
            TableLayoutPanel table = new TableLayoutPanel();
            TextBox weaponMin = new TextBox();
            TextBox weaponMax = new TextBox();
            ComboBox implicitCombo = new ComboBox();
            ComboBox weaponStat1_Combo = new ComboBox();
            ComboBox weaponStat2_Combo = new ComboBox();
            ComboBox weaponStat3_Combo = new ComboBox();
            ComboBox weaponStat4_Combo = new ComboBox();
            ComboBox weaponStat5_Combo = new ComboBox();
            TextBox weaponImplict_Text = new TextBox();
            TextBox weaponStat1_Text = new TextBox();
            TextBox weaponStat2_Text = new TextBox();
            TextBox weaponStat3_Text = new TextBox();
            TextBox weaponStat4_Text = new TextBox();
            TextBox weaponStat5_Text = new TextBox();
            if (num == 1)
            {
                table = panel4.Controls["weapon1_Table"] as TableLayoutPanel;
                weaponMin = table.Controls["weapon1Min_Text"] as TextBox;
                weaponMax = table.Controls["weapon1Max_Text"] as TextBox;
                implicitCombo = table.Controls["weaponImplicit1_Combo"] as ComboBox;
                weaponImplict_Text = table.Controls["weaponImplicit1_Text"] as TextBox;
                weaponStat1_Combo = table.Controls["weapon1Stat1_Combo"] as ComboBox;
                weaponStat2_Combo = table.Controls["weapon1Stat2_Combo"] as ComboBox;
                weaponStat3_Combo = table.Controls["weapon1Stat3_Combo"] as ComboBox;
                weaponStat4_Combo = table.Controls["weapon1Stat4_Combo"] as ComboBox;
                weaponStat5_Combo = table.Controls["weapon1Stat5_Combo"] as ComboBox;
                weaponStat1_Text = table.Controls["weapon1Stat1_Text"] as TextBox;
                weaponStat2_Text = table.Controls["weapon1Stat2_Text"] as TextBox;
                weaponStat3_Text = table.Controls["weapon1Stat3_Text"] as TextBox;
                weaponStat4_Text = table.Controls["weapon1Stat4_Text"] as TextBox;
                weaponStat5_Text = table.Controls["weapon1Stat5_Text"] as TextBox;
            }
            else
            {
                table = panel4.Controls["weapon2_Table"] as TableLayoutPanel;
                weaponMin = table.Controls["weapon2Min_Text"] as TextBox;
                weaponMax = table.Controls["weapon2Max_Text"] as TextBox;
                implicitCombo = table.Controls["weaponImplicit2_Combo"] as ComboBox;
                weaponImplict_Text = table.Controls["weaponImplicit2_Text"] as TextBox;
                weaponStat1_Combo = table.Controls["weapon2Stat1_Combo"] as ComboBox;
                weaponStat2_Combo = table.Controls["weapon2Stat2_Combo"] as ComboBox;
                weaponStat3_Combo = table.Controls["weapon2Stat3_Combo"] as ComboBox;
                weaponStat4_Combo = table.Controls["weapon2Stat4_Combo"] as ComboBox;
                weaponStat5_Combo = table.Controls["weapon2Stat5_Combo"] as ComboBox;
                weaponStat1_Text = table.Controls["weapon2Stat1_Text"] as TextBox;
                weaponStat2_Text = table.Controls["weapon2Stat2_Text"] as TextBox;
                weaponStat3_Text = table.Controls["weapon2Stat3_Text"] as TextBox;
                weaponStat4_Text = table.Controls["weapon2Stat4_Text"] as TextBox;
                weaponStat5_Text = table.Controls["weapon2Stat5_Text"] as TextBox;
            }
            bool implicit_stat = false;
            int counter = 0;
            foreach (var line in ocrResult.Lines)
            {
                bool match = false;
                var lineString = line.ToString();
                switch (lineString)
                {
                    case not null when lineString.Contains("Critical Strike Damage to"):
                        continue;
                    case not null when lineString.Contains("Damage per Hit"):
                        lineString = lineString.Replace(",", "");
                        List<int> dams = new List<int>();
                        foreach (var s in lineString.Split(" "))
                        {
                            
                            if (int.TryParse(s, out var dam))
                            {
                                dams.Add(dam);
                            }
                        }
                        weaponMin.Text = dams[0].ToString();
                        weaponMax.Text = dams[1].ToString();
                        break;
                }
                lineString = lineString.Replace("%", "");
                lineString = lineString.Replace("+", "");
                if (implicit_stat == false)
                {
                    foreach(string choice in implicitCombo.Items)
                    {
                        if (lineString.Contains(choice))
                        {
                            implicitCombo.SelectedIndex = implicitCombo.FindStringExact(choice);
                            implicit_stat = true;
                            foreach (var s in lineString.Split(" "))
                            {
                                if (int.TryParse(s, out var damI))
                                {
                                    weaponImplict_Text.Text = damI.ToString();
                                    break;
                                }
                                if (double.TryParse(s, out var damD))
                                {
                                    weaponImplict_Text.Text = damD.ToString();
                                    break;
                                }
                            }
                        }
                    }
                    if (implicit_stat == true)
                    {
                        continue;
                    }
                }
                if (counter == 0)
                {
                    foreach(string choice in weaponStat1_Combo.Items)
                    {
                        if (lineString.Contains(choice))
                        {
                            weaponStat1_Combo.SelectedIndex = weaponStat1_Combo.FindStringExact(choice);
                            counter++;
                            foreach (var s in lineString.Split(" "))
                            {
                                if (int.TryParse(s, out var damI))
                                {
                                    weaponStat1_Text.Text = damI.ToString();
                                    break;
                                }
                                if (double.TryParse(s, out var damD))
                                {
                                    weaponStat1_Text.Text = damD.ToString();
                                    break;
                                }
                            }
                            match = true;
                        }
                    }
                }
                if (counter == 1 && !match)
                {
                    foreach (string choice in weaponStat2_Combo.Items)
                    {
                        if (lineString.Contains(choice))
                        {
                            weaponStat2_Combo.SelectedIndex = weaponStat2_Combo.FindStringExact(choice);
                            counter++;
                            foreach (var s in lineString.Split(" "))
                            {
                                if (int.TryParse(s, out var damI))
                                {
                                    weaponStat2_Text.Text = damI.ToString();
                                    break;
                                }
                                if (double.TryParse(s, out var damD))
                                {
                                    weaponStat2_Text.Text = damD.ToString();
                                    break;
                                }
                            }
                            match = true;
                        }
                    }
                }
                if (counter == 2 && !match)
                {
                    foreach (string choice in weaponStat3_Combo.Items)
                    {
                        if (lineString.Contains(choice))
                        {
                            weaponStat3_Combo.SelectedIndex = weaponStat3_Combo.FindStringExact(choice);
                            counter++;
                            foreach (var s in lineString.Split(" "))
                            {
                                if (int.TryParse(s, out var damI))
                                {
                                    weaponStat3_Text.Text = damI.ToString();
                                    break;
                                }
                                if (double.TryParse(s, out var damD))
                                {
                                    weaponStat3_Text.Text = damD.ToString();
                                    break;
                                }
                            }
                            match= true;
                        }
                    }
                }
                if (counter == 3 && !match)
                {
                    foreach (string choice in weaponStat4_Combo.Items)
                    {
                        if (lineString.Contains(choice))
                        {
                            weaponStat4_Combo.SelectedIndex = weaponStat4_Combo.FindStringExact(choice);
                            counter++;
                            foreach (var s in lineString.Split(" "))
                            {
                                if (int.TryParse(s, out var damI))
                                {
                                    weaponStat4_Text.Text = damI.ToString();
                                    break;
                                }
                                if (double.TryParse(s, out var damD))
                                {
                                    weaponStat4_Text.Text = damD.ToString();
                                    break;
                                }
                            }
                            match = true;
                        }
                    }
                }
                if (counter == 4 && !match)
                {
                    foreach (string choice in weaponStat5_Combo.Items)
                    {
                        if (lineString.Contains(choice))
                        {
                            weaponStat5_Combo.SelectedIndex = weaponStat5_Combo.FindStringExact(choice);
                            counter++;
                            foreach (var s in lineString.Split(" "))
                            {
                                if (int.TryParse(s, out var damI))
                                {
                                    weaponStat5_Text.Text = damI.ToString();
                                    break;
                                }
                                if (double.TryParse(s, out var damD))
                                {
                                    weaponStat5_Text.Text = damD.ToString();
                                    break;
                                }
                            }
                        }
                    }
                }
            }

        }
            public static OcrResult readImage(Panel panel5)
            {
                var ocr = new IronTesseract();

                ocr.Configuration.WhiteListCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890 +%.,-";


                using (var ocrInput = new OcrInput())
                {

                    Barb_Weapon barb = new Barb_Weapon();
                    PictureBox pic = panel5.Controls["pictureBox1"] as PictureBox;
                    ocrInput.AddImage(pic.Image);



                    //var ocrResult = ocr.Read(ocrInput);
                    return ocr.Read(ocrInput);
                }
            }
                
    }

    
    public class Barb_Weapon
    {
        public List<int> dams = new List<int>();
        public int allstats = new int();
        public float basicDamage = new float();
        public float coreDamage = new float();
        public float CSD = new float();
        public float damToBleed = new float();
        public float damToClose = new float();
        public float damToCC = new float();
        public float damToDist = new float();
        public float damToInj = new float();
        public float damToSlow = new float();
        public float damStunned = new float();
        public float damZerk = new float();
        public float damOP = new float();
        public int strength = new int();
        public float damUlt = new float();
        public float damVuln = new float();
    }

    class Barbarian
    {
        public double CriticalStrikeChance;
        public double CriticalStrikeChancevsInjured;
        public double CriticalStrikeDamage;
        public double CriticalStrikeDamagevsCC;
        public double CriticalStrikeDamagevsVulnerable;
        public double CriticalStrikeDamagewithCore;
        public double VulnerableDamage;
        public double OverpowerChance;
        public double OverpowerDamage;
        public int BaseHealth;
        public int Fortify;

        public double AllDamage;
        public double DamageVsBleeding;
        public double DamageVsCC;
        public double DamageVsClose;
        public double DamageVsDistant;
        public double DamageVsElites;
        public double DamageVsHealthy;
        public double DamageVsInjured;
        public double DamageVsSlowed;
        public double DamageVsStunned;
        public double DamagewhileBerserking;
        public double DamagewhileFortified;
        public double DamagewhileHealthy;
        public double DamagewithBasic;
        public double DamagewithBleeding;
        public double DamagewithBludgeoning;
        public double DamagewithCore;
        public double DamagewithDualWielding;
        public double DamagewithMace;
        public double DamagewithPhysical;
        public double DamagewithPolearm;
        public double DamagewithSlashing;
        public double DamagewithSwappedWeapons;
        public double DamagewithSword;
        public double DamagewithWeaponMastery;

        public double skillBattleFlay;
        public double skillBruteForce;
        public double skillCounteroffenisve;
        public double skillCuttotheBone;
        public double skillEnhancedDeathBlow;
        public double skillEnhancedLungingStrike;
        public double skillExpertiseOneHandedMace;
        public double skillExpertiseTwoHandedAxe;
        public double skillExpertiseTwoHandedMace;
        public double skillExpertiseTwoHandedSword;
        public double skillFuriousHammeroftheAncients;
        public double skillFuriousUpheaval;
        public double skillHeavyHanded;
        public double skillPitFighter;
        public double skillPowerWarCry;
        public double skillPrimeCalloftheAncients;
        public double skillPrimeIronMaelstromCSD;
        public double skillSlayingStrike;
        public double skillSupremeWrathoftheBerserker;
        public double skillUnbridledRage;
        public double skillViolentHammeroftheAncients;
        public double skillViolentRend;
        public double skillViolentWhirlwind;
        public double skillWalkingArsenal;
        public double skillWallop;
        public double skillWarCry;

        public double glyphAmbidextrous;
        public double glyphBrawl;
        public double glyphCleaver;
        public double glyphCrusher;
        public double glyphExecutioner;
        public double glyphMight;
        public double glyphRevenge;
    }

    class Druid
    {
        public double CriticalStrikeChance;
        public double CriticalStrikeChancevsInjured;
        public double CriticalStrikeDamage;
        public double CriticalStrikeDamagevsCC;
        public double CriticalStrikeDamagevsVulnerable;
        public double CriticalStrikeDamagewithCore;
        public double CriticalStrikeDamagewithEarth;
        public double CriticalStrikeDamagewithLightning;
        public double CriticalStrikeDamagewithPoison;
        public double CriticalStrikeDamagewithWerewolf;
        public double VulnerableDamage;
        public double OverpowerChance;
        public double OverpowerDamage;
        public int BaseHealth;
        public int Fortify;

        public double AllDamage;
        public double DamageVsCC;
        public double DamageVsClose;
        public double DamageVsDistant;
        public double DamageVsElites;
        public double DamageVsHealthy;
        public double DamageVsImmobilized;
        public double DamageVsInjured;
        public double DamageVsPoisoned;
        public double DamageVsSlowed;
        public double DamageVsStunned;
        public double DamagewhileFortified;
        public double DamagewhileHealthy;
        public double DamagewhileHuman;
        public double DamagewhileShapeshifted;
        public double DamagewhileWerebear;
        public double DamagewhileWerewolf;
        public double DamagewithBasic;
        public double DamagewithCompanion;
        public double DamagewithCore;
        public double DamagewithEarth;
        public double DamagewithLightning;
        public double DamagewithPhysical;
        public double DamagewithPoison;
        public double DamagewithStorm;
        public double DamagewithWerebear;
        public double DamagewithWerewolf;

        public double skillAvianWrath;
        public double skillBestialRampage;
        public double skillCalloftheWild;
        public double skillCrushingEarth;
        public double skillDefiance;
        public double skillElectricShock;
        public double skillEnhancedRabies;
        public double skillEnhancedTrample;
        public double skillEnhancedWolfPack;
        public double skillEnvenom;
        public double skillLupineFerocity;
        public double skillNaturalDisaster;
        public double skillNaturesReach;
        public double skillOverload;
        public double skillPerfectStorm;
        public double skillPrimalLandslide;
        public double skillPrimalShred;
        public double skillQuickshift;
        public double skillResonance;
        public double skillStoneGuard;
        public double skillSupremeLacerate;
        public double skillUrsineStrength;
        public double skillWildImpulses;

        public double glyphDominate;
        public double glyphEarthandSky;
        public double glyphFangandClaw;
        public double glyphFulminate;
        public double glyphKeeper;
        public double glyphOutmatch;
        public double glyphSpirit;
        public double glyphWilds;
    }
    class Necromancer
    {
        public double CriticalStrikeChance;
        public double CriticalStrikeChancevsInjured;
        public double CriticalStrikeDamage;
        public double CriticalStrikeDamagevsCC;
        public double CriticalStrikeDamagevsVulnerable;
        public double CriticalStrikeDamagewithBlood;
        public double CriticalStrikeDamagewithBone;
        public double CriticalStrikeDamagewithCore;
        public double CriticalStrikeDamagewithShadow;
        public double VulnerableDamage;
        public double OverpowerChance;
        public double OverpowerDamage;
        public int BaseHealth;
        public int Fortify;

        public double AllDamage;
        public double DamageVsCC;
        public double DamageVsClose;
        public double DamageVsDistant;
        public double DamageVsFrozen;
        public double DamageVsHealthy;
        public double DamageVsInjured;
        public double DamageVsSlowed;
        public double DamageVsStunned;
        public double DamagewhileHealthy;
        public double DamagewhileFortified;
        public double DamagewithBasic;
        public double DamagewithBlood;
        public double DamagewithBone;
        public double DamagewithCore;
        public double DamagewithDarkness;
        public double DamagewithMinions;
        public double DamagewithPhysical;
        public double DamagewithShadow;
        public double DamagewithShadowDoT;
        public double DamagefromGolem;
        public double DamagefromBloodOrb;
        public double DamagefromMages;
        public double DamagefromWarriors;

        public double skillAcolytesDecompose;
        public double skillAmplifyDamage;
        public double skillBloodSurge;
        public double skillCoalescedBlood;
        public double skillCompoundFracture;
        public double skillDeathsEmbrace;
        public double skillDeathsReach;
        public double skillEvulsion;
        public double skillFueledbyDeath;
        public double skillGloom;
        public double skillGolemMastery;
        public double skillHellbentCommander;
        public double skillImperfectlyBalanced;
        public double skillMementoMori;
        public double skillOssifiedEssence;
        public double skillPlaguedCorpseExplosion;
        public double skillSacrificeGolemIron;
        public double skillSacrificeMagesBone;
        public double skillSacrificeMagesCold;
        public double skillSacrificeWarriorsReapers;
        public double skillSacrificeWarriorsSkirmishers;
        public double skillShadowBlight;
        public double skillSkeletalMageMastery;
        public double skillSkeletalWarriorMastery;
        public double skillSupernaturalBlight;
        public double skillSupernaturalBloodSurge;
        public double skillSupernaturalSever;
        public double skillTerror;
        public double skillTidesofBlood;

        public double glyphAbyssal;
        public double glyphAmplify;
        public double glyphControl;
        public double glyphCorporeal;
        public double glyphDeadRaiser;
        public double glyphDominate;
        public double glyphEssence;
        public double glyphExploit;
        public double glyphGravekeeper;
        public double glyphRevenge;
        public double glyphSacrificial;
        public double glyphScourge;
    }
    class Rogue
    {
        public double CriticalStrikeChance;
        public double CriticalStrikeChancevsInjured;
        public double CriticalStrikeDamage;
        public double CriticalStrikeDamagevsCC;
        public double CriticalStrikeDamagevsVulnerable;
        public double CriticalStrikeDamagewithCold;
        public double CriticalStrikeDamagewithCore;
        public double CriticalStrikeDamagewithImbued;
        public double CriticalStrikeDamagewithPoison;
        public double CriticalStrikeDamagewithShadow;
        public double VulnerableDamage;
        public double OverpowerChance;
        public double OverpowerDamage;
        public int BaseHealth;
        public int Fortify;

        public double AllDamage;
        public double DamageVsCC;
        public double DamageVsChilled;
        public double DamageVsClose;
        public double DamageVsDazed;
        public double DamageVsDistant;
        public double DamageVsElites;
        public double DamageVsFrozen;
        public double DamageVsHealthy;
        public double DamageVsInjured;
        public double DamageVsKnockedDown;
        public double DamageVsPoisoned;
        public double DamageVsSlowed;
        public double DamageVsStunned;
        public double DamageVsTrapped;
        public double DamagewhileHealthy;
        public double DamagewithBasic;
        public double DamagewithCold;
        public double DamagewithCore;
        public double DamagewithCuttthroat;
        public double DamagewithDualWielding;
        public double DamagewithImbued;
        public double DamagewithImbuement;
        public double DamagewithMarksman;
        public double DamagewithPoison;
        public double DamagewithRanged;
        public double DamagewithShadow;
        public double DamagewithTrap;
        public double DamagefromDodging;

        public double skillAdvancedRapidFire;
        public double skillBlendedPoisonImbuement;
        public double skillCloseQuartersCombat;
        public double skillDeadlyVenom;
        public double skillEnhancedCaltrops;
        public double skillEnhancedDash;
        public double skillEnhancedPenetratingShot;
        public double skillEnhancedSmokeGrenade;
        public double skillEnhancedTwistingBlades;
        public double skillExploit;
        public double skillFrigidFinesse;
        public double skillImpetus;
        public double skillMalice;
        public double skillMixedColdImbuement;
        public double skillMixedShadowImbuement;
        public double skillPrecision;
        public double skillPrimeRainofArrows;
        public double skillSubvertingPoisonTrap;
        public double skillWeaponMasteryCSD;
        public double skillWeaponMasteryDmg;

        public double glyphAmbush;
        public double glyphCanny;
        public double glyphChip;
        public double glyphControl;
        public double glyphDevious;
        public double glyphEfficacy;
        public double glyphPride;
        public double glyphVersatility;
    }
    class Sorc
    {
        public double CriticalStrikeChance;
        public double CriticalStrikeChancevsInjured;
        public double CriticalStrikeDamage;
        public double CriticalStrikeDamagevsCC;
        public double CriticalStrikeDamagevsVulnerable;
        public double CriticalStrikeDamagewithCold;
        public double CriticalStrikeDamagewithCore;
        public double CriticalStrikeDamagewithFire;
        public double CriticalStrikeDamagewithLightning;
        public double VulnerableDamage;
        public double OverpowerChance;
        public double OverpowerDamage;
        public int BaseHealth;
        public int Fortify;

        public double AllDamage;
        public double DamageVsBurning;
        public double DamageVsCC;
        public double DamageVsChilled;
        public double DamageVsClose;
        public double DamageVsDistant;
        public double DamageVsElites;
        public double DamageVsFrozen;
        public double DamageVsHealthy;
        public double DamageVsImmobilized;
        public double DamageVsInjured;
        public double DamageVsSlowed;
        public double DamageVsStunned;
        public double DamagewhileHealthy;
        public double DamagewithBasic;
        public double DamagewithBurning;
        public double DamagewithConjuration;
        public double DamagewithCold;
        public double DamagewithCore;
        public double DamagewithCracklingEnergy;
        public double DamagewithFire;
        public double DamagewithFrost;
        public double DamagewithLightning;
        public double DamagewithPyromancy;
        public double DamagewithShock;

        public double skillAvalanche;
        public double skillCombustion;
        public double skillConjurationMastery;
        public double skillDevouringBlaze;
        public double skillElementalDominance;
        public double skillEnchantmentFireBolt;
        public double skillEndlessPyre;
        public double skillEnhancedBlizzard;
        public double skillEnhancedFirewall;
        public double skillEnhancedFrozenOrb;
        public double skillEnhancedSpark;
        public double skillEsusFerocityCSC;
        public double skillEsusFerocityCSD;
        public double skillGlassCannon;
        public double skillGlintingFireBolt;
        public double skillGreaterChainLightning;
        public double skillGreaterChargedBolts;
        public double skillHoarfrost;
        public double skillIceShardsvsFrozen;
        public double skillIcyTouch;
        public double skillInnerFlames;
        public double skillPermafrost;
        public double skillSummonedLightningSpear;
        public double skillVyrsMastery;

        public double glyphCharged;
        public double glyphControl;
        public double glyphDestruction;
        public double glyphElementalist;
        public double glyphExploit;
        public double glyphFlamefeeder;
        public double glyphPyromaniac;
        public double glyphTactician;
    }
    class Column_1
    {
        [Name("control")]
        public string Control { get; set; }
        [Name("name")]
        public string Name { get; set; }
    }

}

namespace PopulateForm
{
    public class AddToForm
    {
        public static void create_sheets(Panel panel1, Panel panel2, Panel panel3)
        {
            //Panel panel1 = Application.OpenForms["Form1"].Controls["panel1"] as Panel;
            //Panel panel2 = Application.OpenForms["Form1"].Controls["panel2"] as Panel;
            //Panel panel3 = Application.OpenForms["Form1"].Controls["panel3"] as Panel;
            ComboBox class_ComboBox = Application.OpenForms["Form1"].Controls["class_ComboBox"] as ComboBox;
            for (int g = panel1.Controls.Count - 1; g >= 0; --g)
            {
                panel1.Controls[g].Dispose();
            }
            for (int g = panel2.Controls.Count - 1; g >= 0; --g)
            {
                panel2.Controls[g].Dispose();
            }
            for (int g = panel3.Controls.Count - 1; g >= 0; --g)
            {
                panel3.Controls[g].Dispose();
            }
            string class_selection = class_ComboBox.Text;
            string file1 = "";
            string file2 = "";
            string file3 = "";
            string file4 = "";

            switch (class_selection)
            {
                case "Barbarian":
                    file1 = "barb.csv";
                    file2 = "barb2.csv";
                    file3 = "barb_abs.csv";
                    file4 = "barb_glyph.csv";
                    break;
                case "Druid":
                    file1 = "druid.csv";
                    file2 = "druid2.csv";
                    file3 = "druid_abs.csv";
                    file4 = "druid_glyph.csv";
                    break;
                case "Necromancer":
                    file1 = "necro.csv";
                    file2 = "necro2.csv";
                    file3 = "necro_abs.csv";
                    file4 = "necro_glyph.csv";
                    break;
                case "Rogue":
                    file1 = "rogue.csv";
                    file2 = "rogue2.csv";
                    file3 = "rogue_abs.csv";
                    file4 = "rogue_glyph.csv";
                    break;
                case "Sorceror":
                    file1 = "sorc.csv";
                    file2 = "sorc2.csv";
                    file3 = "sorc_abs.csv";
                    file4 = "sorc_glyph.csv";
                    break;

            }
            var cols_1 = new List<Column_1>();
            TableLayoutPanel skillsTable = new TableLayoutPanel();
            TableLayoutPanel absTable = new TableLayoutPanel();
            TableLayoutPanel glyphTable = new TableLayoutPanel();
            absTable.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            absTable.AutoSize = true;
            absTable.BackColor = System.Drawing.Color.FromArgb(72, 68, 68);
            absTable.Name = "absTable";
            glyphTable.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            glyphTable.AutoSize = true;
            glyphTable.BackColor = System.Drawing.Color.FromArgb(72, 68, 68);
            glyphTable.Name = "glyphTable";
            skillsTable.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            skillsTable.AutoSize = true;
            skillsTable.BackColor = System.Drawing.Color.FromArgb(72, 68, 68);
            skillsTable.Name = "skillsTable";
            //skillsTable.SuspendLayout();
            panel1.Controls.Add(skillsTable);
            panel2.Controls.Add(absTable);
            panel3.Controls.Add(glyphTable);
            int i;
            int r = 0;
            //var cols_1 = new List<Column_1>();
            //Barbarian barb = new Barbarian();
            skillsTable.SuspendLayout();
            absTable.SuspendLayout();
            glyphTable.SuspendLayout();
            using (var reader = new StreamReader(file1))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var col = new Column_1
                    {
                        Control = csv.GetField("control"),
                        Name = csv.GetField("name")
                    };
                    cols_1.Add(col);
                }
                reader.Close();
            }
            i = 0;
            r = 0;
            int tabindex = 1;
            foreach (var row in cols_1)
            {
                CheckBox chkbx = new CheckBox();
                chkbx.Text = "";
                chkbx.Name = row.Control + "_CB";
                chkbx.AutoSize = true;
                chkbx.BackColor = System.Drawing.Color.FromArgb(72, 68, 68);
                chkbx.ForeColor = System.Drawing.Color.SkyBlue;
                chkbx.Font = new Font("Calibre", 13);
                chkbx.Dock = DockStyle.Fill;
                chkbx.TabIndex = tabindex;
                chkbx.TextAlign = ContentAlignment.MiddleLeft;
                Label lbl = new Label();
                lbl.Name = row.Control + "_lbl";
                lbl.Text = row.Name;
                lbl.AutoSize = true;
                lbl.BackColor = System.Drawing.Color.FromArgb(72, 68, 68);
                lbl.ForeColor = System.Drawing.Color.LemonChiffon;
                if (row.Name is "Vulnerable Damage") { lbl.ForeColor = System.Drawing.Color.MediumPurple; }
                if (row.Name is "Overpower Chance" || row.Name is "Overpower Damage" || row.Name is "Base Health" || row.Name is "Fortify") { lbl.ForeColor = System.Drawing.Color.Cyan; }
                lbl.Font = new Font("Calibre", 13);
                lbl.Dock = DockStyle.Fill;
                lbl.TextAlign = ContentAlignment.MiddleLeft;
                TextBox txtb = new TextBox();
                txtb.Name = row.Control + "_Text";
                txtb.BackColor = System.Drawing.Color.FromArgb(72, 68, 68);
                txtb.ForeColor = System.Drawing.Color.SkyBlue;
                txtb.Font = new Font("Calibre", 13);
                txtb.Dock = DockStyle.Fill;
                txtb.TabIndex = tabindex + cols_1.Count();
                txtb.Text = "0";
                skillsTable.Controls.Add(chkbx, 0, i);
                skillsTable.Controls.Add(lbl, 1, i);
                skillsTable.Controls.Add(txtb, 2, i);
                tabindex++;
                i++;
            }
            cols_1.Clear();
            using (var reader = new StreamReader(file2))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var col = new Column_1
                    {
                        Control = csv.GetField("control"),
                        Name = csv.GetField("name")
                    };
                    cols_1.Add(col);
                }
            }
            i = 0;
            r = 3;
            tabindex = tabindex + cols_1.Count();
            foreach (var row in cols_1)
            {
                CheckBox chkbx = new CheckBox();
                chkbx.Text = "";
                chkbx.Name = row.Control + "_CB";
                chkbx.AutoSize = true;
                chkbx.BackColor = System.Drawing.Color.FromArgb(72, 68, 68);
                chkbx.ForeColor = System.Drawing.Color.SkyBlue;
                chkbx.Font = new Font("Calibre", 13);
                chkbx.Dock = DockStyle.Fill;
                chkbx.TabIndex = tabindex;
                chkbx.TextAlign = ContentAlignment.MiddleLeft;
                Label lbl = new Label();
                lbl.Name = row.Control + "_lbl";
                lbl.Text = row.Name;
                lbl.AutoSize = true;
                lbl.BackColor = System.Drawing.Color.FromArgb(72, 68, 68);
                lbl.ForeColor = System.Drawing.Color.LightGray;
                lbl.Font = new Font("Calibre", 13);
                lbl.Dock = DockStyle.Fill;
                lbl.TextAlign = ContentAlignment.MiddleLeft;
                TextBox txtb = new TextBox();
                txtb.Name = row.Control + "_Text";
                txtb.BackColor = System.Drawing.Color.FromArgb(72, 68, 68);
                txtb.ForeColor = System.Drawing.Color.SkyBlue;
                txtb.Font = new Font("Calibre", 13);
                txtb.Dock = DockStyle.Fill;
                txtb.TabIndex = tabindex + cols_1.Count();
                txtb.Text = "0";
                skillsTable.Controls.Add(chkbx, r, i);
                skillsTable.Controls.Add(lbl, r + 1, i);
                skillsTable.Controls.Add(txtb, r + 2, i);
                tabindex++;
                i++;
                if (i == (int)((cols_1.Count / 2) + 1))
                {
                    tabindex = tabindex + cols_1.Count;
                    i = 0;
                    r = r + 3;
                }
            }
            skillsTable.ResumeLayout(true);
            skillsTable.PerformLayout();

            cols_1.Clear();
            using (var reader = new StreamReader(file3))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var col = new Column_1
                    {
                        Control = csv.GetField("control"),
                        Name = csv.GetField("name")
                    };
                    cols_1.Add(col);
                }
            }
            i = 0;
            r = 0;
            tabindex = 1;
            foreach (var row in cols_1)
            {

                CheckBox chkbx = new CheckBox();
                chkbx.Text = "";
                chkbx.Name = row.Control + "_CB";
                chkbx.AutoSize = true;
                chkbx.BackColor = System.Drawing.Color.FromArgb(72, 68, 68);
                chkbx.ForeColor = System.Drawing.Color.SkyBlue;
                chkbx.Font = new Font("Calibre", 13);
                chkbx.Dock = DockStyle.Fill;
                chkbx.TabIndex = tabindex;
                chkbx.TextAlign = ContentAlignment.MiddleLeft;
                Label lbl = new Label();
                lbl.Name = row.Control + "_lbl";
                lbl.Text = row.Name;
                lbl.AutoSize = true;
                lbl.BackColor = System.Drawing.Color.FromArgb(72, 68, 68);
                lbl.ForeColor = System.Drawing.Color.Lavender;
                lbl.Font = new Font("Calibre", 13);
                lbl.Dock = DockStyle.Fill;
                lbl.TextAlign = ContentAlignment.MiddleLeft;
                TextBox txtb = new TextBox();
                txtb.Name = row.Control + "_Text";
                txtb.BackColor = System.Drawing.Color.FromArgb(72, 68, 68);
                txtb.ForeColor = System.Drawing.Color.SkyBlue;
                txtb.Font = new Font("Calibre", 13);
                txtb.Dock = DockStyle.Fill;
                txtb.TabIndex = tabindex + cols_1.Count();
                txtb.Text = "0";
                absTable.Controls.Add(chkbx, r, i);
                absTable.Controls.Add(lbl, r + 1, i);
                absTable.Controls.Add(txtb, r + 2, i);
                i++;
                tabindex++;
                if (i == (int)((cols_1.Count / 3) + 1))
                {
                    tabindex = tabindex + cols_1.Count();
                    i = 0;
                    r = r + 3;
                }
            }
            absTable.ResumeLayout(true);
            absTable.PerformLayout();

            cols_1.Clear();
            using (var reader = new StreamReader(file4))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var col = new Column_1
                    {
                        Control = csv.GetField("control"),
                        Name = csv.GetField("name")
                    };
                    cols_1.Add(col);
                }
            }
            i = 0;
            r = 0;
            tabindex = 1;
            foreach (var row in cols_1)
            {

                CheckBox chkbx = new CheckBox();
                chkbx.Text = "";
                chkbx.Name = row.Control + "_CB";
                chkbx.AutoSize = true;
                chkbx.BackColor = System.Drawing.Color.FromArgb(72, 68, 68);
                chkbx.ForeColor = System.Drawing.Color.SkyBlue;
                chkbx.Font = new Font("Calibre", 13);
                chkbx.Dock = DockStyle.Fill;
                chkbx.TabIndex = tabindex;
                chkbx.TextAlign = ContentAlignment.MiddleLeft;
                Label lbl = new Label();
                lbl.Name = row.Control + "_lbl";
                lbl.Text = row.Name;
                lbl.AutoSize = true;
                lbl.BackColor = System.Drawing.Color.FromArgb(72, 68, 68);
                lbl.ForeColor = System.Drawing.Color.Yellow;
                lbl.Font = new Font("Calibre", 13);
                lbl.Dock = DockStyle.Fill;
                lbl.TextAlign = ContentAlignment.MiddleLeft;
                TextBox txtb = new TextBox();
                txtb.Name = row.Control + "_Text";
                txtb.BackColor = System.Drawing.Color.FromArgb(72, 68, 68);
                txtb.ForeColor = System.Drawing.Color.SkyBlue;
                txtb.Font = new Font("Calibre", 13);
                txtb.Dock = DockStyle.Fill;
                txtb.TabIndex = tabindex + cols_1.Count();
                txtb.Text = "0";
                glyphTable.Controls.Add(chkbx, r, i);
                glyphTable.Controls.Add(lbl, r + 1, i);
                glyphTable.Controls.Add(txtb, r + 2, i);
                i++;
                tabindex++;
                /*if (i == (int)((cols_1.Count / 3) + 1))
                {
                    i = 0;
                    r = r + 3;
                }*/
            }
            glyphTable.ResumeLayout(true);
            glyphTable.PerformLayout();
            return;
        }
        public static void populate_combos(Panel panel4)
        {
            ComboBox class_ComboBox = Application.OpenForms["Form1"].Controls["class_ComboBox"] as ComboBox;
            string class_selection = class_ComboBox.Text;
            ComboBox weapon1Stat1_Combo = panel4.Controls["weapon1_Table"].Controls["weapon1Stat1_Combo"] as ComboBox;
            ComboBox weapon1Stat2_Combo = panel4.Controls["weapon1_Table"].Controls["weapon1Stat2_Combo"] as ComboBox;
            ComboBox weapon1Stat3_Combo = panel4.Controls["weapon1_Table"].Controls["weapon1Stat3_Combo"] as ComboBox;
            ComboBox weapon1Stat4_Combo = panel4.Controls["weapon1_Table"].Controls["weapon1Stat4_Combo"] as ComboBox;
            ComboBox weapon1Stat5_Combo = panel4.Controls["weapon1_Table"].Controls["weapon1Stat5_Combo"] as ComboBox;
            ComboBox weapon2Stat1_Combo = panel4.Controls["weapon2_Table"].Controls["weapon2Stat1_Combo"] as ComboBox;
            ComboBox weapon2Stat2_Combo = panel4.Controls["weapon2_Table"].Controls["weapon2Stat2_Combo"] as ComboBox;
            ComboBox weapon2Stat3_Combo = panel4.Controls["weapon2_Table"].Controls["weapon2Stat3_Combo"] as ComboBox;
            ComboBox weapon2Stat4_Combo = panel4.Controls["weapon2_Table"].Controls["weapon2Stat4_Combo"] as ComboBox;
            ComboBox weapon2Stat5_Combo = panel4.Controls["weapon2_Table"].Controls["weapon2Stat5_Combo"] as ComboBox;
            weapon1Stat1_Combo.Items.Clear();
            weapon1Stat2_Combo.Items.Clear();
            weapon1Stat3_Combo.Items.Clear();
            weapon1Stat4_Combo.Items.Clear();
            weapon1Stat5_Combo.Items.Clear();
            weapon2Stat1_Combo.Items.Clear();
            weapon2Stat2_Combo.Items.Clear();
            weapon2Stat3_Combo.Items.Clear();
            weapon2Stat4_Combo.Items.Clear();
            weapon2Stat5_Combo.Items.Clear();
            List<string> optionList = new List<string>();
            switch (class_selection)
            {
                case "Barbarian":
                    optionList.Add("All Stats");
                    optionList.Add("Basic Skill Damage");
                    optionList.Add("Core Skill Damage");
                    optionList.Add("Critical Strike Damage");
                    optionList.Add("Damage Over Time");
                    optionList.Add("Damage to Bleeding Enemies");
                    optionList.Add("Damage to Close Enemies");
                    optionList.Add("Damage to Crowd Controlled Enemies");
                    optionList.Add("Damage to Distant Enemies");
                    optionList.Add("Damage to Injured Enemies");
                    optionList.Add("Damage to Slowed Enemies");
                    optionList.Add("Damage to Stunned Enemies");
                    optionList.Add("Damage while Berserking");
                    optionList.Add("Lucky Hit: Up to [X] Chance to Execute Injured Non-Elites");
                    optionList.Add("Overpower Damage");
                    optionList.Add("Strength");
                    optionList.Add("Ultimate Skill Damage");
                    optionList.Add("Vulnerable Damage");
                    break;
                case "Druid":
                    optionList.Add("All Stats");
                    optionList.Add("Basic Skill Damage");
                    optionList.Add("Core Skill Damage");
                    optionList.Add("Critical Strike Damage");
                    optionList.Add("Critical Strike Damage with Earth Skills");
                    optionList.Add("Critical Strike Damage with Werewolf Skills");
                    optionList.Add("Damage Over Time");
                    optionList.Add("Damage to Close Enemies");
                    optionList.Add("Damage to Crowd Controlled Enemies");
                    optionList.Add("Damage to Distant Enemies");
                    optionList.Add("Damage to Injured Enemies");
                    optionList.Add("Damage to Poisoned Enemies");
                    optionList.Add("Damage to Slowed Enemies");
                    optionList.Add("Damage to Stunned Enemies");
                    optionList.Add("Lightning Critical Strike Damage");
                    optionList.Add("Lucky Hit: Up to [X] Chance to Execute Injured Non-Elites");
                    optionList.Add("Overpower Damage");
                    optionList.Add("Overpower Damage with Werebear Skills");
                    optionList.Add("Ultimate Skill Damage");
                    optionList.Add("Vulnerable Damage");
                    optionList.Add("Willpower");
                    break;
                case "Necromancer":
                    optionList.Add("All Stats");
                    optionList.Add("Basic Skill Damage");
                    optionList.Add("Core Skill Damage");
                    optionList.Add("Critical Strike Damage");
                    optionList.Add("Critical Strike Damage with Bone Skills");
                    optionList.Add("Damage Over Time");
                    optionList.Add("Damage to Affected by Shadow Damage Over Time Enemies");
                    optionList.Add("Damage to Close Enemies");
                    optionList.Add("Damage to Crowd Controlled Enemies");
                    optionList.Add("Damage to Distant Enemies");
                    optionList.Add("Damage to Injured Enemies");
                    optionList.Add("Damage to Slowed Enemies");
                    optionList.Add("Damage to Stunned Enemies");
                    optionList.Add("Intelligence");
                    optionList.Add("Lucky Hit: Up to [X] Chance to Execute Injured Non-Elites");
                    optionList.Add("Overpower Damage");
                    optionList.Add("Ultimate Skill Damage");
                    optionList.Add("Vulnerable Damage");
                    break;
                case "Rogue":
                    optionList.Add("All Stats");
                    optionList.Add("Basic Skill Damage");
                    optionList.Add("Core Skill Damage");
                    optionList.Add("Critical Strike Damage");
                    optionList.Add("Critical Strike Damage with Imbued Skills");
                    optionList.Add("Damage Over Time");
                    optionList.Add("Damage to Chilled Enemies");
                    optionList.Add("Damage to Close Enemies");
                    optionList.Add("Damage to Crowd Controlled Enemies");
                    optionList.Add("Damage to Dazed Enemies");
                    optionList.Add("Damage to Distant Enemies");
                    optionList.Add("Damage to Enemies Affected by Trap Skills");
                    optionList.Add("Damage to Frozen Enemies");
                    optionList.Add("Damage to Injured Enemies");
                    optionList.Add("Damage to Poisoned Enemies");
                    optionList.Add("Damage to Slowed Enemies");
                    optionList.Add("Damage to Stunned Enemies");
                    optionList.Add("Dexterity");
                    optionList.Add("Lucky Hit: Up to [X] Chance to Execute Injured Non-Elites");
                    optionList.Add("Overpower Damage");
                    optionList.Add("Ultimate Skill Damage");
                    optionList.Add("Vulnerable Damage");
                    break;
                case "Sorceror":
                    optionList.Add("All Stats");
                    optionList.Add("Basic Skill Damage");
                    optionList.Add("Core Skill Damage");
                    optionList.Add("Critical Strike Damage");
                    optionList.Add("Damage Over Time");
                    optionList.Add("Damage to Burning Enemies");
                    optionList.Add("Damage to Chilled Enemies");
                    optionList.Add("Damage to Close Enemies");
                    optionList.Add("Damage to Crowd Controlled Enemies");
                    optionList.Add("Damage to Distant Enemies");
                    optionList.Add("Damage to Frozen Enemies");
                    optionList.Add("Damage to Injured Enemies");
                    optionList.Add("Damage to Slowed Enemies");
                    optionList.Add("Damage to Stunned Enemies");
                    optionList.Add("Intelligence");
                    optionList.Add("Lightning Critical Strike Damage");
                    optionList.Add("Lucky Hit: Up to [X] Chance to Execute Injured Non-Elites");
                    optionList.Add("Overpower Damage");
                    optionList.Add("Ultimate Skill Damage");
                    optionList.Add("Vulnerable Damage");
                    break;
            }
            foreach (string s in optionList)
            {
                weapon1Stat1_Combo.Items.Add(s);
                weapon1Stat2_Combo.Items.Add(s);
                weapon1Stat3_Combo.Items.Add(s);
                weapon1Stat4_Combo.Items.Add(s);
                weapon1Stat5_Combo.Items.Add(s);
                weapon2Stat1_Combo.Items.Add(s);
                weapon2Stat2_Combo.Items.Add(s);
                weapon2Stat3_Combo.Items.Add(s);
                weapon2Stat4_Combo.Items.Add(s);
                weapon2Stat5_Combo.Items.Add(s);
            }
        }
    }
}

