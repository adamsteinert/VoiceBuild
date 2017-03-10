using System.Diagnostics;
using Vocal.Model;

namespace Vocal.Executors
{
    public class DefaultBrowserLauncher : ILauncher
    {
        public IProcessHandle Execute(IVoiceCommand command)
        {
            var proc = Process.Start(command.LaunchTarget);

            return new ProcessHandle(proc);
        }
    }
}
