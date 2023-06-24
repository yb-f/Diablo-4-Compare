using CsvHelper;
using IronOcr;
using PopulateForm;
using SixLabors.Fonts.Unicode;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Channels;

namespace Diablo_4_Compare
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void class_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //AddToForm Create_Sheet = new AddToForm();
            AddToForm.create_sheets(panel1, panel2, panel3);
            AddToForm.populate_combos(panel4);
            string s = class_ComboBox.Text;
            switch (s)
            {
                case "Barbarian":
                    MainStat_Lbl.Text = "Strength";
                    break;
                case "Druid":
                    MainStat_Lbl.Text = "Willpower";
                    break;
                case "Rogue":
                    MainStat_Lbl.Text = "Dexterity";
                    break;
                case "Necromancer":
                case "Sorceror":
                    MainStat_Lbl.Text = "Intelligence";
                    break;

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //TableLayoutPanel t = ((TableLayoutPanel)panel1.Controls["skillsTable"]);
            if (!(mainStat_Text.Text == "") && !(skillDamage_Text.Text == "") && !(weapon1Min_Text.Text == "") && !(weapon1Max_Text.Text == "") && !(weapon2Min_Text.Text == "") && !(weapon2Max_Text.Text == "") && !(class_ComboBox.Text == ""))
            {
                Calculate.Calculate_Damage(panel1, panel2, panel3, panel4);
            }
            else
            {
                MessageBox.Show("Please make sure Class, Main Stat, Skill Damage, and Weapon Damages (at a minimum) are filled in.", "Ooops!");
            }

            //string s = ((TextBox)panel1.Controls["skillsTable"].Controls["CriticalStrikeDamage_Text"]).Text;
            //MessageBox.Show(s);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.DrawItem += new DrawItemEventHandler(this.tabControl1_DrawItem);
            pictureBox1.AllowDrop = true;
            class_ComboBox.SelectedItem = Properties.Settings.Default.selected_class;
            TableLayoutPanel skills_table = ((TableLayoutPanel)panel1.Controls["skillsTable"]);
            TableLayoutPanel abs_table = ((TableLayoutPanel)panel2.Controls["absTable"]);
            TableLayoutPanel glyph_table = ((TableLayoutPanel)panel3.Controls["glyphTable"]);
            TableLayoutPanel mainstat_table = ((TableLayoutPanel)panel4.Controls["mainstat_Table"]);
            TableLayoutPanel weapon1_table = ((TableLayoutPanel)panel4.Controls["weapon1_Table"]);
            TableLayoutPanel weapon2_table = ((TableLayoutPanel)panel4.Controls["weapon2_Table"]);
            TableLayoutPanel minmax1_table = ((TableLayoutPanel)panel4.Controls["MinMax1_Table"]);
            TableLayoutPanel minmax2_table = ((TableLayoutPanel)panel4.Controls["MinMax2_Table"]);
            int i = 1;
            for (int d = 0; d + 1 <= skills_table.Controls.Count; d++)
            {
                if (d == 0 || d % 3 == 0)
                {
                    var val = Properties.Settings.Default.PropertyValues["skills_cb_" + i.ToString()];
                    bool check = (bool)val.PropertyValue;
                    CheckBox cb = skills_table.Controls[d] as CheckBox;
                    cb.Checked = check;
                    //cb.SetCurrentValue(CheckBox.IsCheckedProperty, check);
                }
                else if ((d + 1) % 3 == 0)
                {
                    var val = Properties.Settings.Default.PropertyValues["skills_text_" + i.ToString()];
                    string t = (string)val.PropertyValue;
                    TextBox tb = skills_table.Controls[d] as TextBox;
                    tb.Text = t;
                    i++;
                }

                //MessageBox.Show(Properties.Settings.Default.Properties["skills_cb_" + i.ToString()].Name.ToString() + " " + val.PropertyValue.ToString());
            }
            i = 1;
            for (int d = 0; d + 1 <= abs_table.Controls.Count; d++)
            {
                if (d == 0 || d % 3 == 0)
                {
                    var val = Properties.Settings.Default.PropertyValues["abs_cb_" + i.ToString()];
                    //MessageBox.Show(Properties.Settings.Default.Properties["abs_cb_" + i.ToString()].Name.ToString() + " " + val.PropertyValue.ToString());
                    bool check = (bool)val.PropertyValue;
                    CheckBox cb = abs_table.Controls[d] as CheckBox;
                    cb.Checked = check;
                    //cb.SetCurrentValue(CheckBox.IsCheckedProperty, check);
                }
                else if ((d + 1) % 3 == 0)
                {
                    var val = Properties.Settings.Default.PropertyValues["abs_text_" + i.ToString()];
                    string t = (string)val.PropertyValue;
                    TextBox tb = abs_table.Controls[d] as TextBox;
                    tb.Text = t;
                    i++;
                }

                //MessageBox.Show(Properties.Settings.Default.Properties["skills_cb_" + i.ToString()].Name.ToString() + " " + val.PropertyValue.ToString()); ;
            }
            i = 1;
            for (int d = 0; d + 1 <= glyph_table.Controls.Count; d++)
            {
                if (d == 0 || d % 3 == 0)
                {
                    var val = Properties.Settings.Default.PropertyValues["glyph_cb_" + i.ToString()];
                    //MessageBox.Show(Properties.Settings.Default.Properties["abs_cb_" + i.ToString()].Name.ToString() + " " + val.PropertyValue.ToString());
                    bool check = (bool)val.PropertyValue;
                    CheckBox cb = glyph_table.Controls[d] as CheckBox;
                    cb.Checked = check;
                    //cb.SetCurrentValue(CheckBox.IsCheckedProperty, check);
                }
                else if ((d + 1) % 3 == 0)
                {
                    var val = Properties.Settings.Default.PropertyValues["glyph_text_" + i.ToString()];
                    string t = (string)val.PropertyValue;
                    TextBox tb = glyph_table.Controls[d] as TextBox;
                    tb.Text = t;
                    i++;
                }

                //MessageBox.Show(Properties.Settings.Default.Properties["skills_cb_" + i.ToString()].Name.ToString() + " " + val.PropertyValue.ToString()); ;
            }
            mainStat_Text.Text = Properties.Settings.Default.mainStat;
            skillDamage_Text.Text = Properties.Settings.Default.skillDamage;
            weaponImplicit1_Combo.Text = Properties.Settings.Default.weapon1Implicit_Combo;
            weaponImplicit1_Text.Text = Properties.Settings.Default.weapon1Implicit_Text;
            weapon1Stat1_Combo.Text = Properties.Settings.Default.weapon1Stat1_Combo;
            weapon1Stat2_Combo.Text = Properties.Settings.Default.weapon1Stat2_Combo;
            weapon1Stat3_Combo.Text = Properties.Settings.Default.weapon1Stat3_Combo;
            weapon1Stat4_Combo.Text = Properties.Settings.Default.weapon1Stat4_Combo;
            weapon1Stat5_Combo.Text = Properties.Settings.Default.weapon1Stat5_Combo;
            weapon1Stat1_Text.Text = Properties.Settings.Default.weapon1Stat1_Text;
            weapon1Stat2_Text.Text = Properties.Settings.Default.weapon1Stat2_Text;
            weapon1Stat3_Text.Text = Properties.Settings.Default.weapon1Stat3_Text;
            weapon1Stat4_Text.Text = Properties.Settings.Default.weapon1Stat4_Text;
            weapon1Stat5_Text.Text = Properties.Settings.Default.weapon1Stat5_Text;
            weapon1Min_Text.Text = Properties.Settings.Default.weapon1Min_Text;
            weapon1Max_Text.Text = Properties.Settings.Default.weapon1Max_Text;
            weaponImplicit2_Combo.Text = Properties.Settings.Default.weapon2Implicit_Combo;
            weaponImplicit2_Text.Text = Properties.Settings.Default.weapon2Implicit_Text;
            weapon2Stat1_Combo.Text = Properties.Settings.Default.weapon2Stat1_Combo;
            weapon2Stat2_Combo.Text = Properties.Settings.Default.weapon2Stat2_Combo;
            weapon2Stat3_Combo.Text = Properties.Settings.Default.weapon2Stat3_Combo;
            weapon2Stat4_Combo.Text = Properties.Settings.Default.weapon2Stat4_Combo;
            weapon2Stat5_Combo.Text = Properties.Settings.Default.weapon2Stat5_Combo;
            weapon2Stat1_Text.Text = Properties.Settings.Default.weapon2Stat1_Text;
            weapon2Stat2_Text.Text = Properties.Settings.Default.weapon2Stat2_Text;
            weapon2Stat3_Text.Text = Properties.Settings.Default.weapon2Stat3_Text;
            weapon2Stat4_Text.Text = Properties.Settings.Default.weapon2Stat4_Text;
            weapon2Stat5_Text.Text = Properties.Settings.Default.weapon2Stat5_Text;
            weapon2Min_Text.Text = Properties.Settings.Default.weapon2Min_Text;
            weapon2Max_Text.Text = Properties.Settings.Default.weapon2Max_Text;
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            //Get the working area of the TabControl main control
            System.Drawing.Rectangle rec = tabControl1.ClientRectangle;
            //Create a StringFormat object to set the layout of the label text
            StringFormat StrFormat = new StringFormat();
            StrFormat.LineAlignment = StringAlignment.Center;// Set the text to be centered vertically
            StrFormat.Alignment = StringAlignment.Center;// Set the text to be centered horizontally

            // The background fill color of the label, it can also be a picture (e.Graphics.DrawImage)
            SolidBrush backColor = new SolidBrush(System.Drawing.Color.FromArgb(72, 68, 68));
            SolidBrush fontColor;// Label font color
                                 //Draw the background of the main control
            e.Graphics.FillRectangle(backColor, rec);

            //Draw label style
            Font fntTab = e.Font;
            Brush bshBack = new SolidBrush(System.Drawing.Color.DimGray);

            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                bool bSelected = (tabControl1.SelectedIndex == i);
                System.Drawing.Rectangle recBounds = tabControl1.GetTabRect(i);
                System.Drawing.RectangleF tabTextArea = (System.Drawing.RectangleF)tabControl1.GetTabRect(i);
                if (bSelected)
                {
                    e.Graphics.FillRectangle(bshBack, recBounds);
                    fontColor = new SolidBrush(System.Drawing.Color.PaleGreen);
                    e.Graphics.DrawString(tabControl1.TabPages[i].Text, fntTab, fontColor, tabTextArea, StrFormat);
                }
                else
                {
                    fontColor = new SolidBrush(System.Drawing.Color.PaleGreen);
                    e.Graphics.DrawString(tabControl1.TabPages[i].Text, fntTab, fontColor, tabTextArea, StrFormat);
                }
            }
        }

        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.FileDrop);
            if (data != null)
            {
                var filenames = data as string[];
                if (filenames.Length > 0)
                    pictureBox1.Image = System.Drawing.Image.FromFile(filenames[0]);
            }
        }

        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null && !(class_ComboBox.Text == ""))
            {


                OcrResult ocrResult = Read_image.readImage(panel5);

                if (radioWeapon1.Checked == true)
                {
                    Read_image.populate_weapon(ocrResult, panel4, 1);
                }
                if (radioWeapon2.Checked == true)
                {
                    Read_image.populate_weapon(ocrResult, panel4, 2);
                }
                if (radioStats.Checked == true)
                {
                    if (!(class_ComboBox.SelectedIndex == -1))
                    {
                        Read_image.populate_Stats(ocrResult, panel1);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please add an image to be processed, and insure a class has been selected.", "Oops!");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                TableLayoutPanel skills_table = ((TableLayoutPanel)panel1.Controls["skillsTable"]);
                TableLayoutPanel abs_table = ((TableLayoutPanel)panel2.Controls["absTable"]);
                TableLayoutPanel glyph_table = ((TableLayoutPanel)panel3.Controls["glyphTable"]);
                /*TableLayoutPanel mainstat_table = ((TableLayoutPanel)panel4.Controls["mainstat_Table"]);
                TableLayoutPanel weapon1_table = ((TableLayoutPanel)panel4.Controls["weapon1_Table"]);
                TableLayoutPanel weapon2_table = ((TableLayoutPanel)panel4.Controls["weapon2_Table"]);
                TableLayoutPanel minmax1_table = ((TableLayoutPanel)panel4.Controls["MinMax1_Table"]);
                TableLayoutPanel minmax2_table = ((TableLayoutPanel)panel4.Controls["MinMax2_Table"]);*/
                int i = 0;
                List<bool> checks = new List<bool>();
                List<string> text_values = new List<string>();
                foreach (Control c in skills_table.Controls)
                {
                    if (c.Name.ToString().Contains("_CB"))
                    {
                        CheckBox temp = c as CheckBox;
                        if (temp.Checked == true)
                        {
                            checks.Add(true);
                        }
                        else
                        {
                            checks.Add(false);
                        }
                    }
                    else if (c.Name.ToString().Contains("_Text"))
                    {
                        text_values.Add(c.Text);
                    }
                }
                while (i < checks.Count)
                {
                    Properties.Settings.Default["skills_cb_" + (i + 1).ToString()] = checks[i];
                    Properties.Settings.Default["skills_text_" + (i + 1).ToString()] = text_values[i];
                    i++;
                    /*foreach (SettingsProperty currentProperty in Properties.Settings.Default.Properties)
                    {
                        
                        if (currentProperty.Name.ToString().Contains("skills_cb"))
                        {
                            Properties.Settings.Default[currentProperty.Name] = checks[i];
                            i++;
                        }
                    }*/
                }
                checks.Clear();
                text_values.Clear();
                i = 0;
                foreach (Control c in abs_table.Controls)
                {
                    if (c.Name.ToString().Contains("_CB"))
                    {
                        CheckBox temp = c as CheckBox;
                        if (temp.Checked == true)
                        {
                            checks.Add(true);
                        }
                        else
                        {
                            checks.Add(false);
                        }
                    }
                    else if (c.Name.ToString().Contains("_Text"))
                    {
                        text_values.Add(c.Text);
                    }
                }
                while (i < checks.Count)
                {
                    Properties.Settings.Default["abs_cb_" + (i + 1).ToString()] = checks[i];
                    Properties.Settings.Default["abs_text_" + (i + 1).ToString()] = text_values[i];
                    i++;
                    /*foreach (SettingsProperty currentProperty in Properties.Settings.Default.Properties)
                    {
                        
                        if (currentProperty.Name.ToString().Contains("skills_cb"))
                        {
                            Properties.Settings.Default[currentProperty.Name] = checks[i];
                            i++;
                        }
                    }*/
                }
                checks.Clear();
                text_values.Clear();
                i = 0;
                foreach (Control c in glyph_table.Controls)
                {
                    if (c.Name.ToString().Contains("_CB"))
                    {
                        CheckBox temp = c as CheckBox;
                        if (temp.Checked == true)
                        {
                            checks.Add(true);
                        }
                        else
                        {
                            checks.Add(false);
                        }
                    }
                    else if (c.Name.ToString().Contains("_Text"))
                    {
                        text_values.Add(c.Text);
                    }
                }
                while (i < checks.Count)
                {
                    Properties.Settings.Default["glyph_cb_" + (i + 1).ToString()] = checks[i];
                    Properties.Settings.Default["glyph_text_" + (i + 1).ToString()] = text_values[i];
                    i++;
                    /*foreach (SettingsProperty currentProperty in Properties.Settings.Default.Properties)
                    {
                        
                        if (currentProperty.Name.ToString().Contains("skills_cb"))
                        {
                            Properties.Settings.Default[currentProperty.Name] = checks[i];
                            i++;
                        }
                    }*/
                }
                Properties.Settings.Default.mainStat = mainStat_Text.Text;
                Properties.Settings.Default.skillDamage = skillDamage_Text.Text;
                Properties.Settings.Default.weapon1Implicit_Combo = weaponImplicit1_Combo.Text;
                Properties.Settings.Default.weapon1Implicit_Text = weaponImplicit1_Text.Text;
                Properties.Settings.Default.weapon1Stat1_Combo = weapon1Stat1_Combo.Text;
                Properties.Settings.Default.weapon1Stat2_Combo = weapon1Stat2_Combo.Text;
                Properties.Settings.Default.weapon1Stat3_Combo = weapon1Stat3_Combo.Text;
                Properties.Settings.Default.weapon1Stat4_Combo = weapon1Stat4_Combo.Text;
                Properties.Settings.Default.weapon1Stat5_Combo = weapon1Stat5_Combo.Text;
                Properties.Settings.Default.weapon1Stat1_Text = weapon1Stat1_Text.Text;
                Properties.Settings.Default.weapon1Stat2_Text = weapon1Stat2_Text.Text;
                Properties.Settings.Default.weapon1Stat3_Text = weapon1Stat3_Text.Text;
                Properties.Settings.Default.weapon1Stat4_Text = weapon1Stat4_Text.Text;
                Properties.Settings.Default.weapon1Stat5_Text = weapon1Stat5_Text.Text;
                Properties.Settings.Default.weapon1Min_Text = weapon1Min_Text.Text;
                Properties.Settings.Default.weapon1Max_Text = weapon1Max_Text.Text;
                Properties.Settings.Default.weapon2Implicit_Combo = weaponImplicit2_Combo.Text;
                Properties.Settings.Default.weapon2Implicit_Text = weaponImplicit2_Text.Text;
                Properties.Settings.Default.weapon2Stat1_Combo = weapon2Stat1_Combo.Text;
                Properties.Settings.Default.weapon2Stat2_Combo = weapon2Stat2_Combo.Text;
                Properties.Settings.Default.weapon2Stat3_Combo = weapon2Stat3_Combo.Text;
                Properties.Settings.Default.weapon2Stat4_Combo = weapon2Stat4_Combo.Text;
                Properties.Settings.Default.weapon2Stat5_Combo = weapon2Stat5_Combo.Text;
                Properties.Settings.Default.weapon2Stat1_Text = weapon2Stat1_Text.Text;
                Properties.Settings.Default.weapon2Stat2_Text = weapon2Stat2_Text.Text;
                Properties.Settings.Default.weapon2Stat3_Text = weapon2Stat3_Text.Text;
                Properties.Settings.Default.weapon2Stat4_Text = weapon2Stat4_Text.Text;
                Properties.Settings.Default.weapon2Stat5_Text = weapon2Stat5_Text.Text;
                Properties.Settings.Default.weapon2Min_Text = weapon2Min_Text.Text;
                Properties.Settings.Default.weapon2Max_Text = weapon2Max_Text.Text;
                Properties.Settings.Default.selected_class = class_ComboBox.Text;
            }
            catch (NotSupportedException)
            {
            }
            Properties.Settings.Default.Save();
        }
    }
}