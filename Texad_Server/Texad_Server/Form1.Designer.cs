namespace Texad_Server
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
            this.startServerButton = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.connectedClientsLabel = new System.Windows.Forms.Label();
            this.listenForClientsButton = new System.Windows.Forms.Button();
            this.clientListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // startServerButton
            // 
            this.startServerButton.Location = new System.Drawing.Point(12, 12);
            this.startServerButton.Name = "startServerButton";
            this.startServerButton.Size = new System.Drawing.Size(76, 23);
            this.startServerButton.TabIndex = 0;
            this.startServerButton.Text = "Start Server";
            this.startServerButton.UseVisualStyleBackColor = true;
            this.startServerButton.Click += new System.EventHandler(this.startServerButton_Clicked);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(326, 13);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(37, 13);
            this.statusLabel.TabIndex = 3;
            this.statusLabel.Text = "Status";
            // 
            // connectedClientsLabel
            // 
            this.connectedClientsLabel.AutoSize = true;
            this.connectedClientsLabel.Location = new System.Drawing.Point(329, 30);
            this.connectedClientsLabel.Name = "connectedClientsLabel";
            this.connectedClientsLabel.Size = new System.Drawing.Size(115, 13);
            this.connectedClientsLabel.TabIndex = 4;
            this.connectedClientsLabel.Text = "connectedClientsLabel";
            // 
            // listenForClientsButton
            // 
            this.listenForClientsButton.Location = new System.Drawing.Point(95, 13);
            this.listenForClientsButton.Name = "listenForClientsButton";
            this.listenForClientsButton.Size = new System.Drawing.Size(109, 23);
            this.listenForClientsButton.TabIndex = 5;
            this.listenForClientsButton.Text = "Listen For Clients";
            this.listenForClientsButton.UseVisualStyleBackColor = true;
            this.listenForClientsButton.Click += new System.EventHandler(this.listenForClientsButtonClicked);
            // 
            // clientListBox
            // 
            this.clientListBox.FormattingEnabled = true;
            this.clientListBox.Location = new System.Drawing.Point(332, 46);
            this.clientListBox.Name = "clientListBox";
            this.clientListBox.Size = new System.Drawing.Size(229, 95);
            this.clientListBox.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 307);
            this.Controls.Add(this.clientListBox);
            this.Controls.Add(this.listenForClientsButton);
            this.Controls.Add(this.connectedClientsLabel);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.startServerButton);
            this.Name = "Form1";
            this.Text = "Texad Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startServerButton;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label connectedClientsLabel;
        private System.Windows.Forms.Button listenForClientsButton;
        private System.Windows.Forms.ListBox clientListBox;
    }
}

