namespace DrivingLicenseSystem.DrivingLicense.NewLocalDrivingLicense
{
    partial class frmAddUpdateDrivingLicense
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.tcDrivingLicense = new System.Windows.Forms.TabControl();
            this.tbPersonal = new System.Windows.Forms.TabPage();
            this.btnClose = new System.Windows.Forms.Button();
            this.ctrlPersonCardWithFilter1 = new DrivingLicenseSystem.Person.Controls.ctrlPersonCardWithFilter();
            this.tpAppInfo = new System.Windows.Forms.TabPage();
            this.lblCreatedUser = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblClassFees = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cbLicenseClasses = new System.Windows.Forms.ComboBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.lblAppDate = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblAppID = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tcDrivingLicense.SuspendLayout();
            this.tbPersonal.SuspendLayout();
            this.tpAppInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Firebrick;
            this.lblTitle.Location = new System.Drawing.Point(328, 21);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(433, 31);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "New Driving License Application";
            // 
            // tcDrivingLicense
            // 
            this.tcDrivingLicense.Controls.Add(this.tbPersonal);
            this.tcDrivingLicense.Controls.Add(this.tpAppInfo);
            this.tcDrivingLicense.Location = new System.Drawing.Point(41, 55);
            this.tcDrivingLicense.Name = "tcDrivingLicense";
            this.tcDrivingLicense.SelectedIndex = 0;
            this.tcDrivingLicense.Size = new System.Drawing.Size(915, 593);
            this.tcDrivingLicense.TabIndex = 4;
            // 
            // tbPersonal
            // 
            this.tbPersonal.Controls.Add(this.btnClose);
            this.tbPersonal.Controls.Add(this.ctrlPersonCardWithFilter1);
            this.tbPersonal.Location = new System.Drawing.Point(4, 22);
            this.tbPersonal.Name = "tbPersonal";
            this.tbPersonal.Padding = new System.Windows.Forms.Padding(3);
            this.tbPersonal.Size = new System.Drawing.Size(907, 567);
            this.tbPersonal.TabIndex = 0;
            this.tbPersonal.Text = "Personal Info";
            this.tbPersonal.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::DrivingLicenseSystem.Properties.Resources.Next_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(769, 490);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(116, 39);
            this.btnClose.TabIndex = 133;
            this.btnClose.Text = "Next";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // ctrlPersonCardWithFilter1
            // 
            this.ctrlPersonCardWithFilter1.Location = new System.Drawing.Point(12, 13);
            this.ctrlPersonCardWithFilter1.Name = "ctrlPersonCardWithFilter1";
            this.ctrlPersonCardWithFilter1.Size = new System.Drawing.Size(873, 471);
            this.ctrlPersonCardWithFilter1.TabIndex = 0;
            this.ctrlPersonCardWithFilter1.DataBack += new DrivingLicenseSystem.Person.Controls.ctrlPersonCardWithFilter.DataBackEventHandler(this.ctrlPersonCardWithFilter1_DataBack);
            this.ctrlPersonCardWithFilter1.OnPersonSelected += new System.Action<int>(this.ctrlPersonCardWithFilter1_OnPersonSelected);
            // 
            // tpAppInfo
            // 
            this.tpAppInfo.Controls.Add(this.lblCreatedUser);
            this.tpAppInfo.Controls.Add(this.label11);
            this.tpAppInfo.Controls.Add(this.lblClassFees);
            this.tpAppInfo.Controls.Add(this.label8);
            this.tpAppInfo.Controls.Add(this.cbLicenseClasses);
            this.tpAppInfo.Controls.Add(this.pictureBox5);
            this.tpAppInfo.Controls.Add(this.label9);
            this.tpAppInfo.Controls.Add(this.pictureBox4);
            this.tpAppInfo.Controls.Add(this.lblAppDate);
            this.tpAppInfo.Controls.Add(this.label7);
            this.tpAppInfo.Controls.Add(this.pictureBox2);
            this.tpAppInfo.Controls.Add(this.lblAppID);
            this.tpAppInfo.Controls.Add(this.label4);
            this.tpAppInfo.Controls.Add(this.pictureBox8);
            this.tpAppInfo.Controls.Add(this.pictureBox3);
            this.tpAppInfo.Location = new System.Drawing.Point(4, 22);
            this.tpAppInfo.Name = "tpAppInfo";
            this.tpAppInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpAppInfo.Size = new System.Drawing.Size(907, 567);
            this.tpAppInfo.TabIndex = 1;
            this.tpAppInfo.Text = "Application Info";
            this.tpAppInfo.UseVisualStyleBackColor = true;
            // 
            // lblCreatedUser
            // 
            this.lblCreatedUser.AutoSize = true;
            this.lblCreatedUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreatedUser.Location = new System.Drawing.Point(302, 308);
            this.lblCreatedUser.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCreatedUser.Name = "lblCreatedUser";
            this.lblCreatedUser.Size = new System.Drawing.Size(39, 20);
            this.lblCreatedUser.TabIndex = 193;
            this.lblCreatedUser.Text = "???";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(130, 302);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 20);
            this.label11.TabIndex = 192;
            this.label11.Text = "Create By:";
            // 
            // lblClassFees
            // 
            this.lblClassFees.AutoSize = true;
            this.lblClassFees.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClassFees.Location = new System.Drawing.Point(302, 254);
            this.lblClassFees.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClassFees.Name = "lblClassFees";
            this.lblClassFees.Size = new System.Drawing.Size(39, 20);
            this.lblClassFees.TabIndex = 191;
            this.lblClassFees.Text = "???";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(75, 254);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(148, 20);
            this.label8.TabIndex = 190;
            this.label8.Text = "Application Fees:";
            // 
            // cbLicenseClasses
            // 
            this.cbLicenseClasses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLicenseClasses.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLicenseClasses.FormattingEnabled = true;
            this.cbLicenseClasses.Location = new System.Drawing.Point(306, 200);
            this.cbLicenseClasses.Name = "cbLicenseClasses";
            this.cbLicenseClasses.Size = new System.Drawing.Size(219, 28);
            this.cbLicenseClasses.TabIndex = 189;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::DrivingLicenseSystem.Properties.Resources.License_Type_32;
            this.pictureBox5.Location = new System.Drawing.Point(251, 202);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(31, 26);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 188;
            this.pictureBox5.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(98, 202);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(125, 20);
            this.label9.TabIndex = 186;
            this.label9.Text = "License Class:";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::DrivingLicenseSystem.Properties.Resources.Calendar_321;
            this.pictureBox4.Location = new System.Drawing.Point(251, 159);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(31, 26);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 185;
            this.pictureBox4.TabStop = false;
            // 
            // lblAppDate
            // 
            this.lblAppDate.AutoSize = true;
            this.lblAppDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppDate.Location = new System.Drawing.Point(302, 159);
            this.lblAppDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAppDate.Name = "lblAppDate";
            this.lblAppDate.Size = new System.Drawing.Size(39, 20);
            this.lblAppDate.TabIndex = 184;
            this.lblAppDate.Text = "???";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(79, 159);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(152, 20);
            this.label7.TabIndex = 183;
            this.label7.Text = "Application Date: ";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DrivingLicenseSystem.Properties.Resources.Number_321;
            this.pictureBox2.Location = new System.Drawing.Point(251, 117);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(31, 26);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 182;
            this.pictureBox2.TabStop = false;
            // 
            // lblAppID
            // 
            this.lblAppID.AutoSize = true;
            this.lblAppID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppID.Location = new System.Drawing.Point(302, 117);
            this.lblAppID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAppID.Name = "lblAppID";
            this.lblAppID.Size = new System.Drawing.Size(39, 20);
            this.lblAppID.TabIndex = 181;
            this.lblAppID.Text = "???";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(66, 117);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(165, 20);
            this.label4.TabIndex = 180;
            this.label4.Text = "D.L Application ID: ";
            // 
            // pictureBox8
            // 
            this.pictureBox8.Image = global::DrivingLicenseSystem.Properties.Resources.money_32;
            this.pictureBox8.Location = new System.Drawing.Point(251, 254);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(31, 26);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox8.TabIndex = 175;
            this.pictureBox8.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::DrivingLicenseSystem.Properties.Resources.User_32__21;
            this.pictureBox3.Location = new System.Drawing.Point(251, 302);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(31, 26);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 174;
            this.pictureBox3.TabStop = false;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::DrivingLicenseSystem.Properties.Resources.Close_32;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(703, 654);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 39);
            this.button1.TabIndex = 135;
            this.button1.Text = "Close\r\n";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::DrivingLicenseSystem.Properties.Resources.Save_32;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(840, 654);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(116, 39);
            this.btnSave.TabIndex = 134;
            this.btnSave.Text = "Save\r\n";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmAddUpdateDrivingLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 717);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tcDrivingLicense);
            this.Controls.Add(this.lblTitle);
            this.Name = "frmAddUpdateDrivingLicense";
            this.Text = "frmNewDrivingLicense";
            this.Load += new System.EventHandler(this.frmAddUpdateDrivingLicense_Load);
            this.tcDrivingLicense.ResumeLayout(false);
            this.tbPersonal.ResumeLayout(false);
            this.tpAppInfo.ResumeLayout(false);
            this.tpAppInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TabControl tcDrivingLicense;
        private System.Windows.Forms.TabPage tbPersonal;
        private System.Windows.Forms.TabPage tpAppInfo;
        private Person.Controls.ctrlPersonCardWithFilter ctrlPersonCardWithFilter1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label lblAppDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblAppID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbLicenseClasses;
        private System.Windows.Forms.Label lblCreatedUser;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblClassFees;
        private System.Windows.Forms.Label label8;
    }
}