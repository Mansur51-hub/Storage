using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Tools
{
    public class DescriptionConstructor
    {
        public static string GetDescription(string argumentName, string descriptions, bool isRequired, string types)
        {
            var name = $"Parameter name: {argumentName}\n";
            var required = $"Required: {isRequired}\n";
            var description = $"Description: {descriptions}\n";
            var type = $"Type: {types}\n";

            return name + description + required + type + "\n";
        }
    }
}
