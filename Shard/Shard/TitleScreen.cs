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
    public partial class TitleScreen : Form
    {
        private LoginForm login;
        public TitleScreen()
        {
            login = new LoginForm(this);
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btn_about.FlatAppearance.BorderSize = 0;
            btn_help.FlatAppearance.BorderSize = 0;
            btn_options.FlatAppearance.BorderSize = 0;
            btn_start.FlatAppearance.BorderSize = 0;
            btn_HelpBack.FlatAppearance.BorderSize = 0;

            btn_about.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            btn_help.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            btn_options.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            btn_start.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            btn_HelpBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
        }

        private void button_MouseEnter(object sender, EventArgs e)
        {
            Font org = ((System.Windows.Forms.Button)sender).Font;
            FontStyle style = org.Style;
            style |= FontStyle.Bold;
            ((System.Windows.Forms.Button)sender).Font = new Font(org, style);
        }
        private void button_MouseLeave(object sender, EventArgs e)
        {
            Font org = ((System.Windows.Forms.Button)sender).Font;
            FontStyle style = org.Style;
            style &= ~FontStyle.Bold;
            ((System.Windows.Forms.Button)sender).Font = new Font(org, style);
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            login.Close();
            login = new LoginForm(this);
            login.Show();
        }

        private void btn_about_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_HelpBack_Click(object sender, EventArgs e)
        {
            HelpPanel.Visible = false;
        }

        private void btn_help_Click(object sender, EventArgs e)
        {
            HelpPanel.Visible = true;
        }

        private void btn_options_Click(object sender, EventArgs e)
        {
            OptionsPanel.Visible = true;
        }

        private void btn_OptionsBack_Click(object sender, EventArgs e)
        {
            OptionsPanel.Visible = false;
        }
    }
}
