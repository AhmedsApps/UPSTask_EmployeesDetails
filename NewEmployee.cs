using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UPSTask_EmployeesDetails
{
    public partial class NewEmployee : Form
    {
        MasterForm parentForm = null;
        public NewEmployee(MasterForm masterForm)
        {
            InitializeComponent();
            FillLists();
            parentForm = masterForm;
        }

       
        private void FillLists()
        {
            drpStatus.DataSource = Enum.GetValues(typeof(Status));
            drpGender.DataSource = Enum.GetValues(typeof(Gender));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text=="")
            {
                MessageBox.Show("Name Can't be empty");
                return;
            }

            if (txtEmail.Text=="")
            {
                MessageBox.Show("Email Can't be empty");
                return;
            }

            EmployeeInfo employeeInfo = new EmployeeInfo();

       var response=     APICalls.AddEmployee(new EmployeeInfo
            {
                email = txtEmail.Text,
                gender = drpGender.SelectedItem.ToString(),
                name = txtName.Text,
                status = drpStatus.SelectedItem.ToString(),
                created_at = DateTime.Now,
                updated_at = DateTime.Now
            });

            MessageBox.Show("Done " + (response? "Successfully" : "Unsuccessfully"));

        }
    }
}
