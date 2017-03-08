using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocal.Model
{
    public class CommandDefinition
    {
        public LauncherDefinition[] Launchers { get; set; }

        public VoiceCommand[] Commands { get; set; }
    }
}
