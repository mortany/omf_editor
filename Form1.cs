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
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace OMF_Editor
{
    public partial class Form1 : Form
    {
        OMFEditor editor = new OMFEditor();

        AnimationsContainer Main_OMF;

        BindingSource bs = new BindingSource();

        string number_mask = "";
        bool bKeyIsDown = false;
        bool bTextBoxEnabled = false;
        bool bMotMarkPanel = false;
        int current_index = -1;

        List<CheckBox> Boxes = new List<CheckBox>();
        List<TextBox> textBoxes = new List<TextBox>();

        ResourceManager rm = new ResourceManager(typeof(Form1));

        public Form1()
        {
            InitializeComponent();

            number_mask = CultureInfo.CurrentCulture.Name == "ru-RU" ? @"^[0-9,]*$" : @"^[0-9.]*$";

            InitButtons();

            // Very dirty hack
            if (Environment.GetCommandLineArgs().Length > 1) OpenFile(Environment.GetCommandLineArgs()[1]);
            
        }

        private void InitButtons()
        {
            openFileDialog1.Filter = saveFileDialog1.Filter = "OMF file|*.omf";

            this.Text = "OMF editor " + Assembly.GetExecutingAssembly().GetName().Version.ToString();

            cloneToolStripMenuItem.Enabled = false;

            Boxes.Add(chbxStopAtEnd);
            Boxes.Add(chbxNoMix);
            Boxes.Add(chbxSyncPart);
            Boxes.Add(chbxUseFootSteps);
            Boxes.Add(chbxMoveXForm);
            Boxes.Add(chbxIdle);
            Boxes.Add(chbxUseWeaponBone);
            Boxes.Add(chbxHasMotionMarks);

            textBoxes.Add(tbxMotName);
            textBoxes.Add(tbxMotSpeed);
            textBoxes.Add(tbxMotPower);
            textBoxes.Add(tbxMotAcc);
            textBoxes.Add(tbxMotFall);
            textBoxes.Add(tbxMotLenght);

            DisableInput();
        }

        private void OpenFile(string filename)
        {
            Main_OMF = editor.OpenOMF(filename);

            if (Main_OMF != null)
            {
                bs.DataSource = Main_OMF.AnimsParams;
                lbxMotions.DataSource = bs;
                lbxMotions.DisplayMember = "Name";
                Main_OMF.FileName = filename;
            }
        }

        AnimationsContainer OpenSecondOMF(string filename)
        {
            if (Main_OMF == null) return null;

            AnimationsContainer new_omf = editor.OpenOMF(filename);

            if (new_omf == null) return new_omf;

            int error_v = editor.CompareOMF(Main_OMF, new_omf);

            if (error_v == 1)
            {
                DialogResult result = MessageBox.Show(rm.GetString("MERGE_ERROR_1"),"Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DialogResult == DialogResult.No) return null;
            }
            else if (error_v == 2)
            {
                MessageBox.Show(rm.GetString("MERGE_ERROR_2"), "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return new_omf;
        }

        private void UpdateList(bool save_pos = false)
        {
            int pos = lbxMotions.SelectedIndex;
            bs.ResetBindings(false);
            if (save_pos) lbxMotions.SelectedIndex = pos;
            MotionParamsUpdate();
        }

        private void AppendFile(string filename, List<string> list)
        {
            AnimationsContainer new_omf = OpenSecondOMF(filename);
            if (new_omf == null) return;

            for (int i = 0; i < Main_OMF.Anims.Count; i++)
            {
                list.Remove(Main_OMF.Anims[i].MotionName);
            }

            editor.CopyAnims(Main_OMF, new_omf, list);
            UpdateList();

        }

        private void AppendFile(string filename)
        {
            AnimationsContainer new_omf = OpenSecondOMF(filename);
            if (new_omf == null) return;
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

        private void SetT0(float t0)
        {
            GetCurrentMotion().m_marks[listMotionMarksGroup.SelectedIndices[0]].m_params[listMotionMarksParams.SelectedIndices[0]].t0 = t0;
        }

        private void SetT1(float t1)
        {
            GetCurrentMotion().m_marks[listMotionMarksGroup.SelectedIndices[0]].m_params[listMotionMarksParams.SelectedIndices[0]].t1 = t1;
        }

        private AnimationParams GetCurrentMotion()
        {
            if(lbxMotions.SelectedIndex != -1)
                return lbxMotions.SelectedItem as AnimationParams;
            else
                return null;
        }

        private AnimVector GetCurrentAnimVector()
        {
            if (lbxMotions.SelectedIndex != -1)
                return Main_OMF.Anims[lbxMotions.SelectedIndex];
            else
                return null;

        }

        private void MotionParamsUpdate(bool dont_reset_pos = false)
        {
            if(GetCurrentMotion()== null) return;

            if(!bTextBoxEnabled) EnableInput();

            tbxMotName.Text = GetCurrentMotion().Name;
            tbxMotSpeed.Text = GetCurrentMotion().Speed.ToString();
            tbxMotPower.Text = GetCurrentMotion().Power.ToString();
            tbxMotAcc.Text = GetCurrentMotion().Accrue.ToString();
            tbxMotFall.Text = GetCurrentMotion().Falloff.ToString();

            float keys_lenght = GetCurrentAnimVector().GetNumKeys() / (radioButton1.Checked ? 30.0f : 1.0f);

            tbxMotLenght.Text = keys_lenght.ToString();
            FillFlagsStates();

            CheckMotionMarksAvaiableAndUpdate(dont_reset_pos);
        }

        private void listMotionMarksGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bMotionMarksGroupSelected())
                MotionMarksParamUpdate(listMotionMarksGroup.SelectedIndices[0]);
            else
                MotionMarksParamUpdate(-1);
        }

        private void listMotionMarksParams_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(bMotionMarksGroupSelected() && bMotionMarkSelected())
                MotionMarksParamValuesUpdate(listMotionMarksGroup.SelectedIndices[0], listMotionMarksParams.SelectedIndices[0]);
            else
                MotionMarksParamValuesUpdate(-1,-1);
        }

        private void MotionMarksParamValuesUpdate(int index, int index2)
        {
            if (index != -1 && index2 != -1)
            {
                var currentAnimParams = GetCurrentMotion().m_marks[index].m_params[index2];
                ChangeMarksParamsEditState(true);
                boxStartMotionMark.Text = (currentAnimParams.t0 * (radioButton1.Checked ? 1 : 30.0f)).ToString();
                boxEndMotionMark.Text = (currentAnimParams.t1 * (radioButton1.Checked ? 1 : 30.0f)).ToString();
            }
            else
            {
                ChangeMarksParamsEditState(false);
            }
        }

        private void CheckMotionMarksAvaiableAndUpdate(bool dont_reset_pos = false)
        {
            if (!bMotMarkPanel && chbxHasMotionMarks.Checked)
                ChangeMotionMarksPanelState(true);
            else if (bMotMarkPanel && !chbxHasMotionMarks.Checked)
                ChangeMotionMarksPanelState(false);

            if(!dont_reset_pos) MotionMarkPanelReset();

            MotionMarksGroupUpdate(dont_reset_pos);
        }

        private void MotionMarksGroupUpdate(bool save_pos = false)
        {
            int pos = bMotionMarksGroupSelected() && save_pos ? listMotionMarksGroup.SelectedIndices[0] : 0;

            listMotionMarksGroup.Items.Clear();

            if(GetCurrentMotion().m_marks != null)
            {
                foreach (var mot in GetCurrentMotion().m_marks)
                    listMotionMarksGroup.Items.Add(mot.Name);

                if (listMotionMarksGroup.Items.Count > 0)
                    listMotionMarksGroup.Items[pos].Selected = true;
            }
        }

        private void MotionMarksParamUpdate(int index, bool save_pos = false)
        {
            int pos = bMotionMarkSelected() && save_pos ? listMotionMarksParams.SelectedIndices[0] : 0;

            listMotionMarksParams.Items.Clear();

            if (index != -1)
            {
                ChangeMarksParamsPanelState(true);

                var currentAnimParams = GetCurrentMotion().m_marks[index].m_params;

                for (int i = 0; i < currentAnimParams.Count; i++)
                {
                    listMotionMarksParams.Items.Add(index+"_mark" + i);
                }

                if (listMotionMarksParams.Items.Count > 0)
                    listMotionMarksParams.Items[pos].Selected = true;
            }
            else
            {
                ChangeMarksParamsPanelState(false);
                listMotionMarksParams_SelectedIndexChanged(null,null);
            }
        }

        private void FillFlagsStates()
        {
            if (Main_OMF == null) return;

            int Flags = GetCurrentMotion().Flags;

            for(int i = 1; i < 8;i++)
            {
                Boxes[i - 1].Checked = (Flags & (1 << i)) == (1 << i);
            }

            chbxHasMotionMarks.Checked = (Main_OMF.GetMotionVersion() == 4 && GetCurrentMotion().m_marks != null);
        }

        private void WriteAllFlags()
        {
            if (Main_OMF == null) return;

            for(int i = 1; i < 8;i++)
            {
                GetCurrentMotion().Flags = BitSet(GetCurrentMotion().Flags, (1 << i), Boxes[i - 1].Checked);
            }
        }

        //Events

        private void TextBoxFilter(object sender, EventArgs e)
        {
            if (Main_OMF == null || !bTextBoxEnabled) return;

            TextBox current = sender as TextBox;

            if (bKeyIsDown)
            {
                string mask = current.Tag.ToString() == "MotionName" ? @"^[A-Za-z0-9_$]*$" : number_mask;
                Match match = Regex.Match(current.Text, mask);
                if (!match.Success)
                {
                    int temp = current.SelectionStart;
                    current.Text = current.Text.Remove(current.SelectionStart - 1, 1);
                    current.SelectionStart = temp - 1;
                }
            }

            bKeyIsDown = false;

            switch (current.Tag.ToString())
            {
                case "Speed": GetCurrentMotion().Speed = Convert.ToSingle(current.Text); break;
                case "Power": GetCurrentMotion().Power = Convert.ToSingle(current.Text); break;
                case "Accrue": GetCurrentMotion().Accrue = Convert.ToSingle(current.Text); break;
                case "Falloff": GetCurrentMotion().Falloff = Convert.ToSingle(current.Text); break;
                case "MotionName":
                    {
                        if (GetCurrentMotion().Name == current.Text) return;
                        GetCurrentMotion().Name = current.Text;
                        int index = GetCurrentMotion().MotionID;
                        Main_OMF.Anims[index].Name = current.Text;
                        UpdateList(true);
                    }
                    break;
                default: break;
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            SaveOMF(Main_OMF, (sender as SaveFileDialog).FileName);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lbxMotions.SelectedIndex == -1) return;

            Main_OMF.Anims.RemoveAt(lbxMotions.SelectedIndex);
            Main_OMF.AnimsParams.RemoveAt(lbxMotions.SelectedIndex);
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

            var index = lbxMotions.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                contextMenuStrip1.Show(Cursor.Position);
                deleteToolStripMenuItem.Enabled = lbxMotions.Items.Count > 1;
                cloneToolStripMenuItem.Enabled = lbxMotions.Items.Count > 0;
                contextMenuStrip1.Visible = true;

                lbxMotions.SelectedIndex = index;

                //current_index = index;
            }
            else
            {
                contextMenuStrip1.Visible = false;
                //current_index = -1;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (Main_OMF == null) return;

            WriteAllFlags();
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                if (lbxMotions.Items.Count == 1) return;
               
                ListBox.SelectedIndexCollection _list = lbxMotions.SelectedIndices;
                int count = _list.Count;

                while (count > 0 && Main_OMF.AnimsParams.Count > 1)
                {
                    int i = _list[count - 1];
                    Main_OMF.AnimsParams.RemoveAt(i);
                    Main_OMF.Anims.RemoveAt(i);
                    --count;

                }

                Main_OMF.RecalcAllAnimIndex();
                Main_OMF.RecalcAnimNum();
                UpdateList();
            }
        }

        private void buttonAddAnims_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Owner = this;
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.Cancel) form.Dispose();
            if (result == DialogResult.OK)
            {

                if (form.richTextBox1.Text == "") return;

                List<string> list = form.richTextBox1.Text.Split('\n').ToList();
                form.Dispose();

                openFileDialog1.FileName = "";
                DialogResult res = openFileDialog1.ShowDialog();

                if (res == DialogResult.OK)
                {
                    try
                    {
                        AppendFile(openFileDialog1.FileName, list);
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.ToString());
                    }

                }
            }
        }

        private void buttonRepair_Click(object sender, EventArgs e)
        {
            if (Main_OMF == null) return;

            Main_OMF.GunslingerRepair();
        }

        private void textBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            bKeyIsDown = true;
        }

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
            switch (e.KeyData)
            {
                case Keys.F4: buttonLoad_Click(null,null); break;
                case Keys.F5: buttonSave_Click(null,null); break;
                case Keys.F6: buttonSaveAs_Click(null,null); break;
            }
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            DialogResult res = openFileDialog1.ShowDialog();

            if (res == DialogResult.OK)
            {
                try
                {
                    OpenFile(openFileDialog1.FileName);
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }

            }
        }

        private void buttonMerge_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            DialogResult res = openFileDialog1.ShowDialog();

            if (res == DialogResult.OK)
            {
                try
                {
                    AppendFile(openFileDialog1.FileName);
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }

            }

        }

        private void buttonSaveAs_Click(object sender, EventArgs e)
        {
            if (Main_OMF != null)
            {
                saveFileDialog1.FileName = "";
                saveFileDialog1.ShowDialog();
            }

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (Main_OMF != null)
            {
                SaveOMF(Main_OMF, Main_OMF.FileName);
                AutoClosingMessageBox.Show("Saved!", "", 500, MessageBoxIcon.Information);
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bTextBoxEnabled && (lbxMotions.SelectedItems.Count > 1 || Main_OMF == null))
                DisableInput();
            else if (lbxMotions.SelectedItems.Count == 1)
                MotionParamsUpdate();
        }

        private void listMotionMarksGroup_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            GetCurrentMotion().m_marks[e.Item].Name = e.Label;
        }

        private void btnAddMarkGroup_Click(object sender, EventArgs e)
		{
            MotionMark motionMark = new MotionMark();
            motionMark.Name = "NewGroup"+ GetCurrentMotion().m_marks.Count;
            motionMark.m_params = new List<MotionMarkParams>();
            GetCurrentMotion().m_marks.Add(motionMark);
            GetCurrentMotion().MarksCount += 1;

            MotionMarksGroupUpdate(true);

        }

        private void btnAddMark_Click(object sender, EventArgs e)
        {
            MotionMarkParams motionMarkParams = new MotionMarkParams();
            GetCurrentMotion().m_marks[listMotionMarksGroup.SelectedIndices[0]].m_params.Add(motionMarkParams);
            GetCurrentMotion().m_marks[listMotionMarksGroup.SelectedIndices[0]].Count +=1;
            MotionMarksParamUpdate(listMotionMarksGroup.SelectedIndices[0],true);

        }

        private void btnDelMarkGroup_Click(object sender, EventArgs e)
        {
            if(!bMotionMarksGroupSelected()) return;

            GetCurrentMotion().m_marks.RemoveAt(listMotionMarksGroup.SelectedIndices[0]);
            GetCurrentMotion().MarksCount -= 1;
            MotionMarksGroupUpdate();

            listMotionMarksGroup_SelectedIndexChanged(null, null);
        }

        private void btnDelMark_Click(object sender, EventArgs e)
        {
            if(!bMotionMarkSelected()) return;

            GetCurrentMotion().m_marks[listMotionMarksGroup.SelectedIndices[0]].m_params.RemoveAt(listMotionMarksParams.SelectedIndices[0]);
            GetCurrentMotion().m_marks[listMotionMarksGroup.SelectedIndices[0]].Count -= 1;
            MotionMarksParamUpdate(listMotionMarksGroup.SelectedIndices[0]);

            listMotionMarksParams_SelectedIndexChanged(null,null);
        }

        private void chbxHasMotionMarks_Click(object sender, EventArgs e)
		{
            if(!chbxHasMotionMarks.Checked) 
            {
                if(Main_OMF.GetMotionVersion()!=4)
                {
                    DialogResult result = MessageBox.Show("This operation will update OMF version from v3 to v4", "Upgrade.", MessageBoxButtons.YesNo);

                    if(result == DialogResult.Yes)
                        Main_OMF.bone_cont.OGF_V = 4;
                    else
                        return;
                }
                
                chbxHasMotionMarks.Checked = true;
                GetCurrentMotion().m_marks = new List<MotionMark>();

                CheckMotionMarksAvaiableAndUpdate();

            }
            else
            {
                DialogResult result = MessageBox.Show("This operation will delete all existing motion marks. Are you sure?", "Warning!", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    chbxHasMotionMarks.Checked = false;
                    GetCurrentMotion().MarksCount = 0;
                    GetCurrentMotion().m_marks = null;

                    CheckMotionMarksAvaiableAndUpdate();
                }
            }


                
        }

		private void boxMotionMark_TextChanged(object sender, EventArgs e)
		{
            if (Main_OMF == null || !(sender as Control).Enabled) return;

            if(!bMotionMarksGroupSelected() || !bMotionMarkSelected()) return;

            TextBox current = sender as TextBox;

            if (bKeyIsDown)
            {
                Match match = Regex.Match(current.Text, number_mask);
                if (!match.Success)
                {
                    int temp = current.SelectionStart;
                    current.Text = current.Text.Remove(current.SelectionStart - 1, 1);
                    current.SelectionStart = temp - 1;
                }

                float t = radioButton1.Checked ? 1.0f : 30.0f;

                if (current == boxStartMotionMark)
                    SetT0(Convert.ToSingle(boxStartMotionMark.Text)/t);
                else
                    SetT1(Convert.ToSingle(boxEndMotionMark.Text)/t);
            }

            bKeyIsDown = false;
        }

		private void radioButton1_CheckedChanged(object sender, EventArgs e)
		{
            MotionParamsUpdate(true);

        }

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
            this.linkLabel1.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start("https://github.com/mortany/omf_editor");
        }

        private void KeyDownFilter(object sender, KeyEventArgs e)
        {

            bool isMotionName = (!char.IsLetterOrDigit((char)e.KeyData) && (e.KeyValue != 189 && e.Shift)) || e.KeyValue != 52 && e.Shift;

            bool isMotionParam = !char.IsDigit((char)e.KeyData) || e.Shift;

            bool result = (sender as TextBox).Tag.ToString() == "MotionName" ? isMotionName : isMotionParam;

            if(result && e.KeyData != Keys.Back)
            {
                e.SuppressKeyPress = true;
            }
        }
    }
}
