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
    public class Item
    {
        public static readonly Item Loading = new Item("Loading...", GeneralResCache.IconCog.ToBitmap());
        public static readonly Item Root = new Item("root", null);

        private string _name;
        private Image _icon = null;
        private int _score = int.MinValue;

        public Image Icon
        {
            get { return _icon; }
        }
        public string Name
        {
            get { return _name + $" ({_score})"; }
        }

        public int Score
        {
            get => _score;
        }

        public Item(string name, Image icon = null)
        {
            _name = name;
            _icon = icon;
        }

        public void UpdateScore(string nameHint, int depthLimit = 20)
        {
            _score = GetScore(nameHint, depthLimit);
        }

        protected virtual int GetScore(string nameHint, int depthLimit)
        {
            bool simpleCheck = nameHint == null || Name.ToLowerInvariant()
                .Contains(nameHint.ToLowerInvariant());

            return simpleCheck ? 1 : 0;
        }
    }

    public class Item<T> : Item
    {
        private T _content = default;

        public T Content
        {
            get { return _content; }
        }

        public Item(string name, Image icon, T content) : base(name, icon)
        {
            _content = content;
        }
    }
}
