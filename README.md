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


#### `Each`

Passes every element of the sequence to the specified action and returns it afterwards.

```csharp
string[] ringInscriptionLines =
{
    "One Ring to rule them all",
    "One Ring to find them",
    "One Ring to bring them all",
    "and in the darkness bind them"
};

ringInscriptionLines.Each(Console.WriteLine);

// Console output:
//
// One Ring to rule them all
// One Ring to find them
// One Ring to bring them all
// and in the darkness bind them
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
string[] theThreeRings = { "Narya", "Nenya", "Vilya" };

theThreeRings.HasAtLeast(0) // true
theThreeRings.HasAtLeast(2) // true
theThreeRings.HasAtLeast(4) // false
```

Optionally, a predicate can be passed that is called for every element:

```csharp
string[] theThreeRings = { "Narya", "Nenya", "Vilya" };

theThreeRings.HasAtLeast(2, ring => ring.StartsWith("N")) // true
theThreeRings.HasAtLeast(3, ring => ring.StartsWith("N")) // false
```


#### `HasAtMost`

Determines whether a collection contains at most a certain number of items.

```csharp
string[] theThreeRings = { "Narya", "Nenya", "Vilya" };

theThreeRings.HasAtMost(2) // false
theThreeRings.HasAtMost(3) // true
theThreeRings.HasAtMost(4) // true
```

Optionally, a predicate can be passed that is called for every element:

```csharp
string[] theThreeRings = { "Narya", "Nenya", "Vilya" };

theThreeRings.HasAtMost(1, ring => ring.StartsWith("N")) // false
theThreeRings.HasAtMost(2, ring => ring.StartsWith("N")) // true
```


#### `HasExactly`

Determines whether a collection contains exactly a given number of items.

```csharp
string[] theThreeRings = { "Narya", "Nenya", "Vilya" };

theThreeRings.HasExactly(2) // false
theThreeRings.HasExactly(3) // true
theThreeRings.HasExactly(4) // false
```

Optionally, a predicate can be passed that is called for every element:

```csharp
string[] theThreeRings = { "Narya", "Nenya", "Vilya" };

theThreeRings.HasExactly(1, ring => ring.StartsWith("N")) // false
theThreeRings.HasExactly(1, ring => ring.StartsWith("V")) // true
theThreeRings.HasExactly(2, ring => ring.StartsWith("N")) // true
```


#### `Intersperse`

Returns all elements of the collection separated by the given separator.

```csharp
int[] numbers = { 1, 2, 3, 4, 5 };
int[] separatedNumbers = numbers.Intersperse(0).ToArray();

// separatedNumbers = [1, 0, 2, 0, 3, 0, 4, 0, 5]
```


#### `IsEmpty`

Determines whether a collection is empty.

```csharp
new int[0].IsEmpty() // true
new[] { 1, 2, 3 }.IsEmpty() // false
```


#### `IsNullOrEmpty`

Determines whether a collection is null or empty.

```csharp
(null as int[]).IsNullOrEmpty() // true
new int[0].IsNullOrEmpty() // true
new[] { 1, 2, 3 }.IsNullOrEmpty() // false
```

#### `JoinedBy`

Concatenates all items of a sequence using the specified separator between each item.

```csharp
string[] nameParts = { "The", "One", "Ring" };
string ringName = nameParts
    .Select(part => part.ToUpper())
    .JoinedBy(" ");

// ringName = "THE ONE RING"
```

Note that the main purpose of `JoinedBy` is to provide a chainable wrapper around `String.Join`.


#### `None`

Determines whether a collection doesn't contain any elements matching certain criteria.

```csharp
string[] theThreeRings = { "Narya", "Nenya", "Vilya" };
bool allRingsNamed = theThreeRings.None(string.IsNullOrWhiteSpace);

// allRingsNamed = true
```

The `None` method is equivalent to a negated version of `Any`.


#### `Partition`

Uses the given predicate to partition the given sequence into two sequences, one with all the matches and one with all the mismatches.

```csharp
int[] numbers = { 1, 2, 3, 4, 5 };
var partitionedNumbers = numbers.Partition(n => n % 2 == 0);

// partitionedNumbers.Matches = [2, 4]
// partitionedNumbers.Mismatches = [1, 3, 5]
```


#### `Random`

Returns a given number of random elements from a collection.

```csharp
int[] numbers = Enumerable.Range(1, 49).ToArray();
int[] lottoNumbers = numbers
    .Random(6)
    .OrderBy(n => n)
    .ToArray();

// e.g. lottoNumbers = [5, 19, 20, 27, 38, 41]
```


#### `Repeat`

Repeats a given sequence a given number of times.

```csharp
string[] eatingSounds = { "om", "nom", "nom" };
string[] cookieMonsterSounds = eatingSounds.Repeat(3).JoinedBy(" ");

// cookieMonsterSounds = ["om", "nom", "nom", "om", "nom", "nom", "om", "nom", "nom"]
```


#### `Shuffle`

Enumerates the specified input sequence and returns a new sequence which contains all input elements in random order.

```csharp
string[] hobbits = { "Frodo", "Sam", "Merry", "Pippin" };
string[] shuffledHobbits = hobbits.Shuffle().ToArray();

// e.g. shuffledHobbits = ["Sam", "Pippin", "Frodo", "Merry"]
```


#### `SkipEvery`

...


#### `TakeEvery`

...




#### `ToHashSet`

Creates a `HashSet` from a given sequence.

```csharp
string gollumsUtterings = "Nasty hobbitses, gollum, gollum!";
HashSet<string> gollumsVocabulary = gollumsUtterings
    .Split(new[] { ' ', ',', '!' }, StringSplitOptions.RemoveEmptyEntries)
    .ToHashSet();

// gollumsVocabulary = ["Nasty", "hobbitses", "gollum"]
```

Note that the main purpose of `ToHashSet` is to provide a chainable wrapper around the `HashSet` constructor.


#### `WhereNot`

Filters a sequence of values based on a given predicate and returns those values that don't match the predicate.

```csharp
string[] theThreeRings = { "Narya", "Nenya", "Vilya" };
Func<string, bool> startsWithN = value => value.StartsWith("N");
string vilya = theThreeRings.WhereNot(startsWithN).Single();

// vilya = "Vilya"
```

#### `Without`

Returns the specified collection without the specified items.

```csharp
string[] hobbits = { "Frodo", "Sam", "Merry", "Pippin" };
string[] mainHobbits = hobbits.Without("Merry", "Pippin").ToArray();

// mainHobbits = ["Frodo", "Sam"]
```


### Extensions for `NameValueCollection`


#### `ToDictionary`

Returns a new dictionary from the specified collection.

```csharp
var ringBearers = new NameValueCollection
{
    { "Nenya", "Galadriel" },
    { "Narya", "Gandalf" },
    { "Vilya", "Elrond" }
};

Dictionary<string, string> ringBearersDictionary = ringBearers.ToDictionary();
```


#### `ToKeyValuePairs`

Enumerates the specified collection as a sequence of key-value pairs.

```csharp
var ringBearers = new NameValueCollection
{
    { "Nenya", "Galadriel" },
    { "Narya", "Gandalf" },
    { "Vilya", "Elrond" }
};

IEnumerable<KeyValuePair<string, string>> ringBearersKeyValuePairs = ringBearers.ToKeyValuePairs();
```
