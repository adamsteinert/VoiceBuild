using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;
using Vocal.Model;

namespace Vocal.Core
{
    public static class GrammarKeys
    {
        public const string SemanticKey_Command = "CommandKey";
    }

    public interface IVocalGrammarBuilder
    {
        GrammarBuilder CreateGrammar(IEnumerable<IVoiceCommand> commands);
    }

    public class BuildOnlyGrammarBuilder : IVocalGrammarBuilder
    {
        public GrammarBuilder CreateGrammar(IEnumerable<IVoiceCommand> commands)
        {
            //_recognizer.UnloadAllGrammars();

            var builder = new GrammarBuilder();

            // Command Choices.
            builder.Append("Computer", 0, 1);
            builder.Append("Build");
            builder.Append("the", 0, 1);

            // Application types
            var appChoice = new Choices();
            foreach (var item in commands)
            {
                appChoice.Add(new SemanticResultValue(item.Utterance, item.Key));
            }

            builder.Append("project", 0, 1);
            //builder.Append("in", 0, 1);

            builder.Append(new SemanticResultKey(GrammarKeys.SemanticKey_Command, appChoice));

            return builder;
        }

    }
}
