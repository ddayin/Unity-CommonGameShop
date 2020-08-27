using System;
using UnityEngine;
using System.Collections.Generic;

namespace SPStudios.Tools {
    /// <summary>
    /// Singleton interface
    /// </summary>
    public interface ISingleton {
        void Initialize();
    }

    /// <summary>
    /// Single location to access all singletons in the game
    /// </summary>
    public static class Singletons {
        #region Constants
        private const string NON_SINGLETON_TYPE = "Requested a singleton({0}) that does not implement ISingleton.";
        private const string CONFLICTING_SINGLETON_TYPE = "Attempted to assign a singleton({0}) to a type that it does not derive from({1}).";

        /* If you are seeing this error, you have attempted to retrieve an object that extends ISingleton, but could not
         * be registered to Singletons because Singletons does not have instructions on how to instantiate the requested
         * Singleton.  Either you should extend Singleton or MonoSingleton, or if this is not possible, then use
         * Singletons.RegisterSingleton to set the singleton manually.
         * 
         * You can additionally add support for your own Singleton types by following the pattern set out below with 
         * SIMPLE_SINGLETON_TYPE and MONOSINGLETON_TYPE.
         */
        private const string UNSUPPORTED_SINGLETON_TYPE = "Attempted to create an unsupported type of Singleton({0}).";

        //Cached Types for comparison purposes
        private static readonly Type ISINGLETON_TYPE = typeof(ISingleton);
        private static readonly Type SIMPLE_SINGLETON_TYPE = typeof(Singleton);
        private static readonly Type MONOSINGLETON_TYPE = typeof(MonoSingleton);

        //A name for a gameObject container that will hold onto dynamically created MonoSingletons
        private const string SINGLETON_CONTAINER_NAME = "SPStudios(Singletons)";
        //Default size of the singleton dictionary.
        private const int DEFAULT_DICTIONARY_SIZE = 10;
        //Default ExceptionsEnabled state
        private const bool DEFAULT_EXCEPTIONS_ENABLED = true;
        #endregion

        #region Static Initializer
        static Singletons() {
            SingletonContainerTransform = null;
            ExceptionsEnabled = DEFAULT_EXCEPTIONS_ENABLED;
            _singletons = new Dictionary<Type, ISingleton>(DEFAULT_DICTIONARY_SIZE);
        }
        #endregion

        #region Singletons Variables
        /// <summary>
        /// <para>Defines whether to throw an exception or log errors</para>
        /// <para>true - ArgumentExceptions are thrown for errors</para>
        /// <para>false - A Debug.LogError message is output instead for errors</para>
        /// </summary>
        public static bool ExceptionsEnabled { get; set; }
        /// <summary>
        /// A transform to hold onto all dynamically created singletons
        /// </summary>
        public static Transform SingletonContainerTransform { get; private set; }
        /// <summary>
        /// Dictionary mapping between the singleton type and the actual singleton for instant O(1) lookup
        /// </summary>
        private static Dictionary<Type, ISingleton> _singletons;
        #endregion

        #region Singleton Get Operations
        /// <summary>
        /// Either finds the existing singleton or creates it.
        /// </summary>
        /// <typeparam name="T">The type of the singleton</typeparam>
        /// <returns>The requested singleton</returns>
        public static T Get<T>() where T : ISingleton {
            return (T)Get(typeof(T));
        }
        /// <summary>
        /// Either finds the existing singleton or creates it.
        /// </summary>
        /// <param name="singletonType">The type of the singleton</param>
        /// <returns>The requested singleton</returns>
        public static ISingleton Get(Type singletonType) {
            //Ensure the type passed in is valid
            if(!ISINGLETON_TYPE.IsAssignableFrom(singletonType)) {
                BroadcastErrorMessage(NON_SINGLETON_TYPE, singletonType);
                return null;
            }

            //Check if we have cached it
            ISingleton singleton;
            if(_singletons.TryGetValue(singletonType, out singleton)) {
                //If we find it, then return it
                //If it is null, then the reference is stale and must be recreated.
                if(singleton != null) {
                    return singleton;
                }
            }

            //If we haven't cached it, create and setup the singleton based on the singleton type
            if(MONOSINGLETON_TYPE.IsAssignableFrom(singletonType)) {
                singleton = CreateMonoSingleton(singletonType);
            } else if(SIMPLE_SINGLETON_TYPE.IsAssignableFrom(singletonType)) {
                //Easy peasy create it
                singleton = (Singleton)Activator.CreateInstance(singletonType);
            } else {
                BroadcastErrorMessage(UNSUPPORTED_SINGLETON_TYPE, singletonType);
                singleton = null;
            }

            return singleton;
        }
        #endregion

        #region Singleton Creation
        /// <summary>
        /// Finds or creates a MonoSingleton of type requested.  
        /// </summary>
        private static MonoSingleton CreateMonoSingleton(Type singletonType) {
            //Check if it exists in the scene already
            MonoSingleton monoSingleton = (MonoSingleton)GameObject.FindObjectOfType(singletonType);
            if (monoSingleton == null) {
                //If it doesn't exist, create it and put it in the singleton container for nice heirarchy organization
                GameObject go = new GameObject();
                go.name = singletonType.Name.ToString();
                monoSingleton = (MonoSingleton)go.AddComponent(singletonType);

                //Make sure the dynamic singleton container exists
                if (SingletonContainerTransform == null) {
                    //Check if it exists already for some reason...
                    GameObject container = GameObject.Find(SINGLETON_CONTAINER_NAME);
                    if (container == null) {
                        //If it doens't exist, create it and cache it
                        container = new GameObject();
                        container.name = SINGLETON_CONTAINER_NAME;
                    }
                    SingletonContainerTransform = container.transform;
                }

                //Add the newly created singleton to the singleton container
                monoSingleton.transform.parent = SingletonContainerTransform;
                monoSingleton.Initialize();
                RegisterSingleton(singletonType, monoSingleton);
            }
            return monoSingleton;
        }
        #endregion

        #region Singleton Registration
        /// <summary>
        /// <para>Registers a singleton to the specified singletonType.</para>
        /// <para>Use this to set a singleton to any type that it derives from</para>
        /// </summary>
        /// <typeparam name="T">The type that will be mapped</typeparam>
        /// <param name="singleton">The singleton being registered</param>
        public static void RegisterSingleton<T>(ISingleton singleton) where T : ISingleton {
            RegisterSingleton(typeof(T), singleton);
        }
        /// <summary>
        /// <para>Registers a singleton to the specified singletonType.</para>
        /// <para>Use this to set a singleton to any type that it derives from</para>
        /// </summary>
        /// <param name="singletonType">The type that will be mapped</param>
        /// <param name="singleton">The singleton being registered</param>
        public static void RegisterSingleton(Type singletonType, ISingleton singleton) {
            //The singleton should be of the selected type or it must derive from the selected type
            if (!singletonType.IsAssignableFrom(singletonType)) {
                BroadcastErrorMessage(CONFLICTING_SINGLETON_TYPE, singleton.GetType(), singletonType);
                return;
            }

            if(_singletons.ContainsKey(singletonType)) {
                _singletons[singletonType] = singleton;
            } else {
                _singletons.Add(singletonType, singleton);
            }
        }

        /// <summary>
        /// Returns if a singleton has been registered to this type
        /// </summary>
        public static bool IsRegistered<T>() where T : ISingleton {
            return IsRegistered(typeof(T));
        }
        /// <summary>
        /// Returns if a singleton has been registered to this type
        /// </summary>
        public static bool IsRegistered(Type singletonType) {
            //Ensure the type passed in is valid
            if(!ISINGLETON_TYPE.IsAssignableFrom(singletonType)) {
                BroadcastErrorMessage(NON_SINGLETON_TYPE);
                return false;
            }

            return _singletons.ContainsKey(singletonType);
        }
        #endregion

        #region Error Messaging
        /// <summary>
        /// Broadcasts an exception or outputs an error message to the Unity Console
        /// </summary>
        /// <param name="errorMessage">Message to broadcast</param>
        private static void BroadcastErrorMessage(string errorMessage) {
            if(ExceptionsEnabled) {
                throw new ArgumentException(errorMessage);
            } else {
                Debug.LogError(errorMessage, SingletonContainerTransform);
            }
        }
        /// <summary>
        /// Broadcasts an exception or outputs an error message to the Unity Console
        /// </summary>
        /// <param name="errorMessage">Message to broadcast</param>
        /// <param name="args">Message arguments</param>
        private static void BroadcastErrorMessage(string errorMessage, params object[] args) {
            if(ExceptionsEnabled) {
                throw new ArgumentException(string.Format(errorMessage, args));
            } else {
                Debug.LogError(string.Format(errorMessage, args), SingletonContainerTransform);
            }
        }
        #endregion
    }
}