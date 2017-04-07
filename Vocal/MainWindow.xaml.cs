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
        Dictionary<string, Project> _projects;
        SpeechRecognitionEngine _recognizer;
        VocalResponse _responder;


        public MainWindow()
        {
            InitializeComponent();
            _recognizer = new SpeechRecognitionEngine();
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
            //if(!_enabledCommands.ContainsKey(executionId))
            //{
            //    CurrentExecution.Text = "Unrecognized execution id " + executionId;
            //}

            //var command = _enabledCommands[executionId];
            //var launcher = GetLauncher(command);
            //launcher.Execute(command);
        }

        //private ILauncher GetLauncher(IVoiceCommand command)
        //{
        //    Uri uriResult;

        //    if (command == null)
        //        throw new ArgumentNullException("command");

        //    // Is user configured
        //    if (_launchers.ContainsKey(command.LauncherKey))
        //    {
        //        return new GenericProcessLauncher(_launchers[command.LauncherKey]);
        //    }
        //    // Is regular URL
        //    else if (Uri.TryCreate(command.LaunchTarget, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
        //    {
        //        return new DefaultBrowserLauncher();
        //    }
        //    // Default
        //    else
        //    {
        //        return new CommandConsoleLauncher(EmbeddedLauncher.CreateDefaultCmd());
        //    }
        //}

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



                //TODO: enable cancellation
                Task.Run(() =>
                {
                    _responder.Say("Confirming action, no custom text is implemented yet.");
                });

                
                System.Threading.Thread.Sleep(2000);

                System.Diagnostics.Debug.WriteLine("Found semantic key command" + commandKey);

                if (!IsTestOnlyMode)
                {
                    ExecuteCommandInList(commandKey);
                }
            }
            else
            {
                RecognizedText.Text = "I was not able to understand what was just asked.";
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
            //var commandDef = ReloadVoiceCommands(_enabledCommands);
            //CreateLaunchers(commandDef);

            //RebuildGrammar(_recognizer, _enabledCommands);
        }

        public static void RebuildGrammar(SpeechRecognitionEngine engine, Dictionary<string, IVoiceCommand> commandDictionary)
        {
            var builder = new BuildOnlyGrammarBuilder().CreateGrammar(commandDictionary.Values);
            engine.UnloadAllGrammars();
            engine.LoadGrammar(new Grammar(builder));
        }

        public static ProjectConfiguration ReloadVoiceCommands(Dictionary<string, Project> projectDictionary)
        {
            try
            {
                projectDictionary.Clear();
                var definition = JsonProjectLoader.LoadCommandsFromConfiguration();

                foreach (var item in definition.Projects)
                {
                    projectDictionary.Add(item.ProjectName, item);
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
