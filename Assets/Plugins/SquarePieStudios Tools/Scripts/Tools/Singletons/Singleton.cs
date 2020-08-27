using System;
using Debug = UnityEngine.Debug;

namespace SPStudios.Tools {
    /// <summary>
    /// <para>Abstract class for singletons to extend.</para>
    /// <para>Use Singleton&#60;T&#62; for a public Instance reference.</para>
    /// </summary>
    public abstract class Singleton : ISingleton {
        //Error message for when multiple singletons of the same type are found.
        private const string MULTIPLE_SINGLETON_ERROR = "Attempted to create multiple singletons of the same type({0}).";
        //Ensures initialize is only called once.
        private bool _hasInit = false;

        //Public constructor
        public Singleton() {
            Initialize();
        }

        /// <summary>
        /// <para>Initializes the Singleton</para>
        /// <para>Initialize is automatically called during new() and should not need to be called ever again.</para>
        /// </summary>
        public void Initialize() {
            if(!_hasInit && !Singletons.IsRegistered(GetType())) {
                _hasInit = true;
                Singletons.RegisterSingleton(GetType(), this);
                OnInit();
            }

            if(Singletons.Get(GetType()) != this) {
                BroadcastErrorMessage(MULTIPLE_SINGLETON_ERROR, GetType().Name.ToString());
            }
        }

        /// <summary>
        /// <para>Define all initialization behaviour within the OnInit function.</para>
        /// <para>This is called when the singleton is first accessed</para>
        /// <para>Guaranteed to only be called once.</para>
        /// </summary>
        protected virtual void OnInit() { }

        #region Error Messaging
        /// <summary>
        /// Outputs an exception or an error message
        /// </summary>
        /// <param name="errorMessage">Message to broadcast</param>
        protected void BroadcastErrorMessage(string errorMessage) {
            if(Singletons.ExceptionsEnabled) {
                throw new ArgumentException(errorMessage);
            } else {
                Debug.LogError(errorMessage);
            }
        }
        /// <summary>
        /// Outputs an exception or an error message
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="args"></param>
        protected void BroadcastErrorMessage(string errorMessage, params object[] args) {
            if(Singletons.ExceptionsEnabled) {
                throw new ArgumentException(string.Format(errorMessage, args));
            } else {
                Debug.LogError(string.Format(errorMessage, args));
            }
        }
        #endregion
    }

    /// <summary>
    /// Abstract singleton implementation
    /// </summary>
    /// <typeparam name="T">The type of the class being turned into a singleton</typeparam>
    public abstract class Singleton<T> : Singleton where T : Singleton {
        /// <summary>
        /// Public Singleton Instance Accessor
        /// </summary>
        public static T Instance {
            get {
                return Singletons.Get<T>();
            }
        } 
    }
}