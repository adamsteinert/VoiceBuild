using System;
using System.Diagnostics;
using Vocal.Model;

namespace Vocal.Executors
{

    /// <summary>
    /// 
    /// </summary>
    public class GenericCommandWindowLauncher : ILauncher
    {
        Launcher _launcher;

        public GenericCommandWindowLauncher(Launcher launcher)
        {
            this._launcher = launcher;
        }

        public IProcessHandle Execute(IVoiceCommand command)
        {
            //var startInfo = new ProcessStartInfo(@"cmd", @" /K ""C:\Program Files (x86)\Microsoft Visual Studio 14.0\VC\vcvarsall.bat"" & " + command.BuildCommandString());
            var startInfo = new ProcessStartInfo(_launcher.Command, _launcher.DefaultArguments + " & " + command.BuildCommandString());

            var proc = new Process();
            proc.StartInfo = startInfo;
            proc.Start();

            return new ProcessHandle(proc);
        }
    }
}
