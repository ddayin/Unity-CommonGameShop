using System;
using UnityEngine;

namespace SPStudios.Tools {
    /// <summary>
    /// <para>Abstract MonoBehaviour for singletons to extend.</para>
    /// <para>Use MonoSingleton&#60;T&#62; for a public Instance reference.</para>
    /// </summary>
    public abstract class MonoSingleton : MonoBehaviour, ISingleton {
        //Error message for when multiple singletons of the same type are found.
        private const string MULTIPLE_SINGLETON_ERROR = "Attempted to create multiple singletons of the same type({0}).";
        //Ensures initialize is only called once.
        private bool _hasInit = false;

        // Registers itself to the singleton manager and ensures that there is no other singleton of this type 
        // Set to internal to warn users not to try to write an Awake function, use OnInitOrAwake() instead
        internal void Awake() {
            Initialize();
        }

        /// <summary>
        /// <para>Initializes the Singleton</para>
        /// <para>Initialize is automatically called during awake and should not need to be called ever again.</para>
        /// </summary>
        public void Initialize() {
            if(!_hasInit && !Singletons.IsRegistered(GetType())) {
                _hasInit = true;
                Singletons.RegisterSingleton(GetType(), this);
                OnInitOrAwake();
            }

            if(Singletons.Get(GetType()) != this) {
                BroadcastErrorMessage(MULTIPLE_SINGLETON_ERROR, GetType().Name.ToString());
            }
        }

        /// <summary>
        /// <para>Define all initialization behaviour within the OnInitOrAwake function.</para>
        /// <para>This is called during the singleton's Awake function or when first accessed</para>
        /// <para>Guaranteed to only be called once.</para>
        /// </summary>
        protected virtual void OnInitOrAwake() { }

        #region Error Messaging
        /// <summary>
        /// Outputs an exception or an error message
        /// </summary>
        /// <param name="errorMessage">Message to broadcast</param>
        protected void BroadcastErrorMessage(string errorMessage) {
            if(Singletons.ExceptionsEnabled) {
                throw new ArgumentException(errorMessage);
            } else {
                Debug.LogError(errorMessage, this);
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
                Debug.LogError(string.Format(errorMessage, args), this);
            }
        }
        #endregion
    }

    /// <summary>
    /// <para>Abstract MonoBehaviour for singletons to extend.</para>
    /// </summary>
    /// <typeparam name="T">The type of the class being turned into a singleton</typeparam>
    public abstract class MonoSingleton<T> : MonoSingleton where T : MonoSingleton {
        /// <summary>
        /// Public accessor of this instance
        /// </summary>
        public static T Instance {
            get {
                return Singletons.Get<T>();
            }
            set {
                Singletons.RegisterSingleton(typeof(T), value);
            }
        }
    }

    /// <summary>
    /// <para>A MonoSingleton that automatically calls DontDestroyOnLoad when created.</para>
    /// </summary>
    public abstract class PersistentMonoSingleton : MonoSingleton {
        protected override void OnInitOrAwake() {
            base.OnInitOrAwake();
            DontDestroyOnLoad(this);
        }
    }

    /// <summary>
    /// <para>A MonoSingleton that automatically calls DontDestroyOnLoad when created</para>
    /// </summary>
    /// <typeparam name="T">The type of the class being turned into a singleton</typeparam>
    public abstract class PersistentMonoSingleton<T> : MonoSingleton<T> where T : MonoSingleton {
        protected override void OnInitOrAwake() {
            base.OnInitOrAwake();
            DontDestroyOnLoad(this);
        }
    }
}