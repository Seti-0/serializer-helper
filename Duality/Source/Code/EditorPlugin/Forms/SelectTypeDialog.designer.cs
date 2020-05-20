using Duality.Editor.Controls;

namespace Soulstone.Duality.Editor.Serialization.Forms
{
    partial class SelectTypeDialog
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
            this.viewObjectType = new Aga.Controls.Tree.TreeViewAdv();
            this.iconTreeNode = new Aga.Controls.Tree.NodeControls.NodeStateIcon();
            this.txtNodeName = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.txtFilter = new CueTextBox();
            this.buttonExpandAll = new System.Windows.Forms.Button();
            this.InfoLabel = new System.Windows.Forms.Label();
            this.DataLabel = new System.Windows.Forms.Label();
            this.HelpLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // viewObjectType
            // 
            this.viewObjectType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.viewObjectType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(196)))), ((int)(((byte)(196)))));
            this.viewObjectType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.viewObjectType.ColumnHeaderHeight = 0;
            this.viewObjectType.DefaultToolTipProvider = null;
            this.viewObjectType.DragDropMarkColor = System.Drawing.Color.Black;
            this.viewObjectType.FullRowSelect = true;
            this.viewObjectType.FullRowSelectActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.viewObjectType.FullRowSelectInactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(212)))));
            this.viewObjectType.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.viewObjectType.LoadOnDemand = true;
            this.viewObjectType.Location = new System.Drawing.Point(14, 86);
            this.viewObjectType.Model = null;
            this.viewObjectType.Name = "viewObjectType";
            this.viewObjectType.NodeFilter = null;
            this.viewObjectType.SelectedNode = null;
            this.viewObjectType.Size = new System.Drawing.Size(527, 309);
            this.viewObjectType.TabIndex = 0;
            this.viewObjectType.SelectionChanged += new System.EventHandler(this.objectTypeView_SelectionChanged);
            // 
            // iconTreeNode
            // 
            this.iconTreeNode.DataPropertyName = "Icon";
            this.iconTreeNode.LeftMargin = 1;
            this.iconTreeNode.ParentColumn = null;
            this.iconTreeNode.ScaleMode = Aga.Controls.Tree.ImageScaleMode.Clip;
            // 
            // txtNodeName
            // 
            this.txtNodeName.DataPropertyName = "Name";
            this.txtNodeName.IncrementalSearchEnabled = true;
            this.txtNodeName.LeftMargin = 3;
            this.txtNodeName.ParentColumn = null;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Enabled = false;
            this.buttonOk.Location = new System.Drawing.Point(385, 401);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "Select";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(466, 401);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Skip";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // txtFilter
            // 
            this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(196)))), ((int)(((byte)(196)))));
            this.txtFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilter.CueText = "Filter";
            this.txtFilter.Location = new System.Drawing.Point(14, 61);
            this.txtFilter.Margin = new System.Windows.Forms.Padding(2);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(527, 20);
            this.txtFilter.TabIndex = 4;
            this.txtFilter.TextChanged += new System.EventHandler(this.TxtFilterInput_TextChanged);
            // 
            // buttonExpandAll
            // 
            this.buttonExpandAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonExpandAll.FlatAppearance.BorderSize = 0;
            this.buttonExpandAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExpandAll.Image = global::Soulstone.Duality.Editor.Serialization.Properties.Resources.expand_active;
            this.buttonExpandAll.Location = new System.Drawing.Point(14, 401);
            this.buttonExpandAll.Name = "buttonExpandAll";
            this.buttonExpandAll.Size = new System.Drawing.Size(32, 23);
            this.buttonExpandAll.TabIndex = 5;
            this.buttonExpandAll.UseVisualStyleBackColor = true;
            this.buttonExpandAll.Click += new System.EventHandler(this.ButtonExpandAll_Click);
            // 
            // InfoLabel
            // 
            this.InfoLabel.AutoSize = true;
            this.InfoLabel.Location = new System.Drawing.Point(11, 9);
            this.InfoLabel.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.InfoLabel.Name = "InfoLabel";
            this.InfoLabel.Size = new System.Drawing.Size(35, 13);
            this.InfoLabel.TabIndex = 6;
            this.InfoLabel.Text = "label1";
            // 
            // DataLabel
            // 
            this.DataLabel.AutoSize = true;
            this.DataLabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.DataLabel.Location = new System.Drawing.Point(11, 22);
            this.DataLabel.Name = "DataLabel";
            this.DataLabel.Size = new System.Drawing.Size(35, 13);
            this.DataLabel.TabIndex = 7;
            this.DataLabel.Text = "label1";
            // 
            // HelpLabel
            // 
            this.HelpLabel.AutoSize = true;
            this.HelpLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.HelpLabel.Location = new System.Drawing.Point(11, 35);
            this.HelpLabel.Name = "HelpLabel";
            this.HelpLabel.Size = new System.Drawing.Size(35, 13);
            this.HelpLabel.TabIndex = 8;
            this.HelpLabel.Text = "label2";
            // 
            // SelectTypeDialog
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(212)))));
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(550, 446);
            this.Controls.Add(this.HelpLabel);
            this.Controls.Add(this.DataLabel);
            this.Controls.Add(this.InfoLabel);
            this.Controls.Add(this.viewObjectType);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.buttonExpandAll);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectTypeDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Object...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Aga.Controls.Tree.TreeViewAdv viewObjectType;
        private Aga.Controls.Tree.NodeControls.NodeStateIcon iconTreeNode;
        private Aga.Controls.Tree.NodeControls.NodeTextBox txtNodeName;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private CueTextBox txtFilter;
        private System.Windows.Forms.Button buttonExpandAll;
        private System.Windows.Forms.Label InfoLabel;
        private System.Windows.Forms.Label DataLabel;
        private System.Windows.Forms.Label HelpLabel;
    }
}