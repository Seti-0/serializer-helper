using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Duality;
using Duality.Editor;

using Soulstone.Duality.Editor.Serialization.Forms;

namespace Soulstone.Duality.Editor.Serialization.EditorActions
{
    /// <summary>
    /// Explicitly save a Duality Resource. Useful when a change has been made that the editor doesn't
    /// know about, such as adjustments made when deserializing.
    /// </summary>
    public class Resave : EditorAction<Resource>
    {
        public override string Name => "Resave";

        public override void Perform(IEnumerable<Resource> objEnum)
        {
            foreach (var res in objEnum)
                ResaveSingle(res);
        }

        private void ResaveSingle(Resource resource)
        {
            if (resource == null) return;
            if (resource.IsRuntimeResource) return;

            resource.Save(resource.Path, true);
        }
    }
}
