﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Vocal.Executors;
using Vocal.Core;
using Vocal.Model;

namespace Vocal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, IVoiceCommand> _enabledCommands;
        SpeechRecognitionEngine _recognizer;
        VocalResponse _responder;

        Dictionary<string, LauncherDefinition> _launchers = new Dictionary<string, LauncherDefinition>();

        public MainWindow()
        {
            InitializeComponent();
            _recognizer = new SpeechRecognitionEngine();
            _enabledCommands = new Dictionary<string, IVoiceCommand>();
            _responder =  new VocalResponse();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RecognizedText.Text = "Listening...";
            _recognizer.RecognizeAsync();
        }

        #region -- Execution Commands --

      
        public void ExecuteCommandInList(string executionId)
        {
            if(!_enabledCommands.ContainsKey(executionId))
            {
                CurrentExecution.Text = "Unrecognized execution id " + executionId;
            }



            var executor = new GenericCommandWindowLauncher(Launcher.CreateDefaultCmd());


            executor.Execute(_enabledCommands[executionId]);
        }

        private ILauncher GetLauncher()

        #endregion


        #region -- Recognition -- 

        public void InitializeRecognizer()
        {
            _recognizer.SetInputToDefaultAudioDevice();
            _recognizer.RecognizeCompleted += _recognizer_RecognizeCompleted;

            ReloadCommandsAndGrammar();
        }


        private void _recognizer_RecognizeCompleted(object sender, RecognizeCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                RecognizedText.Text = e.Result.Text;

                var semantics = e.Result.Semantics;
                var commandKey = semantics[GrammarKeys.SemanticKey_Command].Value.ToString();

                _responder.Say(_enabledCommands[commandKey].ConfirmationText);

                System.Threading.Thread.Sleep(1000);

                System.Diagnostics.Debug.WriteLine("Found semantic key command" + commandKey);

                if (!IsTestOnlyMode)
                {
                    ExecuteCommandInList(commandKey);
                }
            }
            else
            {
                RecognizedText.Text = "I did not understand your utterance";
            }
        }

        #endregion

        private bool IsTestOnlyMode
        {
            get
            {
                if (!cbTestOnlyMode.IsChecked.HasValue)
                    cbTestOnlyMode.IsChecked = false;

                return cbTestOnlyMode.IsChecked.Value;
            }
        }


        private void ReloadCommandsAndGrammar()
        {
            var commandDef = ReloadVoiceCommands(_enabledCommands);
            CreateLaunchers(commandDef);

            RebuildGrammar(_recognizer, _enabledCommands);
        }

        private void CreateLaunchers(CommandDefinition commandDef)
        {
            _launchers.Clear();

            foreach (var item in commandDef.Launchers)
            {
                _launchers.Add(item.Key, item);
            }
        }

        public static void RebuildGrammar(SpeechRecognitionEngine engine, Dictionary<string, IVoiceCommand> commandDictionary)
        {
            var builder = new BuildOnlyGrammarBuilder().CreateGrammar(commandDictionary.Values);
            engine.UnloadAllGrammars();
            engine.LoadGrammar(new Grammar(builder));
        }

        public static CommandDefinition ReloadVoiceCommands(Dictionary<string, IVoiceCommand> commandDictionary)
        {
            try
            {
                commandDictionary.Clear();
                var definition = JsonCommandLoader.LoadCommandsFromConfiguration();

                foreach (var item in definition.Commands)
                {
                    commandDictionary.Add(item.Key, item);
                }

                return definition;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"There was an error loading voice commands: {ex.Message}", "Loading Error");
                return null;
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _responder.Initialize();
                ReloadCommandsAndGrammar();
                InitializeRecognizer();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"There was an error loading the application: {ex.Message}", "Load error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }
    }
}
