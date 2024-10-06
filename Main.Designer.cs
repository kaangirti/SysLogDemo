namespace SysLogDemo
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label5 = new Label();
            serializerLabel = new Label();
            label3 = new Label();
            txtMessageTitle = new TextBox();
            txtAppName = new TextBox();
            txtPort = new TextBox();
            label2 = new Label();
            label1 = new Label();
            txtHost = new TextBox();
            button1 = new Button();
            label6 = new Label();
            txtStatus = new ComboBox();
            txtSocketSerializer = new ComboBox();
            txtConsole = new TextBox();
            txtProtocol = new ComboBox();
            label7 = new Label();
            txtFacility = new ComboBox();
            label4 = new Label();
            txtMessageDescription = new TextBox();
            txtMessageJSON = new TextBox();
            label8 = new Label();
            label9 = new Label();
            SuspendLayout();
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 218);
            label5.Name = "label5";
            label5.Size = new Size(29, 15);
            label5.TabIndex = 33;
            label5.Text = "Title";
            // 
            // serializerLabel
            // 
            serializerLabel.AutoSize = true;
            serializerLabel.Location = new Point(11, 102);
            serializerLabel.Name = "serializerLabel";
            serializerLabel.Size = new Size(53, 15);
            serializerLabel.TabIndex = 32;
            serializerLabel.Text = "Serializer";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(11, 73);
            label3.Name = "label3";
            label3.Size = new Size(64, 15);
            label3.TabIndex = 31;
            label3.Text = "App Name";
            // 
            // txtMessageTitle
            // 
            txtMessageTitle.Location = new Point(105, 215);
            txtMessageTitle.Multiline = true;
            txtMessageTitle.Name = "txtMessageTitle";
            txtMessageTitle.ScrollBars = ScrollBars.Both;
            txtMessageTitle.Size = new Size(212, 52);
            txtMessageTitle.TabIndex = 30;
            // 
            // txtAppName
            // 
            txtAppName.Location = new Point(104, 70);
            txtAppName.Name = "txtAppName";
            txtAppName.Size = new Size(212, 23);
            txtAppName.TabIndex = 28;
            txtAppName.Text = "WEBDEMO";
            // 
            // txtPort
            // 
            txtPort.Location = new Point(104, 41);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(212, 23);
            txtPort.TabIndex = 27;
            txtPort.Text = "514";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(11, 44);
            label2.Name = "label2";
            label2.Size = new Size(29, 15);
            label2.TabIndex = 26;
            label2.Text = "Port";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 15);
            label1.Name = "label1";
            label1.Size = new Size(17, 15);
            label1.TabIndex = 25;
            label1.Text = "IP";
            // 
            // txtHost
            // 
            txtHost.Location = new Point(104, 12);
            txtHost.Name = "txtHost";
            txtHost.Size = new Size(212, 23);
            txtHost.TabIndex = 24;
            txtHost.Text = "127.0.0.1";
            // 
            // button1
            // 
            button1.Location = new Point(221, 389);
            button1.Name = "button1";
            button1.Size = new Size(95, 50);
            button1.TabIndex = 23;
            button1.Text = "YEAH!";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 189);
            label6.Name = "label6";
            label6.Size = new Size(39, 15);
            label6.TabIndex = 34;
            label6.Text = "Status";
            // 
            // txtStatus
            // 
            txtStatus.FormattingEnabled = true;
            txtStatus.Items.AddRange(new object[] { "INFO", "WARNING", "ERROR" });
            txtStatus.Location = new Point(104, 186);
            txtStatus.Name = "txtStatus";
            txtStatus.Size = new Size(212, 23);
            txtStatus.TabIndex = 35;
            txtStatus.Text = "INFO";
            // 
            // txtSocketSerializer
            // 
            txtSocketSerializer.FormattingEnabled = true;
            txtSocketSerializer.Items.AddRange(new object[] { "RFC3164", "RFC5424", "CUSTOM" });
            txtSocketSerializer.Location = new Point(105, 99);
            txtSocketSerializer.Name = "txtSocketSerializer";
            txtSocketSerializer.Size = new Size(211, 23);
            txtSocketSerializer.TabIndex = 36;
            txtSocketSerializer.Text = "RFC3164";
            txtSocketSerializer.SelectedIndexChanged += ddlSerializerChanged;
            // 
            // txtConsole
            // 
            txtConsole.BackColor = Color.Black;
            txtConsole.ForeColor = Color.White;
            txtConsole.Location = new Point(322, 11);
            txtConsole.Multiline = true;
            txtConsole.Name = "txtConsole";
            txtConsole.ScrollBars = ScrollBars.Both;
            txtConsole.Size = new Size(472, 428);
            txtConsole.TabIndex = 37;
            // 
            // txtProtocol
            // 
            txtProtocol.FormattingEnabled = true;
            txtProtocol.Items.AddRange(new object[] { "TCP", "UDP" });
            txtProtocol.Location = new Point(105, 157);
            txtProtocol.Name = "txtProtocol";
            txtProtocol.Size = new Size(211, 23);
            txtProtocol.TabIndex = 39;
            txtProtocol.Text = "TCP";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(11, 160);
            label7.Name = "label7";
            label7.Size = new Size(52, 15);
            label7.TabIndex = 38;
            label7.Text = "Protocol";
            // 
            // txtFacility
            // 
            txtFacility.FormattingEnabled = true;
            txtFacility.Items.AddRange(new object[] { "KernelMessages", "UserLevelMessages", "MailSystem", "SystemDaemons", "SecurityOrAuthorizationMessages1", "InternalMessages", "LinePrinterSubsystem", "NetworkNewsSubsystem", "UUCPSubsystem", "ClockDaemon1", "SecurityOrAuthorizationMessages2", "FTPDaemon", "NTPSubsystem", "LogAudit", "LogAlert", "ClockDaemon2", "LocalUse0", "LocalUse1", "LocalUse2", "LocalUse3", "LocalUse4", "LocalUse5", "LocalUse6", "LocalUse7" });
            txtFacility.Location = new Point(105, 128);
            txtFacility.Name = "txtFacility";
            txtFacility.Size = new Size(211, 23);
            txtFacility.TabIndex = 41;
            txtFacility.Text = "UserLevelMessages";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(11, 131);
            label4.Name = "label4";
            label4.Size = new Size(44, 15);
            label4.TabIndex = 40;
            label4.Text = "Facility";
            // 
            // txtMessageDescription
            // 
            txtMessageDescription.Location = new Point(105, 273);
            txtMessageDescription.Multiline = true;
            txtMessageDescription.Name = "txtMessageDescription";
            txtMessageDescription.ScrollBars = ScrollBars.Both;
            txtMessageDescription.Size = new Size(212, 52);
            txtMessageDescription.TabIndex = 42;
            // 
            // txtMessageJSON
            // 
            txtMessageJSON.Location = new Point(105, 331);
            txtMessageJSON.Multiline = true;
            txtMessageJSON.Name = "txtMessageJSON";
            txtMessageJSON.ScrollBars = ScrollBars.Both;
            txtMessageJSON.Size = new Size(212, 52);
            txtMessageJSON.TabIndex = 43;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(12, 276);
            label8.Name = "label8";
            label8.Size = new Size(67, 15);
            label8.TabIndex = 44;
            label8.Text = "Description";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(12, 334);
            label9.Name = "label9";
            label9.Size = new Size(91, 15);
            label9.TabIndex = 45;
            label9.Text = "JSON Properties";
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(806, 447);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(txtMessageJSON);
            Controls.Add(txtMessageDescription);
            Controls.Add(txtFacility);
            Controls.Add(label4);
            Controls.Add(txtProtocol);
            Controls.Add(label7);
            Controls.Add(txtConsole);
            Controls.Add(txtSocketSerializer);
            Controls.Add(txtStatus);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(serializerLabel);
            Controls.Add(label3);
            Controls.Add(txtMessageTitle);
            Controls.Add(txtAppName);
            Controls.Add(txtPort);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtHost);
            Controls.Add(button1);
            MaximizeBox = false;
            Name = "Main";
            ShowIcon = false;
            Text = "SysLog Demo";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label5;
        private Label serializerLabel;
        private Label label3;
        private TextBox txtMessageTitle;
        private TextBox txtAppName;
        private TextBox txtPort;
        private Label label2;
        private Label label1;
        private TextBox txtHost;
        private Button button1;
        private Label label6;
        private ComboBox txtStatus;
        private ComboBox txtSocketSerializer;
        private TextBox txtConsole;
        private ComboBox txtProtocol;
        private Label label7;
        private ComboBox txtFacility;
        private Label label4;
        private TextBox txtMessageDescription;
        private TextBox txtMessageJSON;
        private Label label8;
        private Label label9;
    }
}