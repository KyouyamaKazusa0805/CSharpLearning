# C# 3 之查询表达式（八）：LINQ 里的聚合函数

## Part 1 什么是聚合函数？

在 LINQ 的使用过程之中，有一些 LINQ 提供的额外函数可以用来获取这些数据里的属性信息，例如求得一个整数集合 `IEnumerable<int>` 的最大值、平均值，或是获取总个数等等。这些函数并不是我们使用查询表达式而能够达到的基本语义，而必须使用这样的函数来达成我们需要的目的。我们把这种函数称为**聚合函数**（Aggregate Function）。

那么，LINQ 为我们提供了哪一些聚合函数呢？我们下面就来看看它们。

## Part 2 聚合函数使用一览

### 2-1 `Max` 和 `Min` 方法

显然，这两个方法用来求 `IEnumerable<数值类型>` 的最大值的。

```c#
int max = selection.Max(); // Get the maximum value.
int min = selection.Min(); // Get the minimum one.
```

它还有一堆重载方法。都是看的集合内元素的类型，进而去确定与之匹配的重载方法。

### 2-2 `Average` 方法

这个用法和求最大最小值没有差别。

```c#
double result = selection.Average();
```

当然，请注意，这个返回值一定是 `double` 类型的。虽然 C# 提供了 `decimal` 类型，会更加精确，但 LINQ 里的求平均值用的是 `double`。

另外，它也有一堆重载方法。也是和上面的思路一样。

### 2-3 `Count` 方法和 `Any` 无参方法

这个方法用于获取一个 `IEnumerable<T>` 集合里到底有多少个元素。

```c#
int count = selection.Count();
```

注意，由于 LINQ 的实现用的是扩展方法，所以 `Count` 在这里是一个方法名，而不是属性，所以必须要加上这一对小括号。另外，如果你需要使用这个数值来判断一个集合是否有元素的话，一般可以写成

```c#
bool existAnyElement = selection.Count() != 0;
```

不过，我建议你使用 `Any` 方法（无参的这个重载），来判断集合是否有元素。

```c#
bool existAnyElement = selection.Any();
```

### 2-4 存在量词 `Any` 函数和全称量词 `All` 函数

接着是两个比较常用的聚合函数，它们用于判断集合里是否有元素满足指定条件，或者是全部元素都满足指定条件。

举个例子。我们要查询集合里是否所有元素都小于 7，它的写法是这样的：

```c#
bool allLessThanEight = selection.All(v => v <= 8);
```

而如果查找集合里是否存在任意一个元素，它的数值小于 8，那么写作

```c#
bool anyValueLessThanEight = selection.Any(v => v <= 8);
```

> 注意不要把 Lambda 运算符 `=>` 和小于等于符号 `<=` 以及大于等于符号 `>=` 看混了。

刚才也说过，`Any` 的无参重载表示的是这个集合是否包含元素。所以这里就不提及了。

### 2-5 `Concat` 函数

这个函数用于拼接两个元素类型一致的 `IEnumerable<T>` 集合。

```c#
IEnumerable<int> l1 = new List<int> { 3, 8, 1, 6, 5 };
IEnumerable<int> l2 = new Stack<int> { 4, 7, 2, 9, 0 };

// Call the function `Concat`.
var result = l1.Concat(l2);

// Use the result.
foreach (var v in result)
{
    Console.WriteLine(v);
}
```

需要注意的是，两个集合即使类型不是一致的，但如果都实现了 `IEnumerable<T>` 接口的话，那么第 1、2 行给出的写法就一定奏效（隐式转换），然后调用 `Concat` 方法一定是可以的。但是，迭代器将会取决于具体类型的实现。比如例子给出的实现里，第二个则用的是一个栈（先进后出集合），所以最终拼接并遍历出来的结果顺序必然是 3、8、1、6、5、0、9、2、7、4，即后五个新加入的元素的顺序应倒过来。

### 2-6 `Cast` 函数

如果我们有一个 `IEnumerable` 集合，知道它的元素的类型，却发现我们无法使用 LINQ 函数，怎么办？我们可以调用该方法将其转换为泛型集合 `IEnumerable<T>` 对象。例如：

```c#
IEnumerable helloStr = new List<string> { "Hello", ",", " ", "world", "!" };

// Call the function `Cast`.
IEnumerable<string> result = helloStr.Cast<string>();
```

注意，为这个方法添加泛型参数，来表示这个集合需要转换为这个类型。当然，如果你尝试把它转换为其它类型，那么只会出现报错，提示转换失败；而且，`IEnumerable` 接口在迭代的时候，每一个元素都是 `object` 类型。如果内部存放的是值类型的话，将会产生拆箱操作。

### 2-7 `Distinct` 函数

这个函数类似于 SQL 语句里的 `distinct` 关键字，它用来筛选集合里的重复数据。重复的项将会只保留一个，但前提是，这个集合的元素类型必须是可以比较相等操作的（即要么它本身实现了 `IEquatable<T>` 或 `IEquatable` 接口，要么就手动规定比较规则，即使用 `IEqualityComparer<T>` 或 `IEqualityComparer` 接口）。

例如，我们要对一个集合去重，那么写法如下。

```c#
IEnumerable<int> values = new List<int> { 3, 2, 1, 1, 2, 2, 2, 3, 3, 1, 2, 2 };

// Call the function `Distinct`.
// `result` will include only three values: 3, 2 and 1.
IEnumerable<int> result = values.Distinct();
```

### 2-8 `Range` 函数（静态函数）

这个函数用于生成一个 `IEnumerable<int>` 集合，且集合从指定数值增大，一次一个单位。

```c#
IEnumerable<int> values = Enumerable.Range(0, 9);
```

这样将会获取 0 到 8，一共 9 个元素。第一个参数表示起始数值，第二个参数表示一共要取出多少个元素。

注意，这个函数是静态函数，所以调用的时候，请使用 `Enumerable.Range` 的方式调用。

### 2-9 `Repeat` 函数（静态函数）

`Repeat` 函数和 `Range` 不同，它虽然也是静态函数，但生成的集合并不是按序增大的，而是重复的。比如：

```c#
IEnumerable<string> strings = Enumerable.Repeat("I like programming.", 15);
```

这样将会生成一个拥有 15 个元素的、且元素全都是 `I like programming` 字符串的字符串列表。

### 2-10 `Take` 和 `Skip` 函数

`Take` 函数用于获取集合的前多少个元素（类似于 SQL 的 `top` 关键字），而 `Skip` 函数则用于跳过指定个数的元素。

```c#
IEnumerable<int> values = new List<int> { 3, 2, 1, 1, 2, 2, 2, 3, 3, 1, 2, 2 };

IEnumerable<int> r1 = values.Take(3); // 3, 2, 1
IEnumerable<int> r2 = values.Skip(3).Take(2); // 1, 2
```

需要注意的是，`Skip` 函数返回值依然是 `IEnumerable<int>` 类型，这样做的好处是可以继续使用成员访问运算符向下继续串联代码，而使用它所得到的集合，一定是从跳过了指定个数的元素后，作为第一项开始的新集合 `IEnumerable<int>`。换句话说，`values.Skip(3)` 将会返回的是 1、2、2、2、3、3、1、2、2 序列。

### 2-11 `ToArray` 和 `ToList` 函数

这两个函数用于将集合转换为数组 `T[]` 和列表集合 `List<T>`。

```c#
var values = new Queue<string> { "hello", ",", " ", "world", "!" };

string[] r1 = values.ToArray();
List<string> r2 = values.ToList();
```

当然，LINQ 还提供了 `ToDictionary` 和 `ToHashSet` 函数，用于转字典和哈希表对象。这种转换就不在这里提及了，因为用得比较少，而且学习起来不太难。还有一个 `ToLookUp` 方法，用于生成仅用于 LINQ 的类似于字典的分组表集合，这种集合专门用来存放分组数据，表示一对多的映射关系。

### 2-12 `First(OrDefault)` 和 `Single(OrDefault)` 函数

`First` 方法有两个重载。一个是无参的，表示取出集合的第一个元素；第二个是有一个 Lambda 表达式参数的，表示获取满足指定条件的第一个元素。但是，这两个方法都有一个问题。当元素全部不满足要求的时候，运行将会抛出 `InvalidOperationException` 异常。

显然，这样做是容易出错的，于是我们提供了新方法：`FirstOrDefault`，它的操作和上一个一样，但当找不到元素的时候的行为不同。`FirstOrDefault` 方法在找不到满足要求的元素的时候，将会返回和元素的类型完全一样的这个类型的默认数值。比如一个集合是 `int` 类型的元素构成的，那么当找不到元素时，将会返回 `default(int)`，即 0。

`Single` 和 `SingleOrDefault` 方法的行为则不同。它们除了计算整个集合满足要求的元素以外，还要要求整个集合只能有一个元素满足要求。`Single` 方法调用时，如果集合有超过一个元素满足该要求，则会直接抛出异常，而 `SingleOrDefault` 方法被调用时，如何有集合超过一个元素满足要求时，则会返回和集合元素类型一致的类型的默认值 `default(T)`。

### 2-13 `Last(OrDefault)` 函数

当然了，这两个函数和上述用法基本一样，只是用来获取集合最后一个满足要求的元素。当然，找不到的话，`Last` 会产生 `InvalidOperationException`，而 `LastOrDefault` 将会返回默认数值 `default(T)`。

### 2-14 `ElementAt` 函数

这个函数你完全可以理解为 `IEnumerable<T>` 集合类型的索引器。这里就不举例子了。

### 2-15 `Aggregate` 函数

最后讲一个比较常见的聚合函数。把 aggregate 这个单词直译过来，实际上就是“聚合”的意思。真·聚合函数！

这个函数将每一次集合遍历过程的时候，通过指定的公式进行计算和处理，并得到最终的结果的行为就是这个函数的使用方式了。它有三个重载。

第一个重载是非常简单的，除了扩展方法的 `IEnumerable<T>` 集合自己，只需要给出一个额外参数，这个参数是一个 Lambda 表达式，表示一个聚合公式，把所有元素按照这个公式计算的结果聚合起来。

```c#
var values = new List<string> { "hello", ",", " ", "world", "!" };

var result = values.Aggregate((s, v) => $"{s}{v}");
Console.WriteLine(result);
```

可以看懂这个聚合函数的意义吗？这个函数将字符串序列按照顺序依次拼接，得到字符串结果。Lambda 表达式里的 `s` 表示临时变量，用于积累元素处理期间的数据的，而这个 `v` 就是集合在每一次遍历过程之中的元素。我们把用来积累的变量 `s` 往后添加 `v`，就凑成了字符串拼接后的新字符串，然后再往后添加下一个字符串，直到集合遍历完成。当然最终这个结果就是一个完整的字符串了。

第二个重载需要加入一个初始值 `seed` 参数。即可以改写一下上面的写法：

```c#
var result = values.Aggregate("Sunnie, ", (s, v) => $"{s}{v}");
```

第三个重载，则在最后增加一个参数，用于指定聚合的处理情况。如下代码所示。

```c#
var result = values.Aggregate("Sunnie, ", (s, v) => $"{s}{v}", v => v.ToUpper());
```

这个聚合操作，将会把所有聚合的元素全部先转为大写，然后输出。自然结果就是 `"SUNNIE, HELLO, WORLD!"` 了。是不是很神奇呢？

### 2-16 `Zip` 函数

这个函数有点神奇。它可以把两个完全不相干的序列（只要他们的长度一样）归并到一起，给相同索引上的元素打包成一个对象，然后将这两个序列的所有元素按这样的操作进行处理，最终返回全部的打包对象。这么说不太好理解，你可以当成这样的查询表达式等价的语义。假设我有两个数组，包含相同个数的元素，但是两个数组里的数字和数字之间没有关联。

```csharp
int[] arr1 = { ... };
int[] arr2 = { ... };
```

查询表达式要想对位获取打包后的序列，我们可以使用这样的写法：

```csharp
var selection =
    from i in Enumerable.Range(0, arr1.Length)
    let a = arr1[i]
    let b = arr2[i]
    select new { First = a, Second = b };
```

就好比这样的操作。不过换成方法调用的话，是这么写：

```csharp
var selection = arr1.Zip(arr2, (l, r) => new { First = l, Second = r });
```

因为它是扩展方法，所以可以这么写。看不太明白可以使用静态方法的调用模式来书写：

```csharp
var selection = Enumerable.Zip(
    arr1,
    arr2,
    (l, r) => new { First = l, Second = r }
);
```

该方法有三个参数。第一个参数和第二个参数分别对应了打包的两个数组序列。而第三个参数则对应的是两个对应位取出的元素，应该怎么打包。这里我们使用 Lambda 表达式，使得取出的两个元素按照匿名类型的模式进行打包：传入 `l` 和 `r` 两个元素，然后返回的打包元素为 `new { First = l, Second = r }`。

怎么用结果呢？很简单，我们只需要 `foreach` 一下就可以了：

```csharp
foreach (var pair in selection)
{
    Console.Write(pair.First);
    Console.Write(", ");
    Console.Write(pair.Second);
    Console.WriteLine();
}
```

这样就可以了。