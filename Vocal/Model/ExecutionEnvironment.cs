using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocal.Model
{
    /// <summary>
    /// Configurable definition of the commands required to create the execution environment for a voice command.
    /// </summary>
    public class Launcher
    {
        public string Key { get; set; }

        public string Command { get; set; }

        public string DefaultArguments { get; set; }


        public static Launcher CreateDefaultCmd()
        {
            return new Launcher
            {
                Key = "CmdVs14",
                Command = "cmd",
                DefaultArguments = @" /K ""C:\Program Files (x86)\Microsoft Visual Studio 14.0\VC\vcvarsall.bat"" "
            };
        }
    }
}
