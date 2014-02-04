using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Shard
{
    public partial class EmployeeDatabaseInterfaceForm : Form
    {
        DatabaseConnection objConnect;
        string conString;

        DataSet ds;
        DataRow dRow;

        int MaxRows;
        int inc = 0;

        public EmployeeDatabaseInterfaceForm()
        {
            InitializeComponent();
        }

        private void EmployeeDatabaseInterfaceForm_Load(object sender, EventArgs e)
        {
            try
            {
                objConnect = new DatabaseConnection();
                conString = Properties.Settings.Default.DatabaseConnectionString;
                objConnect.Connection_string = conString;
                objConnect.Sql = Properties.Settings.Default.SQL_Employee;
                ds = objConnect.GetConnection;
                MaxRows = ds.Tables[0].Rows.Count;

                NavigateRecords();

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void NavigateRecords()
        {
            dRow = ds.Tables[0].Rows[inc];

            firstNameTextBox.Text = dRow.ItemArray.GetValue(1).ToString();
            surnameTextBox.Text = dRow.ItemArray.GetValue(2).ToString();
            jobTitleTextBox.Text = dRow.ItemArray.GetValue(3).ToString();
            departmentTextBox.Text = dRow.ItemArray.GetValue(4).ToString();
        }

        private void btn_Next_Click(object sender, EventArgs e)
        {
            if (inc != MaxRows - 1)
            {
                inc++;
                NavigateRecords();
            }
            else
                MessageBox.Show("No More Valid Rows");
        }

        private void btn_Previous_Click(object sender, EventArgs e)
        {
            if (inc > 0)
            {
                inc--;
                NavigateRecords();
            }
            else
                MessageBox.Show("No Previous Valid Rows");
        }
    }
}
