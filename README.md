# ExtraLINQ

ExtraLINQ provides additional extension methods for working with .NET collections.


## Installation

ExtraLINQ is [available as a NuGet package](http://www.nuget.org/packages/ExtraLINQ):

```powershell
Install-Package ExtraLINQ
```


## Extensions

Extensions for collections of type `IEnumerable<T>`:

- [`Chunk`](#chunk)
- [`Cycle`](#cycle)
- [`Distinct`](#distinct)
- [`Flatten`](#flatten)
- [`HasExactly`](#hasexactly)
- [`HasAtMost`](#hasatmost)
- [`HasAtLeast`](#hasatleast)
- [`Intersperse`](#intersperse)
- [`IsEmpty`](#isempty)
- [`IsNullOrEmpty`](#isnullorempty)
- [`JoinedBy`](#joinedby)
- [`None`](#none)
- [`Partition`](#partition)
- [`Random`](#random)
- [`Repeat`](#repeat)
- [`Shuffle`](#shuffle)
- [`SkipEvery`](#skipevery)
- [`TakeEvery`](#takeevery)
- [`Tap`](#tap)
- [`ToHashSet`](#tohashset)
- [`WhereNot`](#wherenot)
- [`Without`](#without)

Extensions for collections of type `NameValueCollection`:

- [`ToDictionary`](#todictionary)
- [`ToKeyValuePairs`](#tokeyvaluepairs)


### Extensions for `IEnumerable<T>`


#### `Chunk`

...


#### `Cycle`

...


#### `Distinct`

...


#### `Flatten`

...


#### `HasExactly`

Determines whether a collection contains exactly a given number of items.


#### `HasAtMost`

Determines whether a collection contains at most a certain number of items.


#### `HasAtLeast`

Determines whether a collection contains at least a certain number of items.


#### `Intersperse`

Returns all elements of the collection separated by the given separator.


#### `IsEmpty`

Determines whether a collection is empty.


#### `IsNullOrEmpty`

Determines whether a collection is null or empty.


#### `JoinedBy`

...


#### `None`

Determines whether a collection doesn't contain any elements matching certain criteria.


#### `Partition`

...


#### `Random`

Returns a given number of random elements from a collection.


#### `Repeat`

...


#### `Shuffle`

Returns the items of the given collection in random order.


#### `SkipEvery`

...


#### `TakeEvery`

...


#### `Tap`

...


#### `ToHashSet`

...


#### `WhereNot`

...


#### `Without`

Returns the specified collection without the specified items.


### Extensions for `NameValueCollection`


#### `ToDictionary`

Returns a new dictionary from the specified collection.


#### `ToKeyValuePairs`

Enumerates the specified collection as a sequence of key-value pairs.
