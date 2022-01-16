> 欢迎来到第 100 讲！

# C# 3 之查询表达式（四）：`orderby` 关键字

今天我们继续看新的查询表达式的从句。今天要说的是 `orderby` 从句。这个词也是一个两个单词构成的组合关键字，和 `readonly`、`foreach` 这些关键字是类似的。

## Part 1 引例：按指定属性执行排序操作

我们仍然使用之前介绍的例子来说明。我们在前面学习了如何定义临时变量、如何筛选对象、并且如何使用映射机制。今天我们来说排序。

考虑一种情况。我还是那些同学，我想按照名字的顺序来排序。名字因为刚好都写成英语，所以我们干脆就按照英语单词的字典序比较方式来对这些学生进行排序，并得到排序后的结果。前面的查询表达式是没办法排序的，今天我们来学习一下排序的做法。

排序我们使用 `orderby` 关键字来表示：

```csharp
var orderedResult =
    from student in students
    orderby student.Name ascending
    select student;
```

请注意这个写法。今天这个写法有一点点理解难度。第 3 行 `orderby student.Name ascending` 表示将 `student.Name` 取出来，然后排序。每一个学生都会如此，因此我们只需要写 `student.Name` 就可以表达“我想要按照每一个学生的 `Name` 属性排序”的意思。接着，在 `student.Name` 后有一个全新的关键字：`ascending`。这个词比较生疏，它在编程里的意思是“升序的”，换句话说，就是按照从小到大的顺序表达出来的过程。对于字符串而言，从小到大的概念就等价于字典序。字母表里越靠前的字母越小，越靠后的越大。因此，a 字母最小、z 字母最大。这刚好复合我们需要的排序手段；如果你要降序排序的话，用的是 `descending` 这个关键字来代替 `ascending`。

接着，`orderby` 从句也不能作为结尾，因此它后面还是得继续写一句 `select student`。请注意这个时候的 `student` 变量。`student` 在整个查询表达式都是同一个变量，但是实际上在第 3 行的时候，我们将其已经排序完成了。最后的 `select` 从句里就相当于要把它给拿出来返回，因此我们直接写 `select student` 即可。如果你要排序之后只获取所有学生的名字的话，这里就改成 `select student.Name` 即可。

可以发现，这样的排序操作未免过于简单了一些。直接写出来甚至都不用考虑底层。这就是 `orderby` 关键字的用法。另外需要注意的是，如果 `orderby` 从句如果是升序排序的话，是可以省略不写 `ascending` 关键字的；但如果要降序排序的话，就必须要写上 `ascending` 关键字了。比如前面的代码用的是升序，因此可以不写 `ascending`。

```csharp
var orderedResult =
    from student in students
    orderby student.Name
    select student;
```

这样就可以了。

## Part 2 递进排序

假设我们把前面的代码稍微改一下。假设我要排序学生的名字，如果学生名字一样的话，我们就继续按学生的年龄升序排序。这样的代码需要遵循两个排序规则，不过它们肯定不是同时发生的，而是前者在得到一致结果之后，再按照后者的内容排序。这样的语句我们可以使用逗号分隔，写在一起：

```csharp
var orderedResult =
    from student in students
    orderby student.Name ascending, student.Age ascending
    select student;
```

是的，`orderby` 关键字只需要写一次，但是逗号要分开每一个排序的过程。每一个过程都带有排序的属性名称，以及排序的升序或降序的关键字。和前面一样，如果是 `ascending` 的话可以省略。也就是说，这样的代码也可以简写为这样：

```csharp
var orderedResult =
    from student in students
    orderby student.Name, student.Age
    select student;
```

## Part 3 不建议出现多个 `orderby` 从句

`orderby` 从句和 `let`、`where` 类似，都是写在 `from` 和 `select` 期间的，它不能当开头也不能当结尾。但是，可以多次出现 `orderby` 从句。比如下面的代码：

```csharp
var orderedResult =
    from s in students
    orderby s.Name
    where s.Age >= 18
    orderby s.Age
    select s;
```

操作很简单，就是排序 `Name` 属性，然后筛选出来所有成年的学生，然后再次对筛选结果进行排序，按 `Age` 属性排序。最后取出学生对象即可。

可以看到，这个例子里用到了两次 `orderby` 从句，不过它们是互不影响的。按照道理来说，先按照 `Name` 属性排序后，序列变为这样，然后再次按 `Age` 重新排序，那么原来的序列的顺序就再一次会被打乱。不过这个例子里，中间有一个 `where` 从句，因此会去掉一些不满足条件的对象，随后再次排序也仅仅针对于成年的学生，然后才进行排序的。

不过，按照逻辑合理性来说的话，其实多次 `orderby` 并不是非常好的写法。如果真的这样的语句是合理的的话，那么我删掉前面的 `orderby` 语句好像也没问题：

```csharp
var orderedResult =
    from s in students
    where s.Age >= 18
    orderby s.Age
    select s;
```

我只保留 `orderby s.Age` 好像也没问题。所以，我们不太建议多次排序同一个对象，还按照不同的排序形式进行排序，这样的话，本来排序后的序列会被再一次打乱，那么前一次的排序操作其实并没有任何用处。

## Part 4 排序依据跟对象无关的情况

### 4-1 排序依据是通过对象本身的数据计算得到的

考虑一下。我们需要按多个属性进行混合使用，然后排序的话，我们要怎么做呢？

最容易想到的场景，就是平均数。假设我要把学生按平均数进行降序排序的话，我们的代码是这样的：

```csharp
var orderedResult =
    from s in students
    let total = s.Math + s.English + s.Chinese
    let average = total / 3.0
    orderby average descending
    select s;
```

请观察这个排序操作。`orderby` 从句的排序依据虽然是平均成绩，跟学生还是有关系，但实际上在书写代码的时候，我们已经单独创建了新的临时变量，因此排序的结果实际上跟 `s` 关系已经不大了。

当然，你可以不使用 `let` 从句，而改用表达式的形式书写：

```csharp
var orderedResult =
    from s in students
    orderby (s.Math + s.English + s.Chinese) / 3.0 descending
    select s;
```

C# 是允许表达式书写进来的。

### 4-2 排序依据是一个跟序列完全无关的常量

排序行为不一定非得是来自于对象本身的属性或字段取值，它可以是表达式，甚至是一个常量。我们来看一个比较奇怪的例子。

```csharp
var orderedResult =
    from number in new[] { 3, 8, 1, 6, 5, 4, 7, 2, 9 }
    orderby 0
    select number;
```

假设我要给这个数组排个序。结果，我在 `orderby` 里写的是一个常量 0。你知道这个排序机制意味着什么吗？排序的依据已经跟数组的每一个元素都没有任何关系了。前面的例子好歹还有关系，这个压根没有任何关系了。

那么你知道结果如何吗？其实很简单。因为排序依据是常量，也就意味着每一次排序和比较操作都是这个数值作为判断的依据和标准。因此，排序的东西是什么已经无所谓了，这样的数据传入进来，怎么进来的就怎么出去。因此使用常量作为排序依据的查询表达式，执行结果和原始数据的序列顺序相比，没有任何变动。

这个不合常理的写法可以推广到任何常量上。因为常量是不变的，因此所有的常量当作排序依据最终的结果都不会发生任何变动。哪怕你写一个字符串、写一个整数、写一个浮点数，甚至写多个都是常量的排序依据，全部没有任何关系。

```csharp
var orderedResult =
    from number in new[] { 3, 8, 1, 6, 5, 4, 7, 2, 9 }
    orderby 0, 1, 2, 3, 4, 5, 6, 7, 8, 9
    select number;
```

你以为写得多就有变化？笑话。因为排序依据全部是常量，所以你写再多都不会变。因此，**我们强烈不建议将常量作为排序依据写进 `orderby` 从句里**，虽然 C# 查询表达式允许你这么做，它毕竟不是语法错误。

## Part 5 其它细节

### 5-1 不要往 `orderby` 从句里写不能排序的对象和表达式

排序好用是好用，但是也有一点比较隐蔽的问题。`orderby` 里是可以写任何东西的，因此有些并不支持排序的对象也可以放进去。比如我们之前设计的 `Student` 类型，它并不包含比较的任何操作，如果我们直接这么写的话：

```csharp
var orderedResult =
    from student in students
    orderby student // Weird.
    select student;
```

再极端一点。我写一个空的数据类型 `C`：

```csharp
class C { }
```

然后我实例化三个 `C` 类型的对象，然后放进一个数组里去：

```csharp
var cs = new[] { new C(), new C(), new C() };
```

然后就直接开始排序：

```csharp
var result = from c in cs orderby c select c;
```

你觉得这样的代码它合理吗？显然不合理。对象 `c` 是 `C` 类型的，但这个类型都压根不能比较。我们知道，要想对象参与排序操作，必须对象至少得支持比较操作。但很明显，这个 `C` 类型，还有前面的 `Student` 类型并没有实现 `IComparable` 和 `IComparable<>` 接口。继续的排序操作都没有，怎么可能可以呢？

但是，C# 考虑到一些处理机制的灵活性，它允许我们这么写代码。说实话，它确实并不算是一个语法错误。但是，运行期间，如果程序执行代码期间没有发现你这个排序的对象对应的类型里包含实现了 `IComparable` 或 `IComparable<>` 接口的话，就会产生 `InvalidOperationException` 类型的异常，告诉你对象不能比较大小，因此排序失败。

### 5-2 如果 `where` 和 `orderby` 挨着，一般先 `where` 后 `orderby`

考虑前面给的代码：

```csharp
var orderedResult =
    from s in students
    where s.Age >= 18
    orderby s.Age
    select s;
```

如果我写成这样：

```csharp
var orderedResult =
    from s in students
    orderby s.Age
    where s.Age >= 18
    select s;
```

好像排序的结果是一样的，先后顺序好像显得不是那么重要。不过性能上是有区别的。前者先使用 `where` 的话，或多或少会删掉一些不满足条件的元素，因此排序的元素数量可能没有原始整个集合那么多；但是后者就不会这样了。因为后者先写的是 `orderby`，因此它会优先执行一次对整个集合的排序操作，然后排序完成后才开始筛选序列。显然排序的速度会慢一些，因此我们强烈不建议使用后者这样的模式去书写代码，即**我们不建议在 `where` 和 `orderby` 从句紧挨着出现的时候，先写 `orderby` 后写 `where`**。

###  5-3 排序的底层怎么排序的呢？用的什么排序方式呢？

呃，这个确实是超纲了，因为现在仅凭单纯的关键字确实不太好讲清楚。我只能告诉你答案了：底层用的是快速排序法。快速排序是不稳定的排序，如果序列的数值比较极端，可能会导致交换次数超出预期的情况，排序效果不好；但多数时候会非常快速得到结果，正是因为如此，这个算法才会叫快速排序，因为它确实很快。

如果你要看代码的话，实际上三两句是讲不清楚的，虽然你可以大体看出执行的逻辑是快速排序，但具体怎么按具体的排序依据进行排序的细节我们仍旧没办法说清楚，所以自己看代码吧：

https://source.dot.net/#System.Linq/System/Linq/OrderedEnumerable.cs,a25a731c74bcfb10,references

你放心，它们的底层我们肯定会说的，只是现在因为先说的话难度过大，所以就暂时卖个关子。在后面讲解“任意类型的查询表达式”的深层次 LINQ 机制的时候，会给大家讲解原理。

### 5-4 `orderby` 从句的结果是 `IOrderedEnumerable<>` 接口实例

`orderby` 从句在运行之后，运行出来的结果是 `IOrderedEnumerable<>` 接口类型的实例，而这个接口是一个全新的类型。不过，这个接口类型仍然实现了 `IEnumerable<>` 接口，因此这种接口类型的实例仍然支持 `foreach` 循环去迭代它。所以用法和使用体验上几乎和一般的 `IEnumerable<>` 接口没有任何的区别。不过因为它的底层有些麻烦，所以现在还说不清楚这样设计的好处，所以咱先当成结论记住就行。之后讲解原理的时候我们会阐述细节。

## Part 6 总结

至此我们就对 `orderby` 关键字有了一个比较完善的认知。包括单排序、多排序的语法、多次使用 `orderby` 从句的性能问题、`orderby` 和 `where` 先后顺序的性能问题以及不建议使用跟集合序列无关的量作为排序依据的相关内容。

从下一节的内容开始，就比较难一些了。最难理解的莫属 `group-by` 系列从句和 `join` 系列从句了，我们也只剩下了这俩没有讲了。当然了，说完了查询表达式的基本语法和关键字，也只说完了整个 LINQ 体系的三分之一，后面还有惊险刺激的内容等着你去学习。加油吧骚年！
