using CsvHelper;
using IronOcr;
using PopulateForm;
using SixLabors.Fonts.Unicode;
using System;
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
    }
}