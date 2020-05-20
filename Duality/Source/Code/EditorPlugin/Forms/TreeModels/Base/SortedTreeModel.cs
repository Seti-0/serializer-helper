using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

using System.Windows.Forms;
using System.ComponentModel;

using Aga.Controls.Tree;

using Duality;
using Duality.Editor;
using Duality.Editor.Properties;

namespace Soulstone.Duality.Editor.Serialization.Base
{
    /// <summary>
    /// Maintains a directory-like tree of data which can be loaded async,
    /// and lazily evaluates filters in it.
    /// </summary>
    public class SortedTreeModel<TNode, TLeaf> : ITreeModel
        where TNode : SortedTreeNode<TNode, TLeaf>
        where TLeaf : SortedTreeItem
    {
        private enum State
        {
            Uninitialized,
            Loading,
            Ready
        }

        private IList<TNode>        _roots           = null;
        private string              _nameHint        = null;
        private int                 _maxSearchDepth  = 20;

        private State _state = State.Uninitialized;

        public event EventHandler Initialized;

        public event EventHandler<TreePathEventArgs> StructureChanged;

        // These are currently never called. Perhaps they should be?
        public event EventHandler<TreeModelEventArgs> NodesChanged;
        public event EventHandler<TreeModelEventArgs> NodesInserted;
        public event EventHandler<TreeModelEventArgs> NodesRemoved;

        protected IList<TNode> RootNodes
        {
            get { return _roots; }
        }

        public string NameHint
        {
            get { return _nameHint; }
            set
            {
                _nameHint = value;
                ApplyStructure();
            }
        }

        public int MaxSearchDepth
        {
            get { return _maxSearchDepth; }
            set
            {
                _maxSearchDepth = value;
                ApplyStructure();
            }
        }

        protected virtual string EmptyMessage
        {
            get { return "No valid content found"; }
        }

        public void Init()
        {
            if (_state != State.Uninitialized)
                return;

            _state = State.Loading;

            _roots = new List<TNode>();

            var worker = new BackgroundWorker();
            worker.DoWork += (s, e) => OnInit();
            worker.RunWorkerCompleted += (s, e) =>
            {
                _state = State.Ready;

                OnStructureChanged(new TreePathEventArgs());
                OnInitialized(new EventArgs());
            };

            worker.RunWorkerAsync();
        }

        protected void OnStructureChanged(TreePathEventArgs e)
        {
            StructureChanged?.Invoke(this, e);
        }

        protected void OnInitialized(EventArgs e)
        {
            Initialized?.Invoke(this, e);
        }

        protected virtual void OnInit() {}

        protected void ApplyStructure()
        {
            //if (_state == State.Ready && invalidateFilterHints)
            //    _root.InvalidateHintCache();

            StructureChanged?.Invoke(this, new TreePathEventArgs());
        }

        public System.Collections.IEnumerable GetChildren(TreePath treePath)
        {
            var items = new List<SortedTreeItem>();

            if (_state != State.Ready)
            {
                if (treePath.LastNode == null)
                    items.Add(new SortedTreeItem("Loading...", GeneralResCache.IconCog.ToBitmap()));

                return items;
            }

            
            if(treePath.LastNode == null)
            {
                foreach(var root in _roots)
                {
                    items.AddRange(root.ChildLeaves.Values);
                    items.AddRange(root.ChildNodes.Values);
                }
            }
            else if (treePath.LastNode is TNode node)
            {
                items.AddRange(node.ChildLeaves.Values);
                items.AddRange(node.ChildNodes.Values);
            }

            foreach (var item in items)
                item.UpdateScore(NameHint);

            items = items.OrderByDescending(x => x.Score).ToList();

            if (!items.Any())
                items.Add(new SortedTreeItem(EmptyMessage, GeneralResCache.IconCog.ToBitmap()));

            return items;
        }

        public bool IsLeaf(TreePath treePath)
        {
            if (treePath.LastNode is TNode node)
            {
                return node.ChildNodes.Count == 0 && node.ChildLeaves.Count == 0;
            }
            else return true;
        }
    }
}
