# 类型良构规范（五）：`IEnumerable` 接口

今天我们要完成最后一个常用接口的学习：`IEnumerable` 接口。`IEnumerable` 接口是最常见也是最重要的接口类型，它的重要程度基本上远超过 `IDisposable` 接口，基本和  `IComparable` 接口可以并驾齐驱。但是呢，内容可能也不是你想象得那么简单，所以需要多思考今天的东西。

## Part 1 `IEnumerable` 接口和 `foreach` 循环

试想一下，像是 `ArrayList` 这样的数据结构，可以从类型本身就可以看出，它并不是基本的数据类型。但为什么这样的数据类型也直接支持 `foreach` 循环呢？难道它有什么魔法吗？

这是因为，**只要这个类型实现了 `IEnumerable` 接口后，这个类型就可以使用 `foreach` 循环了**。它和前一节说明 `IDisposable` 接口和 `using` 声明的规则差不多：只要实现了 `IDisposable` 接口，这个对象类型就可以使用 `using` 指令来完成对象内存自动释放的行为。那么，我们如何去自己实现 `IEnumerable` 接口呢？下面我们将给大家介绍两种可实现 `IEnumerable` 接口的方式。

## Part 2 通过可自动迭代的类型直接实现

### 2-1 引例

考虑一种情况，我现在实现了一个数据类型，叫 `List`。这个数据类型和 `ArrayList` 差不多，不过是我自己实现的数据类型。大概设计的代码有这样一些：

```csharp
public class List
{
    private const int MaxCapacity = 10;

    private int _count = 0;
    private readonly int[] _elements = new int[MaxCapacity];

    public List() { }

    public int Count { get { return _count; } }

    public int this[int index] { get { return _elements[index]; } }

    public void Add(int element)
    {
        if (_count >= MaxCapacity)
            return;

        _elements[_count++] = element;
    }
}
```

因为这个对象类型没有实现 `IEnumerable` 接口，因此我们并不能使用如下的代码书写格式：

```csharp
foreach (object element in list)
    ...
```

现在，我们就把这个不可能变为可能。

首先，我们思考一下，我们要想让整个序列产生迭代，显然我们不能直接使用这个 `_elements` 字段完成，因为这个字段里一共可以容纳 10 个元素（请注意参看 `MaxCapacity` 常量在这个类型里的使用），但是我们并不一定让这个类型装满 10 个元素，所以这个数组是有空位的。那么，我们需要把数组的其中前面的那一部分取出来，然后作为结果迭代出来。因此，我们需要专门写一个方法，来把有效的数据取出来。

有效的数据只有哪些呢？在这个类型里包含一个 `_count` 字段记录了目前存储了多少个元素，这正好可以完成我们的任务：我们只需要提取从第 0 号下标的元素到第 `_count - 1` 号下标的元素就可以了。因为数组是从 0 开始算下标的，所以 0 到 `_count - 1` 一共是 `_count` 个元素。

那么，我们可以这么去设计代码：

```csharp
private int[] ToArray()
{
    int[] result = new int[_count];
    for (int i = 0; i < _count; i++)
        result[i] = _elements[i];

    return result;
}
```

即直接拷贝 `_elements` 数组里的元素到 `result` 里，然后返回出去即可。当然，因为数组从 `Array` 类型派生，因此 `Array` 类型里包含一个 `Copy` 静态方法，可以直接完成我们的拷贝复制数组元素的任务，而不需要自己写循环：

```csharp
private int[] ToArray()
{
    int[] result = new int[_count];
    Array.Copy(_elements, 0, result, 0, _count);

    return result;
}
```

简单说一下 `Array.Copy(_elements, 0, result, 0, _count);` 语句的五个参数的意思。

* 第一个参数（`_elements`）：表示拷贝数组的时候，从哪个数组里取数据；
* 第二个参数（`0`）：表示拷贝数组的时候，从这个数组的哪个下标开始；
* 第三个参数（`result`）：表示拷贝数组的时候，拷贝到哪个数组里去；
* 第四个参数（`0`）：表示拷贝数组的时候，这个接收的数组从哪个下标开始往后放元素；
* 第五个参数（`_count`）：表示拷贝多少个元素。

这个方法相当好用，它避免了你手写循环带来的隐藏的 bug。这个方法甚至可以移动数组里的元素，即第一个和第三个参数传入同样的数组，这个方法也是可以完成任务的，只要你写的数据是合适的。

> 但是切记，`result` 数组要能使用 `Array.Copy` 方法之前，需要先自己 `new` 一下，因为 `result` 如果没有合理初始化的话，参数是不能参与使用的，甚至会产生异常。一定要保证这个数组用 `new` 初始化的时候，初始化容量要足够放一会儿存进来的所有元素。

有了这个之后，`ToArray` 方法最终会返回一个新的数组，表示里面的合理的数据。因此，我们为了让这个 `List` 类型可以使用 `foreach` 循环，我们需要做如下两个步骤。

### 2-2 在类型声明上添加 `IEnumerable` 接口

这么写就可以了：

```diff
- class List
+ class List : IEnumerable
```

需要注意的是，今天说的 `IEnumerable` 接口并不在 `System` 命名空间里，而是在 `System.Collections` 里。这个是第一个目前没有在 `System` 命名空间里的类型。因此，如果要写上接口名，你需要添加 `using` 指令：

```csharp
using System.Collections;
```

或者是直接写全名：

```csharp
class List : System.Collections.IEnumerable
```

都是可以的。这样就算完成了第一步。

不过，接口里面的方法还没实现呢。没关系，马上我们来说怎么去实现。

### 2-3 实现 `IEnumerable` 接口

这个接口有点绕的地方是，它里面需要实现的方法是这样的：

```csharp
interface IEnumerable
{
    IEnumerator GetEnumerator();
}
```

这个接口稍微有点麻烦的地方是，这个里面唯一一个需要实现的成员是一个方法，并且返回了一个新的接口类型：`IEnumerator` 接口。这个接口是什么东西呢？

如果 `IEnumerable` 接口是用来和 `foreach` 绑定起来的迭代器，那么 `IEnumerator` 就是提供迭代功能的提供方。这么说不太懂，你可以把 `IEnumerable` 接口想象成一个完整的工具，它包裹了一系列的功能可以提供给你直接用；但是 `IEnumerator` 接口则是这个完整工具的其中一个部件，它的存在才保证了这个工具的这个功能能够得以使用。

而实际上，数组本身已经实现了这一套的迭代功能，所以，你可以这么去写代码：

```csharp
IEnumerator result = ToArray().GetEnumerator();
```

此时的 `ToArray` 方法就是前面我们实现的方法；而这个方法的结果是一个 `int[]`。我们经常使用 `foreach` 循环，就是因为这个数组本身就已经实现了 `IEnumerable` 接口，所以我们可以直接使用 `GetEnumerator` 方法。

另外，这个语句放在哪里呢？放在 `List` 类型里的需要实现的 `GetEnumerator` 方法里：

```csharp
class List : IEnumerable
{
    ...

    public IEnumerator GetEnumerator()
    {
        IEnumerator result = ToArray().GetEnumerator();
        return result;
    }
}
```

这样就 OK 了。因为你实现了 `List` 类型的 `IEnumerable` 接口了，因此，我们直接可以直接使用 `foreach` 循环对于 `List` 类型的对象了：

```csharp
List l = new List();
l.Add(1);
l.Add(2);
l.Add(5);
l.Add(10);

foreach (object element in l)
    Console.WriteLine(element);
```

注意，第 7 行代码写的是 `object element in l` 而不是 `int element in l`，这一点我们将在后面的内容给大家介绍原因。实际上这里是可以写 `int element in l` 的，不过它牵扯到转换机制需要后面一节才能讲明白。这里就先不说了。

> 最后顺带一说，所有的循环过程都称为**迭代**（Iteration）。比如说 `foreach` 循环，我们需要写成 `foreach (object element in list)` 的格式，那么这里就可以说成“我现在在用 `foreach` 迭代 `list` 列表”。就是这么一个说法。

## Part 3 自己实现迭代器

### 3-1 `foreach` 循环的等价代码以及原理

要知道，`foreach` 循环等价于如下的这个行为：

```csharp
IEnumerator enumerator = instance.GetEnumerator();
while (enumerator.MoveNext())
{
    // Code using the value 'enumerator.Current'.
}
```

简单说一下这个代码的意思。首先第一行 `enumerator` 用 `instance.GetEnumerator` 初始化。这个 `instance` 假设为某个已经实现了 `IEnumerable` 接口的类型实例化出来的对象实例；然后 `GetEnumerator` 方法的调用，就是我们前面实现的 `IEnumerable` 接口里这个必须实现的方法 `GetEnumerator`。

接着，`foreach` 改成了一个 `while` 循环。循环的条件是看这个 `enumerator` 是否能“前进”。此时，你把 `enumerator` 变量想象成指向数组的第一个元素的“头指针”，每执行一次 `MoveNext` 方法的时候，这个“指针”就会往后移动一个元素单位，指向第二个元素；接着是第三个元素、第四个元素，等等。如果“指针”可以往下移动，那么 `MoveNext` 方法就会返回 `true`；如果元素到底了，`foreach` 相当于说是要终结了，那么这个 `MoveNext` 方法的调用就会得到 `false` 的结果，这样 `while` 循环就不再得到执行。

接着，在 `while` 循环里面，我们使用 `enumerator.Current` 来获取当前“指针”指向的元素到底是哪个。然后你可以在里面写一些很复杂的处理代码，什么 `if` 啊之类的，都是可以的，只要你知道了 `enumerator.Current` 用于取值就 OK。

最后，在 `while` 条件不成立，即整个循环都执行完成后，整个 `while` 循环结束，这也就意味着我们等价的 `foreach` 循环也就结束了。这个是 `foreach` 的底层。

### 3-2 完成迭代器实现

如果你需要对性能要求较高的话，在值类型（结构）实现 `IEnumerable` 很有可能导致装箱，或者复杂的处理实例化一个引用类型的对象处理，导致性能的损失。C# 为我们提供了一种机制，使得我们甚至不需要实现 `IEnumerable` 接口也可以使用 `foreach` 循环，不过这个处理就比较麻烦了。

#### 3-2-1 鸭子类型的概念

要想知道我们如何使用下面的内容，我们必须引入一个新的概念：**鸭子类型**（Duck Type）。鸭子类型是一个术语（尽管这个词你不一定看得出来它是一个术语词），它表示一种数据类型，即使不实现接口，C# 语言也可以让其支持一些特殊语法的魔法。

鸭子类型的“鸭子”取自一句编程界~~英语~~里的谚语：如果这个玩意儿长得像鸭子，叫声像鸭子，那么它就是鸭子。这句话看起来好像有着很多逻辑问题，但它被广泛使用到编程里。

如果一个数据类型，满足 C# 语法里规定的一些指定条件的话，那么它即使不去实现接口，也相当于是做到了接口该做的事情，那么这个类型我们就称为鸭子类型。就比如说前文说到的这个 `IEnumerable` 接口。

#### 3-2-2 可使用 `foreach` 循环的鸭子类型条件

如果一个类型满足了如下的条件，那么这个类型就是可以使用 `foreach` 循环的：这个数据类型包含一个 `public` 修饰的 `GetEnumerator` 方法，并返回一个数据类型（本来是返回 `IEnumerator` 类型的对吧）需要满足如下的条件：

1. 这个数据类型里带有 `public` 修饰的、非静态 `Current` 属性；
2. 这个数据类型里带有 `public` 修饰的、非静态 `MoveNext` 方法，无参但返回 `bool` 类型。

只要满足这样的条件，那么这个类型就可以作为 `IEnumerator` 的替代者，然后整个类型也不需要实现 `IEnumerable` 接口，也可以使用 `foreach` 循环。

#### 3-2-3 例子

假设我有一个 `BitSet` 的结构，它用来存储和记录哪些编号上的位置是“开”还是“关”的值。举个例子吧，比如我要表示这个班级哪些人及格了，我用 `BitSet` 类型（把学生先编号 1 到 64），然后把对应编号上的数位用 1 表示这个人及格了，而 0 表示这个人没及格。大概 `BitSet` 类型这么用，那么它的基本实现是这样的：

```csharp
public struct BitSet
{
    private const int MaxCapacity = sizeof(long) * 8; // 64.

    private long _mask;

    public int Count { get { return GetCount(); } }
    public long Mask { get { return _mask; } }

    public int this[int index]
    {
        get
        {
            for (int i = 0, count = 0; i < MaxCapacity; i++)
                if ((_mask >> i & 1) != 0 && ++count == index)
                    return i;

            return -1;
        }
    }

    public void Add(int position) { _mask |= 1L << position; }
    public void Remove(int position) { _mask &= ~(1L << position); }
    private int GetCount()
    {
        int result = 0;
        for (int i = 0; i < MaxCapacity; i++)
            if ((_mask >> i & 1) != 0)
                result++;

        return result;
    }
}
```

比如说我这么使用代码：

```csharp
BitSet b = new BitSet();
b.Add(1);
b.Add(2);
b.Add(5);
b.Add(10);
```

就可以把一系列的元素追加到 `BitSet` 的实例里去。那么，这个类型要想遍历里面所有是 1 的数值的编号，怎么做呢？

### 3-3 实现 `Enumerator` 嵌套结构

我们必须从低自上分析和构建这个数据类型。因为我们需要使用 `GetEumerator` 方法来完成 `BitSet` 数据类型的 `foreach` 循环的使用功能，但因为它需要返回一个非 `IEnumerator` 的数据类型，但我们尚未实现，所以我们需要先实现这个替代数据类型。

这里最合适的做法是，在 `BitSet` 的下面追加一个嵌套结构叫做 `Enumerator`，专门表示一个用于实现 `Current` 属性和 `MoveNext` 方法的类型。然后，因为它实现了这些成员，因此我们可以套用到 `BitSet` 类型里面需要实现的 `GetEnumerator` 方法里当返回值了：

```csharp
public Enumerator GetEnumerator()
{
    return new Enumerator(this);
}
```

不过这里稍微要注意的是，这个 `Enumerator` 是为这个 `BitSet` 类型服务的，但因为 `BitSet` 是实例在迭代，因此如果不给 `Enumerator` 传递这个实例的话，那么 `Enumerator` 到底应该迭代什么东西呢？所以，我们需要完成 `this` 实例的传入，才能保证下面能够使用上这些合适的数据。

那么，我们来实现 `Enumerator` 类型吧。

```csharp
public struct Enumerator
{
    private readonly long _mask;
    private int _index;

    public Enumerator(BitSet bitSet) { _index = -1; _mask = bitSet._mask; }

    public int Current { get { return _index; } }

    public bool MoveNext()
    {
        while (++_index < MaxCapacity)
            if ((_mask >> _index & 1) != 0)
                return true;

        return false;
    }
}
```

如代码所示，这样的代码就算是完成了对 `Enumerator` 类型的一个基本实现过程。首先，我们需要把 `BitSet` 里的这个掩码 `_mask` 字段传过来，因为它是用来迭代的；接着，我们要给 `_index` 字段赋值 -1。这个 `_index` 字段就表示我们一会儿要迭代返回出去的编号。初始化的时候赋值的是 -1 而不是 0，一会儿我们来说为什么。

接着，我们因为要把 `_index` 字段作为迭代的结果返回出去，所以我们的 `Current` 属性的 `get` 方法里写的是 `return _index;`。最后我们来看 `MoveNext` 方法，也是这个类型里最难的方法。

这个方法主要是控制每次 `while` 循环的条件。既然是条件，那么必然是用返回值体现“我这次移动游标是不是可以”。我们把 `BitSet` 数据类型设计成用比特位的方式，就是为了节省内存空间的同时能够最大化表示出信息。那么，我们要迭代整个数据类型，一般来说就是按比特位搜寻，看哪些数位上是数字 1。如果是 1 的，那么对应的编号就是我们要返回的结果；而如果对应位是 0，那么就说明这个位置不是我们要搜索的数值，就得继续移动游标。

不过，我们总不能移动一次游标就停止吧。比如说这个比特位序列是 `010001`，按照计算顺序，我们先从最右侧的 1 开始，但中间有 3 个 0，我们要取的是这两个 1 的位置（即编号，因为 `010001` 的第 0 和第 4 个比特位是 1，所以我们就相当于是把 0 和 4 作为结果进行迭代），可如果 `MoveNext` 只移动一下游标就退出的话，显然是不够的：因为移动到中间 0 的位置的时候，这些 0 并不是合适的数据，所以游标会在这个时候继续往下移动，直到碰到下一个 1 为止。那么，这个设计起来就比较复杂了：**如果这个当前游标指向的比特位是 1，那么我们就算找到了合适的结果，那么 `MoveNext` 方法返回 `true` 作为结果即可；但是如果我们这个当前游标指向的比特位是 0 的话，那么就说明不是合适的数据，就继续往下移动游标，知道游标指向了数字 1 为止；另外，如果在移动游标的过程中超过了这个 `BitSet` 存储的总容量（64 个比特）的话，也返回 `false` 作为结束，表示我无法继续移动游标了，象征迭代过程完全结束**。

那么，写出来的话，代码就跟上面这个代码是一样的。

```csharp
public bool MoveNext()
{
    while (++_index < MaxCapacity)
        if ((_mask >> _index & 1) != 0)
            return true;

    return false;
}
```

首先进来就给 `_index` 字段增大一个单位。看到没有，这就是为什么我们初始化的时候给 `_index` 字段设置 -1 的真正原因。因为 `MoveNext` 是刚开始必然会执行的，所以初始化为 -1 是为了保证这里不出 bug。这里设置为 -1，那么 `MoveNext` 方法移动一次游标直接让 `_index` 字段从 0 开始，这才是符合逻辑的。

接着，判断 `_index` 此时的数值是不是小于 `MaxCapacity` 常量（常量值是 64）。如果小于，我们就认为我们迭代过程没有超出整个 `_mask` 存储的范畴，而 `_index` 此时充当编号的角色，也可以认为是位右移运算的移动次数。如果 `while` 条件成立，我们就去始终循环，看是否当前这个 `_index` 上的比特位是不是 1。计算当前位置的比特位是不是 1 我们用的是 `_mask >> _index & 1` 的这个公式。按照优先级规则，我们是先计算 `>>`，然后是 `&`。先看 `_mask >> _index`，表示这个 `_mask` 位右移 `_index` 这么多个比特位，目的是把我们要找的这个比特位始终放在最后一位上去。接着，我们使用 `& 1` 计算这个比特位是不是 1。如果是 1，那么 `1 & 1` 才会得到 1；如果不是 1 而是 0 的话，就会得到 `0 & 1` 的结果 0。

此时，我们按照这个公式计算，当前比特位是不是 1。如果是 1 的话，我们就认为这个比特位是我们需要的结果，于是，我们返回 `true` 表示迭代是成功的。

如果 `while` 循环一轮走下来，都没有找到 1，就说明遇到的数字全是 0，那么我们自然返回 `false` 就表示迭代结束，这样就可以了。

那么，整个 `Enumerator` 数据类型的实现我们就算完成了。

### 3-4 使用

既然已经实现了 `Enumerator` 类型，以及给 `BitSet` 配备了 `GetEnumerator` 方法，实现是正确的、合适的的话，我们就可以使用 `foreach` 循环了：

```csharp
foreach (int element in b)
    Console.WriteLine(element);
```

这个时候，我们写的是 `foreach (int element in b)`。这里的 `b` 是表示 `BitSet` 的实例。那么这里为什么是 `int` 作为 `foreach` 每次迭代的变量类型呢？

## Part 4 关于 `foreach` 的迭代变量类型

我们已经实现了 `foreach` 循环可使用的两种实现方式，一种是通过实现接口 `IEnumerable` 里的 `GetEnumerator` 方法来完成；另外一种则是通过自定义一个数据类型，然后完成迭代的操作。不过，可以看到 `foreach` 循环的迭代变量，类型是不一样的，这是为什么呢？

这其实是取决于你实现的 `GetEnumerator` 方法，返回值的那个类型，它里面的 `Current` 到底是什么类型的。这句话有点绕也有点长，说白了，就是看这个 `Current` 属性是什么类型，那么这个迭代变量就理应是什么类型的。

可以看到，后面这个自己实现的类型里，`Current` 用的是 `int` 类型（毕竟里面的 `_index` 字段是 `int` 类型的），所以 `foreach` 循环里，我们用的是 `int`；而前面是通过接口的 `GetEnumerator` 方法实现的，但它为什么是 `object` 类型呢？

我刚才说过，是看 `Current` 属性的类型。这个通过接口本身去实现的方式，我们是不是得去看 `IEnumerator` 接口里面的 `Current` 类型啊？有人可能就会问，我前面也没提到 `IEnumerator` 里有 `Current` 属性啊。你想想，我刚在 Part 3 完整阐述了自定义类型实现 `foreach` 循环的迭代了，那么条件是不是应该和原本使用 `IEnumerable` 接口实现的逻辑是一致的啊？既然是一致的，那么按常理来说，`IEnumerable` 接口里就必然有 `GetEnumerator` 方法，而 `GetEnumerator` 方法就必然返回 `IEnumerator` 接口，而 `IEnumerator` 接口和我们前面自己定义的 `Enumerator` 应该就是异曲同工的设计，里面就应该包含 `Current` 属性。

实际上，查阅文档资料（网上查资料，或者直接去看元数据^[1]^），都可以看到，实际上确实包含这个 `Current` 方法，不过它的返回值是 `object` 类型的。正是因为如此，我们最后使用接口实现的 `foreach` 循环的方式，使用的是 `object` 类型作为迭代变量的类型。

> [1]：**元数据**（Metadata），这个术语有点高大上。实际上，Visual Studio 提供了一个机制，让我们在不看源代码的情况下，查看库里包含的这个数据类型，里面都有什么成员。查看方式是这样的：首先，我们瞄准我们书写的代码里的其中一个数据类型，比如鼠标单击 `foreach (int element in list)` 里的这个 `int`，然后按键盘 F12（或者点鼠标右键，选择“查看定义”，Visual Studio 的英文版里写的是 Go to Definition），就可以进入到元数据页面了，然后你就可以查看到，这个数据类型里都有一些什么方法。按照这个方式，我们就可以查看到 `IEnumerator` 接口里是不是带有 `Current` 属性，并且是什么类型的。

光有这点语法内容还不够。因为我们每次都直接写 `object` 太麻烦了。所以 C# 的 `foreach` 循环是可以写具体类型的，即使这个迭代的数据类型并不是它：

```csharp
List list = new List();
// Add elements.

foreach (int item in list)
    Console.WriteLine(item);
```

如代码所示，我们知道里面是一个一个的 `int` 类型的元素，但我们因为用的是 `IEnumerable` 接口里的 `GetEnumerator` 方法实现的行为，因此这里的类型原本必须是写 `object` 的。可问题在于，我们既然都知道里面是 `int` 的元素，那么每次都写 `object` 的话，`Console.WriteLine` 方法里使用元素还得去强制转换。所以，C# 提供了一个固有语法：**如果这个类型可以使用 `foreach` 循环的话，那么这个迭代变量的数据类型可以取这个类型可以通过强制转换或隐式转换变化过去的那个类型**。这句话的意思是，比如我 `foreach` 循环里的迭代变量的类型应该是 `Shape` 类型的，那么我们在迭代变量的类型上可以改写成 `Shape` 或者 `Shape` 自己的派生类型（比如 `Circle` 类型啊、`Rectangle` 类型啊这些）都是允许的；但跟 `Shape` 类型无关的别的数据类型是不行的，比如这个时候，因为迭代类型是 `Shape`，结果你用 `int` 类型去接收，就是不行的。

那么，我们就把 `IEnumerable` 接口给大家介绍了一下。那么接口使用的基本内容就给大家介绍到这里。
