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

namespace DrivingLicenseSystem.Applications
{
    public partial class frmApplicationTypes : Form
    {


        private DataTable _dtAllTestTypes; 
        public frmApplicationTypes()
        {
            InitializeComponent();
        }

   
 
        private void frmApplicationTypes_Load(object sender, EventArgs e)
        {


            _dtAllTestTypes = clsApplicationType.GetAllApplicationTypes();
            dgvApplicationTypes.DataSource = _dtAllTestTypes;

            lblRecordsCount.Text = dgvApplicationTypes.Rows.Count.ToString();

            if (dgvApplicationTypes.Rows.Count > 0)
            {
                dgvApplicationTypes.Columns[0].HeaderText = "ID";
                dgvApplicationTypes.Columns[0].Width = 110;

                dgvApplicationTypes.Columns[1].HeaderText = "Title";
                dgvApplicationTypes.Columns[1].Width = 400;

                dgvApplicationTypes.Columns[2].HeaderText = "Fees";
                dgvApplicationTypes.Columns[2].Width = 100;


            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int AppTypeID = (int)dgvApplicationTypes.CurrentRow.Cells[0].Value;
            frmEditApplicationType frm = new frmEditApplicationType(AppTypeID);
            frm.ShowDialog();
            frmApplicationTypes_Load(null , null ); 
        }
    }
}
