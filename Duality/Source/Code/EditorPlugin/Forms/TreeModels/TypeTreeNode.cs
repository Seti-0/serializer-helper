using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Duality.Editor;
using Duality.Editor.Properties;

using Soulstone.Duality.Editor.Serialization.Base;

namespace Soulstone.Duality.Editor.Serialization.Base
{
    /// <summary>
    /// Represents a node in a TypeTreeModel, which corresponds to a namespace. 
    /// </summary>
    public class TypeTreeNode : SortedTreeNode<TypeTreeNode, TypeTreeItem>
    {
        public string Namespace { get; }

        public TypeTreeNode(TypeTreeNode parent, string name)
            : base(parent, name, GeneralRes.IconNamespace) 
        {
            Namespace = name;
        }
    }
}
