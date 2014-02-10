using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Shard
{
    public partial class LoginForm : Form
    {
        DatabaseConnection databaseConnect;
        DataSet dataset;
        DataRow dataRow;
        int numRows;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            try
            {
                databaseConnect = new DatabaseConnection();
                databaseConnect.Connection_string = EmployeeDatabase.Properties.Settings.Default.DatabaseConnectionString;
                databaseConnect.Sql = EmployeeDatabase.Properties.Settings.Default.SQL_LoginInformation;
                dataset = databaseConnect.GetConnection;
                numRows = dataset.Tables[0].Rows.Count;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            if (termsAndConditionsCheckBox.CheckState.Equals(CheckState.Checked))
            {
                string username = usernameTextBox.Text;
                string password = passwordTextBox.Text;
                if(username == null || username.Equals("") || username.Length <= 0)
                    MessageBox.Show("Unable to Register.\nInvalid Username");
                else if(DatasetColumnContains(username, 0))
                    MessageBox.Show("Unable to Register.\nUsername already in use");
                else if (password == null || password.Equals("") || password.Length <= 0)
                    MessageBox.Show("Unable to Register.\nInvalid Password");
                else if (username != null && password != null && username.Length > 0 && password.Length > 0 && !DatasetColumnContains(username, 0))
                {
                    DataRow row = dataset.Tables[0].NewRow();
                    row[0] = username;
                    row[1] = password;
                    dataset.Tables[0].Rows.Add(row);
                    try
                    {
                        databaseConnect.UpdateDatabase(dataset);
                        numRows++;
                        MessageBox.Show("Account Registration Successful");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message);
                    }
                }
            }
            else
                MessageBox.Show("Unable to Register.\nTerms and Conditions must be accepted.");
        }

        #region Dataset Exploration Methods

        private bool DatasetContains(string query)
        {
            for (int i = 0; i < numRows; i++)
            {
                dataRow = dataset.Tables[0].Rows[i];
                for (int q = 0; q < dataRow.ItemArray.Length; q++)
                {
                    if (dataRow.ItemArray.GetValue(q).ToString().Equals(query))
                        return true;
                }
            }
            return false;
        }

        private bool DatasetColumnContains(string query, int column)
        {
            for (int i = 0; i < numRows; i++)
            {
                dataRow = dataset.Tables[0].Rows[i];
                if (column >= 0 && column < dataRow.ItemArray.Length)
                {
                    if (dataRow.ItemArray.GetValue(column).ToString().Equals(query))
                        return true;
                }
            }
            return false;
        }

        private bool DatasetRowContains(string query, int row)
        {
            if (row > -1 && row < numRows)
            {
                dataRow = dataset.Tables[0].Rows[row];
                for (int c = 0; c < dataRow.ItemArray.Length; c++)
                {
                    if (dataRow.ItemArray.GetValue(c).ToString().Equals(query))
                        return true;
                }
            }
            return false;
        }

        private bool ValidLogin(string username, string password)
        {
            for (int r = 0; r < numRows; r++)
            {
                dataRow = dataset.Tables[0].Rows[r];
                if (dataRow.ItemArray.GetValue(0).ToString().Equals(username) && dataRow.ItemArray.GetValue(1).ToString().Equals(password))
                    return true;
            }
            return false;
        }

        #endregion

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (ValidLogin(usernameTextBox.Text, passwordTextBox.Text))
            {
                Thread gameThread = new Thread(StartGame);
                gameThread.Start();
                this.Close();
            }
            else
                MessageBox.Show("Invalid Login Information");
        }

        private void StartGame()
        {
            ShardGame game = new ShardGame();
            game.Run();
        }
       
    }
}
