using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.Xml.Linq;

using Duality.Serialization;
using Duality.Editor;
using System.IO;

using Soulstone.Duality.Editor.Serialization.Forms;

namespace Soulstone.Duality.Editor.Serialization.EditorActions
{
    public class Inspect // : EditorAction<object> (Stop it appearing in the editor for now)
    {
        /* 
         This is a WIP. I probably won't come back to it for a while.
             */

        public /*override*/ string Name => "Inspect";

        public /*override*/ void Perform(IEnumerable<object> objEnum)
        {
            if (!objEnum.Any())
                return;

            var subject = objEnum.First();

            using (var stream = new MemoryStream())
            {
                Serializer.WriteObject(subject, stream);
                
                string text = Encoding.UTF8.GetString(stream.ToArray());

                stream.Position = 0;
                var xmlReader = XmlReader.Create(stream);

                while (xmlReader.NodeType != XmlNodeType.Element)
                    xmlReader.Read();

                XElement element = XElement.Load(xmlReader);

                var dialog = new InspectionDialog(element);
                dialog.Show();
            }
        }
    }
}
