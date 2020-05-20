using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Linq;

using Duality.Editor;
using Duality.Editor.Properties;

using Soulstone.Duality.Editor.Serialization.Base;

namespace Soulstone.Duality.Editor.Serialization
{
    public class XmlTreeItem : SortedTreeItem
    {
        public XAttribute Content { get; }

        public XmlTreeItem(XAttribute attribute) 
            : base(attribute.Name.ToString(), GeneralRes.IconNamespace)
        {
            Content = attribute;
        }

        protected override int GetScore(string nameHint, int depthLimit)
        {
            return 0;
        }
    }
}
