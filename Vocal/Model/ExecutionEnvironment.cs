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
    public class ExecutionEnvironment
    {
        public string Key { get; set; }

        public string Command { get; set; }

        public string Initialization { get; set; }


        public static ExecutionEnvironment CreateDefaultCmd()
        {
            return new ExecutionEnvironment
            {
                Key = "CmdVs14",
                Command = "cmd",
                Initialization = @" /K ""C:\Program Files (x86)\Microsoft Visual Studio 14.0\VC\vcvarsall.bat"" "
            };
        }
    }
}
