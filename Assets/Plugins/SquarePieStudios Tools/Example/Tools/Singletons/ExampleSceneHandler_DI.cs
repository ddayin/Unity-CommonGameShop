using SPStudios.Tools; //Always include this in order to use Square Pie Studios' singletons
using System;

namespace SPStudios.Examples.Singleton {
    using LoggerType = ExampleLogger.DefaultLoggerType; //Shortcut
    /// <summary>
    /// Behaviour for controlling the example scene for demonstrating singletons with singleton dependency injection
    /// </summary>
    public class ExampleSceneHandler_DI : ExampleSceneBase {
        #region Singleton Example Functionality
        /// <summary>
        /// Sets the logger based on the requested logger type.
        /// </summary>
        /// <param name="loggerType">The requested logger type</param>
        public override void SetLoggerByType(LoggerType loggerType) {
            //This example shows three different ways to access your singletons
            switch(loggerType) {
                case LoggerType.Log:
                    //A simple Singletons.Get<TYPE>() works just great!
                    SetLogger(Singletons.Get<MessageLogger>());
                    break;
                case LoggerType.Warn:
                    //If you have the type, but don't know it at compile time, you can pass the type as a paramater
                    Type type = typeof(WarnLogger);
                    SetLogger((ExampleLogger)Singletons.Get(type));
                    break;
                case LoggerType.Error:
                    //If you want to make an instanced reference, you can derive from Singleton<T>.
                    //An example logger won't fit with the current design, so here's a basic example:
                    //      public class ExampleSingleton : Singleton<ExampleSingleton> { }
                    //      Allows a reference via ExampleSingleton.Instance
                    SetLogger(InstancedErrorLogger.Instance);
                    break;
            }

            //Logger type is not the custom logger, so ensure the logger window is empty
            Singletons.Get<ExampleCustomLogger>().EmptyLoggerWindow();
        }

        /// <summary>
        /// Logs a message using the selected logger
        /// </summary>
        /// <param name="message">The message to be output</param>
        public override void LogMessage(string message) {
            Singletons.Get<ExampleLogger>().LogMessage(message);
        }

        /// <summary>
        /// Sets the logger to the custom logger created for this example.
        /// </summary>
        public override void SetLoggerToCustom() {
            SetLogger(Singletons.Get<ExampleCustomLogger>());
        }
        #endregion

        /// <summary>
        /// Sets the logger to message with
        /// </summary>
        private static void SetLogger(ExampleLogger logger) {
            Singletons.RegisterSingleton<ExampleLogger>(logger);
        }
    }
}