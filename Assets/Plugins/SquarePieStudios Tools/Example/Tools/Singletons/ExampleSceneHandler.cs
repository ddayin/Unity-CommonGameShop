using SPStudios.Tools; //Always include this in order to use Square Pie Studios' singletons
using System;

namespace SPStudios.Examples.Singleton {
    using LoggerType = ExampleLogger.DefaultLoggerType; //Shortcut
    /// <summary>
    /// Behaviour for controlling the example scene for demonstrating normal singleton use
    /// </summary>
    public class ExampleSceneHandler : ExampleSceneBase {
        #region Local Vars
        /// <summary> Currently selected logger type </summary>
        public LoggerType SelectedLogger = LoggerType.Log;
        /// <summary> Determines if the logger should use the custom logger or not. </summary>
        public bool UseCustomLogger = false;
        #endregion

        #region Singleton Example Functionality
        /// <summary>
        /// Sets the logger based on the requested logger type.
        /// </summary>
        /// <param name="loggerType">The requested logger type</param>
        public override void SetLoggerByType(LoggerType loggerType) {
            SelectedLogger = loggerType;
            UseCustomLogger = false;

            //Logger type is not the custom logger, so ensure the logger window is empty
            Singletons.Get<ExampleCustomLogger>().EmptyLoggerWindow();
        }

        /// <summary>
        /// Logs a message using the selected logger
        /// </summary>
        /// <param name="message">The message to be output</param>
        public override void LogMessage(string message) {
            //Use the custom logger, or use a predefined logger.
            if(UseCustomLogger) {
                Singletons.Get<ExampleCustomLogger>().LogMessage(message);
            } else {
                //This example shows three different ways to access your singletons
                switch(SelectedLogger) {
                    case LoggerType.Log:
                        //A simple Singletons.Get<TYPE>() works just great!
                        Singletons.Get<MessageLogger>().LogMessage(message);
                        break;
                    case LoggerType.Warn:
                        //If you have the type, but don't know it at compile time, you can pass the type as a paramater
                        Type loggerType = typeof(WarnLogger);
                        ((ExampleLogger)Singletons.Get(loggerType)).LogMessage(message);
                        break;
                    case LoggerType.Error:
                        //If you want to make an instanced reference, you can derive from Singleton<T>.
                        //An example of this type won't fit with the logger's design, so here's a basic example:
                        //      public class ExampleSingleton : Singleton<ExampleSingleton> { }
                        //      Allows a reference via ExampleSingleton.Instance
                        InstancedErrorLogger.Instance.LogMessage(message);
                        break;
                }
            }
        }

        /// <summary>
        /// Sets the logger to the custom logger created for this example.
        /// </summary>
        public override void SetLoggerToCustom() {
            UseCustomLogger = true;
        }
        #endregion
    }
}