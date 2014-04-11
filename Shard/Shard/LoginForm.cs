using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Xml.Linq;

namespace Shard
{
    public partial class LoginForm : Form
    {
        private XMLDatabase database;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            database = new XMLDatabase("login.xml");
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            if (termsAndConditionsCheckBox.CheckState.Equals(CheckState.Checked))
            {
                string username = usernameTextBox.Text;
                string password = passwordTextBox.Text;
                if (username == null || username.Equals("") || username.Length <= 0)
                    MessageBox.Show("Unable to Register.\nInvalid Username");
                else if (DatasetContains("username", username))
                    MessageBox.Show("Unable to Register.\nUsername already in use");
                else if (password == null || password.Equals("") || password.Length <= 0)
                    MessageBox.Show("Unable to Register.\nInvalid Password");
                else if (username != null && password != null && username.Length > 0 && password.Length > 0 && !DatasetContains("username", username))
                {
                    database.addNodeSequential(loginToNode(username, password));
                    database.save();
                }
            }
            else
                MessageBox.Show("Unable to Register.\nTerms and Conditions must be accepted.");
        }

        #region Dataset Exploration Methods

        private bool DatasetContains(string element, string data)
        {
            foreach (XElement xe in database.getDocument().Root.Elements())
            {
                if (xe.Element(element).Value.Equals(data))
                    return true;
            }
            return false;
        }

        private bool ValidLogin(string username, string password)
        {
            foreach (XElement xe in database.getDocument().Root.Elements())
            {
                if (xe.Element("username").Value.Equals(username) && xe.Element("password").Value.Equals(password))
                    return true;
            }
            return false;
        }

        private XElement loginToNode(string username, string password)
        {
            XElement node =
                new XElement("login",
                    new XElement("username", username),
                    new XElement("password", password)
                    );
            return node;
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
            ShardGame game = new ShardGame(usernameTextBox.Text);
            game.Run();
        }

    }
}
