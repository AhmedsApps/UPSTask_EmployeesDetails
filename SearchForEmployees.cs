using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UPSTask_EmployeesDetails
{
    public partial class SearchForEmployee : Form
    {
        public Root CurrentList;
        public int CurrentPage = 0;

        MasterForm parentForm = null;
        public SearchForEmployee(MasterForm masterForm)
        {
            InitializeComponent();
            parentForm = masterForm;
        }

        //public SearchForEmployee()
        //{
        //    InitializeComponent();
        //}

        void DoSearch(int  page)

        {
            if (chkShowAll.Checked)
            {
                CurrentList = APICalls.GetAllEmployees(page);
            }
            else
            {
                CurrentList = APICalls.SearchForEmployees(txtNameFilter.Text,txtEmailFilter.Text, page);
            }

                grdEmployees.AutoGenerateColumns = false;

                grdEmployees.DataSource = CurrentList.data;
                lblCurrentPage.Text = CurrentList.meta.pagination.page.ToString() + " Of "
                    + CurrentList.meta.pagination.pages.ToString();
                CurrentPage = CurrentList.meta.pagination.page;

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            CurrentPage = 1;
            DoSearch(0);
        }

        private void chkShowAll_CheckedChanged(object sender, EventArgs e)
        {
            txtEmailFilter.Enabled = txtNameFilter.Enabled = !chkShowAll.Checked;
        }

        private void btnPageAfter_Click(object sender, EventArgs e)
        {
            CurrentPage++;

            DoSearch(CurrentPage);
        }

        private void btnDeleted_Click(object sender, EventArgs e)
        {
            bool response=false;
            if (grdEmployees.Rows.Count>0)
            {
                foreach (DataGridViewRow selectedemployee in grdEmployees.Rows)
                {
                    bool IsSelected = false;
                    if (bool.TryParse(selectedemployee.Cells["ColumnDelete"].FormattedValue.ToString(), out IsSelected))
                    {
                        if (IsSelected)
                         response=   APICalls.DeleteEmployee(int.Parse(selectedemployee.Cells[0].Value.ToString()));
                    }
                       
                }
            }

            MessageBox.Show("Done " + (response ? "Successfully" : "Unsuccessfully"));
        }
    }
}
