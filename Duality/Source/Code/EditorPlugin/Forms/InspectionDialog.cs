using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using System.Xml.Linq;

using Duality;
using Duality.Editor;
using Duality.Editor.Controls;
using Duality.Editor.Forms;

using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;

using Soulstone.Duality.Editor.Serialization;
using Soulstone.Duality.Editor.Serialization.Properties;

namespace Soulstone.Duality.Editor.Serialization.Forms
{
    public partial class InspectionDialog : Form
    {
        /*
         * Note to future self: If forms starts having problems with a Duality. ... .CueTextbox, just remove the namespace
         * i.e. new CueTextBox in the auto-generated code.
         */


        private XmlTreeMdel _xmlModel;

        private bool _expandAll = true;

        private string _infoText = "", _dataText = "", _helpText = "";
        private string _startingText = "";


        public string InfoText
        {
            get => _infoText;

            set
            {
                _infoText = value ?? "";
                InfoLabel.Text = _infoText;
            }
        }

        public string DataText
        {
            get => _dataText;

            set
            {
                _dataText = value ?? "";
                DataLabel.Text = _dataText;
            }
        }

        public string HelpText
        {
            get => _helpText;

            set
            {
                _helpText = value ?? "";
                HelpLabel.Text = _helpText;
            }
        }

        public string StartingText
        {
            get => _startingText;

            set
            {
                _startingText = value ?? "";
            }
        }

        public InspectionDialog(XElement subject)
        {
            InitializeComponent();

            viewObjectType.NodeControls.Add(iconTreeNode);
            viewObjectType.NodeControls.Add(txtNodeName);

            //txtNodeName.DrawText += treeNodeName_DrawText;

            _xmlModel = new XmlTreeMdel(subject);
            _xmlModel.Init();
            _xmlModel.Initialized += _typeModel_Initialized;
        }

        private void _typeModel_Initialized(object sender, EventArgs e)
        {
            UpdateView();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            txtFilter.Text = StartingText;
            txtFilter.SelectAll();

            viewObjectType.Model = _xmlModel;
            _xmlModel.NameHint = txtFilter.Text;
            UpdateView();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        /*
        private void treeNodeName_DrawText(object sender, DrawTextEventArgs e)
        {
            if (IsNodeValid(e.Node))
                e.TextColor = viewObjectType.ForeColor;
            else
                e.TextColor = Color.FromArgb(128, viewObjectType.ForeColor);
        }
        */

        private void TxtFilterInput_TextChanged(object sender, EventArgs e)
        {
            _xmlModel.NameHint = txtFilter.Text;
            UpdateView();
        }

        private void UpdateView()
        {
            if (_expandAll)
            {
                buttonExpandAll.Image = Resources.expand_active;
                viewObjectType.ExpandAll();
            }
            else
            {
                buttonExpandAll.Image = Resources.expand;
                viewObjectType.CollapseAll();
            }
        }

        private void ButtonExpandAll_Click(object sender, EventArgs e)
        {
            _expandAll = !_expandAll;
            UpdateView();
        }
    }
}
