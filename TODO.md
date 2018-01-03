TODO:

* Object values (such as Birthday in the testbed) can serialize as null
* References (a class can be marked as [SerializeAsReference] where the first time it serializes it will serialize the whole thing and then subsequent ReferenceEqual() or Equal() will serialize an indicator that it should deserialize the same instance of the class that was previously serialized -- keep in a large internal table)
	1. Read the reference number uint
	2. Lookup in table
	3. Return that value
* Look into POTENTIALLY (huge focus on POTENTIALLY) not even serializing type? If we aren't using it in as many places anymore, maybe we don't need it anywhere?
* More unit tests (so many more)