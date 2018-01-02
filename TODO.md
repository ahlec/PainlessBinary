TODO:

* Object values (such as Birthday in the testbed) can serialize as null
* References (a class can be marked as [SerializeAsReference] where the first time it serializes it will serialize the whole thing and then subsequent ReferenceEqual() or Equal() will serialize an indicator that it should deserialize the same instance of the class that was previously serialized -- keep in a large internal table)

**Reworking the item header.**
Currently we have something where we start a new item by reading its type, instantiating, and then reading from there. However, this isn't super extensible, nor good for special meaning things like null or a reference table instead of deserializing the value again.

We should change the general order to something more like the following:

1. Read byte for **serialization type**
	1. `0000` = null
	2. `0001` = regular value (what we currently have)
	3. `0010` = reference to something de/serialized earlier
	4. ... (continues as necessary whenever we have a new thing)
2. Switch on the serialization type
	1. If null, end here and return `null`
	2. If regular value:
		1. Read the type
		2. Instantiate the value
		3. Read the value and return
	3. If reference:
		1. Read the reference number uint
		2. Lookup in table
		3. Return that value