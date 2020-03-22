TODO:

* Inheritance (does Goodies and Baddies, which inherit from Character, work correctly? Do inherited members work?)
* Adding support for [OnSerializing] and [OnSerialized] and such
* Do members even need a user defined order? Can we n it have some kind of deterministic ordering code-side?
* Being able to use the private/protected { set; } and private/protected constructor
* Look into POTENTIALLY (huge focus on POTENTIALLY) not even serializing type? If we aren't using it in as many places anymore, maybe we don't need it anywhere?
* More unit tests (so many more)

* Being able to process parameterised constructors
	1. When registering a type, we could pass in an optional Constructor class
		* Actually, a [BinaryConstructorManager( typeof( CharacterConstructorManager ) )]
			* This ConstructorManager must inherit from the abstract class defined in PainlessBinary, and MUST also have a parameterless constructor
			* Activator.CreateInstance() on that type, if provided?
	2. The Constructor abstract class has a special property that allows you to define additional members to serialize that will get serialized before the object is constructed (in a header)
		1. For example, you could declare that you need two custom UInt32
		2. Before deserialization, it will read two UInt32
		3. On serialization, the Constructor can be used to pull out the two UInt32 values from an existing object
	3. Reading that custom header beforehand (in a controlled way -- we never actually expose a the reader/writer to the constructor) it will then internally be allowed to use parameterised constructors