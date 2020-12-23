
namespace GoogleMeetLogsNavigator
{
    partial class Download_Form
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.Titolo_about = new System.Windows.Forms.Label();
            this.List_logs_box = new System.Windows.Forms.ListBox();
            this.vScrollBarLogs = new System.Windows.Forms.VScrollBar();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.panel1.Controls.Add(this.vScrollBarLogs);
            this.panel1.Controls.Add(this.List_logs_box);
            this.panel1.Controls.Add(this.Titolo_about);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(306, 453);
            this.panel1.TabIndex = 2;
            // 
            // Titolo_about
            // 
            this.Titolo_about.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Titolo_about.Location = new System.Drawing.Point(3, 15);
            this.Titolo_about.Name = "Titolo_about";
            this.Titolo_about.Size = new System.Drawing.Size(282, 74);
            this.Titolo_about.TabIndex = 2;
            this.Titolo_about.Text = "File Logs";
            this.Titolo_about.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Titolo_about.Click += new System.EventHandler(this.Titolo_about_Click);
            // 
            // List_logs_box
            // 
            this.List_logs_box.BackColor = System.Drawing.Color.Gainsboro;
            this.List_logs_box.FormattingEnabled = true;
            this.List_logs_box.Location = new System.Drawing.Point(12, 92);
            this.List_logs_box.Name = "List_logs_box";
            this.List_logs_box.Size = new System.Drawing.Size(282, 342);
            this.List_logs_box.TabIndex = 3;
            // 
            // vScrollBarLogs
            // 
            this.vScrollBarLogs.Location = new System.Drawing.Point(270, 92);
            this.vScrollBarLogs.Name = "vScrollBarLogs";
            this.vScrollBarLogs.Size = new System.Drawing.Size(24, 342);
            this.vScrollBarLogs.TabIndex = 8;
            // 
            // Download_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 450);
            this.Controls.Add(this.panel1);
            this.Name = "Download_Form";
            this.Text = "Download Logs";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label Titolo_about;
        private System.Windows.Forms.ListBox List_logs_box;
        private System.Windows.Forms.VScrollBar vScrollBarLogs;
    }
}