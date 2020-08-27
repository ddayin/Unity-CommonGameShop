using UnityEngine;
using System.Collections.Generic;

namespace SPStudios.Examples.Singleton {
    using LoggerType = ExampleLogger.DefaultLoggerType; //Shortcut
    /// <summary>
    /// Base MonoBehaviour for controlling the example scenes for demonstrating singletons
    /// No Singleton functionality is included here.  Skip this class if you don't care
    /// how the scene is put together.
    /// </summary>
    public abstract class ExampleSceneBase : MonoBehaviour {
        #region constants
        /// <summary> The default logger type...duh! /// </summary>
        private const LoggerType DEFAULT_LOGGER = LoggerType.Log;
        private const string TEST_MESSAGE_TEMPLATE = "Output({0})";

        //GUI Spacing for the example scene
        public const float BUTTON_SPACING = 15f;
        public const float SCREEN_HORIZONTAL_MARGIN = 20f;
        public const float SCREEN_VERTICAL_MARGIN = 15f;

        public const int NUM_DISPLAY_ROWS = 3;  //2 rows of buttons 1 row for the custom log

        //A label for the custom logger button
        private readonly GUIContent _customLoggerLabel = new GUIContent("Try Custom Logger");
        /// <summary> A preconfigured dictionary of logger types and their button labels </summary>
        private readonly Dictionary<LoggerType, GUIContent> _defaultLoggerLabels = new Dictionary<LoggerType, GUIContent> {
            { LoggerType.Log, new GUIContent("Log Messages") },
            { LoggerType.Warn, new GUIContent("Warn Messages") },
            { LoggerType.Error, new GUIContent("Erorr Messages") },
        };
        /// <summary> A preconfigured list of possible test messages </summary>
        private readonly List<string> TEST_MESSAGES = new List<string> {
            "Test (1)",
            "2nd Test",
            "Test #3",
        };
        #endregion

        /// <summary> Cached button labels for test message buttons </summary>
        protected Dictionary<string, GUIContent> _messageLabels = new Dictionary<string, GUIContent>();

        private void Start() {
            //Cache the button labels for the test message buttons
            for(int i = 0; i < TEST_MESSAGES.Count; i++) {
                _messageLabels.Add(TEST_MESSAGES[i], new GUIContent(string.Format(TEST_MESSAGE_TEMPLATE, TEST_MESSAGES[i])));
            }
            SetLoggerByType(DEFAULT_LOGGER);
        }

        #region Scene GUI Functionality
        /// <summary>
        /// Just a helper function for dividing up the screen for the example scene
        /// </summary>
        public static float DivideUpScreenChunk(float fullChunkSize, float margins, int numEntries, float entrySpacing) {
            float screenChunkMinusMargins = fullChunkSize - (margins * 2);
            return (screenChunkMinusMargins - (entrySpacing * (numEntries - 1))) / numEntries;
        }

        /// <summary>
        /// Returns the width of a button to fit naturally on the screen and be properly spaced apart
        /// </summary>
        /// <param name="numButtons">The number of buttons in a row</param>
        public static float GetButtonWidth(int numButtons) {
            return DivideUpScreenChunk(Screen.width, SCREEN_HORIZONTAL_MARGIN, numButtons, BUTTON_SPACING);
        }
        /// <summary>
        /// Returns the height of a row to fit naturally on the screen and be properly spaced apart
        /// </summary>
        public static float GetRowHeight() {
            return DivideUpScreenChunk(Screen.height, SCREEN_VERTICAL_MARGIN, NUM_DISPLAY_ROWS, BUTTON_SPACING);
        }

        //Draws to the screen for the example scene
        public void OnGUI() {
            float buttonWidth = GetButtonWidth(_defaultLoggerLabels.Count + 1); //1 For each default logger and 1 for custom
            Rect buttonRect = new Rect(SCREEN_HORIZONTAL_MARGIN, SCREEN_VERTICAL_MARGIN, buttonWidth, GetRowHeight());

            //Create a button for each default logger type.
            Dictionary<LoggerType, GUIContent>.Enumerator loggerEnumerator = _defaultLoggerLabels.GetEnumerator();
            while(loggerEnumerator.MoveNext()) {
                //Create button
                if(GUI.Button(buttonRect, loggerEnumerator.Current.Value)) {
                    //If pressed, set the logger type
                    SetLoggerByType(loggerEnumerator.Current.Key);
                }
                //Move along horizontally to place the next button
                buttonRect.x += buttonRect.width + BUTTON_SPACING;
            }
            //Create a button allowing the user to select a "custom" logger
            if(GUI.Button(buttonRect, _customLoggerLabel)) {
                //If pressed, set the logger to use the custom logger type
                SetLoggerToCustom();
            }

            //Reset to the next line
            buttonRect.x = SCREEN_HORIZONTAL_MARGIN;
            buttonRect.width = GetButtonWidth(TEST_MESSAGES.Count);
            buttonRect.y += buttonRect.height + BUTTON_SPACING;

            //Create a button for each test message
            Dictionary<string, GUIContent>.Enumerator messageEnumerator = _messageLabels.GetEnumerator();
            while(messageEnumerator.MoveNext()) {
                //Create button
                if(GUI.Button(buttonRect, messageEnumerator.Current.Value)) {
                    //If pressed, output the message.
                    LogMessage(messageEnumerator.Current.Key);
                }
                //Move along horizontally to place the next button
                buttonRect.x += buttonRect.width + BUTTON_SPACING;
            }
        }
        #endregion

        #region Abstract Singleton Example Functionality
        /// <summary>
        /// Sets the logger based on the requested logger type.
        /// </summary>
        /// <param name="loggerType">The requested logger type</param>
        public abstract void SetLoggerByType(LoggerType loggerType);

        /// <summary>
        /// Logs a message using the selected logger
        /// </summary>
        /// <param name="message">The message to be output</param>
        public abstract void LogMessage(string message);

        /// <summary>
        /// Sets the logger to the custom logger created for this example.
        /// </summary>
        public abstract void SetLoggerToCustom();
        #endregion
    }
}