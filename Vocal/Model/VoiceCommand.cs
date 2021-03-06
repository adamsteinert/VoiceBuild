﻿using System;
using System.Linq;

namespace Vocal.Model
{
    /// <summary>
    /// Represents required properties of a command to be executed.
    /// </summary>
    public class VoiceCommand : IVoiceCommand
    {
        /// <summary>
        /// Unique identifier for the command.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// The text used to identify the command within the grammar
        /// </summary>
        public string Utterance { get; set; }

        /// <summary>
        /// Confirmation text to be repeated back before command execution.
        /// </summary>
        public string ConfirmationText { get; set; }

        /// <summary>
        /// The executable associated with the command
        /// </summary>
        public string Executable { get; set; }

        /// <summary>
        /// Directory in which to execute the command.
        /// </summary>
        public string InitialDirectory { get; set; }


        public string LauncherKey { get; set; }

        public string LaunchArgs { get; set; }

        /// <summary>
        /// Builds a command string to be run within the execution environment
        /// 
        /// Example:
        /// cd C:\source\JMC\champs && .\build.bat
        /// </summary>
        /// <returns></returns>
        public string BuildCommandString()
        {
            return @"cd " + InitialDirectory + " & " + Executable + " ";
        }
    }
}
