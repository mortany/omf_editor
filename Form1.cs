using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace OMF_Editor
{
    public partial class Form1 : Form
    {
        OMFEditor editor = new OMFEditor();

        AnimationsContainer Main_OMF;

        BindingSource bs = new BindingSource();

        int current_index = -1;

        public Form1()
        {
            InitializeComponent();
            InitButtons();
        }

        private void InitButtons()
        {
            openFileDialog1.Filter = "OMF file|*.omf";
            saveFileDialog1.Filter = "OMF file|*.omf";

            //contextMenuStrip1.AccessibilityObject = listBox1;
        }

        private void OpenFile(string filename)
        {
            Main_OMF = editor.OpenOMF(filename);

            if (Main_OMF != null)
            {
                bs.DataSource = Main_OMF.AnimsParams;
                listBox1.DisplayMember = "Name";
                listBox1.DataSource = bs;
            }
        }

        private void UpdateList()
        {
            //listBox1.DisplayMember = "Name";

           bs.ResetBindings(false);

        }

        private void AppendFile(string filename)
        {
            if (Main_OMF == null) OpenFile(filename);

            AnimationsContainer new_omf = editor.OpenOMF(filename);

            if (new_omf == null) return;

            int error_v = editor.CompareOMF(Main_OMF, new_omf);

            if(error_v==1)
            {
                MessageBox.Show("Скелеты OMF файлов различаются, объединение невозможно!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if(error_v == 2)
            {
                MessageBox.Show("Версии OMF отличаются, параметры анимаций будут преобразованы под текущую версию OMF", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            editor.CopyAnims(Main_OMF, new_omf);
            UpdateList();

        }

        private void SaveOMF(AnimationsContainer omf_file, string file_name)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Create(file_name)))
            {
                editor.WriteOMF(writer, omf_file);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Tag = "Open";
            openFileDialog1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Tag = "Append";
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                if(openFileDialog1.Tag.ToString() == "Open")
                    OpenFile(openFileDialog1.FileName);
                else
                    AppendFile(openFileDialog1.FileName);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(Main_OMF != null)
            {
                saveFileDialog1.FileName = "";
                saveFileDialog1.ShowDialog();
            }
                
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Main_OMF == null) return;

            textBox1.Text = (listBox1.SelectedItem as AnimationParams).Name;
            textBox3.Text = (listBox1.SelectedItem as AnimationParams).Speed.ToString();
            textBox4.Text = (listBox1.SelectedItem as AnimationParams).Power.ToString();
            textBox5.Text = (listBox1.SelectedItem as AnimationParams).Accrue.ToString();
            textBox6.Text = (listBox1.SelectedItem as AnimationParams).Falloff.ToString();
        }

        private void TextBoxFilter(object sender, EventArgs e)
        {
            if (Main_OMF == null) return;

            TextBox current = sender as TextBox;
            string mask = current.Tag.ToString() == "MotionName" ? @"^\w*$" : @"^[0-9,]*$";           
            Match match = Regex.Match(current.Text, mask);
            if (!match.Success) current.Text = current.Text.Remove(current.Text.Length - 1, 1);
            current.SelectionStart = current.Text.Length;

            switch(current.Tag.ToString())
            {
                case "Speed": (listBox1.SelectedItem as AnimationParams).Speed = Convert.ToSingle(current.Text); break;
                case "Power": (listBox1.SelectedItem as AnimationParams).Power = Convert.ToSingle(current.Text); break;
                case "Accrue": (listBox1.SelectedItem as AnimationParams).Accrue = Convert.ToSingle(current.Text); break;
                case "Falloff": (listBox1.SelectedItem as AnimationParams).Falloff = Convert.ToSingle(current.Text); break;
                case "MotionName":
                    {
                        (listBox1.SelectedItem as AnimationParams).Name = current.Text; 
                        int index = (listBox1.SelectedItem as AnimationParams).MotionID;
                        Main_OMF.Anims[index].Name = current.Text;
                    }break;
                default: break;
            }
        }


        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            SaveOMF(Main_OMF, (sender as SaveFileDialog).FileName);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (current_index == -1) return;

            Main_OMF.Anims.RemoveAt(current_index);
            Main_OMF.AnimsParams.RemoveAt(current_index);
            Main_OMF.RecalcAllAnimIndex();
            Main_OMF.RecalcAnimNum();
            UpdateList();
        }

        private void cloneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (current_index == -1) return;
        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            var index = listBox1.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                contextMenuStrip1.Show(Cursor.Position);
                contextMenuStrip1.Visible = true;
                current_index = index;
            }
            else
            {
                contextMenuStrip1.Visible = false;
                current_index = -1;
            }
        }
    }
}
