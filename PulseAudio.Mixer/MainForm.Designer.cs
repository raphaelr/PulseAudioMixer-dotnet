namespace PulseAudio.Mixer
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notificationMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.notifyBtnQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.btnQuit = new System.Windows.Forms.Button();
            this.tableSliders = new System.Windows.Forms.TableLayoutPanel();
            this.notificationMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.notificationMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "PulseAudio Mixer";
            this.notifyIcon.Visible = true;
            this.notifyIcon.Click += new System.EventHandler(this.NotifyIconOnClick);
            // 
            // notificationMenu
            // 
            this.notificationMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.notifyBtnQuit});
            this.notificationMenu.Name = "notificationMenu";
            this.notificationMenu.Size = new System.Drawing.Size(98, 26);
            // 
            // notifyBtnQuit
            // 
            this.notifyBtnQuit.Name = "notifyBtnQuit";
            this.notifyBtnQuit.Size = new System.Drawing.Size(97, 22);
            this.notifyBtnQuit.Text = "Quit";
            this.notifyBtnQuit.Click += new System.EventHandler(this.NotifyButtonQuitOnClick);
            // 
            // btnQuit
            // 
            this.btnQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnQuit.Location = new System.Drawing.Point(0, 0);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(0, 0);
            this.btnQuit.TabIndex = 1;
            this.btnQuit.Text = "Quit";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.ButtonQuitOnClick);
            // 
            // tableSliders
            // 
            this.tableSliders.ColumnCount = 1;
            this.tableSliders.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableSliders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableSliders.Location = new System.Drawing.Point(0, 0);
            this.tableSliders.Name = "tableSliders";
            this.tableSliders.RowCount = 1;
            this.tableSliders.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableSliders.Size = new System.Drawing.Size(512, 333);
            this.tableSliders.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnQuit;
            this.ClientSize = new System.Drawing.Size(512, 333);
            this.Controls.Add(this.tableSliders);
            this.Controls.Add(this.btnQuit);
            this.Enabled = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "PulseAudio Mixer - Connecting...";
            this.notificationMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip notificationMenu;
        private System.Windows.Forms.ToolStripMenuItem notifyBtnQuit;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.TableLayoutPanel tableSliders;
    }
}

