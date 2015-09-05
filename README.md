# ExtraLINQ

ExtraLINQ provides a set of extension methods for various .NET sequence types.


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

Splits the given sequence into chunks of the given size. If the sequence length isn't evenly divisible by the chunk size, the last chunk will contain all remaining elements.

```csharp
int[] numbers = { 1, 2, 3, 4, 5, 6, 7 };
int[][] chunks = numbers.Chunk(3).ToArray();

// chunks = [[1, 2, 3], [4, 5, 6], [7]]
```


#### `Cycle`

Turns a finite sequence into a circular one, or equivalently, repeats the original sequence indefinitely.

```csharp
int[] bits = { 0, 1 };
int[] alternatingBits = bits.Cycle().Take(5).ToArray();

// alternatingBits = [0, 1, 0, 1, 0]
```


#### `Distinct`

Returns distinct elements from the given sequence using the default equality comparer to compare projected values.

```csharp
string[] spellingsOfJavaScript = { "JavaScript", "Javascript", "javascript" };
string[] distinctSpellings = spellingsOfJavaScript
	.Distinct(n => n.ToLower())
	.ToArray();

// distinctSpellings = ["JavaScript"]
```


#### `Flatten`

Returns a flattened sequence that contains the concatenation of all the nested sequences' elements.

```csharp
int[][] numbers =
{
    new[] { 1, 2, 3 },
    new[] { 4, 5 },
    new[] { 6 }
};

int[] flattenedNumbers = numbers.Flatten().ToArray();

// flattenedNumbers = [1, 2, 3, 4, 5, 6]
```


#### `HasAtLeast`

Determines whether a collection contains at least a certain number of items.

```csharp
string letters = "abcd";

letters.HasAtLeast(0) // true
letters.HasAtLeast(2) // true
letters.HasAtLeast(4) // true
```

Optionally, a predicate can be passed that is called for every element:

```csharp
string[] fruits = { "apple", "apricot", "banana" };

fruits.HasAtLeast(2, fruit => fruit.StartsWith("a")) // true
fruits.HasAtLeast(3, fruit => fruit.StartsWith("a")) // false
```


#### `HasAtMost`

Determines whether a collection contains at most a certain number of items.


#### `HasExactly`

Determines whether a collection contains exactly a given number of items.


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
