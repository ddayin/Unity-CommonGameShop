using SPStudios.Tools; //Always include this in order to use Square Pie Studios' singletons

namespace SPStudios.Examples.Singleton {
    /// <summary>
    /// A custom logger example to demonstrate the use of user defined logger
    /// </summary>
    public class ExampleCustomLogger : ExampleLogger {
        public override void LogMessage(string message) {
            Singletons.Get<ExampleSceneTextDisplay>().DisplayMessage(message);
        }

        /// <summary>
        /// Empty's the logger's logger window
        /// </summary>
        public void EmptyLoggerWindow() {
            //Example use of MonoSingleton<T> allowing access to a public Instance property
            ExampleSceneTextDisplay.Instance.DisplayMessage(string.Empty);
        }
    }
}