namespace FilmKolcsonzo
{
    partial class BejelentkezoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BejelentkezoForm));
            this.TextBoxEmail = new System.Windows.Forms.TextBox();
            this.TextBoxJelszo = new System.Windows.Forms.TextBox();
            this.EmailLabel = new System.Windows.Forms.Label();
            this.JelszoLabel = new System.Windows.Forms.Label();
            this.BejelentkezesButton = new System.Windows.Forms.Button();
            this.RegisztracioButton = new System.Windows.Forms.Button();
            this.MegsemButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TextBoxEmail
            // 
            this.TextBoxEmail.Location = new System.Drawing.Point(128, 117);
            this.TextBoxEmail.Name = "TextBoxEmail";
            this.TextBoxEmail.Size = new System.Drawing.Size(144, 20);
            this.TextBoxEmail.TabIndex = 0;
            // 
            // TextBoxJelszo
            // 
            this.TextBoxJelszo.Location = new System.Drawing.Point(128, 143);
            this.TextBoxJelszo.Name = "TextBoxJelszo";
            this.TextBoxJelszo.Size = new System.Drawing.Size(144, 20);
            this.TextBoxJelszo.TabIndex = 1;
            // 
            // EmailLabel
            // 
            this.EmailLabel.AutoSize = true;
            this.EmailLabel.Location = new System.Drawing.Point(15, 120);
            this.EmailLabel.Name = "EmailLabel";
            this.EmailLabel.Size = new System.Drawing.Size(65, 13);
            this.EmailLabel.TabIndex = 2;
            this.EmailLabel.Text = "E-mail címe:";
            // 
            // JelszoLabel
            // 
            this.JelszoLabel.AutoSize = true;
            this.JelszoLabel.Location = new System.Drawing.Point(29, 146);
            this.JelszoLabel.Name = "JelszoLabel";
            this.JelszoLabel.Size = new System.Drawing.Size(51, 13);
            this.JelszoLabel.TabIndex = 3;
            this.JelszoLabel.Text = "Jelszava:";
            // 
            // BejelentkezesButton
            // 
            this.BejelentkezesButton.Location = new System.Drawing.Point(12, 227);
            this.BejelentkezesButton.Name = "BejelentkezesButton";
            this.BejelentkezesButton.Size = new System.Drawing.Size(85, 23);
            this.BejelentkezesButton.TabIndex = 4;
            this.BejelentkezesButton.Text = "Bejelentkezés";
            this.BejelentkezesButton.UseVisualStyleBackColor = true;
            this.BejelentkezesButton.Click += new System.EventHandler(this.BejelentkezesButton_Click);
            // 
            // RegisztracioButton
            // 
            this.RegisztracioButton.Location = new System.Drawing.Point(103, 227);
            this.RegisztracioButton.Name = "RegisztracioButton";
            this.RegisztracioButton.Size = new System.Drawing.Size(89, 23);
            this.RegisztracioButton.TabIndex = 5;
            this.RegisztracioButton.Text = "Regisztráció";
            this.RegisztracioButton.UseVisualStyleBackColor = true;
            this.RegisztracioButton.Click += new System.EventHandler(this.RegisztracioButton_Click);
            // 
            // MegsemButton
            // 
            this.MegsemButton.Location = new System.Drawing.Point(198, 227);
            this.MegsemButton.Name = "MegsemButton";
            this.MegsemButton.Size = new System.Drawing.Size(75, 23);
            this.MegsemButton.TabIndex = 6;
            this.MegsemButton.Text = "Mégse";
            this.MegsemButton.UseVisualStyleBackColor = true;
            this.MegsemButton.Click += new System.EventHandler(this.MegsemButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(265, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "A belépéshez add meg az e-mail címed és a jelszavad.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(269, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Ha még nem regisztráltál, akkor kattints a regisztrációra.";
            // 
            // BejelentkezoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MegsemButton);
            this.Controls.Add(this.RegisztracioButton);
            this.Controls.Add(this.BejelentkezesButton);
            this.Controls.Add(this.JelszoLabel);
            this.Controls.Add(this.EmailLabel);
            this.Controls.Add(this.TextBoxJelszo);
            this.Controls.Add(this.TextBoxEmail);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BejelentkezoForm";
            this.Text = "Bejelentkezes";
            this.Load += new System.EventHandler(this.BejelentkezoForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextBoxEmail;
        private System.Windows.Forms.TextBox TextBoxJelszo;
        private System.Windows.Forms.Label EmailLabel;
        private System.Windows.Forms.Label JelszoLabel;
        private System.Windows.Forms.Button BejelentkezesButton;
        private System.Windows.Forms.Button RegisztracioButton;
        private System.Windows.Forms.Button MegsemButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}