using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Duality.Editor;
using Duality.Editor.Properties;

using Soulstone.Duality.Editor.Serialization.Base;

namespace Soulstone.Duality.Editor.Serialization
{
    public class TypeTreeItem : SortedTreeItem
    {
        public Type Content { get; }

        public TypeTreeItem(Type type) 
            : base(type.GetFriendlyName(), type.GetEditorImage() ?? GeneralRes.IconClass)
        {
            Content = type;
        }

        protected override int GetScore(string nameHint, int depthLimit)
        {
            if (Content == null) return 0;

            // This currently is not accounting for invalid nodes
            return StringHelper.CoderScore(Content.Namespace, Content.GetFriendlyName(), nameHint);
        }

        
    }
}
