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

namespace Soulstone.Duality.Editor.Serialization.Base
{
    public class SortedTreeNode<TNode, TLeaf> : SortedTreeItem 
        where TNode : SortedTreeNode<TNode, TLeaf>
        where TLeaf : SortedTreeItem
    {
        public TNode Parent { get; }
        public IDictionary<string, TNode> ChildNodes { get; } = new Dictionary<string, TNode>();
        public IDictionary<string, TLeaf> ChildLeaves { get; } = new Dictionary<string, TLeaf>();

        public string Path { get; }

        public SortedTreeNode(TNode parent, string name, Image icon = null) : base(name, icon)
        {
            Parent = parent;

            if (Parent == null)
                Path = name;
            else
                Path = Parent.Path + "/" + name;
        }

        protected override int GetScore(string nameHint, int depthLimit)
        {
            int score = int.MinValue;

            if (nameHint == null)
                score = 1;

            else if (depthLimit > 1)
            {
                foreach (var leaf in ChildLeaves.Values)
                    leaf.UpdateScore(nameHint);

                foreach (var node in ChildNodes.Values)
                    node.UpdateScore(nameHint, --depthLimit);

                var leafScores = ChildLeaves.Values
                    .Select(x => x.Score);

                var nodeScores = ChildNodes.Values
                    .Select(x => x.Score);

                if (nodeScores.Any())
                    score = MathF.Max(score, nodeScores.Max());

                if (leafScores.Any())
                    score = MathF.Max(score, leafScores.Max());
            }

            return score;
        }
    }
}
