using System.Diagnostics;
using Vocal.Model;

namespace Vocal.Executors
{
    public class GenericProcessLauncher  : ILauncher
    {
        Launcher _launcher;

        public GenericProcessLauncher(Launcher launcher)
        {
            this._launcher = launcher;
        }

        public IProcessHandle Execute(IVoiceCommand command)
        {
            var proc = Process.Start(_launcher.Command, command.LaunchArgs);

            return new ProcessHandle(proc);
        }
    }
}
