using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Linq;

using Duality.Editor;
using Duality.Editor.Properties;

using Soulstone.Duality.Editor.Serialization.Base;

namespace Soulstone.Duality.Editor.Serialization.Base
{
    /// <summary>
    /// Represents a node in an XmlElement, which corresponds to an element in the xml tree. 
    /// </summary>
    public class XmlTreeNode : SortedTreeNode<XmlTreeNode, XmlTreeItem>
    {
        public XElement Element;

        public XmlTreeNode(XmlTreeNode parent, XElement element)
            : base(parent, element.Name.ToString(), GeneralRes.IconClass)
        {
            Element = element;
        }

        protected override int GetScore(string nameHint, int depthLimit)
        {
            if (Element == null || nameHint == null) return 0;

            return StringHelper.CoderScore("", Element.ToString(), nameHint);
        }
    }
}
