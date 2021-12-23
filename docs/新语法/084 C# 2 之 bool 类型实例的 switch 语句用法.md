# C# 2 之 `bool` 类型实例的 `switch` 语句用法

真香，真香……

## Part 1 为什么 `switch` 早期不能判断 `bool`？

早期 `switch` 语句只让使用对整数类型和字符串类型进行 `switch` 判断，而 `bool` 和浮点数是不行的。浮点数不能用 `switch` 的原因很简单，因为比较的数据是没办法精确表达的，所以这样会导致很多很隐蔽的问题；而 `bool` 不能用的原因是，分支太少。

考虑 `bool` 用在 `switch` 里，会如何？

```csharp
bool condition = ...;

switch (condition)
{
    case true:
        // ...
        break;
    case false:
        // ...
        break;
    default: // What the hell?
        // ...
        break;
}
```

一个 `case true` 和 `case false` 就能表示完全的情况，但 `default` 此时就没有意义了：因为永远都不可能遇到了，前面俩都能直接处理掉。所以，原因就是：分支太少。少到压根用不到 `default`，也确实没必要：条件运算符 `?:` 和 `if`-`else` 完全可以应付这种小场面，你说是吧。

因此，C# 最开始的规则是不允许 `bool` 和浮点数作为判断的；而 `char`、`string`、整数类型都是可以使用 `switch` 的。

## Part 2 为什么 C# 2 又可以这么写了呢？

C# 2 允许了 `bool` 作为 `switch` 的判断。当然，它肯定是不让你 `case true` 和 `case false` 都有了的情况下还写一句 `default` 的。C# 2 规定，`switch` 判断 `bool` 量的时候，只有如下三种情况：

* `case true` 和 `default`（`default` 等价 `case false`）；
* `case false` 和 `default`（`default` 等价 `case true`）；
* `case true` 和 `case false`。

这三种情况，两种标签判断是可以同时出现的，而 `case true`、`case false` 和 `default` 不允许三种混用。

那么，为什么这种机制在 C# 2 里又可以用了呢？因为三值布尔 `bool?` 类型在 C# 2 里腾空出世。C# 2 的三值布尔类型就是可空的 `bool` 类型；而 `bool` 类型有两种取值情况，再算上 `null`，三值布尔一共是三种取值情况：`true`、`false` 和 `null`。如果还不开放的话，未免就显得很难用：

```csharp
bool? c = true;
if (c == true)
{
    // ...
}
else if (c == false)
{
    // ...
}
else
{
    // ...
}
```

显然 `c == 数值` 就不必写两次。有了 `switch`，代码的可读性会提高不少：

```csharp
switch (c)
{
    case true:
        // ...
        break;
    case false:
        // ...
        break;
    default: // Here 'default' is equivalent to 'case null':
        // ...
        break;
}
```

当然，这里你写 `case null:` 代替 `default:` 也是可以的。

多了一种情况后，单纯的 `true` 和 `false` 判断就不够了，因为还多了一个 `null` 的判断情况。

## Part 3 性能上和 `if` 语句还有 `?:` 的区别

那么，`?:` 和 `if` 这种单纯的格式，和 `switch` 写起来，有什么区别吗？

### 3-1 `bool` 的 `switch` 语句

明确地告诉你，没有区别。我们来看这个代码：

```csharp
bool condition = ...;
switch (condition)
{
    case true:
        Console.WriteLine("A");
        break;
    case false:
        Console.WriteLine("B");
        break;
}
```

它最终会被编译器改写成这样：

```csharp
if (condition)
{
    Console.WriteLine("A");
}
else
{
    Console.WriteLine("B");
}
```

是吧，是一样的吧。

### 3-2 `bool?` 和 `switch` 语句

你可能会问三值布尔的情况。假设代码是这样：

```csharp
switch (condition)
{
    case true:
        Console.WriteLine("A");
        break;
    case false:
        Console.WriteLine("B");
        break;
    case null:
        Console.WriteLine("C");
        break;
}
```

它会被编译器改写代码成这样：

```csharp
if (condition.HasValue)
{
    if (condition.GetValueOrDefault())
    {
        Console.WriteLine("A");
    }
    else
    {
        Console.WriteLine("B");
    }
}
else
{
    Console.WriteLine("C");
}
```

三值布尔运算总归是可空值类型吧，所以它也具有 `HasValue` 和 `GetValueOrDefault` 这些相关的成员。稍微注意一下，`GetValueOrDefault` 方法将获取 `bool?` 类型的真正存储数值；如果失败，则获取当前类型的默认数值（也就是 `default(T)` 表达式的结果）。因此，不论有没有数值，`bool?` 类型也会通过这个方法返回 `bool` 的结果。

## Part 4 没了？

嗯，没了。