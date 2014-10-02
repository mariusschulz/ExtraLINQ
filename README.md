# ExtraLINQ

ExtraLINQ provides additional extension methods for working with .NET collections.


## Extensions

Extensions for collections of type `IEnumerable<T>`:

- `CountsExactly` — Determines whether a collection contains exactly a certain number of items.
- `CountsMax` — Determines whether a collection contains at most a certain number of items.
- `CountsMin` — Determines whether a collection contains at least a certain number of items.
- `Intersperse` — Returns all elements of the collection separated by the given separator.
- `IsEmpty` — Determines whether a collection is empty.
- `IsNullOrEmpty` — Determines whether a collection is null or empty.
- `None` — Determines whether a collection doesn't contain any elements matching certain criteria.
- `Random` — Returns a given number of random elements from a collection.
- `Shuffle` — Returns the items of the given collection in random order.
- `Without` — Returns the specified collection without the specified items.

Extensions for collections of type `NameValueCollection`:

- `ToDictionary` — Returns a new dictionary from the specified collection.
- `ToKeyValuePairs` — Enumerates the specified collection as a sequence of key-value pairs.


## Installation

ExtraLINQ is [available as a NuGet package](http://www.nuget.org/packages/ExtraLINQ):

    Install-Package ExtraLINQ
