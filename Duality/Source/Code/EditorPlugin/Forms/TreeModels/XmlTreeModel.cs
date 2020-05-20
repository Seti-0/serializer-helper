using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

using System.Xml.Linq;

using Aga.Controls.Tree;

using Duality;
using Duality.Editor;

using Soulstone.Duality.Editor.Serialization.Base;

namespace Soulstone.Duality.Editor.Serialization
{
    // This is ver similar to the model found in Duality.Editor.Controls.TreeModels.TypeHierarchy as of writing this,
    // but has better support for filtering large numbers of Types.

    /// <summary>
    /// Maintains a tree of Types from all loaded assemblies, and lazily evaluates filters.
    /// </summary>
    public class XmlTreeMdel : SortedTreeModel<XmlTreeNode, XmlTreeItem>
	{
        private XElement _root;

        protected override string EmptyMessage
        {
            get { return "(Empty)"; }
        }

		public XmlTreeMdel(XElement root)
		{
            _root = root;
		}

        protected override void OnInit()
        {
            if (_root == null)
                return;

            AddXElement(null, _root);
        }

        private void AddXElement(XmlTreeNode parent, XElement child)
        {
            var node = new XmlTreeNode(parent, child);

            if (parent == null)
                RootNodes.Add(node);
            else
                parent.ChildNodes.Add(node.Name, node);

            foreach (var grandChild in child.Elements())
                AddXElement(node, grandChild);

            foreach (var attribute in child.Attributes())
            {
                var item = new XmlTreeItem(attribute);
                node.ChildLeaves.Add(item.Name, item);
            }
        }
	}
}
