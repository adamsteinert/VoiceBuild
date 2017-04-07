using System;
using System.Collections.Generic;
using System.Speech.Recognition;
using System.Threading.Tasks;
using Vocal.Model;

namespace Vocal.VoiceActions
{
    public interface IVoiceAction
    {
        string VoiceActionKey { get; }
        GrammarBuilder CreateGrammar(IEnumerable<Project> projects);
        Task Execute(Project project, Action<string> writeToLog);
    }
}