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

        //int StopAtEnd = 1 << 1;
        //int NoMix = 1 << 2;
        //int SyncPart = 1 << 3;
        //int UseFootSteps = 1 << 4;
        //int MoveXForm = 1 << 5;
        //int Idle = 1 << 6;
        //int UseWeaponBone = 1 << 7;

        int current_index = -1;

        List<CheckBox> Boxes = new List<CheckBox>();

        public Form1()
        {
            InitializeComponent();
            InitButtons();
            // Very dirty hack
            if (Environment.GetCommandLineArgs().Length > 1) OpenFile(Environment.GetCommandLineArgs()[1]);
        }

        private void InitButtons()
        {
            openFileDialog1.Filter = "OMF file|*.omf";
            saveFileDialog1.Filter = "OMF file|*.omf";

            cloneToolStripMenuItem.Enabled = false;

            Boxes.Add(checkBox1);
            Boxes.Add(checkBox2);
            Boxes.Add(checkBox3);
            Boxes.Add(checkBox4);
            Boxes.Add(checkBox5);
            Boxes.Add(checkBox6);
            Boxes.Add(checkBox7);
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
                DialogResult result = MessageBox.Show("Скелеты OMF файлов различаются, вы уверены что хотите объединить?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DialogResult == DialogResult.No) return;
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

            FillFlagsStates();
        }

        private void TextBoxFilter(object sender, EventArgs e)
        {
            if (Main_OMF == null) return;

            TextBox current = sender as TextBox;
            string mask = current.Tag.ToString() == "MotionName" ? @"^\w*$" : @"^[0-9,]*$";           
            Match match = Regex.Match(current.Text, mask);
            if (!match.Success)
            {
                int temp = current.SelectionStart;
                current.Text = current.Text.Remove(current.SelectionStart-1, 1); 
                current.SelectionStart = temp-1;
            }

            AnimationParams CurrentAnim = listBox1.SelectedItem as AnimationParams;

            switch (current.Tag.ToString())
            {
                case "Speed": CurrentAnim.Speed = Convert.ToSingle(current.Text); break;
                case "Power": CurrentAnim.Power = Convert.ToSingle(current.Text); break;
                case "Accrue": CurrentAnim.Accrue = Convert.ToSingle(current.Text); break;
                case "Falloff": CurrentAnim.Falloff = Convert.ToSingle(current.Text); break;
                case "MotionName":
                    {
                        CurrentAnim.Name = current.Text; 
                        int index = CurrentAnim.MotionID;
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
            if (current_index != 0) listBox1.SelectedIndex = current_index - 1;
            else listBox1.SelectedIndex = 0;

            current_index = -1;
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
                deleteToolStripMenuItem.Enabled = listBox1.Items.Count > 1;
                contextMenuStrip1.Visible = true;
                current_index = index;
            }
            else
            {
                contextMenuStrip1.Visible = false;
                current_index = -1;
            }
        }

        private void FillFlagsStates()
        {
            if (Main_OMF == null) return;

            AnimationParams CurrentAnim = listBox1.SelectedItem as AnimationParams;
            int Flags = CurrentAnim.Flags;

            for(int i = 1; i < 8;i++)
            {
                Boxes[i - 1].Checked = (Flags & (1 << i)) == (1 << i);
            }
        }

        private void WriteAllFlags()
        {
            if (Main_OMF == null) return;
            AnimationParams CurrentAnim = listBox1.SelectedItem as AnimationParams;

            for(int i = 1; i < 8;i++)
            {
                CurrentAnim.Flags = BitSet(CurrentAnim.Flags, (1 << i), Boxes[i - 1].Checked);
            }
        }

        private int BitSet(int flags, int mask,bool bvalue)
        {
            if(bvalue)
                return flags |= mask;
            else 
                return flags &= ~mask;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (Main_OMF == null) return;

            WriteAllFlags();
        }
    }
}
