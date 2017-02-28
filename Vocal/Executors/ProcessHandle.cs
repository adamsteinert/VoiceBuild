using System;
using System.Diagnostics;
using System.Linq;

namespace Vocal.Executors
{
    public class ProcessHandle : IProcessHandle, IDisposable
    {
        public Process Process { get; private set; }

        public ProcessHandle(Process process)
        {
            Process = process;
        }
        public void Dispose()
        {
            if (Process != null)
                Process.Dispose();
        }
    }
}
