namespace AWGP
{
    partial class EditorForm
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
            this.saveConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.creditsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label14 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.comboBox10 = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.comboBox9 = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.comboBox8 = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.isMouseActive = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.resolutionSelect = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.isFullScreen = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.windowTitleBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.isOtherActive = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.isUserActive = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.isScreenActive = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.isPhysicsActive = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.isInputActive = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.label1 = new System.Windows.Forms.Label();
            this.ReloadButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveConfigToolStripMenuItem
            // 
            this.saveConfigToolStripMenuItem.Name = "saveConfigToolStripMenuItem";
            this.saveConfigToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.saveConfigToolStripMenuItem.Text = "Save Config";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadConfigToolStripMenuItem,
            this.saveConfigToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadConfigToolStripMenuItem
            // 
            this.loadConfigToolStripMenuItem.Name = "loadConfigToolStripMenuItem";
            this.loadConfigToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.loadConfigToolStripMenuItem.Text = "Load Config";
            // 
            // creditsToolStripMenuItem
            // 
            this.creditsToolStripMenuItem.Name = "creditsToolStripMenuItem";
            this.creditsToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.creditsToolStripMenuItem.Text = "Credits";
            this.creditsToolStripMenuItem.Click += new System.EventHandler(this.creditsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.creditsToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(44, 20);
            this.toolStripMenuItem1.Text = "Help";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(415, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label14);
            this.tabPage3.Controls.Add(this.button3);
            this.tabPage3.Controls.Add(this.comboBox10);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.button2);
            this.tabPage3.Controls.Add(this.comboBox9);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.textBox3);
            this.tabPage3.Controls.Add(this.comboBox8);
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(383, 143);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Screen Settings";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(273, 121);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(100, 13);
            this.label14.TabIndex = 26;
            this.label14.Text = "* this opens a editor";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(242, 64);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(131, 23);
            this.button3.TabIndex = 25;
            this.button3.Text = "Edit";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // comboBox10
            // 
            this.comboBox10.FormattingEnabled = true;
            this.comboBox10.Items.AddRange(new object[] {
            "Game",
            "Menu",
            "Splash"});
            this.comboBox10.Location = new System.Drawing.Point(95, 64);
            this.comboBox10.Name = "comboBox10";
            this.comboBox10.Size = new System.Drawing.Size(131, 21);
            this.comboBox10.TabIndex = 24;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 68);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(66, 13);
            this.label13.TabIndex = 23;
            this.label13.Text = "Edit Screen*";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(242, 36);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(131, 23);
            this.button2.TabIndex = 22;
            this.button2.Text = "Confirm Deletion";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // comboBox9
            // 
            this.comboBox9.FormattingEnabled = true;
            this.comboBox9.Items.AddRange(new object[] {
            "Game",
            "Menu",
            "Splash"});
            this.comboBox9.Location = new System.Drawing.Point(95, 36);
            this.comboBox9.Name = "comboBox9";
            this.comboBox9.Size = new System.Drawing.Size(131, 21);
            this.comboBox9.TabIndex = 21;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 40);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(84, 13);
            this.label12.TabIndex = 20;
            this.label12.Text = "Remove Screen";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(95, 10);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(131, 20);
            this.textBox3.TabIndex = 19;
            this.textBox3.Text = "\r";
            // 
            // comboBox8
            // 
            this.comboBox8.FormattingEnabled = true;
            this.comboBox8.Items.AddRange(new object[] {
            "Game",
            "Menu",
            "Splash"});
            this.comboBox8.Location = new System.Drawing.Point(242, 9);
            this.comboBox8.Name = "comboBox8";
            this.comboBox8.Size = new System.Drawing.Size(131, 21);
            this.comboBox8.TabIndex = 12;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(2, 12);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(88, 13);
            this.label11.TabIndex = 11;
            this.label11.Text = "Add New Screen";
            // 
            // isMouseActive
            // 
            this.isMouseActive.FormattingEnabled = true;
            this.isMouseActive.Items.AddRange(new object[] {
            "true",
            "false"});
            this.isMouseActive.Location = new System.Drawing.Point(208, 87);
            this.isMouseActive.Name = "isMouseActive";
            this.isMouseActive.Size = new System.Drawing.Size(169, 21);
            this.isMouseActive.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Mouse Activity";
            // 
            // resolutionSelect
            // 
            this.resolutionSelect.FormattingEnabled = true;
            this.resolutionSelect.Items.AddRange(new object[] {
            "640  x  360 ",
            "854  x  480",
            "960  x  540",
            "1024  x  576 ",
            "1280  x  720",
            "1366  x  768",
            "1600  x  900",
            "1920  x  1080",
            "2048  x  1152",
            "2560  x  1440"});
            this.resolutionSelect.Location = new System.Drawing.Point(208, 60);
            this.resolutionSelect.Name = "resolutionSelect";
            this.resolutionSelect.Size = new System.Drawing.Size(169, 21);
            this.resolutionSelect.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Window Resolution";
            // 
            // isFullScreen
            // 
            this.isFullScreen.FormattingEnabled = true;
            this.isFullScreen.Items.AddRange(new object[] {
            "true",
            "false"});
            this.isFullScreen.Location = new System.Drawing.Point(208, 33);
            this.isFullScreen.Name = "isFullScreen";
            this.isFullScreen.Size = new System.Drawing.Size(169, 21);
            this.isFullScreen.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Window Title";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.isMouseActive);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.resolutionSelect);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.windowTitleBox);
            this.tabPage1.Controls.Add(this.isFullScreen);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(383, 143);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Application Settings";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // windowTitleBox
            // 
            this.windowTitleBox.Location = new System.Drawing.Point(208, 7);
            this.windowTitleBox.Name = "windowTitleBox";
            this.windowTitleBox.Size = new System.Drawing.Size(169, 20);
            this.windowTitleBox.TabIndex = 4;
            this.windowTitleBox.Text = "\r\n";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Window Mode";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.isOtherActive);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.isUserActive);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.isScreenActive);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.isPhysicsActive);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.isInputActive);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(383, 143);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Components Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // isOtherActive
            // 
            this.isOtherActive.Location = new System.Drawing.Point(208, 114);
            this.isOtherActive.Name = "isOtherActive";
            this.isOtherActive.Size = new System.Drawing.Size(169, 20);
            this.isOtherActive.TabIndex = 18;
            this.isOtherActive.Text = "\r";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 117);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "Other Modules";
            // 
            // isUserActive
            // 
            this.isUserActive.FormattingEnabled = true;
            this.isUserActive.Items.AddRange(new object[] {
            "true",
            "false"});
            this.isUserActive.Location = new System.Drawing.Point(208, 87);
            this.isUserActive.Name = "isUserActive";
            this.isUserActive.Size = new System.Drawing.Size(169, 21);
            this.isUserActive.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 90);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "User Module";
            // 
            // isScreenActive
            // 
            this.isScreenActive.FormattingEnabled = true;
            this.isScreenActive.Items.AddRange(new object[] {
            "true",
            "false"});
            this.isScreenActive.Location = new System.Drawing.Point(208, 60);
            this.isScreenActive.Name = "isScreenActive";
            this.isScreenActive.Size = new System.Drawing.Size(169, 21);
            this.isScreenActive.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Screen Module";
            // 
            // isPhysicsActive
            // 
            this.isPhysicsActive.FormattingEnabled = true;
            this.isPhysicsActive.Items.AddRange(new object[] {
            "true",
            "false"});
            this.isPhysicsActive.Location = new System.Drawing.Point(208, 33);
            this.isPhysicsActive.Name = "isPhysicsActive";
            this.isPhysicsActive.Size = new System.Drawing.Size(169, 21);
            this.isPhysicsActive.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Physics Module";
            // 
            // isInputActive
            // 
            this.isInputActive.FormattingEnabled = true;
            this.isInputActive.Items.AddRange(new object[] {
            "true",
            "false"});
            this.isInputActive.Location = new System.Drawing.Point(208, 6);
            this.isInputActive.Name = "isInputActive";
            this.isInputActive.Size = new System.Drawing.Size(169, 21);
            this.isInputActive.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Input Module";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 62);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(391, 169);
            this.tabControl1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(394, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Welcome to the Hydra Engine Editor, please use this application to make changes";
            // 
            // ReloadButton
            // 
            this.ReloadButton.Location = new System.Drawing.Point(11, 237);
            this.ReloadButton.Name = "ReloadButton";
            this.ReloadButton.Size = new System.Drawing.Size(391, 23);
            this.ReloadButton.TabIndex = 5;
            this.ReloadButton.Text = "Reload Application";
            this.ReloadButton.UseVisualStyleBackColor = true;
            this.ReloadButton.Click += new System.EventHandler(this.ReloadButton_Click);
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 270);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ReloadButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "EditorForm";
            this.Text = "Hydra Engine - Editor";
            this.Load += new System.EventHandler(this.EditorForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem saveConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem creditsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ComboBox isMouseActive;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox resolutionSelect;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox isFullScreen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox windowTitleBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ReloadButton;
        private System.Windows.Forms.ComboBox isScreenActive;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox isPhysicsActive;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox isInputActive;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox isOtherActive;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox isUserActive;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboBox9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.ComboBox comboBox8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox comboBox10;
        private System.Windows.Forms.Label label13;
    }
}