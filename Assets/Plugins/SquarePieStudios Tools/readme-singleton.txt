NOTE: Don't forget to include "using SPStudios.Tools;" at the start of your file to have access to the singleton functionality

Creating and using singletons is simple and straightforward.  Here are some simple examples.  More complex examples can be found
in the Examples/Tools/Singletons folder.
		
1. Singleton
	ExampleImplementation:
		public class ExampleSingleton : Singleton {
			public void ExampleFunction_ExampleSingleton() { }
		}
	 
	 Use:
		a. Singletons.Get<ExampleSingleton>().ExampleFunction_ExampleSingleton();
		b. ((ExampleSingleton)Singletons.Get(typeof(ExampleSingleton))).ExampleFunction_ExampleSingleton();

	Notes:
		a. This is the basic singleton class.  It does not include an Instance reference.
        b. To make a MonoBehaviour a singleton, extend MonoSingleton instead.
		c. Extend your class with PersistentMonoSingleton instead to ensure your MonoSingleton
			isn't destroyed when a new level is loaded
		
2. Singleton<T>
	ExampleImplementation
		public class ExampleSingleton_WithInstance : Singleton<ExampleSingleton_WithInstance> {
			public void ExampleFunction_InstancedVersion() { }
		}
	 
	 Use:
		a. Singletons.Get<ExampleSingleton_WithInstance>().ExampleFunction_InstancedVersion();
        b. ((ExampleSingleton_WithInstance)Singletons.Get(typeof(ExampleSingleton_WithInstance))).ExampleFunction_InstancedVersion();
        c. ExampleSingleton_WithInstance.Instance.ExampleFunction_InstancedVersion();
	
	Notes:
		a. This is the basic singleton class.  By including the name of the singleton
			within the brackets of the Singleton extension, it adds a public Instance accessor.
        b. Same rules apply from above with the use of MonoSingleton and PersistentMonoSingleton.

While Singleton<T> can be very convenient for simple use cases.  The normal setup can be used to support singleton
dependency injection by leveraging the Singletons' RegisterSingleton functionality.  An example on how to use dependency
injection with Singletons can be examined within the ExampleLoggerHandler.  While the design of the logger
is a bit ridiculous, it is a great example to demonstrate the power and versatility of using this sort of polymorphism.

If you have any questions, contact me at SquarePieStudios@gmail.com