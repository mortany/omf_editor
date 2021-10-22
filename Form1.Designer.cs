namespace OMF_Editor
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.lbxMotions = new System.Windows.Forms.ListBox();
			this.btnLoad = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.btnSave = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.label10 = new System.Windows.Forms.Label();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.tbxMotLenght = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.chbxHasMotionMarks = new System.Windows.Forms.CheckBox();
			this.chbxUseWeaponBone = new System.Windows.Forms.CheckBox();
			this.chbxIdle = new System.Windows.Forms.CheckBox();
			this.chbxMoveXForm = new System.Windows.Forms.CheckBox();
			this.chbxUseFootSteps = new System.Windows.Forms.CheckBox();
			this.chbxSyncPart = new System.Windows.Forms.CheckBox();
			this.chbxNoMix = new System.Windows.Forms.CheckBox();
			this.chbxStopAtEnd = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this.tbxMotFall = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.tbxMotAcc = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.tbxMotPower = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.tbxMotSpeed = new System.Windows.Forms.TextBox();
			this.tbxMotName = new System.Windows.Forms.TextBox();
			this.btnMergeWith = new System.Windows.Forms.Button();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.cloneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.btnAddAnimsFrom = new System.Windows.Forms.Button();
			this.btnTryRepair = new System.Windows.Forms.Button();
			this.btnSaveAs = new System.Windows.Forms.Button();
			this.groupMotomMarks = new System.Windows.Forms.GroupBox();
			this.btnDelMark = new System.Windows.Forms.Button();
			this.btnAddMark = new System.Windows.Forms.Button();
			this.btnDelMarkGroup = new System.Windows.Forms.Button();
			this.btnAddMarkGroup = new System.Windows.Forms.Button();
			this.listMotionMarksParams = new System.Windows.Forms.ListView();
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.listMotionMarksGroup = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.label7 = new System.Windows.Forms.Label();
			this.boxEndMotionMark = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.boxStartMotionMark = new System.Windows.Forms.TextBox();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.groupMotomMarks.SuspendLayout();
			this.SuspendLayout();
			// 
			// lbxMotions
			// 
			resources.ApplyResources(this.lbxMotions, "lbxMotions");
			this.lbxMotions.FormattingEnabled = true;
			this.lbxMotions.Name = "lbxMotions";
			this.lbxMotions.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.lbxMotions.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
			this.lbxMotions.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox1_KeyDown);
			this.lbxMotions.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDown);
			// 
			// btnLoad
			// 
			resources.ApplyResources(this.btnLoad, "btnLoad");
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.UseVisualStyleBackColor = true;
			this.btnLoad.Click += new System.EventHandler(this.buttonLoad_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// btnSave
			// 
			resources.ApplyResources(this.btnSave, "btnSave");
			this.btnSave.Name = "btnSave";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.groupBox2);
			this.groupBox1.Controls.Add(this.tbxMotLenght);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.chbxHasMotionMarks);
			this.groupBox1.Controls.Add(this.chbxUseWeaponBone);
			this.groupBox1.Controls.Add(this.chbxIdle);
			this.groupBox1.Controls.Add(this.chbxMoveXForm);
			this.groupBox1.Controls.Add(this.chbxUseFootSteps);
			this.groupBox1.Controls.Add(this.chbxSyncPart);
			this.groupBox1.Controls.Add(this.chbxNoMix);
			this.groupBox1.Controls.Add(this.chbxStopAtEnd);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.tbxMotFall);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.tbxMotAcc);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.tbxMotPower);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.tbxMotSpeed);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.tbxMotName);
			resources.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.radioButton2);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.radioButton1);
			resources.ApplyResources(this.groupBox2, "groupBox2");
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.TabStop = false;
			// 
			// radioButton2
			// 
			resources.ApplyResources(this.radioButton2, "radioButton2");
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.UseVisualStyleBackColor = true;
			this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
			// 
			// label10
			// 
			resources.ApplyResources(this.label10, "label10");
			this.label10.Name = "label10";
			// 
			// radioButton1
			// 
			resources.ApplyResources(this.radioButton1, "radioButton1");
			this.radioButton1.Checked = true;
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.TabStop = true;
			this.radioButton1.UseVisualStyleBackColor = true;
			this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
			// 
			// tbxMotLenght
			// 
			resources.ApplyResources(this.tbxMotLenght, "tbxMotLenght");
			this.tbxMotLenght.Name = "tbxMotLenght";
			this.tbxMotLenght.ReadOnly = true;
			this.tbxMotLenght.Tag = "Falloff";
			// 
			// label9
			// 
			resources.ApplyResources(this.label9, "label9");
			this.label9.Name = "label9";
			// 
			// chbxHasMotionMarks
			// 
			this.chbxHasMotionMarks.AutoCheck = false;
			resources.ApplyResources(this.chbxHasMotionMarks, "chbxHasMotionMarks");
			this.chbxHasMotionMarks.Name = "chbxHasMotionMarks";
			this.chbxHasMotionMarks.Tag = "";
			this.chbxHasMotionMarks.UseVisualStyleBackColor = true;
			this.chbxHasMotionMarks.Click += new System.EventHandler(this.chbxHasMotionMarks_Click);
			// 
			// chbxUseWeaponBone
			// 
			resources.ApplyResources(this.chbxUseWeaponBone, "chbxUseWeaponBone");
			this.chbxUseWeaponBone.Name = "chbxUseWeaponBone";
			this.chbxUseWeaponBone.Tag = "UseWeaponBone";
			this.chbxUseWeaponBone.UseVisualStyleBackColor = true;
			this.chbxUseWeaponBone.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// chbxIdle
			// 
			resources.ApplyResources(this.chbxIdle, "chbxIdle");
			this.chbxIdle.Name = "chbxIdle";
			this.chbxIdle.Tag = "Idle";
			this.chbxIdle.UseVisualStyleBackColor = true;
			this.chbxIdle.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// chbxMoveXForm
			// 
			resources.ApplyResources(this.chbxMoveXForm, "chbxMoveXForm");
			this.chbxMoveXForm.Name = "chbxMoveXForm";
			this.chbxMoveXForm.Tag = "Move XForm";
			this.chbxMoveXForm.UseVisualStyleBackColor = true;
			this.chbxMoveXForm.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// chbxUseFootSteps
			// 
			resources.ApplyResources(this.chbxUseFootSteps, "chbxUseFootSteps");
			this.chbxUseFootSteps.Name = "chbxUseFootSteps";
			this.chbxUseFootSteps.Tag = "UseFootSteps";
			this.chbxUseFootSteps.UseVisualStyleBackColor = true;
			this.chbxUseFootSteps.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// chbxSyncPart
			// 
			resources.ApplyResources(this.chbxSyncPart, "chbxSyncPart");
			this.chbxSyncPart.Name = "chbxSyncPart";
			this.chbxSyncPart.Tag = "Sync Part";
			this.chbxSyncPart.UseVisualStyleBackColor = true;
			this.chbxSyncPart.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// chbxNoMix
			// 
			resources.ApplyResources(this.chbxNoMix, "chbxNoMix");
			this.chbxNoMix.Name = "chbxNoMix";
			this.chbxNoMix.Tag = "No Mix";
			this.chbxNoMix.UseVisualStyleBackColor = true;
			this.chbxNoMix.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// chbxStopAtEnd
			// 
			resources.ApplyResources(this.chbxStopAtEnd, "chbxStopAtEnd");
			this.chbxStopAtEnd.Name = "chbxStopAtEnd";
			this.chbxStopAtEnd.Tag = "Stop At End";
			this.chbxStopAtEnd.UseVisualStyleBackColor = true;
			this.chbxStopAtEnd.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// label6
			// 
			resources.ApplyResources(this.label6, "label6");
			this.label6.Name = "label6";
			// 
			// tbxMotFall
			// 
			resources.ApplyResources(this.tbxMotFall, "tbxMotFall");
			this.tbxMotFall.Name = "tbxMotFall";
			this.tbxMotFall.Tag = "Falloff";
			this.tbxMotFall.TextChanged += new System.EventHandler(this.TextBoxFilter);
			this.tbxMotFall.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.textBox1_PreviewKeyDown);
			// 
			// label5
			// 
			resources.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			// 
			// tbxMotAcc
			// 
			resources.ApplyResources(this.tbxMotAcc, "tbxMotAcc");
			this.tbxMotAcc.Name = "tbxMotAcc";
			this.tbxMotAcc.Tag = "Accrue";
			this.tbxMotAcc.TextChanged += new System.EventHandler(this.TextBoxFilter);
			this.tbxMotAcc.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.textBox1_PreviewKeyDown);
			// 
			// label4
			// 
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			// 
			// tbxMotPower
			// 
			resources.ApplyResources(this.tbxMotPower, "tbxMotPower");
			this.tbxMotPower.Name = "tbxMotPower";
			this.tbxMotPower.Tag = "Power";
			this.tbxMotPower.TextChanged += new System.EventHandler(this.TextBoxFilter);
			this.tbxMotPower.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.textBox1_PreviewKeyDown);
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			// 
			// tbxMotSpeed
			// 
			resources.ApplyResources(this.tbxMotSpeed, "tbxMotSpeed");
			this.tbxMotSpeed.Name = "tbxMotSpeed";
			this.tbxMotSpeed.Tag = "Speed";
			this.tbxMotSpeed.TextChanged += new System.EventHandler(this.TextBoxFilter);
			this.tbxMotSpeed.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.textBox1_PreviewKeyDown);
			// 
			// tbxMotName
			// 
			resources.ApplyResources(this.tbxMotName, "tbxMotName");
			this.tbxMotName.Name = "tbxMotName";
			this.tbxMotName.Tag = "MotionName";
			this.tbxMotName.TextChanged += new System.EventHandler(this.TextBoxFilter);
			this.tbxMotName.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.textBox1_PreviewKeyDown);
			// 
			// btnMergeWith
			// 
			resources.ApplyResources(this.btnMergeWith, "btnMergeWith");
			this.btnMergeWith.Name = "btnMergeWith";
			this.btnMergeWith.UseVisualStyleBackColor = true;
			this.btnMergeWith.Click += new System.EventHandler(this.buttonMerge_Click);
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cloneToolStripMenuItem,
            this.deleteToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
			// 
			// cloneToolStripMenuItem
			// 
			this.cloneToolStripMenuItem.Name = "cloneToolStripMenuItem";
			resources.ApplyResources(this.cloneToolStripMenuItem, "cloneToolStripMenuItem");
			this.cloneToolStripMenuItem.Click += new System.EventHandler(this.cloneToolStripMenuItem_Click);
			// 
			// deleteToolStripMenuItem
			// 
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			resources.ApplyResources(this.deleteToolStripMenuItem, "deleteToolStripMenuItem");
			this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
			// 
			// btnAddAnimsFrom
			// 
			resources.ApplyResources(this.btnAddAnimsFrom, "btnAddAnimsFrom");
			this.btnAddAnimsFrom.Name = "btnAddAnimsFrom";
			this.btnAddAnimsFrom.UseVisualStyleBackColor = true;
			this.btnAddAnimsFrom.Click += new System.EventHandler(this.buttonAddAnims_Click);
			// 
			// btnTryRepair
			// 
			resources.ApplyResources(this.btnTryRepair, "btnTryRepair");
			this.btnTryRepair.Name = "btnTryRepair";
			this.btnTryRepair.UseVisualStyleBackColor = true;
			this.btnTryRepair.Click += new System.EventHandler(this.buttonRepair_Click);
			// 
			// btnSaveAs
			// 
			resources.ApplyResources(this.btnSaveAs, "btnSaveAs");
			this.btnSaveAs.Name = "btnSaveAs";
			this.btnSaveAs.UseVisualStyleBackColor = true;
			this.btnSaveAs.Click += new System.EventHandler(this.buttonSaveAs_Click);
			// 
			// groupMotomMarks
			// 
			this.groupMotomMarks.Controls.Add(this.btnDelMark);
			this.groupMotomMarks.Controls.Add(this.btnAddMark);
			this.groupMotomMarks.Controls.Add(this.btnDelMarkGroup);
			this.groupMotomMarks.Controls.Add(this.btnAddMarkGroup);
			this.groupMotomMarks.Controls.Add(this.listMotionMarksParams);
			this.groupMotomMarks.Controls.Add(this.listMotionMarksGroup);
			this.groupMotomMarks.Controls.Add(this.label7);
			this.groupMotomMarks.Controls.Add(this.boxEndMotionMark);
			this.groupMotomMarks.Controls.Add(this.label8);
			this.groupMotomMarks.Controls.Add(this.boxStartMotionMark);
			resources.ApplyResources(this.groupMotomMarks, "groupMotomMarks");
			this.groupMotomMarks.Name = "groupMotomMarks";
			this.groupMotomMarks.TabStop = false;
			// 
			// btnDelMark
			// 
			resources.ApplyResources(this.btnDelMark, "btnDelMark");
			this.btnDelMark.Name = "btnDelMark";
			this.btnDelMark.UseVisualStyleBackColor = true;
			this.btnDelMark.Click += new System.EventHandler(this.btnDelMark_Click);
			// 
			// btnAddMark
			// 
			resources.ApplyResources(this.btnAddMark, "btnAddMark");
			this.btnAddMark.Name = "btnAddMark";
			this.btnAddMark.UseVisualStyleBackColor = true;
			this.btnAddMark.Click += new System.EventHandler(this.btnAddMark_Click);
			// 
			// btnDelMarkGroup
			// 
			resources.ApplyResources(this.btnDelMarkGroup, "btnDelMarkGroup");
			this.btnDelMarkGroup.Name = "btnDelMarkGroup";
			this.btnDelMarkGroup.UseVisualStyleBackColor = true;
			this.btnDelMarkGroup.Click += new System.EventHandler(this.btnDelMarkGroup_Click);
			// 
			// btnAddMarkGroup
			// 
			resources.ApplyResources(this.btnAddMarkGroup, "btnAddMarkGroup");
			this.btnAddMarkGroup.Name = "btnAddMarkGroup";
			this.btnAddMarkGroup.UseVisualStyleBackColor = true;
			this.btnAddMarkGroup.Click += new System.EventHandler(this.btnAddMarkGroup_Click);
			// 
			// listMotionMarksParams
			// 
			resources.ApplyResources(this.listMotionMarksParams, "listMotionMarksParams");
			this.listMotionMarksParams.AutoArrange = false;
			this.listMotionMarksParams.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
			this.listMotionMarksParams.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.listMotionMarksParams.HideSelection = false;
			this.listMotionMarksParams.MultiSelect = false;
			this.listMotionMarksParams.Name = "listMotionMarksParams";
			this.listMotionMarksParams.ShowGroups = false;
			this.listMotionMarksParams.UseCompatibleStateImageBehavior = false;
			this.listMotionMarksParams.View = System.Windows.Forms.View.Details;
			this.listMotionMarksParams.SelectedIndexChanged += new System.EventHandler(this.listMotionMarksParams_SelectedIndexChanged);
			// 
			// columnHeader2
			// 
			resources.ApplyResources(this.columnHeader2, "columnHeader2");
			// 
			// listMotionMarksGroup
			// 
			resources.ApplyResources(this.listMotionMarksGroup, "listMotionMarksGroup");
			this.listMotionMarksGroup.AutoArrange = false;
			this.listMotionMarksGroup.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.listMotionMarksGroup.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.listMotionMarksGroup.HideSelection = false;
			this.listMotionMarksGroup.LabelEdit = true;
			this.listMotionMarksGroup.MultiSelect = false;
			this.listMotionMarksGroup.Name = "listMotionMarksGroup";
			this.listMotionMarksGroup.ShowGroups = false;
			this.listMotionMarksGroup.UseCompatibleStateImageBehavior = false;
			this.listMotionMarksGroup.View = System.Windows.Forms.View.Details;
			this.listMotionMarksGroup.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listMotionMarksGroup_AfterLabelEdit);
			this.listMotionMarksGroup.SelectedIndexChanged += new System.EventHandler(this.listMotionMarksGroup_SelectedIndexChanged);
			// 
			// columnHeader1
			// 
			resources.ApplyResources(this.columnHeader1, "columnHeader1");
			// 
			// label7
			// 
			resources.ApplyResources(this.label7, "label7");
			this.label7.Name = "label7";
			// 
			// boxEndMotionMark
			// 
			resources.ApplyResources(this.boxEndMotionMark, "boxEndMotionMark");
			this.boxEndMotionMark.Name = "boxEndMotionMark";
			this.boxEndMotionMark.Tag = "Power";
			this.boxEndMotionMark.TextChanged += new System.EventHandler(this.boxMotionMark_TextChanged);
			this.boxEndMotionMark.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.textBox1_PreviewKeyDown);
			// 
			// label8
			// 
			resources.ApplyResources(this.label8, "label8");
			this.label8.Name = "label8";
			// 
			// boxStartMotionMark
			// 
			resources.ApplyResources(this.boxStartMotionMark, "boxStartMotionMark");
			this.boxStartMotionMark.Name = "boxStartMotionMark";
			this.boxStartMotionMark.Tag = "Speed";
			this.boxStartMotionMark.TextChanged += new System.EventHandler(this.boxMotionMark_TextChanged);
			this.boxStartMotionMark.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.textBox1_PreviewKeyDown);
			// 
			// linkLabel1
			// 
			resources.ApplyResources(this.linkLabel1, "linkLabel1");
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.TabStop = true;
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// Form1
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.groupMotomMarks);
			this.Controls.Add(this.lbxMotions);
			this.Controls.Add(this.btnSaveAs);
			this.Controls.Add(this.btnTryRepair);
			this.Controls.Add(this.btnAddAnimsFrom);
			this.Controls.Add(this.btnMergeWith);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnLoad);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.contextMenuStrip1.ResumeLayout(false);
			this.groupMotomMarks.ResumeLayout(false);
			this.groupMotomMarks.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbxMotions;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbxMotFall;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxMotAcc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxMotPower;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxMotSpeed;
        private System.Windows.Forms.TextBox tbxMotName;
        private System.Windows.Forms.Button btnMergeWith;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cloneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.CheckBox chbxUseWeaponBone;
        private System.Windows.Forms.CheckBox chbxIdle;
        private System.Windows.Forms.CheckBox chbxMoveXForm;
        private System.Windows.Forms.CheckBox chbxUseFootSteps;
        private System.Windows.Forms.CheckBox chbxSyncPart;
        private System.Windows.Forms.CheckBox chbxNoMix;
        private System.Windows.Forms.CheckBox chbxStopAtEnd;
        private System.Windows.Forms.Button btnAddAnimsFrom;
        private System.Windows.Forms.Button btnTryRepair;
		private System.Windows.Forms.Button btnSaveAs;
		private System.Windows.Forms.CheckBox chbxHasMotionMarks;
		private System.Windows.Forms.GroupBox groupMotomMarks;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox boxEndMotionMark;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox boxStartMotionMark;
		private System.Windows.Forms.ListView listMotionMarksGroup;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ListView listMotionMarksParams;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Button btnDelMark;
		private System.Windows.Forms.Button btnAddMark;
		private System.Windows.Forms.Button btnAddMarkGroup;
		private System.Windows.Forms.Button btnDelMarkGroup;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton radioButton2;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.TextBox tbxMotLenght;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.LinkLabel linkLabel1;
	}
}

