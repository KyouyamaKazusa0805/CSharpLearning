# C# 3 之查询表达式（三）：`where` 关键字

今天我们要讲一个新的关键字：`where`。它用来筛选，可以得到只满足条件的序列。

## Part 1 引例

我们还是使用之前的学生的序列来说明用法。

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

假设我要获取这些学生里成年了的人。我不管它们生活的那个异世界是怎么规定和计算年龄的，我们假设把传入的第三个参数（假设叫它 `Age` 属性）当成年龄数值，然后获取出所有年龄都大于 18 岁的。

我们之前的关键字都无法做到，因为它们都不具有筛选功能。今天我们要介绍的 `where` 关键字就表示筛选。用法很简单，当成类似 `let` 从句那样，插入到中间当成筛选就可以了。

```csharp
var adults =
    from student in students
    where student.Age >= 18
    select student;
```

我们使用 `where student.Age >= 18` 来计算和筛选对象。和之前一样，我们将 `from` 后的变量当成迭代变量，然后 `where` 就好比一个 `if` 条件一样。所以这个语句等价的 `foreach` 循环的代码是这样的：

```csharp
foreach (var student in students)
    if (student.Age >= 18)
        yield return student;
```

很简单，对吧。这就是 `where` 从句的用法。讲完了。

## Part 2 `where` 从句也可以多次使用

C# 的查询表达式灵活就灵活在，它可以随便用而不去考虑底层是怎么去实现的。

```csharp
var student =
    from student in students
    let age = student.Age
    where age >= 18
    let total = student.Chinese + student.English + student.Math
    let average = total / 3.0
    where average >= 60
    let name = student.Name
    select name;
```

如果你用熟练了的话，你就可以写出这么长的查询表达式。我们挨个来分析一下。

`from student in students` 表示在迭代 `students` 集合，然后每一个元素我们用 `student` 临时表示一下。它类似 `foreach` 循环的迭代变量；`let age = student.Age` 在定义临时变量，表示 `age` 是这个学生的年龄信息，用临时变量表达起来；`where age >= 18` 表示获取年龄大于等于 18 岁的；`let total = student.Chinese + student.English + student.Math` 表示计算这个学生的总成绩；`let average = total / 3.0` 表示计算这个总分除以 3 的结果，即平均分；`where average >= 60` 又是一次筛选，表示学生的平均分是否大于等于 60；`let name = student.Name` 表示临时变量的定义，得到这个学生的名称；最后的 `select name` 表示获取这个名字作为迭代结果。

因此，整个表达式可以等价于下面这个 `foreach` 循环：

```csharp
foreach (var student in students)
{
    int age = student.Age;
    if (age >= 18)
    {
        int total = student.Chinese + student.English + student.Math;
        double average = total / 3.0;
        if (average >= 60)
        {
            string name = student.Name;
            yield return name;
        }
    }
}
```

是的。`let` 等价于一个变量的定义，`where` 则直接改成 `if` 即可。注意两次 `where` 从句，先出现的是外层的 `if`，而出现的则是内层的 `if`。

如果你觉得大括号导致的层次级别太多，你可以取反一下逻辑：

```csharp
foreach (var student in students)
{
    int age = student.Age;
    if (age < 10)
        continue;

    int total = student.Chinese + student.English + student.Math;
    double average = total / 3.0;
    if (average < 60)
        continue;

    string name = student.Name;
    yield return name;
}
```

我们直接取反 `if` 条件，然后直接把后面的代码减少一次缩进，再把原来直接跟在 `if` 后的执行语句改成 `continue;` 即可。顺带一说，这也是编程的时候最习惯的书写模式了。我们总是将每次得到的、不满足条件的对象通过 `continue` 语句直接跳过计算，代码可以少一些缩进，在代码效率保证了的前提下还减少了缩进，增强了可读性。

另外，从这样的 `foreach` 循环的等价代码理解可以看出，多次 `where` 从句筛选出来的结果是必须对这些给出的条件全部都满足的迭代对象。

## Part 3 紧挨着的 `where` 从句

刚才我们说到一个结论。`where` 从句表示一种筛选过程，当条件满足的时候，这样的对象才可能被迭代返回出来。如果多次条件筛选的话，那就相当于是 `&&` 处理一样的效果。

比如下面这样的代码：

```csharp
var student =
    from student in students
    where student.Age >= 18
    where student.Chinese + student.English + student.Math >= 180
    select student.Name;
```

我省去了一些 `let` 从句，实际代码和原来的逻辑是一样的，都是“获取所有成年并且及格的学生的名字”。不过，这次我们连着使用了两次 `where` 从句。这样其实是不好的。因为两个 `where` 条件挨着就意味着代码是这样的：

```csharp
foreach (var student in students)
{
    if (student.Age >= 18)
    {
        if (student.Chinese + student.English + student.Math >= 180)
            yield return student.Name;
    }
}
```

请注意两个嵌套的 `if`。如果 `if` 出现嵌套，我们就可以认为，两个 `if` 可以用 `&&` 连起来。因为两个 `if` 里除了条件判断以外，没有别的执行代码。因此，我们合并到一起判断，也是一样的效果：

```csharp
foreach (var student in students)
{
    if (student.Age >= 18
        && student.Chinese + student.English + student.Math >= 180)
        yield return student.Name;
}
```

因此，对应的 `where` 从句也可以这么做。我们将紧挨着的两个甚至更多的 `where` 从句里面的条件全部使用 `&&` 连起来，然后直接写成一个 `where` 从句，即可完成简化：

```csharp
var student =
    from student in students
    where student.Age >= 18 && student.Chinese + student.English + student.Math >= 180
    select student.Name;
```

当然，如果你不考虑性能的话，其实写成原来的写法也没啥问题，两种写法执行出来的结果是一致的，只是性能上有点区别罢了。

但请注意，连续出现的 `where` 是 `&&` 的逻辑，而不是 `||` 的逻辑。你不能把两个应该用 `||` 连起来的条件拆解为两个连续的 `where` 从句表达出来，也不能将两个连续的 `where` 从句给的条件用 `||` 连起来。初学这里的时候经常有同学会忘记这一点，导致逻辑出现问题。

## Part 4 不太算是题外话的题外话

### 4-1 `from` 从句可以不挨在一起用

我们在说查询表达式的最开始的内容里就说过，`from` 是可以多次使用的，它就等价于多层的 `foreach` 循环。不过，前面的例子还不够多，所以我们的想象力限制了 `from` 的用法。实际上，`from` 是可以分开用的，也就是说，有一个 `from` 从句后，跟上一些别的从句（比如 `let`、`where` 从句什么的）之后，然后再来一个 `from` 从句，也是可以的。只不过之前没有 `where` 从句，所以不太好对这种情况进行举例说明。下面我们来介绍一种用法来这么去使用 `from`。

考虑一种情况，我现在想对整个学生集合进行一男一女的 CP 配对操作。我们可以这么去写代码：

```csharp
var selection =
    from student1 in students
    let name1 = student1.Name
    let gender1 = student1.Gender
    from student2 in students
    let name2 = student2.Name
    let gender2 = student2.Gender
    where name1 != name2 && gender1 != gender2
    where gender1 == Gender.Male
    select new { Male = student1, Female = student2 };
```

我们从这个例子里可以看出，这个例子用到了 `from` 从句后跟了两次 `let` 从句，然后才是新的 `from`。这也是允许的。它的等价 `foreach` 代码也不必多说：

```csharp
foreach (var student1 in students)
{
    string name1 = student1.Name;
    var gender1 = student1.Gender;
    foreach (var student2 in students)
    {
        string name2 = student2.Name;
        var gender2 = student2.Gender;
        if (name1 != name2 && gender1 != gender2)
        {
            if (gender1 == Gender.Male)
                yield return new { Male = student1, Female = student2 };
        }
    }
}
```

当然，你也可以将两次挨着的 `where` 写到一起，用 `&&` 连接起来。

```csharp
foreach (var student1 in students)
{
    string name1 = student1.Name;
    var gender1 = student1.Gender;
    foreach (var student2 in students)
    {
        string name2 = student2.Name;
        var gender2 = student2.Gender;
        if (name1 != name2 && gender1 != gender2 && gender1 == Gender.Male)
            yield return new { Male = student1, Female = student2 };
    }
}
```

下面我们来说说，这些语句都是什么意思。

我们再次照搬上面的查询表达式：

```csharp
var selection =
    from student1 in students
    let name1 = student1.Name
    let gender1 = student1.Gender
    from student2 in students
    let name2 = student2.Name
    let gender2 = student2.Gender
    where name1 != name2 && gender1 != gender2
    where gender1 == Gender.Male
    select new { Male = student1, Female = student2 };
```

我们要习惯去看查询表达式而不是 `foreach` 循环的等价写法。如果每次都要自己脑补 `foreach` 循环然后去反推查询表达式的话，学习起来就比较慢，效率就不太高了。

我们首先迭代了 `students` 序列，我们将每一个学生的名字和性别取出来留着稍后使用。接着我们再次重新迭代 `students` 序列。注意这次我们写在上面 `from` 的下方，因此它等价于的是 `foreach` 的嵌套。你想想看，`where` 多次用的话，前面的 `where` 改成了外层的 `if`，而后面的 `where` 则改成了内层的 `if`。所以 `from` 的话，也是这个道理。

两层嵌套的 `foreach` 循环一般都是用来进行两两组合的，刚好我们这里就需要一男一女组合，所以完全符合我们这里的需求。继续往下看，在得到两位学生（两个 `students` 迭代期间的临时变量 `student1` 和 `student2`）的性别和名字后，我们需要判断这两个对象是否需要配对。因为我们只需要一男一女组合，因此我们需要判断性别和名字两个属性的数值。

为什么连名字也要判断呢？因为两层嵌套的 `foreach` 循环都迭代的是相同的序列，那么完全有可能在迭代期间遇到两个循环迭代到同一个对象的情况。为了避免程序的 bug，我们必须排除掉这种特殊情况。因此可以在第 8 行的筛选条件里看到 `name1 != name2` 的判断。当然，一男一女就意味着两个人的性别是不一致的，所以 `gender1 != gender2` 就比较好理解了。

> 当然，如果你想更加巧妙地使用代码的话，你可以这么去处理性别比较。假设我们这个例子里 `Gender` 是个枚举类型，只包含 `Male` 和 `Female` 两个数值情况的话，那么我们很容易会认为 `Male` 对应特征数值 0、`Female` 对应特征数值 1（当然，你要反过来也行）。那么，要想保证两个人的性别不一致，那么我们可以使用异或运算。异或运算恰好保证的就是两个数值一致的时候为 0；反而不一致的时候是 1。因此，`gender1 != gender2` 也可以写成 `(gender1 ^ gender2) != 0`。
>
> 呃好像代码更长了……我只是说有这么个用法，是这么个写法，是这个意思。

那么，第 9 行的 `where` 条件又是个什么情况呢？之所以没有内联到上一个 `where` 从句里去，是因为它需要单独说明逻辑。反正这么写和写在一起也没有运行上的差别。这个第 9 行的代码，其实是为了保证我们给出的 `student1`（也就是外层 `foreach` 循环的迭代变量）一定是个男学生。因为前一个 `where` 从句（第 8 行）判断了两个学生不同性别，因此 `student1` 保证了是男学生的话，那么 `student2` 就肯定是女学生了。这样我们就可以大大方方在第 10 行的 `select` 语句里直接指定 `Male` 和 `Female` 属性到匿名类型的初始化器里去了。这就是为什么需要额外判断一下 `student1` 是不是男性的原因——为了保证一男一女表达出来的时候，先给的 `student1` 一定是男生，而后给的 `student2` 一定是女生。

> 下面请你自己思考一个问题。这样筛选会不会漏掉组合的情况？答案自己想哈，我就不给出解释了。

### 4-2 巧用匿名类型简化多次定义的 `let` 从句

例如上面这样的定义，因为我们必须判断 `Name` 和 `Gender` 属性，因此我们需要用四次 `let` 从句来获取数值。这样定义是没有问题，但是显然多次定义可以简化一下。

我们可以巧妙利用匿名类型来完成这个任务。我们将同一个对象的定义过程放在一个匿名类型的初始化器里赋值，然后赋值给一个变量就行了：

```diff
  var selection =
      from student1 in students
-     let name1 = student1.Name
-     let gender1 = student1.Gender
+     let pair1 = new { student1.Name, student1.Gender }
      from student2 in students
-     let name2 = student2.Name
-     let gender2 = student2.Gender
+     let pair2 = new { student2.Name, student2.Gender }
-     where name1 != name2 && gender1 != gender2
+     where pair1.Name != pair2.Name && pair1.Gender != pair2.Gender
-     where gender1 == Gender.Male
+     where pair1.Gender == Gender.Male
      select new { Male = student1, Female = student2 };
```

我们注意一下修改的地方。我们删除了赋值过程，取而代之的是新的赋值过程。注意赋值的对象发生了变化：原来是属性的赋值，现在是匿名类型的赋值。这样的赋值我们只需要写两次 `let` 从句。这样可以定义和赋值到同一个变量里去。接着，我们判断名字和性别是否都不同，它被改成了 `pair1` 和 `pair2` 两个匿名类型之间的属性的比较。最后我们的 `where` 要保证第一个对象必须是男性，所以现在我们也需要同步地改变写法，改用 `pair1` 来比较。

> 注意改变后的判断语句可能会变得更长。这也是没办法的事情。另外，结合之前我们学了匿名类型的语法的知识点，我这里留一个问题希望各位思考一下。
>
> 请问，改写的代码 `where pair1.Name != pair2.Name && pair1.Gender != pair2.Gender`（第 11 行）能否写成 `!pair1.Equals(pair2)` 或 `pair1 != pair2`？请说明理由。

## Part 5 总结

总的来说，我们介绍了 `where` 的用法，它表示筛选条件。多个挨着的 `where` 可以合并到一起，用 `&&` 连起来。

下面我们将介绍一个新的从句：`orderby` 从句。