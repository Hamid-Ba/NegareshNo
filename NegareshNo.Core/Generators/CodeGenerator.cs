using System;
using System.Collections.Generic;
using System.Text;

namespace NegareshNo.Core.Generators
{
    public class CodeGenerator
    {
        public static string GenerateUniqueCode() => Guid.NewGuid().ToString().Replace("-", "");
    }
}
