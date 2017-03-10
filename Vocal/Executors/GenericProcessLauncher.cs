using System.Diagnostics;
using Vocal.Model;

namespace Vocal.Executors
{
    public class GenericProcessLauncher  : ILauncher
    {
        CustomLauncher _launcher;

        public GenericProcessLauncher(CustomLauncher launcher)
        {
            this._launcher = launcher;
        }

        public IProcessHandle Execute(IVoiceCommand command)
        {
            var proc = Process.Start(_launcher.Process, command.LaunchTarget);

            return new ProcessHandle(proc);
        }
    }
}
