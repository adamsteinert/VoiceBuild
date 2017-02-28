using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using Vocal.Executors;
using Vocal.Model;

namespace Vocal.Core
{
    public class VocalResponse
    {
        SpeechSynthesizer _synth;

        public VocalResponse()
        {
            _synth = new SpeechSynthesizer();
        }

        public void Initialize()
        {
            _synth.SetOutputToDefaultAudioDevice();

            var voices = _synth.GetInstalledVoices();

            _synth.SelectVoice(voices[1].VoiceInfo.Name); // .SelectVoiceByHints(VoiceGender.Female);
        }


        public void Say(string text)
        {
            _synth.SpeakAsync(text);
        }

        public void Say(IVoiceCommand command)
        {
            _synth.SpeakAsync(command.ConfirmationText);
        }
    }
}
