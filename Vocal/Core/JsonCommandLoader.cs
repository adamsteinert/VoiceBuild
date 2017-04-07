using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Vocal.Model;

namespace Vocal.Core
{
    public class JsonProjectLoader
    {
        const string AppDataFolder = "VocalBuildEngine";
        const string DefaultConfigFile = "commandsTemplate.json";
        const string WriteableCommandFile = "commands.json";

        public static ProjectConfiguration LoadCommandsFromConfiguration()
        {
            var projectFile = GetDefaultPath(WriteableCommandFile);

            if (!File.Exists(projectFile))
            {
                var file = new FileInfo(projectFile);

                if (!Directory.Exists(file.Directory.FullName))
                    Directory.CreateDirectory(file.Directory.FullName);

                CreateWriteableConfigFile(DefaultConfigFile, projectFile);
            }

            if (!File.Exists(projectFile))
                throw new Exception($"There was an error creating the command configuration file {projectFile}. You may need to create this file manually.");


            return JsonConvert.DeserializeObject<ProjectConfiguration>(File.ReadAllText(projectFile));
        }

       
        public static void CreateWriteableConfigFile(string source, string destination)
        {
            if (!File.Exists(source))
                throw new Exception($"The source configuration file {source} does not exist. A writeable configuration cannot be created.  See the documentation for more details.");

            File.Copy(source, destination);
        }

        public static string GetDefaultPath(string fileName)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), AppDataFolder, fileName);
        }
    }
}
