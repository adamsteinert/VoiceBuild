using System;
using System.Linq;

namespace Vocal.Model
{
    public interface IVoiceCommand
    {
        /// <summary>
        /// Builds a command string to be run within the execution environment
        /// 
        /// Example:
        /// cd C:\source\JMC\champs && .\build.bat
        /// </summary>
        /// <returns></returns>
        string BuildCommandString();

        /// <summary>
        /// Confirmation text to be repeated back before command execution.
        /// </summary>
        string ConfirmationText { get; set; }
        /// <summary>
        /// The executable associated with the command
        /// </summary>
        string Executable { get; set; }
        /// <summary>
        /// Unique identifier for the command.
        /// </summary>
        string Key { get; set; }
        /// <summary>
        /// Directory in which to execute the command.
        /// </summary>
        string InitialDirectory { get; set; }
        /// <summary>
        /// The text used to identify the command within the grammar
        /// </summary>
        string Utterance { get; set; }
    }
}
