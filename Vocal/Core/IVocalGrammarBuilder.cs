using System.Collections.Generic;
using System.Speech.Recognition;
using Vocal.Model;

namespace Vocal.Core
{
    public interface IVocalGrammarBuilder
    {
        GrammarBuilder CreateGrammar(IEnumerable<IVoiceCommand> commands);
    }
}
