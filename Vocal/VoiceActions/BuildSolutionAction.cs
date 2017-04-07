using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;
using Vocal.Core;
using Vocal.Model;

namespace Vocal.VoiceActions
{
    public class OpenSolutionAction : IVoiceAction
    {
        public string VoiceActionKey { get; } = "OpenSolutionAction";

        public GrammarBuilder CreateGrammar(IEnumerable<Project> projects)
        {
            var builder = new GrammarBuilder();

            // Command Choices.
            builder.Append("open", 0, 1);
            builder.Append("the", 0, 1);
            builder.Append("solution");
            builder.Append("for", 0, 1);

            // Application types
            var appChoice = new Choices();
            foreach (var project in projects)
            {
                appChoice.Add(new SemanticResultValue(project.ProjectName, project));
            }

            builder.Append(new SemanticResultKey(GrammarKeys.ProjectKey, appChoice));

            return builder;
        }

        public Task Execute(Project project, Action<string> writeToLog)
        {
            var result = FindSolutions(project.RootDirectory);

            if(result.HasSolution)
            {
                writeToLog($"Opening primary solution {result.PrimarySolutionPath}");
            }
            else
            {
                writeToLog($"There were no solution files found in the directory {project.RootDirectory}");
            }

            return Task.Delay(0);
        }


        public static SolutionInfo FindSolutions(string rootDirectory)
        {
            const string filter = "*.sln";

            var slnList = Directory.GetFiles(rootDirectory, filter).ToList();
            string primary = null;

            if(slnList.Count > 0)
            {
                primary = slnList.First();
            }
            else
            {
                slnList = Directory.EnumerateFiles(rootDirectory, filter, SearchOption.AllDirectories).ToList();
                primary = slnList.FirstOrDefault();
            }

            return new SolutionInfo
            {
                PrimaryPath = primary,
                AllSolutions = slnList
            };
        }


        public class SolutionInfo
        {
            public string PrimaryPath { get; set; }

            public List<string> AllSolutions { get; set; }


            public string PrimarySolutionPath
            {
                get
                {
                    if (PrimaryPath != null)
                        return PrimaryPath;

                    return AllSolutions.First();
                }
            }

            public bool HasSolution
            {
                get
                {
                    return PrimaryPath != null ||
                        AllSolutions != null && AllSolutions.Count > 0;
                }
            }
        }
    }
}
