using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Duality;
using Duality.Editor;
using Duality.Editor.Forms;

using Duality.Resources;

using Soulstone.Duality.Editor.Serialization.Forms;

namespace Soulstone.Duality.Editor.Serialization
{
	/// <summary>
	/// Defines a Duality editor plugin.
	/// </summary>
    public class Bable : EditorPlugin
	{
		private SelectTypeDialog _dialog = null;

		public override string Id
		{
			get { return "Soulstone.Duality.Editor.Serialization"; }
		}

		protected override void InitPlugin(MainForm main)
		{
			base.InitPlugin(main);

			ReflectionHelper.TypeResolve += ReflectionHelper_TypeResolve;

			DualityApp.PluginManager.AssemblyLoader.AssemblyLoaded += AssemblyLoader_AssemblyLoaded;
			DualityEditorApp.PluginManager.AssemblyLoader.AssemblyLoaded += AssemblyLoader_AssemblyLoaded;
		}

		private void AssemblyLoader_AssemblyLoaded(object sender, global::Duality.Backend.AssemblyLoadedEventArgs e)
		{
			_dialog?.Dispose();
			_dialog = null;
		}

		protected override void OnDisposePlugin()
		{
			base.OnDisposePlugin();
			ReflectionHelper.TypeResolve -= ReflectionHelper_TypeResolve;

			DualityApp.PluginManager.AssemblyLoader.AssemblyLoaded -= AssemblyLoader_AssemblyLoaded;
			DualityEditorApp.PluginManager.AssemblyLoader.AssemblyLoaded -= AssemblyLoader_AssemblyLoaded;
		}

		private void ReflectionHelper_TypeResolve(object sender, ResolveMemberEventArgs e)
		{
			if (e?.MemberId == null)
				return;

			if (_dialog == null)
				_dialog = new SelectTypeDialog();

			_dialog.InfoText = "The Duality ReflectionHelper does not recognize the following type: ";
			_dialog.DataText = e.MemberId ?? "<Member ID null>";
			_dialog.HelpText = "You can specify the type manually here, (If it was renamed, for example), or click \"skip\" to ignore the error and move on.";

			string initialText = e.MemberId;

			if(e.MemberId.Contains('.'))
			{
				int lastDot = initialText.LastIndexOf('.');
				if (lastDot < initialText.Length - 1)
					initialText = initialText.Substring(lastDot + 1);
			}

			_dialog.StartingText = initialText;

			var result = _dialog.ShowDialog();

			if(result == System.Windows.Forms.DialogResult.OK)
			{
				e.ResolvedMember = _dialog.SelectedType;
			}
		}
	}
}
