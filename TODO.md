TODO:

* Object values (such as Birthday in the testbed) can serialize as null
* References (a class can be marked as [SerializeAsReference] where the first time it serializes it will serialize the whole thing and then subsequent ReferenceEqual() or Equal() will serialize an indicator that it should deserialize the same instance of the class that was previously serialized -- keep in a large internal table)