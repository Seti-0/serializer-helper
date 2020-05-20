using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

using Duality;
using Duality.Editor.Properties;

namespace SoulStone.Duality.Editor.Tale.Forms.TreeModels.Base
{
    public class NodeItem<T> : Item<T>
    {
        private FilterTreeNode<T> _node;

        public FilterTreeNode<T> Node
        {
            get { return _node; }
            set { _node = value; }
        }

        public NodeItem(string name, Image icon, T content, FilterTreeNode<T> node) : base(name, icon, content)
        {
            Node = node;
        }

        protected override int GetScore(string nameHint, int depthLimit)
        {
            int score = int.MinValue;

            if (nameHint == null)
                score = 1;

            else if (depthLimit > 1)
            {
                foreach (var leaf in Node.ChildLeaves.Values)
                    leaf.UpdateScore(nameHint);

                foreach (var node in Node.ChildNodes.Values)
                    node.Item.UpdateScore(nameHint, --depthLimit);

                var leafScores = Node.ChildLeaves.Values
                    .Select(x => x.Score);

                var nodeScores = Node.ChildNodes.Values
                        .Select(x => x.Item.Score);

                if (nodeScores.Any())
                    score = MathF.Max(score, nodeScores.Max());

                if (leafScores.Any())
                    score = MathF.Max(score, leafScores.Max());
            }

            return score;
        }
    }
}
