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

namespace SoulStone.Duality.Editor.Tale.Forms.TreeModels.Base
{
    /// <summary>
    /// Maintains a directory-like tree of data which can be loaded async,
    /// and lazily evaluates filters in it.
    /// </summary>
    public class FilterTreeModel<T> : ITreeModel
    {
        private enum State
        {
            Uninitialized,
            Loading,
            Ready
        }

        private FilterTreeNode<T>   _root            = null;
        private Predicate<T>        _contentFilter   = null;
        private string              _nameHint        = null;
        private int                 _maxSearchDepth  = 20;

        private State _state = State.Uninitialized;

        protected FilterTreeNode<T> Root
        {
            get { return _root; }
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

        public Predicate<T> ContentFilter
        {
            get { return _contentFilter; }
            set
            {
                _contentFilter = value;
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

            _root = new FilterTreeNode<T>(null, Item.Root);

            var worker = new BackgroundWorker();
            worker.DoWork += (s, e) => OnInit();
            worker.RunWorkerCompleted += (s, e) =>
            {
                _state = State.Ready;

                // This doesn't seem to work atm, the tree structure with non-leaves unfiltered
                //_root.InvalidateHintCache();
                foreach (var node in _root.ChildNodes.Values)
                    node.Item.UpdateScore(_nameHint, _maxSearchDepth);
                // Setting the filter once from the Dialog.OnLoad works, but I don't really see why - this is async

                StructureChanged?.Invoke(this, new TreePathEventArgs());

                OnInitialized(new EventArgs());
            };

            worker.RunWorkerAsync();
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
            List<Item> items = new List<Item>();

            if (_state != State.Ready)
            {
                if (treePath.LastNode == null)
                    items.Add(Item.Loading);

                return items;
            }

            FilterTreeNode<T> parentNode = null;

            if (treePath.LastNode == null)
                parentNode = _root;
            else if (treePath.LastNode is NodeItem<T> nodeItem)
                parentNode = nodeItem.Node;

            if (parentNode != null)
            {
                foreach (var node in parentNode.ChildNodes.Values)
                    node.Item.UpdateScore(_nameHint, _maxSearchDepth);

                foreach (var leaf in parentNode.ChildLeaves.Values)
                    leaf.UpdateScore(_nameHint);

                var nodes = parentNode.ChildNodes.Values
                    .Select(x => x.Item);

                var leaves = parentNode.ChildLeaves.Values;

                items.AddRange(nodes);
                items.AddRange(leaves);
                items = items
                    .OrderByDescending(x => x.Score)
                    .ToList();
            }

            if (items.Count == 0)
                items.Add(new Item(EmptyMessage, GeneralResCache.IconCog.ToBitmap()));

            return items;
        }

        public bool IsLeaf(TreePath treePath)
        {
            if (treePath.LastNode is NodeItem<T> item)
            {
                return item.Node.ChildNodes.Count == 0 && item.Node.ChildLeaves.Count == 0;
            }
            else return true;
        }

        public event EventHandler Initialized;

        public event EventHandler<TreeModelEventArgs> NodesChanged;
        public event EventHandler<TreeModelEventArgs> NodesInserted;
        public event EventHandler<TreeModelEventArgs> NodesRemoved;
        public event EventHandler<TreePathEventArgs> StructureChanged;
    }
}
