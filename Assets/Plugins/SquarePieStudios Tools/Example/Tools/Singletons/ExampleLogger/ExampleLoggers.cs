using SPStudios.Tools; //Include this to use Square Pie Studios' Singleton class
using Debug = UnityEngine.Debug;

namespace SPStudios.Examples.Singleton {
    /// <summary>
    /// Abstract base for the example logger class
    /// </summary>
    public abstract class ExampleLogger : SPStudios.Tools.Singleton {
        /// <summary>
        /// Different logger types.
        /// </summary>
        [System.Serializable]
        public enum DefaultLoggerType {
            Log = 0,
            Warn = 1,
            Error = 2,
        }

        /// <summary>
        /// Logs a message
        /// </summary>
        /// <param name="message">Message to be logged</param>
        public abstract void LogMessage(string message);
    }

    /// <summary>
    /// ExampleLogger implementation that outputs messages via Debug.Log
    /// </summary>
    public class MessageLogger : ExampleLogger {
        public override void LogMessage(string message) {
            Debug.Log(message);
        }
    }

    /// <summary>
    /// ExampleLogger implementation that outputs messages via Debug.LogWarning
    /// </summary>
    public class WarnLogger : ExampleLogger {
        public override void LogMessage(string message) {
            Debug.LogWarning(message);
        }
    }

    /// <summary>
    /// ExampleLogger implementation that outputs messages via Debug.LogError
    /// </summary>
    public class ErrorLogger : ExampleLogger {
        public override void LogMessage(string message) {
            Debug.LogError(message);
        }
    }

    /// <summary>
    /// Created to simply demonstrate using an instanced singleton.
    /// Does not require a body, because the Instance will refer to the
    /// ErrorLogger singleton described above.
    /// </summary>
    public class InstancedErrorLogger : Singleton<ErrorLogger> { }
}