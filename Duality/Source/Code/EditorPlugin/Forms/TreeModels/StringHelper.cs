using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulstone.Duality.Editor.Serialization
{
    internal static class StringHelper
    {
        public static int CoderScore(string parent, string source, string target)
        {
            // This is a very silly way of doing things, I should really learn how to compare strings nicely... 

            int score = 0;
            int j = 0;

            CoderPoints(parent, target, ref j);
            var parentJ = j;
            score += 100 * j;

            CoderPoints(source, target, ref j);
            score += 100 * (j - parentJ - target.Length);

            score -= source.Length;

            return score;
        }

        private static void CoderPoints(string source, string target, ref int j)
        {
            int i = 0;

            while (i < source.Length && j < target.Length)
            {
                while (j < target.Length && char.IsWhiteSpace(target[j]))
                    j++;

                if (j == target.Length)
                    break;

                if (source[i] == target[j])
                    j++;

                i++;
            }
        }
    }
}
