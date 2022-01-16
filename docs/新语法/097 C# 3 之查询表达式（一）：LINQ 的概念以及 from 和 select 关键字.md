# C# 3 之查询表达式（一）：LINQ 的概念以及 `from` 和 `select` 关键字

欢迎来到 LINQ 语法。LINQ 是 C# 里面最复杂的一个语法体系了，它的复杂程度估计就跟面向对象差不太多，是一个巨庞大的体系。下面我们将使用十多讲来完成对 LINQ 的介绍和全面讲解。

今天我们要说的是 LINQ 的基本概念，以及 LINQ 的简易使用。

## Part 1 引例

在早期的 C# 里，很多东西使用起来都已经比较方便了，但是有些时候，我们对集合的数据搜寻来说，仍旧有些不便。考虑一种情况。假设我要获取一个数组里的所有奇数，我们的写法是这样的：

```csharp
var list = new List<int>();
foreach (int element in array)
    if (element % 2 == 1)
        list.Add(element);
```

通过 `foreach` 循环，我们可以得到所有 `array` 里的奇数。当然，现在我们有了扩展方法，我们可以封装一下：

```csharp
static class ArrayExtensions
{
    public static IEnumerable<T> GetValues<T>(this T[] array, Predicate<T> predicate)
    {
        if (array.Length == 0)
            yield break;

        foreach (int element in array)
            if (predicate(element))
                yield return element;
    }
}
```

封装了之后，调用起来就简单多了：

```csharp
var result = array.GetValues(e => e % 2 == 1);
```

我们使用传入 Lambda 表达式的方式，来完成搜寻奇数的目的。这里我们用到了新语法有：

* C# 2 的静态类；
* C# 2 的泛型；
* C# 2 的 `yield return` 表达式；
* C# 3 的扩展方法；
* C# 3 的 Lambda 表达式。

试想一下，Lambda 表达式是挺好用，但有没有稍微优雅一点的、专门用来搜索集合元素的语法来处理这个？答案是有的，下面我们就来说一下，C# 3 的 LINQ。

通过 C# 3 的 LINQ，我们可以这么写代码：

```csharp
var result = from e in array where e % 2 == 1 select e;
```

是的，这个 `from e in array where e % 2 == 1 select e` 是一个表达式。这个表达式有些长，不过挺有意思的。而至于 `from`、`in`、`select` 和 `where` 等等关键字，是我们这里要说的东西。

## Part 2 LINQ 是什么？

LINQ，全称 Language-Integrated Query，直接翻译出来叫做“集成语言查询”。这个是什么意思呢？**查询**（Query）这个词语，在整个 IT 界都小有名气，它表示搜索我们需要的东西的行为。查询是一个术语词，大概可以理解为搜索的意思。

所谓的“集成语言查询”，这个应该理解为“语言集成的查询机制”，这个“语言”指的是 C# 这个编程语言，而集成指的是在 C# 这门语言里有内置语法，我们可以通过这样的语法来完成查询操作和机制。

C# 的 LINQ 包含如下的一些关键字：

* `from`：用来表示迭代变量；
* `in`：表示迭代的集合；
* `select`：表示我们需要去让什么变量作为返回；
* `into`：表示继续使用迭代结果；
* `where`：表示条件；
* `orderby`：表示排序集合；
* `ascending`：表示集合升序排序；
* `descending`：表示集合降序排序；
* `group`：表示分组集合；
* `by`：表示分组集合的依据；
* `join`：表示在集合配对和连接别的数据；
* `on`：表示 `join` 连接期间的条件；
* `equals`：表示连接的相等的成员比较。

可以看出，关键字使用情况相当多。正是因为这样的复杂性，所以 LINQ 三言两语肯定是说不完的。今天我们要介绍的是前面四个关键字：`from`、`in`、`select` 和 `into` 语句。

## Part 3 `from-in-select` 查询

### 3-1 语法

下面我们针对于集合迭代过程来简易说明一下使用。先介绍的是最基础的 `from-in-select` 语句。

`from-in-select` 语句，用连字符连起来的意思就是说，它们三个关键字是顺次书写的，语法是这样的：

```antlr
映射表达式
    'from' 数据类型? 变量名 'in' 集合 'select' 表达式
```

我们把 `from-in-select` 语句称为**映射表达式**（Projection Expression）。举个例子，我们要将集合里的所有元素都加一个单位，然后反馈出来的话，我们可以使用的写法是这样：

```csharp
var selection = from element in array select element + 1;
```

我们直接在 `select` 从句的后面写上 `element + 1` 作为表达式，这表示我们获取的元素是 `element + 1` 作为结果；而 `from` 和 `in` 这一部分则表示的是集合迭代的过程，书写的写法总是 `from` 后跟上迭代变量的名称，而 `in` 后跟上的是集合变量。

这样的语法看不习惯，可以使用 `foreach` 循环进行等价转换：

```csharp
public static IEnumerable<int> AddOne(this T[] array)
{
    foreach (var element in array)
        yield return element + 1;
}
```

是的。就等价于这个。`foreach (var element in array)` 被代替为 `from element in array`，而 `yield return element + 1` 被代替为 `select element + 1`。因此，你可以说，`from-in` 部分表示 `foreach` 循环的意思，而 `select` 部分表示的是 `yield return` 的意思。

不过，`from-in-select` 是不可分割的，也就是说一旦出现就必须全都包含，不能缺少任何其中的一部分。比如说，只有 `from-in` 的语句是错误的：

```csharp
// Wrong.
var selection = from element in array;
```

请注意返回值的类型。我们在等价变回 `foreach` 循环后，这个方法的名称 `AddOne` 返回的结果类型是 `IEnumerable<int>`。是的，这一点需要你注意。因为等价的转换的关系，`from-in-select` 整个部分我们称为一个表达式（因为它可以写在等号右边，进行赋值给左边的变量），而这个表达式的结果类型是 `IEnumerable<>` 类型的。换句话说，实际上这里的 `var` 就表示 `IEnumerable<int>`：

```csharp
IEnumerable<int> selection = from element in array select element + 1;
```

不过，有些时候比较长，因此你可以换行：

```csharp
var selection =
    from element in array
    select element + 1;
```

都是可以的。不过我们建议你换行的时候将 `from` 和 `select` 单独作为一行，而不要断开 `from` 和 `in`，因为它们被等价为 `foreach` 循环的声明的头部了，它们是不可拆分的。

我们把 `from element in array select element + 1` 称为一个**查询表达式**（Query Expression），它是一个表达式，而且功能是用来查询，因此叫做查询表达式；另外，查询表达式不只是 `from-in-select` 表达式，还有别的，后面我们会慢慢接触到它们。

另外，我们把 `from` 后跟的变量也称为**迭代变量**（Iteration Variable），不过在 C# 里，它只在这个表达式里才能使用，比大括号的级别还要小，因此我们把这样的变量也称为**范围变量**（Range Variable）。

### 3-2 `select` 从句就用迭代变量的情况

如果使用 `select` 的时候，我们只写本身的话，会如何呢：

```csharp
var selection = from element in array select element;
```

我 `from` 里声明的 `element` 变量直接写在 `select` 后了。如果把它转换为 `foreach` 循环的话，是这样的：

```csharp
foreach (var element in array)
    yield return element;
```

这不是多此一举吗？我故意使用 `foreach` 将数组的每一个元素都迭代出来，结果我又使用 `yield return` 把每一个 `element` 给整合起来。这种写法也不是错的，不过没有必要这么写，对吧。

### 3-3 `select` 从句用常量的情况

考虑下面的查询表达式：

```csharp
var selection = from element in array select 42;
```

请问，这么写有意义吗？没有。因为 `element` 没有用到，而每一个迭代操作最终都反馈了 42 这个常量出去，因此它等价于这样的代码：

```csharp
foreach (var element in array) // Unused variable 'element'.
    yield return 42;
```

因此，我们也尽量避免这种写法。真要说这个迭代的结果是什么，那就只能表示成“和集合元素数量一样多的 42 构成的序列”。

### 3-4 使用查询表达式需要引用 `System.Linq` 命名空间

在使用上述这样的查询表达式而不是 `foreach` 循环的时候，我们需要在文件最开头补充 `using System.Linq;` 命名空间的引用，原因今天讲不了，这个我们将在后面说到。

## Part 4 显式指定迭代变量的类型

可以从前文给出的语法规则看出，映射表达式的 `from` 后是有一个变量类型可以写的，不过它被标记了 `?` 说明可以没有。前面讲的是没有的情况，下面我们来说一下带有变量类型的情况。

考虑使用 `ArrayList` 这样的集合。如果这样的代码在迭代的时候，将会产生错误：

```csharp
var list = new ArrayList { 3, 8, 1, 6, 5, 4, 7, 2, 9 };
var selection = from e in list select e + 1;
```

但是，这样书写有一个问题。`ArrayList` 类型是很早的数据类型，它虽然是一个集合，但里面的元素是 `object` 类型的，虽然我们知道，我们这个集合里只存 `int` 元素，但因为它自身只实现了 `IEnumerable` 接口，而没有实现泛型的版本（它自己在没有泛型之前就有了），因此 `from-in` 在书写的时候就会失败。所以，上面的代码是不合理的。

你想想，你转换为 `foreach` 后，代码是不是这样：

```csharp
foreach (var e in list)
    yield return e + 1;
```

可 `e` 是什么类型呢？`object`，对吧：

```csharp
foreach (object e in list)
    yield return e + 1; // Emmm...
```

可是，`e + 1` 就不对了。于是，我们会试着改变 `object`：

```csharp
foreach (int e in list) // Use explicit cast implicitly.
    yield return e + 1;
```

我们通过显式指定数据类型 `int` 来代替 `object`，这样我们就可以在 `foreach` 的底层自动进行 `object` 到 `int` 的强制转换，因为我们知道每一个元素都是 `int` 类型，所以这么写是可以的。于是，这样的声明是合理的。

但仔细想想，前面我们介绍的例子迭代的是 `int[]`，也就是说，`foreach` 循环我们完全不必声明它迭代变量的类型，写成 `var` 编译器也知道；但是 `ArrayList` 不行，不写就不知道具体类型，因此我们必须强制写出来元素自己的实际类型，然后做一次隐式的强制转换，是的，隐式的强制转换。隐式指的是它在底层才知道有这个转换逻辑，而直接看 `foreach` 是看不太出来的；而强制转换是背后执行的逻辑和机制。

那么，LINQ 里要怎么做呢？`from e in list select e + 1` 吗？肯定不行。于是 LINQ 允许我们在迭代变量的左边配上它的实际类型，表示进行强制转换的具体类型。于是，改写为这样就可以了：

```csharp
var list = new ArrayList { 3, 8, 1, 6, 5, 4, 7, 2, 9 };
var selection = from int e in list select e + 1;
```

是的，`from int e in list`。

## Part 5 叠加 `from-in` 从句

`from-in` 从句是可以叠加的。考虑一下，我有两个集合需要迭代，然后凑一对，我们可以这么做：

```csharp
foreach (var a in collection1)
    foreach (var b in collection2)
        yield return new { a, b };
```

我们可以构成一个数对，然后用匿名类型表示出来，然后两层循环，将两个集合的元素两两组合。C# 的 LINQ 也能做这个事情：

```csharp
var selection =
    from a in collection1
    from b in collection2
    select new { a, b };
```

是的。我们只需要叠加起来即可。我们使用两层 `from-in` 从句，就可以达到两层循环的效果。这就是 LINQ 的魅力之处。当然，`from-in` 并未要求必须最多几个，实际上你可以继续叠加：

```csharp
var selection =
    from a in c1
    from b in c2
    from c in c3
    from d in c4
    select new { a, b, c, d };
```

> 是的，匿名类型是不用写出来属性名称的，C# 的匿名类型具有属性名推断的功能，如果你写的匿名类型的表达式是 `new { a, b }` 的话，那么生成的对应匿名类型的具体类型，会直接使用变量名称 `a` 和 `b` 表示这两个实际属性，而如果是 `new { 1 }` 就不行了，因为 1 是常量，它没有对应的变量名称，因此无法将其当作属性名使用，这种情况下就必须写属性名称了。
>
> 另请注意 `new[] { a, b }` 和 `new { a, b }` 的区别。`new[] { a, b }` 是数组的隐式类型的初始化器，因为有个方括号；而 `new { a, b }` 没有方括号了，因此会被视为匿名类型的表达式。

## Part 6 嵌套查询和 `into` 从句

有些时候，`foreach` 仅仅是上面那样的话，就显得比较简单了，一些复杂的东西可能前面的内容就做不到了。下面我们来看一些灵活的处理。

### 6-1 嵌套查询

考虑一种情况，我们要通过 `from-in-select` 表达式得到一个新集合，然后将这个集合再一次使用和映射起来。我们可以这么做：

```csharp
var temp1 = from t in c1 select Math.Sqrt(t);
var temp2 = from t in c2 select Math.Sqrt(t);
var result = from a in temp1 from b in temp2 select a + b;
```

是的，我们迭代 `c1` 集合，得到每一个元素的平方根，然后组成新的序列；与此同时我们迭代 `c2` 集合，得到这个序列每一个元素的平方根。最后，我们使用叠加的 `from-in` 从句，来进行两两配对，最后得到每一组两个元素的平方根的和。

> 这里稍微提一句。注意 `temp1` 和 `temp2` 的查询表达式写法。在 `from` 后，两个查询表达式用的是相同的标识符 `t`，但是因为前文说过，查询表达式内的变量我们叫迭代变量，它仅用于转化为 `foreach` 循环后的这个循环体内部使用，因此在查询表达式里，迭代变量仅可以提供给整个查询表达式的范围使用，超出这个范围的任何其它的地方都是无法“看”到它的。因此，两个 `t` 并不会冲突，正是因为如此，我们才叫它范围变量，因为它比大括号的级别还要小，只在表达式的小范围里才能随便使用它。

这样的写法是可以，不过 `temp1` 和 `temp2` 不必显式用变量写出来，于是我们可以内联到同一个语句里去。

想一想，`from-in` 的 `in` 后面是不是就是集合啊？要想是一个集合，首先是不是得至少可以允许它进行 `foreach` 循环啊？有了这个循环我们才能将查询表达式和 `foreach` 进行等价转换。那么至少这个集合就得是 `IEnumerable<>` 或者是 `IEnumerable` 的。欸，等下，是不是很熟悉？是的，查询表达式自己的结果就是这两个接口类型的。那如果我直接将结果内联写到 `in` 的后面不就可以了？

```csharp
var result =
    from a in (from t in c1 select Math.Sqrt(t))
    from b in (from t in c2 select Math.Sqrt(t))
    select a + b;
```

是的。LINQ 的灵活远超我们预期。查询表达式允许我们这么写代码。甚至编译器知道 `in` 后面的是一个单独的查询表达式，它甚至允许我们去掉小括号：

```csharp
var result =
    from a in from t in c1 select Math.Sqrt(t)
    from b in from t in c2 select Math.Sqrt(t)
    select a + b;
```

这样也是可以的。

我们把第 2、3 行的 `in` 关键字后的查询表达式称为**嵌套查询表达式**（Nested Query Expression），它嵌套在整个 `from-in-from-in-select` 表达式的里面。不过注意使用和计算顺序，先计算出内层的结果，然后才能执行和得到整个 `from-in-from-in-select` 表达式的结果。

### 6-2 `into` 从句

我们可以使用 `select-into` 从句来完成对临时查找的结果继续进行迭代操作的“接续”。考虑一下前面的代码，我们发现一个通用的结论：`select` 总是被放在末尾。因为 `select` 用来映射结果，因此它肯定只能放末尾；但有些时候也不一定。

考虑一种情况，我给出了一系列的学生信息：

```csharp
var students = new List<Student>
{
    new Student("ShioriSaginomiya", "20210103", 16, Gender.Female),
    new Student("ShinonomeNano", "20120203", 1, Gender.Female),
    new Student("NoharaShinnosuke", "20200205", 15, Gender.Male),
    new Student("TomCat", "20200529", 19, Gender.Male),
    new Student("JerryMouse", "20210529", 19, Gender.Female),
    new Student("Enmu", "20190303", 2333, Gender.Male),
    new Student("Tohru", "20200101", 18, Gender.Female),
    new Student("KannaKamui", "20210101", 16, Gender.Female),
    new Student("HirasawaYui", "20150201", 28, Gender.Female),
    new Student("NakanoAzusa", "20160201", 26, Gender.Female)
};
```

我给出了假机器人鹭宫诗织、真机器人东云名乃、蜡笔~~山~~小新、汤姆猫、杰瑞鼠、托尔酱、康娜酱、下弦之一魇梦、平泽唯和阿梓喵这几个学生的基本信息（说这段好像没啥意思，但是我就是故意说的，~~我二次元浓度真的不高~~）。

我想将列表的所有学生都看一遍，看看是不是都成年了，然后给每一个学生都配上结果。我不管你是不是来自异次元，咱只看地球上的标准——看这些学生是不是满 18 岁。那么代码应该是这样：

```csharp
var selection =
    from student in students
    select new { Student = student, IsAdult = student.Age >= 18 } into temp
    select temp;
```

是的，这里我们使用到了一个新的语法：`select-into` 从句。请注意第 3 行。代码使用到的语句写法是 `select new { ... } into ...`，这表示什么呢？这表示的是，我将前面的学生的信息，按 `new { ... }` 这个匿名类型的表达式转换一下，然后将转换的这个结果，使用 `temp` 表示成变量。

换句话说，`select-into` 语句允许我们定义临时变量在中间。它将 `select` 后的表达式进行中间化，避免 `select` 一定要放在最后的问题。

当然了，上述这样的代码也可以被精简一下：

```csharp
var selection =
    from student in students
    select new { Student = student, IsAdult = student.Age >= 18 };
```

是的，这样写其实也没毛病，只是说初学的话可能不太好理解。

### 6-3 “副作用”：`into` 从句会阻断范围变量的使用范围

这是一个比较“奇怪”的设计。可能你会觉得是这样，不过 `select-into` 设计出来是别有用途的，但目前我们还没办法说，下一讲内容我们会讲解 `let` 关键字，到时候我们就会说明 `select-into` 这种副作用的真正原因。

比如下面这样写代码，就是不对的：

```csharp
var selection =
    from d1 in c1
    from d2 in c2
    select d2 into digit
    select new { digit, d1 }; // Wrong. Try to reference the unreachable range variable 'd1'.
```

`new { digit, d1 }` 是不对的写法，因为使用 `d1` 变量是不可以的，因为前面有 `select-into` 从句，这意味着 `into digit` 之前定义的变量就不能在 `into digit` 之后使用了。

## Part 7 查询表达式当成参数和临时表达式使用

前面我们就说过，查询表达式是一个表达式，它是 `IEnumerable<>` 类型的。因此，任何可以用表达式的地方，只要类型对得上，这样的语法就可以无处不在。

比如 `string.Join` 方法。这个方法用来给序列的元素和元素之间插入分隔符。假设我有一组元素，要在每个元素之间插入逗号，我们可以这么写代码：

```csharp
string s = string.Join(
    ',',
    from element in list select element.ToString()
);
```

是的，我们直接将查询表达式当成参数传入到第二个位置上去。这是允许的写法。

另外，我们也可以使用前面讲过的扩展方法来拓展用法。假设我想要获取整个集合的第一个元素的话：

```csharp
public static T First<T>(this IEnumerable<T> list)
{
    foreach (var element in list)
        return element; // Returns the first encountered element.
}
```

我们这里有了扩展方法了。下面我们就可以这么做了：

```csharp
string s = (
    from student in students
    select student.Name
).First();
```

我们使用查询表达式得到所有学生的名字，然后使用 `First` 扩展方法的语法来获取这个序列里的第一个元素。因此这个整个计算过程的结果，其实就是名字序列里的第一个，因此 `s` 就是这个名字，是 `string` 类型的。

稍微注意一下的是，由于查询表达式的完整性和复杂性，我们必须在调用扩展方法之前，使用小括号把整个查询表达式给括起来，这是在这种用法下唯一需要我们注意的地方。

## Part 8 总结

今天我们讲解了使用 `from`、`in`、`select` 关键字来完成一些操作，知道了什么叫做查询表达式，范围变量的具体范围又是哪些。另外，我们在这一节里大量使用到匿名类型，也对我们之前学习匿名类型来说有了一个比较合理的认知。

下一节我们将给大家介绍 `let` 从句，用来临时定义变量。