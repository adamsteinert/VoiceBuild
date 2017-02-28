using System.Diagnostics;
using Vocal.Model;

namespace Vocal.Executors
{
    /// <summary>
    /// 
    /// </summary>
    public class GenericProcessExecutor
    {
        ExecutionEnvironment environment;

        public GenericProcessExecutor(ExecutionEnvironment environment)
        {
            this.environment = environment;
        }

        public IProcessHandle Execute(IVoiceCommand command)
        {
            //var startInfo = new ProcessStartInfo(@"cmd", @" /K ""C:\Program Files (x86)\Microsoft Visual Studio 14.0\VC\vcvarsall.bat"" & " + command.BuildCommandString());
            var startInfo = new ProcessStartInfo(environment.Command, environment.Initialization + " & " + command.BuildCommandString());

            var proc = new Process();
            proc.StartInfo = startInfo;
            proc.Start();

            return new ProcessHandle(proc);
        }
    }
}
