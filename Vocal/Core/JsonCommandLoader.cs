using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Vocal.Model;

namespace Vocal.Core
{
    public class JsonCommandLoader
    {
        const string DefaultConfigFile = "commandsTemplate.json";
        const string WriteableConfigFile = "commands.json";

        public static IEnumerable<IVoiceCommand> LoadCommandsFromConfiguration()
        {
            if(!File.Exists(WriteableConfigFile))
            {
                CreateWriteableConfigFile(DefaultConfigFile, WriteableConfigFile);
            }

            if (!File.Exists(WriteableConfigFile))
                throw new Exception($"There was an error creating the command configuration file {WriteableConfigFile}. You may need to create this file manually.");


            return JsonConvert.DeserializeObject<VoiceCommand[]>(File.ReadAllText(WriteableConfigFile));
        }

       
        public static void CreateWriteableConfigFile(string source, string destination)
        {
            if (!File.Exists(source))
                throw new Exception($"The source configuration file {source} does not exist. A writeable configuration cannot be created.  See the documentation for more details.");

            File.Copy(source, destination);
        }
    }
}
