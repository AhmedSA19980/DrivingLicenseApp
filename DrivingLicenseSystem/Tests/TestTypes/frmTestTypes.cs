using DLSBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DLSBusinessLayer.clsTestTypes;

namespace DrivingLicenseSystem.Tests.TestTypes
{
    public partial class frmTestTypes : Form
    {
        private DataTable _dtAllTestTypes;
        public frmTestTypes()
        {
            InitializeComponent();
        }

        private void frmTestTypes_Load(object sender, EventArgs e)
        {

            _dtAllTestTypes = clsTestTypes.GetAllTestTypes();
            dgvTestTypes.DataSource = _dtAllTestTypes;
            lblRecordsCount.Text = dgvTestTypes.Rows.Count.ToString();

            dgvTestTypes.Columns[0].HeaderText = "ID";
            dgvTestTypes.Columns[0].Width = 120;

            dgvTestTypes.Columns[1].HeaderText = "Title";
            dgvTestTypes.Columns[1].Width = 200;

            dgvTestTypes.Columns[2].HeaderText = "Description";
            dgvTestTypes.Columns[2].Width = 400;

            dgvTestTypes.Columns[3].HeaderText = "Fees";
            dgvTestTypes.Columns[3].Width = 100;
        }
    

     

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            
            enTestType TestTypeID = (clsTestTypes.enTestType)dgvTestTypes.CurrentRow.Cells[0].Value;
            frmEditTestType frm = new frmEditTestType(TestTypeID);
            frm.ShowDialog();
            frmTestTypes_Load(null, null);
        }
    }
}
