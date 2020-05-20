using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

using Aga.Controls.Tree;

using Duality;
using Duality.Editor;

using Soulstone.Duality.Editor.Serialization.Base;

namespace Soulstone.Duality.Editor.Serialization
{
    // This is ver similar to the model found in Duality.Editor.Controls.TreeModels.TypeHierarchy as of writing this,
    // but has better support for filtering large numbers of Types.

    /// <summary>
    /// Maintains a tree of Types from all loaded assemblies, and lazily evaluates filters.
    /// </summary>
    public class TypeTreeModel : SortedTreeModel<TypeTreeNode, TypeTreeItem>
	{
		private	Type _baseType = typeof(object);

        protected override string EmptyMessage
        {
            get { return "No types found assignable to " + _baseType.GetFriendlyName(); }
        }

        public Type BaseType
		{
			get { return _baseType; }
			set
			{
				_baseType = value ?? typeof(object);
                ApplyStructure();
            }
		}

		public TypeTreeModel(Type baseType = null)
		{
			this._baseType = baseType ?? typeof(object);
            // This is disabled at the moment.
            //ContentFilter = CheckContent;
		}

        protected bool CheckContent(Type type)
        {
            //bool acceptBase = _baseType == null || ImplicitCaster.AssignmentPathExists(_baseType, type);
            //bool acceptType = acceptBase && !type.IsGenericType;

            return true;
        }

        protected override void OnInit()
        {
            Assembly[] loadedAssemblies =
                DualityApp.AssemblyLoader.LoadedAssemblies
                .Where(a => !DualityApp.PluginManager.DisposedPlugins.Contains(a))
                .ToArray();

            HashSet<Assembly> selectableAssemblies = new HashSet<Assembly>();
            foreach (Assembly coreAssembly in DualityApp.GetDualityAssemblies())
            {
                selectableAssemblies.Add(coreAssembly);

                AssemblyName[] referencedAssemblies = coreAssembly.GetReferencedAssemblies();
                foreach (AssemblyName reference in referencedAssemblies)
                {
                    string shortName = reference.GetShortAssemblyName();
                    Assembly dependency = loadedAssemblies.FirstOrDefault(a => a.GetShortAssemblyName() == shortName);
                    if (dependency != null)
                        selectableAssemblies.Add(dependency);
                }
            }

            var assemblies = selectableAssemblies.ToArray();
            //ImplicitCaster.Init(assemblies);

            foreach (var type in assemblies.SelectMany(a => SafeGetTypes(a)))
            {
                if (!string.IsNullOrEmpty(type.Namespace))
                {
                    var Sn = GetNamespaceNode(type.Namespace);

                    if (!Sn.ChildLeaves.ContainsKey(type.Name))
                        Sn.ChildLeaves.Add(type.Name, new TypeTreeItem(type));
                }
            }
        }

        private Type[] SafeGetTypes(Assembly assembly)
        {
            Type[] result;
            try
            {
                result = assembly.GetExportedTypes();
            }
            catch (NotSupportedException)
            {
                result = new Type[0];
            }

            return result;
        }

        private TypeTreeNode GetNamespaceNode(string path)
        {
            IList<string> names = path?.Split('.');

            if (names == null || names.Count == 0 || names.Where(x => string.IsNullOrWhiteSpace(x)).Any())
                throw new ArgumentException("Invalid path: "+ path ?? "null", nameof(path));

            TypeTreeNode current = RootNodes
                .Where(x => x.Namespace == names[0])
                .FirstOrDefault();
            
            if (current == null)
            {
                current = new TypeTreeNode(null, names[0]);
                RootNodes.Add(current);
            }

            for (int i = 0; i < names.Count; i++)
            {
                var name = names[i];

                if (!current.ChildNodes.ContainsKey(name))
                {
                    var node = new TypeTreeNode(current, name);
                    current.ChildNodes.Add(name, node);
                }

                current = current.ChildNodes[name];
            }

            return current;
        }
	}
}
