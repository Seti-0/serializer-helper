using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using System.Collections.Concurrent;
using System.Threading;

using Duality;

namespace Soulstone.Duality.Editor.Serialization
{
    public static class TypeExtMethods
    {
        private static readonly Dictionary<Type, string> Aliases = new Dictionary<Type, string>
            {
                {typeof(int), "int"},
                {typeof(uint), "uint"},
                {typeof(long), "long"},
                {typeof(ulong), "ulong"},
                {typeof(short), "short"},
                {typeof(ushort), "ushort"},
                {typeof(byte), "byte"},
                {typeof(sbyte), "sbyte"},
                {typeof(bool), "bool"},
                {typeof(float), "float"},
                {typeof(double), "double"},
                {typeof(decimal), "decimal"},
                {typeof(char), "char"},
                {typeof(string), "string"},
                {typeof(object), "object"},
                {typeof(void), "void"}
            };

        /// <summary>
        /// Returns a friendly name for this type, taking generics, arrays, nullables and keywords into account.
        /// </summary>
        public static string GetFriendlyName(this Type type, int recursionLimit = 100)
        {
            // The recursion limit is awkward, but I'm keenly aware of not fully understanding c# type reflection
            // and the possible recursion loops that could happen here.
            if (recursionLimit < 1)
            {
                Logs.Editor.WriteWarning(nameof(GetFriendlyName) + " from the RenameHelper plugin reached its recursion limit.");
                return "";
            }

            string name = "";
            string currentDebugName = type.FullName;

            if (Aliases.ContainsKey(type))
                name = Aliases[type];

            else if (type.IsArray)
                name = GetFriendlyName(type.GetElementType(), --recursionLimit) + "[]";

            else if (type.IsGenericParameter)
                name = type.Name;

            else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                name = type.GetGenericArguments()[0].GetFriendlyName(--recursionLimit) + "?";

            else if (type.IsGenericType)
            {
                name = type.Name.Split('`')[0] + "<";

                var arguments = type.GetGenericArguments();
                var debug = arguments.Select(x => x.Name);

                name += string.Join(",", arguments.Select(x => x.GetFriendlyName(--recursionLimit)).ToArray());

                name += ">";
            }

            else
                name = type.Name;

            if (type.IsNested && !type.IsGenericParameter)
                name = GetFriendlyName(type.DeclaringType, --recursionLimit) + "+" + name;

            return name;
        }
    }
}