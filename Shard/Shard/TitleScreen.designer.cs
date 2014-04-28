namespace Shard
{
    partial class TitleScreen
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
            this.MainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_help = new System.Windows.Forms.Button();
            this.btn_options = new System.Windows.Forms.Button();
            this.btn_about = new System.Windows.Forms.Button();
            this.HelpPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_HelpInfo = new System.Windows.Forms.Label();
            this.btn_HelpBack = new System.Windows.Forms.Button();
            this.OptionsPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btn_OptionsBack = new System.Windows.Forms.Button();
            this.MainPanel.SuspendLayout();
            this.HelpPanel.SuspendLayout();
            this.OptionsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.BackColor = System.Drawing.Color.Transparent;
            this.MainPanel.BackgroundImage = global::Shard.Resources.shard;
            this.MainPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MainPanel.ColumnCount = 4;
            this.MainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.MainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.MainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.MainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.MainPanel.Controls.Add(this.btn_start, 1, 1);
            this.MainPanel.Controls.Add(this.btn_help, 2, 1);
            this.MainPanel.Controls.Add(this.btn_options, 1, 2);
            this.MainPanel.Controls.Add(this.btn_about, 2, 2);
            this.MainPanel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.MainPanel.Location = new System.Drawing.Point(-1, -2);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.RowCount = 3;
            this.MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 64.07186F));
            this.MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.92814F));
            this.MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 137F));
            this.MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.MainPanel.Size = new System.Drawing.Size(967, 477);
            this.MainPanel.TabIndex = 0;
            // 
            // btn_start
            // 
            this.btn_start.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_start.BackColor = System.Drawing.Color.Transparent;
            this.btn_start.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_start.Font = new System.Drawing.Font("Copperplate Gothic Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_start.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_start.Location = new System.Drawing.Point(215, 251);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(148, 54);
            this.btn_start.TabIndex = 1;
            this.btn_start.Text = "Start";
            this.btn_start.UseVisualStyleBackColor = false;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            this.btn_start.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btn_start.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // btn_help
            // 
            this.btn_help.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_help.BackColor = System.Drawing.Color.Transparent;
            this.btn_help.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_help.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_help.Font = new System.Drawing.Font("Copperplate Gothic Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_help.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_help.Location = new System.Drawing.Point(601, 251);
            this.btn_help.Name = "btn_help";
            this.btn_help.Size = new System.Drawing.Size(148, 54);
            this.btn_help.TabIndex = 2;
            this.btn_help.Text = "Help";
            this.btn_help.UseVisualStyleBackColor = false;
            this.btn_help.Click += new System.EventHandler(this.btn_help_Click);
            this.btn_help.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btn_help.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // btn_options
            // 
            this.btn_options.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_options.BackColor = System.Drawing.Color.Transparent;
            this.btn_options.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_options.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_options.Font = new System.Drawing.Font("Copperplate Gothic Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_options.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_options.Location = new System.Drawing.Point(215, 381);
            this.btn_options.Name = "btn_options";
            this.btn_options.Size = new System.Drawing.Size(148, 54);
            this.btn_options.TabIndex = 3;
            this.btn_options.Text = "Options";
            this.btn_options.UseVisualStyleBackColor = false;
            this.btn_options.Click += new System.EventHandler(this.btn_options_Click);
            this.btn_options.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btn_options.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // btn_about
            // 
            this.btn_about.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_about.BackColor = System.Drawing.Color.Transparent;
            this.btn_about.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_about.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_about.Font = new System.Drawing.Font("Copperplate Gothic Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_about.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_about.Location = new System.Drawing.Point(601, 381);
            this.btn_about.Name = "btn_about";
            this.btn_about.Size = new System.Drawing.Size(148, 54);
            this.btn_about.TabIndex = 4;
            this.btn_about.Text = "Quit";
            this.btn_about.UseVisualStyleBackColor = false;
            this.btn_about.Click += new System.EventHandler(this.btn_about_Click);
            this.btn_about.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btn_about.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // HelpPanel
            // 
            this.HelpPanel.BackColor = System.Drawing.Color.Transparent;
            this.HelpPanel.BackgroundImage = global::Shard.Resources.shard;
            this.HelpPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.HelpPanel.ColumnCount = 4;
            this.HelpPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.HelpPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.HelpPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.HelpPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.HelpPanel.Controls.Add(this.lbl_HelpInfo, 1, 0);
            this.HelpPanel.Controls.Add(this.btn_HelpBack, 2, 2);
            this.HelpPanel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.HelpPanel.Location = new System.Drawing.Point(-1, -2);
            this.HelpPanel.Name = "HelpPanel";
            this.HelpPanel.RowCount = 3;
            this.HelpPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 64.07186F));
            this.HelpPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.92814F));
            this.HelpPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 137F));
            this.HelpPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.HelpPanel.Size = new System.Drawing.Size(967, 477);
            this.HelpPanel.TabIndex = 1;
            this.HelpPanel.Visible = false;
            // 
            // lbl_HelpInfo
            // 
            this.lbl_HelpInfo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl_HelpInfo.AutoSize = true;
            this.HelpPanel.SetColumnSpan(this.lbl_HelpInfo, 2);
            this.lbl_HelpInfo.Location = new System.Drawing.Point(186, 163);
            this.lbl_HelpInfo.Name = "lbl_HelpInfo";
            this.lbl_HelpInfo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.HelpPanel.SetRowSpan(this.lbl_HelpInfo, 2);
            this.lbl_HelpInfo.Size = new System.Drawing.Size(591, 13);
            this.lbl_HelpInfo.TabIndex = 0;
            this.lbl_HelpInfo.Text = "Assuming we actually get here, words will eventually be here about the game that " +
                "should provide some benefit for the player.";
            // 
            // btn_HelpBack
            // 
            this.btn_HelpBack.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_HelpBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_HelpBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_HelpBack.Location = new System.Drawing.Point(619, 389);
            this.btn_HelpBack.Name = "btn_HelpBack";
            this.btn_HelpBack.Size = new System.Drawing.Size(111, 38);
            this.btn_HelpBack.TabIndex = 1;
            this.btn_HelpBack.Text = "Back";
            this.btn_HelpBack.UseVisualStyleBackColor = true;
            this.btn_HelpBack.Click += new System.EventHandler(this.btn_HelpBack_Click);
            // 
            // OptionsPanel
            // 
            this.OptionsPanel.BackColor = System.Drawing.Color.Transparent;
            this.OptionsPanel.BackgroundImage = global::Shard.Resources.shard;
            this.OptionsPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.OptionsPanel.ColumnCount = 4;
            this.OptionsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.OptionsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.OptionsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.OptionsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.OptionsPanel.Controls.Add(this.btn_OptionsBack, 2, 7);
            this.OptionsPanel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.OptionsPanel.Location = new System.Drawing.Point(-1, -2);
            this.OptionsPanel.Name = "OptionsPanel";
            this.OptionsPanel.RowCount = 8;
            this.OptionsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 175F));
            this.OptionsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.OptionsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.OptionsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.OptionsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.OptionsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.OptionsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.OptionsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.OptionsPanel.Size = new System.Drawing.Size(967, 477);
            this.OptionsPanel.TabIndex = 2;
            this.OptionsPanel.Visible = false;
            // 
            // btn_OptionsBack
            // 
            this.btn_OptionsBack.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_OptionsBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_OptionsBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_OptionsBack.Location = new System.Drawing.Point(619, 381);
            this.btn_OptionsBack.Name = "btn_OptionsBack";
            this.btn_OptionsBack.Size = new System.Drawing.Size(111, 38);
            this.btn_OptionsBack.TabIndex = 1;
            this.btn_OptionsBack.Text = "Back";
            this.btn_OptionsBack.UseVisualStyleBackColor = true;
            this.btn_OptionsBack.Click += new System.EventHandler(this.btn_OptionsBack_Click);
            // 
            // TitleScreen
            // 
            this.AcceptButton = this.btn_start;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(966, 474);
            this.Controls.Add(this.OptionsPanel);
            this.Controls.Add(this.HelpPanel);
            this.Controls.Add(this.MainPanel);
            this.Name = "TitleScreen";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MainPanel.ResumeLayout(false);
            this.HelpPanel.ResumeLayout(false);
            this.HelpPanel.PerformLayout();
            this.OptionsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel MainPanel;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Button btn_help;
        private System.Windows.Forms.Button btn_options;
        private System.Windows.Forms.Button btn_about;
        private System.Windows.Forms.TableLayoutPanel HelpPanel;
        private System.Windows.Forms.Label lbl_HelpInfo;
        private System.Windows.Forms.Button btn_HelpBack;
        private System.Windows.Forms.TableLayoutPanel OptionsPanel;
        private System.Windows.Forms.Button btn_OptionsBack;
    }
}

