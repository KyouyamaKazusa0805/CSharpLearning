# C# 3 之查询表达式（七）：LINQ 阶段性练习（一）

咱们今天来做做练习题。学了很多的 LINQ 的知识了，关键字就算是告一段落。因为内容繁多，所以咱们搞一个练习题的专题。所有题目的答案都会写在当前部分的最后一点的地方，方便翻阅，但不建议做题期间进行参考。

## 题目

请给出如下的例子需要满足的查询表达式。

**示例**：现在有一个集合 `int[] numbers`，包含一系列元素。先要获取其中的所有正奇数（或者叫单数），请写出查询表达式。

**示例题答案**：

```csharp
var selection =
    from number in numbers
    where number % 2 == 1
    select number;
```

**解答**：这个问题不大，就不展开说明了。注意取模运算符 `%` 的结果和被除数的正负性一致，所以 `number % 2 == 1` 包含了对 `number > 0` 的判断过程，因为取模运算的结果比较是和 1 这个正整数在比较，而如果是负奇数的话，那么取模运算结果应该是 -1 而不是 1，此时就不再满足条件 `number % 2 == 1`，因而会被筛选掉。

下面是练习题。一共有 7 个题。

* （容易）**题目 1**：给出一个集合 `int[] numbers`，包含至少两个元素，而且互相不重复。现在要获取其中任意两个数字的和为 17 的这对数字，请写出查询表达式。
* （一般）**题目 2**：给出一个集合 `int[] numbers`，包含至少两个元素，可能有相同数字，也可能数字之间不相同。现在要获取所有其中任取的两个数字之间的乘积结果，请写出查询表达式。
* （一般）**题目 3**：给出一个集合 `string[] words`，请给出所有单词里用到的所有字母的使用个数的情况，并按出现个数进行升序排序。
* （困难）**题目 4**：请使用查询表达式得到星期一到星期天这七天的英语单词（Monday、Tuesday 等）。不用考虑国际化，换句话说，外国有拿周日当成第一天的，中国用的是周一当第一天的。咱们不考虑谁第一天这个问题，只需要你能够得到合适的这七个单词的正确结果即可。
* （困难）**题目 5**：请使用查询表达式来打乱一个序列 `int[] numbers`。
* （一般）**题目 6**：给定一个集合 `string[] words`，请使用查询表达式给出这个单词序列里只出现了一次的单词。
* （困难）**题目 7**：给定一个集合 `string[] words`，请使用查询表达式给出这个单词序列里全大写的单词。比如说单词序列是 `{ "abc", "Abc", "ABC" }`，那么整个序列就只有第三个元素 `"ABC"` 是全大写字母的单词，那么查询表达式找的就是这样的单词。
* （极难）**题目 8**：给定一个锯齿数组 `int[][] matrix`，请将其当成矩阵，将其进行转置。请使用查询表达式来表示转置后的序列。所谓的转置就是行列变换：将每一行改成每一列的形式。

## 答案

**题目 1**

```csharp
var selection =
    from n1 in numbers
    from n2 in numbers
    where n1 != n2 && n1 + n2 == 17
    select new { First = n1, Second = n2 };
```

**题目 2**

```csharp
var selection =
    from i in Enumerable.Range(0, numbers.Length - 1)
    from j in Enumerable.Range(i + 1, numbers.Length - 1 - i)
    select numbers[i] * numbers[j];
```

本题用到了一个不是很好想到的方法 `Enumerable.Range`。该方法可以允许用户产生一个序列，序列的开始数值是第一个参数，第二个参数则表示从这刚才给的第一个参数的数值开始，一共要迭代多少个数字。比如说 `Enumerable.Range(0, 3)` 就表示从开始，迭代 0、1、2 这三个数字；`Enumerable.Range(3, 4)` 就表示从 3 开始，迭代 3、4、5、6 这四个数字。

通过这样的笛卡尔积，我们可以完成 `i` 和 `j` 的类似两层 `for` 循环的效果。注意我们给出的笛卡尔积的限制范围，第一个参数只需要从 0 到“数组长度 - 2”即可。

> `numbers.Length - 1` 作为第二个参数表示的是迭代的总元素个数，而这个数值是数组总元素数量还少一个，说明我们并未把数组整个序列遍历完成，而还有一个元素并未被遍历到，毕竟第一个参数的 0 要求我们迭代的初始是从 0 开始的

而第二个变量 `j` 则需要避免和 `i` 重复，因此故意选取 `i + 1` 作为开始迭代的初始数值。注意，乘积是满足交换律的，换句话说就是两个数字相乘，谁乘以谁结果都是一样的。然后，迭代的总元素数量应为“长度 - 1 - i”。这个式子需要理解为“长度 - (i + 1)”，是因为我们第二个变量 `j` 必须要迭代到数组的末端，而第一个参数我们控制的时候是迭代到“长度 - 2”的，所以并未考虑最后一个元素，因此我们需要保证第二个参数要能够到这里去。`i` 越大，我们遍历的元素数量就得越少，这是为了保证数组越界的问题。比如 `i` 是 0 的时候，式子“长度 - i - 1”就等于是“长度 - 1”，而初始迭代的位置是 `i + 1`，此时 `i` 为 0 所以就是 1，因此 `j` 的迭代范围是从 1 开始，一直能够到“长度 - 1”的位置上去。这点要搞清楚。

最后，因为 `i` 和 `j` 已经保证了不重复，所以我们直接将 `i` 和 `j` 视为遍历的索引，然后将其放进索引器里参与运算，就有了 `numbers[i] * numbers[j]` 的写法，这就可以得到最终结果了。

**题目 3**

```csharp
var selection =
    from word in words
    from letter in word
    group letter by letter into letterGroup
    orderby letterGroup.Count() ascending
    select letterGroup;
```

**题目 4**

```csharp
// 答案 1
var selection =
    from dayOfWeek in Enum.GetValues<DayOfWeek>()
    select dayOfWeek.ToString();

// 答案 2
var selection =
    from i in Enumerable.Range(0, 7)
    select ((DayOfWeek)i).ToString();
```

答案 1 用的是 `Enum.GetValues` 方法获取一个枚举类型的字段，来迭代；而答案 2 则使用 `Enumerable.Range` 方法获取一个从 0 到 6 的完整数字序列，然后执行对 `DayOfWeek` 这个枚举类型的强制转换（整数和枚举类型可以互相转换），最后输出结果。如果你实在是不知道 `Enumerable.Range` 的话，也可以写成数组：`new[] { 0, 1, 2, 3, 4, 5, 6 }`。

**题目 5**

```csharp
var selection =
    from number in numbers
    orderby Random.Shared.Next() ascending
    select number;
```

本题难度比较大。`orderby` 里是可以跟一个跟 `numbers` 序列里元素无关的东西作为排序依据的。我顺势就写成了随机数生成的操作，这样 `orderby` 得到的排序依据自然就是这些生成的随机数值。我只要让每一个元素都绑定上这些随机数数值，然后让随机数数值进行大小排序，是不是就意味着原来的序列也就排序了啊？

**题目 6**

```csharp
var selection =
    from word in words
    group word by word into wordGroup
    where wordGroup.Count() == 1
    select wordGroup.Key;
```

利用好 `group x by x` 的语义就是这个题目破题的关键。我们知道 `group` 和 `by` 是用来分组的，那么如果拿它自己来分组是啥意思呢？是不是就是找到和自己相同的单词，然后构成一组？那么整个序列就可以通过这样的分组机制得到一组一组的单词序列，每一组内的单词是相同的，而组和组之间是不同的单词。那么只要判断每一组到底是不是只有一个元素，就可以知道它是不是只出现一次了。

**题目 7**

```csharp
var selection =
    from word in words
    where word == word.ToUpper()
    select word;
```

这个题目的 `word == word.ToUpper()` 不好想到。

**题目 8**

转置就是行列交换，因此落实到每一个元素的话，自然就是 `a[x][y]` 到 `a[y][x]` 的操作。

```csharp
var selection =
    from i in Enumerable.Range(0, matrix.Length)
    select from eachRow in matrix select eachRow[i];
```

属于是套娃用法了。注意这里的套娃是写在 `select` 后面的。这个用法出现极其罕见，不过这里确实有帮助。本题需要转置整个数组，由于锯齿数组是数组的数组，所以我们可以试着遍历数组，然后进行逐个元素的转换。

这里的 `Enumerable.Range(0, matrix.Length)` 等于是获取整个锯齿数组有多少个元素。注意，这个数组是锯齿数组，所以要被看成是 数组的数组，因此整个序列只有 5 个元素——它是由 5 个 `int[]` 类型的元素构成的一个一维数组。那么，这个地方迭代的时候，相当于是  `new[] { 0, 1, 2, 3, 4 }` 的意思，只不过写成刚才那样就会比较通用化一些。

接着，注意套娃用法。我们在里面使用了一个查询表达式，作为外层查询表达式的映射表达式结果。看下内层这个查询表达式，`from eachRow in matrix` 还比较好理解，因为 `matrix` 是数组的数组，所以它迭代出来的每一个变量自然就是 `int[]` 类型的。而序列是横着写的，所以我们可以认为这个矩阵其实就是一行一行存储的这么一个数据结构。接着，`eachRow[i]` 是在获取这个行里的指定位置上的元素。看清楚，这里我们用的是外层查询表达式里的 `i` 变量。而一次外层的迭代，我们就可以获取一整个查询表达式的结果，这个才是这个完整查询表达式的书写逻辑。

可以看到，`i` 的每一次更新，都会引发一整个内层查询表达式 `from eachRow in matrix select eachRow[i]` 的表达式完整返回。`i` 是 0 的时候，我们可以得到 `from eachRow in matrix select eachRow[0]`，是不是就是在获取矩阵每一行的第 1 个元素？而再次更新  `i` 为 1 的时候，这个查询表达式就变为了获取矩阵每一行的第 2 个元素？

这么算下来，我一次 `i` 的变化，都映射到“每一行的第 n 个元素”，这是不是就是在取出每一列的元素？所以，我们在调用和使用这个 `selection` 变量的时候，可以发现，它需要两层循环来迭代，大概是这样的格式：

```csharp
foreach (var row in selection)
{
    foreach (var number in row)
    {
        Console.Write(number + " ");
    }
    Console.WriteLine();
}
```

是的，这次我们需要两层循环。外层循环是 `selection` 的每一个元素，对应了矩阵的每一列的元素。然后内层则表示遍历的是每一个 `selection` 元素（是一个序列）里的每一个元素。

这么一来，自然就是转置成功了。