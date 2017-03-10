using System;
using System.Diagnostics;
using Vocal.Model;

namespace Vocal.Executors
{

    /// <summary>
    /// 
    /// </summary>
    public class CommandConsoleLauncher : ILauncher
    {
        EmbeddedLauncher _launcher;

        public CommandConsoleLauncher(EmbeddedLauncher launcher)
        {
            this._launcher = launcher;
        }

        public IProcessHandle Execute(IVoiceCommand command)
        {
            //var startInfo = new ProcessStartInfo(@"cmd", @" /K ""C:\Program Files (x86)\Microsoft Visual Studio 14.0\VC\vcvarsall.bat"" & " + command.BuildCommandString());
            var startInfo = new ProcessStartInfo(_launcher.Command, _launcher.DefaultArguments + " & " + command.BuildCommandStringInInitDirectory());

            var proc = new Process();
            proc.StartInfo = startInfo;
            proc.Start();

            return new ProcessHandle(proc);
        }
    }
}
