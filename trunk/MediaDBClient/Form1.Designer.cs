namespace MediaDBClient
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnResetView = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.txtBoxFileInfo = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TagList = new System.Windows.Forms.TreeView();
            this.contextTagList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripDeleteTag = new System.Windows.Forms.ToolStripMenuItem();
            this.btnUpdatetagsFromChkbox = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new EasyVLC.VlcUserControl();
            this.btnStop = new System.Windows.Forms.Button();
            this.txtEntry = new System.Windows.Forms.TextBox();
            this.lsFileBrowser = new System.Windows.Forms.ListView();
            this.TagSelector = new System.Windows.Forms.CheckedListBox();
            this.txtBulkFilename = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.bulktagList = new System.Windows.Forms.CheckedListBox();
            this.btnBulkTag = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnCreateNewTag = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.contextTagList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1188, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importFolderToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // importFolderToolStripMenuItem
            // 
            this.importFolderToolStripMenuItem.Name = "importFolderToolStripMenuItem";
            this.importFolderToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.importFolderToolStripMenuItem.Text = "Import Folder";
            this.importFolderToolStripMenuItem.Click += new System.EventHandler(this.importFolderToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 595);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1188, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // btnResetView
            // 
            this.btnResetView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnResetView.Location = new System.Drawing.Point(67, 374);
            this.btnResetView.Name = "btnResetView";
            this.btnResetView.Size = new System.Drawing.Size(75, 23);
            this.btnResetView.TabIndex = 4;
            this.btnResetView.Text = "Snapshot";
            this.btnResetView.UseVisualStyleBackColor = true;
            this.btnResetView.Click += new System.EventHandler(this.ResetView_Click);
            // 
            // txtBoxFileInfo
            // 
            this.txtBoxFileInfo.Location = new System.Drawing.Point(3, 47);
            this.txtBoxFileInfo.Name = "txtBoxFileInfo";
            this.txtBoxFileInfo.Size = new System.Drawing.Size(149, 32);
            this.txtBoxFileInfo.TabIndex = 8;
            this.txtBoxFileInfo.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "File Info";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 256);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Tags";
            // 
            // TagList
            // 
            this.TagList.ContextMenuStrip = this.contextTagList;
            this.TagList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TagList.Location = new System.Drawing.Point(0, 0);
            this.TagList.Name = "TagList";
            this.TagList.Size = new System.Drawing.Size(132, 571);
            this.TagList.TabIndex = 12;
            // 
            // contextTagList
            // 
            this.contextTagList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDeleteTag});
            this.contextTagList.Name = "contextTagList";
            this.contextTagList.Size = new System.Drawing.Size(108, 26);
            // 
            // toolStripDeleteTag
            // 
            this.toolStripDeleteTag.Name = "toolStripDeleteTag";
            this.toolStripDeleteTag.Size = new System.Drawing.Size(152, 22);
            this.toolStripDeleteTag.Text = "Delete";
            this.toolStripDeleteTag.Click += new System.EventHandler(this.toolStripDeleteTag_Click);
            // 
            // btnUpdatetagsFromChkbox
            // 
            this.btnUpdatetagsFromChkbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdatetagsFromChkbox.Location = new System.Drawing.Point(2, 298);
            this.btnUpdatetagsFromChkbox.Name = "btnUpdatetagsFromChkbox";
            this.btnUpdatetagsFromChkbox.Size = new System.Drawing.Size(118, 23);
            this.btnUpdatetagsFromChkbox.TabIndex = 14;
            this.btnUpdatetagsFromChkbox.Text = "Update Tags";
            this.btnUpdatetagsFromChkbox.UseVisualStyleBackColor = true;
            this.btnUpdatetagsFromChkbox.Click += new System.EventHandler(this.btnUpdatetagsfromchkbox_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(3, 85);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(145, 136);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Blue;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 750);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 50);
            this.panel1.Size = new System.Drawing.Size(447, 368);
            this.panel1.TabIndex = 0;
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStop.Location = new System.Drawing.Point(3, 374);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(58, 23);
            this.btnStop.TabIndex = 17;
            this.btnStop.Text = "Pause";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // txtEntry
            // 
            this.txtEntry.Location = new System.Drawing.Point(3, 272);
            this.txtEntry.Name = "txtEntry";
            this.txtEntry.Size = new System.Drawing.Size(149, 20);
            this.txtEntry.TabIndex = 18;
            // 
            // lsFileBrowser
            // 
            this.lsFileBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsFileBrowser.Location = new System.Drawing.Point(0, 0);
            this.lsFileBrowser.MultiSelect = false;
            this.lsFileBrowser.Name = "lsFileBrowser";
            this.lsFileBrowser.Size = new System.Drawing.Size(420, 571);
            this.lsFileBrowser.TabIndex = 20;
            this.lsFileBrowser.TileSize = new System.Drawing.Size(32, 32);
            this.lsFileBrowser.UseCompatibleStateImageBehavior = false;
            // 
            // TagSelector
            // 
            this.TagSelector.CheckOnClick = true;
            this.TagSelector.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TagSelector.FormattingEnabled = true;
            this.TagSelector.Location = new System.Drawing.Point(0, 327);
            this.TagSelector.Name = "TagSelector";
            this.TagSelector.Size = new System.Drawing.Size(177, 244);
            this.TagSelector.TabIndex = 22;
            // 
            // txtBulkFilename
            // 
            this.txtBulkFilename.Location = new System.Drawing.Point(145, 86);
            this.txtBulkFilename.Name = "txtBulkFilename";
            this.txtBulkFilename.Size = new System.Drawing.Size(148, 20);
            this.txtBulkFilename.TabIndex = 23;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.Location = new System.Drawing.Point(0, 403);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(447, 168);
            this.tabControl1.TabIndex = 24;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.bulktagList);
            this.tabPage1.Controls.Add(this.btnBulkTag);
            this.tabPage1.Controls.Add(this.txtBulkFilename);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(439, 142);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Bulk Taging";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(132, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(270, 39);
            this.label3.TabIndex = 27;
            this.label3.Text = "Select wanted tags, then set your filename string match.\r\n IE %me% will match str" +
                "ings like \'james\' \'madmen\'\r\nWill overwrite any existing tags on content!";
            // 
            // bulktagList
            // 
            this.bulktagList.FormattingEnabled = true;
            this.bulktagList.Location = new System.Drawing.Point(6, 15);
            this.bulktagList.Name = "bulktagList";
            this.bulktagList.Size = new System.Drawing.Size(120, 94);
            this.bulktagList.TabIndex = 26;
            // 
            // btnBulkTag
            // 
            this.btnBulkTag.Location = new System.Drawing.Point(313, 86);
            this.btnBulkTag.Name = "btnBulkTag";
            this.btnBulkTag.Size = new System.Drawing.Size(86, 23);
            this.btnBulkTag.TabIndex = 25;
            this.btnBulkTag.Text = "Tag Matches";
            this.btnBulkTag.UseVisualStyleBackColor = true;
            this.btnBulkTag.Click += new System.EventHandler(this.btnBulkTag_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Controls.Add(this.btnCreateNewTag);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(439, 142);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Create Tag";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 98);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(173, 20);
            this.textBox1.TabIndex = 1;
            // 
            // btnCreateNewTag
            // 
            this.btnCreateNewTag.Location = new System.Drawing.Point(322, 98);
            this.btnCreateNewTag.Name = "btnCreateNewTag";
            this.btnCreateNewTag.Size = new System.Drawing.Size(75, 23);
            this.btnCreateNewTag.TabIndex = 0;
            this.btnCreateNewTag.Text = "Create Tag";
            this.btnCreateNewTag.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.TagList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lsFileBrowser);
            this.splitContainer1.Size = new System.Drawing.Size(556, 571);
            this.splitContainer1.SplitterDistance = 132;
            this.splitContainer1.TabIndex = 22;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer2.Panel1.Controls.Add(this.txtEntry);
            this.splitContainer2.Panel1.Controls.Add(this.TagSelector);
            this.splitContainer2.Panel1.Controls.Add(this.btnUpdatetagsFromChkbox);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitter1);
            this.splitContainer2.Panel2.Controls.Add(this.panel1);
            this.splitContainer2.Panel2.Controls.Add(this.btnResetView);
            this.splitContainer2.Panel2.Controls.Add(this.btnStop);
            this.splitContainer2.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer2.Size = new System.Drawing.Size(628, 571);
            this.splitContainer2.SplitterDistance = 177;
            this.splitContainer2.TabIndex = 25;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.txtBoxFileInfo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(177, 273);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 403);
            this.splitter1.TabIndex = 25;
            this.splitter1.TabStop = false;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 24);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer3.Size = new System.Drawing.Size(1188, 571);
            this.splitContainer3.SplitterDistance = 556;
            this.splitContainer3.TabIndex = 26;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1188, 617);
            this.Controls.Add(this.splitContainer3);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextTagList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button btnResetView;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.RichTextBox txtBoxFileInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TreeView TagList;
        private System.Windows.Forms.Button btnUpdatetagsFromChkbox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private EasyVLC.VlcUserControl panel1;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.TextBox txtEntry;
        private System.Windows.Forms.ListView lsFileBrowser;
        private System.Windows.Forms.CheckedListBox TagSelector;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.TextBox txtBulkFilename;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckedListBox bulktagList;
        private System.Windows.Forms.Button btnBulkTag;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnCreateNewTag;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ContextMenuStrip contextTagList;
        private System.Windows.Forms.ToolStripMenuItem toolStripDeleteTag;
    }
}

