using SPStudios.Tools;
using UnityEngine;

namespace SPStudios.Examples.Singleton {
    /// <summary>
    /// An instanced singleton meant for displaying messages at the bottom of the example scene.
    /// </summary>
    public class ExampleSceneTextDisplay : MonoSingleton<ExampleSceneTextDisplay> {
        //The current message to be displayed
        private string _curMessage = string.Empty;
        private void OnGUI() {
            //If there's a message to display, display it
            if (!string.IsNullOrEmpty(_curMessage)) {
                //Create the display rect on the bottom row
                Rect displayRect = new Rect(ExampleSceneBase.SCREEN_HORIZONTAL_MARGIN, ExampleSceneBase.SCREEN_VERTICAL_MARGIN,
                    ExampleSceneBase.DivideUpScreenChunk(Screen.width, ExampleSceneBase.SCREEN_HORIZONTAL_MARGIN, 1, ExampleSceneBase.BUTTON_SPACING),
                    ExampleSceneBase.GetRowHeight());
                //Move the displayRect to the final row of the screen
                displayRect.y += (displayRect.height + ExampleSceneBase.BUTTON_SPACING) *
                                                       (ExampleSceneBase.NUM_DISPLAY_ROWS - 1);
                //Display the message
                GUI.TextArea(displayRect, _curMessage);
            }
        }

        /// <summary>
        /// Sets the display message
        /// </summary>
        /// <param name="message">The message to display</param>
        public void DisplayMessage(string message) {
            _curMessage = message;
        }
    }
}