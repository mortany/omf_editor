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

        List<AnimationsContainer> OMFFiles = new List<AnimationsContainer>();

        public Form1()
        {
            InitializeComponent();
            InitButtons();
        }

        private void InitButtons()
        {
            openFileDialog1.Filter = "OMF file|*.omf";
            openFileDialog1.FileName = "";
        }

        private void OpenFile(string filename)
        {
            if(editor.OpenOMF(filename, OMFFiles))
            {
                MessageBox.Show("Done!");
                listBox1.DisplayMember = "Name";
                listBox1.DataSource = OMFFiles[0].AnimsParams;
            }
    
        }

        private void SaveOMF(AnimationsContainer omf_file)
        {
            string path = Directory.GetCurrentDirectory()+"\\test.omf";

            using (BinaryWriter writer = new BinaryWriter(File.Create(path)))
            {
                editor.WriteOMF(writer, omf_file);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                OMFFiles.Clear();
                OpenFile(openFileDialog1.FileName);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(OMFFiles.Count != 0)
                SaveOMF(OMFFiles[0]);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OMFFiles.Count == 0) return;

            textBox1.Text = (listBox1.SelectedItem as AnimationParams).Name;
            textBox3.Text = (listBox1.SelectedItem as AnimationParams).Speed.ToString();
            textBox4.Text = (listBox1.SelectedItem as AnimationParams).Power.ToString();
            textBox5.Text = (listBox1.SelectedItem as AnimationParams).Accrue.ToString();
            textBox6.Text = (listBox1.SelectedItem as AnimationParams).Falloff.ToString();


        }

        private void TextBoxFilter(object sender, EventArgs e)
        {
            string mask = @"^\w*$";
            TextBox current =  sender as TextBox;
            Match match = Regex.Match(current.Text, mask);
            if (!match.Success) current.Text = current.Text.Remove(current.Text.Length-1, 1);
            current.SelectionStart = current.Text.Length;

            (listBox1.SelectedItem as AnimationParams).Name = current.Text;

            int index = (listBox1.SelectedItem as AnimationParams).MotionID;

            OMFFiles[0].Anims[index].Name = current.Text;
        }



        private void TextBoxFilterNumber(object sender, EventArgs e)
        {
            string mask = @"^[0-9,]*$";
            TextBox current = sender as TextBox;
            Match match = Regex.Match(current.Text, mask);
            if (!match.Success) current.Text = current.Text.Remove(current.Text.Length - 1, 1);
            current.SelectionStart = current.Text.Length;

            switch(current.Tag.ToString())
            {
                case "Speed": (listBox1.SelectedItem as AnimationParams).Speed = Convert.ToSingle(current.Text); break;
                case "Power": (listBox1.SelectedItem as AnimationParams).Power = Convert.ToSingle(current.Text); break;
                case "Accrue": (listBox1.SelectedItem as AnimationParams).Accrue = Convert.ToSingle(current.Text); break;
                case "Falloff": (listBox1.SelectedItem as AnimationParams).Falloff = Convert.ToSingle(current.Text); break;
                default: break;
            }

        }


    }
}
