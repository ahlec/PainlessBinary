TODO:

* Inheritance (does Goodies and Baddies, which inherit from Character, work correctly? Do inherited members work?)
* Adding support for [OnSerializing] and [OnSerialized] and such
* Do members even need a user defined order? Can we n it have some kind of deterministic ordering code-side?
* Being able to use the private/protected { set; } and private/protected constructor
* Look into POTENTIALLY (huge focus on POTENTIALLY) not even serializing type? If we aren't using it in as many places anymore, maybe we don't need it anywhere?
* More unit tests (so many more)
