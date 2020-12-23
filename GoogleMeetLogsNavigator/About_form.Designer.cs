
namespace GoogleMeetLogsNavigator
{
    partial class About_form
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
            this.cloase_about_button = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.panel1.Controls.Add(this.Titolo_about);
            this.panel1.Controls.Add(this.cloase_about_button);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(1, -2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(290, 453);
            this.panel1.TabIndex = 1;
            // 
            // Titolo_about
            // 
            this.Titolo_about.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Titolo_about.Location = new System.Drawing.Point(3, 15);
            this.Titolo_about.Name = "Titolo_about";
            this.Titolo_about.Size = new System.Drawing.Size(282, 74);
            this.Titolo_about.TabIndex = 2;
            this.Titolo_about.Text = "Information";
            this.Titolo_about.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cloase_about_button
            // 
            this.cloase_about_button.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cloase_about_button.Location = new System.Drawing.Point(160, 336);
            this.cloase_about_button.Name = "cloase_about_button";
            this.cloase_about_button.Size = new System.Drawing.Size(125, 31);
            this.cloase_about_button.TabIndex = 1;
            this.cloase_about_button.Text = "Close";
            this.cloase_about_button.UseVisualStyleBackColor = false;
            this.cloase_about_button.Click += new System.EventHandler(this.cloase_about_button_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gainsboro;
            this.panel2.Location = new System.Drawing.Point(3, 92);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(282, 238);
            this.panel2.TabIndex = 0;
            // 
            // About_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 373);
            this.Controls.Add(this.panel1);
            this.Name = "About_form";
            this.Text = "About Us";
            this.Load += new System.EventHandler(this.About_form_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label Titolo_about;
        private System.Windows.Forms.Button cloase_about_button;
        private System.Windows.Forms.Panel panel2;
    }
}