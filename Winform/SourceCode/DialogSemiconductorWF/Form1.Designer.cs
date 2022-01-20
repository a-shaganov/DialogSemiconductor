
namespace DialogSemiconductorWF
{
    partial class mainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.generalMenu = new System.Windows.Forms.ToolStrip();
            this.deleteTemperature = new DialogSemiconductorWF.Components.ToolStripBindableButton();
            this.upTemperature = new DialogSemiconductorWF.Components.ToolStripBindableButton();
            this.downTemperature = new DialogSemiconductorWF.Components.ToolStripBindableButton();
            this.sepTempAndTypeSlots = new DialogSemiconductorWF.Components.ToolStripBinableSeparator();
            this.typeSlotsChooser = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new DialogSemiconductorWF.Components.ToolStripBinableSeparator();
            this.selectAllSlots = new DialogSemiconductorWF.Components.ToolStripBindableButton();
            this.deselectAllSlots = new DialogSemiconductorWF.Components.ToolStripBindableButton();
            this.toolStripSeparator2 = new DialogSemiconductorWF.Components.ToolStripBinableSeparator();
            this.executeProgramm = new DialogSemiconductorWF.Components.ToolStripBindableButton();
            this.returnToSettings = new DialogSemiconductorWF.Components.ToolStripBindableButton();
            this.slotsSelector = new System.Windows.Forms.Panel();
            this.lbTemperatures = new System.Windows.Forms.ListBox();
            this.pnlAddTemperature = new System.Windows.Forms.Panel();
            this.btnAddTemperature = new System.Windows.Forms.Button();
            this.tbAddTemperature = new System.Windows.Forms.TextBox();
            this.lblTemperatures = new System.Windows.Forms.Label();
            this.lblAddTemperature = new System.Windows.Forms.Label();
            this.pnlTemperatures = new System.Windows.Forms.Panel();
            this.generalMenu.SuspendLayout();
            this.pnlAddTemperature.SuspendLayout();
            this.pnlTemperatures.SuspendLayout();
            this.SuspendLayout();
            // 
            // generalMenu
            // 
            this.generalMenu.AutoSize = false;
            this.generalMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteTemperature,
            this.upTemperature,
            this.downTemperature,
            this.sepTempAndTypeSlots,
            this.typeSlotsChooser,
            this.toolStripSeparator1,
            this.selectAllSlots,
            this.deselectAllSlots,
            this.toolStripSeparator2,
            this.executeProgramm,
            this.returnToSettings});
            this.generalMenu.Location = new System.Drawing.Point(0, 0);
            this.generalMenu.Name = "generalMenu";
            this.generalMenu.Size = new System.Drawing.Size(800, 31);
            this.generalMenu.TabIndex = 0;
            this.generalMenu.Text = "toolStrip1";
            // 
            // deleteTemperature
            // 
            this.deleteTemperature.AutoSize = false;
            this.deleteTemperature.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteTemperature.Image = ((System.Drawing.Image)(resources.GetObject("deleteTemperature.Image")));
            this.deleteTemperature.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteTemperature.Name = "deleteTemperature";
            this.deleteTemperature.Size = new System.Drawing.Size(28, 28);
            this.deleteTemperature.Click += new System.EventHandler(this.deleteTemperature_Click);
            // 
            // upTemperature
            // 
            this.upTemperature.AutoSize = false;
            this.upTemperature.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.upTemperature.Image = ((System.Drawing.Image)(resources.GetObject("upTemperature.Image")));
            this.upTemperature.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.upTemperature.Name = "upTemperature";
            this.upTemperature.Size = new System.Drawing.Size(28, 28);
            this.upTemperature.Text = "toolStripButton1";
            this.upTemperature.Click += new System.EventHandler(this.upTemperature_Click);
            // 
            // downTemperature
            // 
            this.downTemperature.AutoSize = false;
            this.downTemperature.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.downTemperature.Image = ((System.Drawing.Image)(resources.GetObject("downTemperature.Image")));
            this.downTemperature.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.downTemperature.Name = "downTemperature";
            this.downTemperature.Size = new System.Drawing.Size(28, 28);
            this.downTemperature.Text = "toolStripButton1";
            this.downTemperature.Click += new System.EventHandler(this.downTemperature_Click);
            // 
            // sepTempAndTypeSlots
            // 
            this.sepTempAndTypeSlots.Name = "sepTempAndTypeSlots";
            this.sepTempAndTypeSlots.Size = new System.Drawing.Size(6, 31);
            // 
            // typeSlotsChooser
            // 
            this.typeSlotsChooser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeSlotsChooser.Name = "typeSlotsChooser";
            this.typeSlotsChooser.Size = new System.Drawing.Size(150, 31);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // selectAllSlots
            // 
            this.selectAllSlots.AutoSize = false;
            this.selectAllSlots.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.selectAllSlots.Image = ((System.Drawing.Image)(resources.GetObject("selectAllSlots.Image")));
            this.selectAllSlots.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.selectAllSlots.Name = "selectAllSlots";
            this.selectAllSlots.Size = new System.Drawing.Size(28, 28);
            this.selectAllSlots.Text = "toolStripBindableButton1";
            this.selectAllSlots.Click += new System.EventHandler(this.selectAllSlots_Click);
            // 
            // deselectAllSlots
            // 
            this.deselectAllSlots.AutoSize = false;
            this.deselectAllSlots.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deselectAllSlots.Image = ((System.Drawing.Image)(resources.GetObject("deselectAllSlots.Image")));
            this.deselectAllSlots.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deselectAllSlots.Name = "deselectAllSlots";
            this.deselectAllSlots.Size = new System.Drawing.Size(28, 28);
            this.deselectAllSlots.Text = "toolStripBindableButton1";
            this.deselectAllSlots.Click += new System.EventHandler(this.deselectAllSlots_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // executeProgramm
            // 
            this.executeProgramm.AutoSize = false;
            this.executeProgramm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.executeProgramm.Image = ((System.Drawing.Image)(resources.GetObject("executeProgramm.Image")));
            this.executeProgramm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.executeProgramm.Name = "executeProgramm";
            this.executeProgramm.Size = new System.Drawing.Size(28, 28);
            this.executeProgramm.Text = "toolStripBindableButton1";
            this.executeProgramm.Click += new System.EventHandler(this.executeProgramm_Click);
            // 
            // returnToSettings
            // 
            this.returnToSettings.AutoSize = false;
            this.returnToSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.returnToSettings.Image = ((System.Drawing.Image)(resources.GetObject("returnToSettings.Image")));
            this.returnToSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.returnToSettings.Name = "returnToSettings";
            this.returnToSettings.Size = new System.Drawing.Size(28, 28);
            this.returnToSettings.Text = "toolStripBindableButton1";
            this.returnToSettings.Click += new System.EventHandler(this.returnToSettings_Click);
            // 
            // slotsSelector
            // 
            this.slotsSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.slotsSelector.AutoScroll = true;
            this.slotsSelector.BackColor = System.Drawing.SystemColors.ControlLight;
            this.slotsSelector.Location = new System.Drawing.Point(184, 31);
            this.slotsSelector.Name = "slotsSelector";
            this.slotsSelector.Size = new System.Drawing.Size(613, 506);
            this.slotsSelector.TabIndex = 1;
            // 
            // lbTemperatures
            // 
            this.lbTemperatures.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTemperatures.Location = new System.Drawing.Point(5, 6);
            this.lbTemperatures.Name = "lbTemperatures";
            this.lbTemperatures.Size = new System.Drawing.Size(166, 368);
            this.lbTemperatures.TabIndex = 3;
            this.lbTemperatures.DragDrop += new System.Windows.Forms.DragEventHandler(this.lbTemperatures_DragDrop);
            this.lbTemperatures.DragEnter += new System.Windows.Forms.DragEventHandler(this.lbTemperatures_DragEnter);
            this.lbTemperatures.DragOver += new System.Windows.Forms.DragEventHandler(this.lbTemperatures_DragOver);
            this.lbTemperatures.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbTemperatures_MouseDown);
            // 
            // pnlAddTemperature
            // 
            this.pnlAddTemperature.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlAddTemperature.Controls.Add(this.btnAddTemperature);
            this.pnlAddTemperature.Controls.Add(this.tbAddTemperature);
            this.pnlAddTemperature.Location = new System.Drawing.Point(5, 50);
            this.pnlAddTemperature.Name = "pnlAddTemperature";
            this.pnlAddTemperature.Size = new System.Drawing.Size(176, 80);
            this.pnlAddTemperature.TabIndex = 4;
            // 
            // btnAddTemperature
            // 
            this.btnAddTemperature.Location = new System.Drawing.Point(7, 39);
            this.btnAddTemperature.Name = "btnAddTemperature";
            this.btnAddTemperature.Size = new System.Drawing.Size(163, 31);
            this.btnAddTemperature.TabIndex = 1;
            this.btnAddTemperature.Text = "button1";
            this.btnAddTemperature.UseVisualStyleBackColor = true;
            this.btnAddTemperature.Click += new System.EventHandler(this.btnAddTemperature_Click);
            // 
            // tbAddTemperature
            // 
            this.tbAddTemperature.Location = new System.Drawing.Point(8, 12);
            this.tbAddTemperature.Name = "tbAddTemperature";
            this.tbAddTemperature.Size = new System.Drawing.Size(161, 20);
            this.tbAddTemperature.TabIndex = 0;
            this.tbAddTemperature.Text = "100";
            this.tbAddTemperature.TextChanged += new System.EventHandler(this.tbAddTemperature_TextChanged);
            // 
            // lblTemperatures
            // 
            this.lblTemperatures.AutoSize = true;
            this.lblTemperatures.Location = new System.Drawing.Point(12, 141);
            this.lblTemperatures.Name = "lblTemperatures";
            this.lblTemperatures.Size = new System.Drawing.Size(34, 13);
            this.lblTemperatures.TabIndex = 5;
            this.lblTemperatures.Text = "Temp";
            // 
            // lblAddTemperature
            // 
            this.lblAddTemperature.AutoSize = true;
            this.lblAddTemperature.Location = new System.Drawing.Point(15, 33);
            this.lblAddTemperature.Name = "lblAddTemperature";
            this.lblAddTemperature.Size = new System.Drawing.Size(56, 13);
            this.lblAddTemperature.TabIndex = 6;
            this.lblAddTemperature.Text = "Add Temp";
            // 
            // pnlTemperatures
            // 
            this.pnlTemperatures.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlTemperatures.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlTemperatures.Controls.Add(this.lbTemperatures);
            this.pnlTemperatures.Location = new System.Drawing.Point(5, 157);
            this.pnlTemperatures.Name = "pnlTemperatures";
            this.pnlTemperatures.Size = new System.Drawing.Size(176, 380);
            this.pnlTemperatures.TabIndex = 7;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 539);
            this.Controls.Add(this.pnlTemperatures);
            this.Controls.Add(this.lblAddTemperature);
            this.Controls.Add(this.lblTemperatures);
            this.Controls.Add(this.pnlAddTemperature);
            this.Controls.Add(this.slotsSelector);
            this.Controls.Add(this.generalMenu);
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.Name = "mainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_FormClosing);
            this.generalMenu.ResumeLayout(false);
            this.generalMenu.PerformLayout();
            this.pnlAddTemperature.ResumeLayout(false);
            this.pnlAddTemperature.PerformLayout();
            this.pnlTemperatures.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip generalMenu;
        private DialogSemiconductorWF.Components.ToolStripBindableButton deleteTemperature;
        private DialogSemiconductorWF.Components.ToolStripBinableSeparator sepTempAndTypeSlots;
        private DialogSemiconductorWF.Components.ToolStripBindableButton upTemperature;
        private DialogSemiconductorWF.Components.ToolStripBindableButton downTemperature;
        private System.Windows.Forms.ToolStripComboBox typeSlotsChooser;
        private DialogSemiconductorWF.Components.ToolStripBinableSeparator toolStripSeparator1;
        private Components.ToolStripBindableButton selectAllSlots;
        private Components.ToolStripBindableButton deselectAllSlots;
        private DialogSemiconductorWF.Components.ToolStripBinableSeparator toolStripSeparator2;
        private Components.ToolStripBindableButton executeProgramm;
        private System.Windows.Forms.Panel slotsSelector;
        private System.Windows.Forms.ListBox lbTemperatures;
        private System.Windows.Forms.Panel pnlAddTemperature;
        private System.Windows.Forms.Label lblTemperatures;
        private System.Windows.Forms.TextBox tbAddTemperature;
        private System.Windows.Forms.Button btnAddTemperature;
        private System.Windows.Forms.Label lblAddTemperature;
        private Components.ToolStripBindableButton returnToSettings;
        private System.Windows.Forms.Panel pnlTemperatures;
    }
}

