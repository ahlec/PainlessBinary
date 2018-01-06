# PainlessBinary #

**PainlessBinary** (previously **SonezakiMasaki**) was a project originally started in order to support data serialization and deserialization for my closed source Pokémon Kristall engine. It is a library that is designed to make serialization and deserialization to and from custom binary files easy on the programmer and to allow them to focus only on the stuff they want to be working on.

## History ##

Long ago in the before-time, I was manually writing all of my serialization and deserialization code, whether it be to XML or, later, to binary files (like I said, "the before-time"). This was such a *PAIN* though. When something changed, I needed to make sure that I updated the serialization and deserialization code, and there were so many places that everything could go wrong and topple over on me. And debugging *those* kinds of problems was an absolute PAIN.

Then, I discovered how easy it was to use [Json.NET](https://github.com/JamesNK/Newtonsoft.Json). Suddenly, all you needed to do was mark up a couple of fields in a class that you wanted to serialize, and it would take care of the rest. It was game changing, and I couldn't go back to doing it the old way. But I still wanted to use binary files to store my data. Wouldn't it be amazing though if there were something out there that let me serialize to binary but be as easy to use as Json.NET? And it was there that a niche was found.

## Usage ##

The idea behind PainlessBinary is that you provide simple markup for any data type that you want to serialize, register it as a serializable type, and then do nothing else. The process is supposed to be minimally intrusive for the end user programmer, and to read easily while still allowing for control.

All that the end user needs to do is first mark up their class.
```
[BinaryDataType]
public sealed class Character
{
    [BinaryMember( 1 )] // First property serialized
    public string Name { get; set; }

    [BinaryMember( 2 )] // Second property
    public int Age { get; set; }

    [BinaryMember( 3 )] // Third
    public bool IsFriendly { get; set; }

    [BinaryMember( 4 )] // Fourth
    public List<Item> Inventory { get; set; } = new List<Item>();
}
```

And then they need to register their custom data type with the library.
```
TypeRegistry typeRegistry = new TypeRegistry();
typeRegistry.RegisterType( typeof( Character ) );
```

After that, we're all good to go! That's all that's necessary to get us up and running (and not even in a this-works-fine-for-a-demo-but-not-how-we-should-do-it way)! We're free to go ahead and serialize and deserialize to our heart's content!
```
Serializer serializer = new Serializer( typeRegistry );
using ( Stream stream = File.OpenRead( "shopkeeper.txt" ) )
{
    Character shopkeeper = serializer.DeserializeFile<Character>( stream ).Payload;
    ...
}
```
