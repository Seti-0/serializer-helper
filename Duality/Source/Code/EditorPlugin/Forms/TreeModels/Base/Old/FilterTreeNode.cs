using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

using Aga.Controls.Tree;

using Duality;
using Duality.Editor;
using Duality.Editor.Properties;

namespace SoulStone.Duality.Editor.Tale.Forms.TreeModels.Base
{
    public class FilterTreeNode<T>
    {
        public FilterTreeNode<T> Parent { get; }
        public Item Item { get; }

        public IDictionary<string, FilterTreeNode<T>> ChildNodes { get; } = new Dictionary<string, FilterTreeNode<T>>();
        public IDictionary<string, Item<T>> ChildLeaves { get; } = new Dictionary<string, Item<T>>();

        public string Path { get; }

        public FilterTreeNode(FilterTreeNode<T> parent, Item item)
        {
            Parent = parent;
            Item = item;

            if (Item == Item.Root)
                Path = "";
            else if (Parent == null)
                Path = Item.Name;
            else
                Path = Parent.Path + "/" + Item.Name;
        }
    }
}
