﻿using DLSBusinessLayer;
using DrivingLicenseSystem.Licenses.control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingLicenseSystem.Licenses
{
    public partial class frmShowPersonLicenseHistory : Form
    {

   
        private int _PersonID = -1; 
        clsPerson _Person;
        public frmShowPersonLicenseHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }

       
        private void frmShowPersonLicenseHistory_Load(object sender, EventArgs e)
        {


            if(_PersonID != -1)
            {
                ctrlPersonCardWithFilter1.LoadPersonInfo(_PersonID);
                ctrlPersonCardWithFilter1.EnableFilter(false);
                ctrlDriverLicense1.LoadLicensesInfoByPersonID(_PersonID);
            }
            else
            {
                ctrlPersonCardWithFilter1.Enabled = true;
                ctrlPersonCardWithFilter1.FilterFocus();
            }
          
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _PersonID = obj;
            if (_PersonID == -1)
            {
                ctrlDriverLicense1.Clear();
            }
            else
                ctrlDriverLicense1.LoadLicensesInfoByPersonID(_PersonID);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
