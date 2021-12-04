# 类型良构规范（二）：`IFormattable` 接口

在说完前文给定的这些基本内容后，我们应该对面向对象有了一个全新而又陌生的认识。这些内容对于我们来说不得不去接受，即使它比较多。以后我们会在程序项目里，或者在你自己的程序里使用到它们。为了以后写出来的代码更具有可读性，我们才有了这个篇章的内容。

本篇章的内容还有非常多其它的东西，除了基本规范外，我们还要对一些 C# 的库里自带的数据类型（特别是接口）作出一定程度的介绍，比如

* `IFormattable` 和 `IFormatProvider`
* `IEquatable`、`IComparable` 和 `IEqualityComparer`
* `IDisposable`
* `IEnumerable` 和 `IEnumerator`

在这些接口内容全部介绍了之后，我们还会给大家介绍 C# 相关的 SOLID 实现原则，以及设计模式，这对我们以后写代码都会有相当大的帮助。

## Part 1 自定义格式化处理

考虑使用 `Console.WriteLine` 和 `string.Format` 方法的时候，我们会在最开始的第一个参数里传入带有一定数量占位符的模式字符串。后面的参数都是在补充说明占位符的输出效果，以及填充位、格式等信息。比如说之前说到的：`{0:f,10}`这样的字符串，表达的是第 1 个占位符，占 10 个字符空间的显示长度，并以 `"f"` 作为格式化字符串来格式化处理数据。

如果我们定义了一个自己的数据类型的话，我们必然会需要自己对它实现格式化输出的效果，但它和整数这些数据类型不同，它不是系统自带的数据类型，因此我们不能直接在网上查资料就可以学会。因此，这里我们要带给大家的是一个和格式化输出字符串有关系的接口：`IFormattable` 接口。

假设现在我们设计了一个数据类型叫做“温度”。这个类型可以以一个数值的形式表示一个温度的数值，并根据温度的单位呈现不同的温度结果，比如摄氏度、开尔文热力学温度（开氏度）和华氏度。

```csharp
public struct Temperature
{
    public static readonly Temperature MinValue = new Temperature(MinTempValue);

    private const decimal MinTempValue = -273.15M;
    private readonly decimal _temp;

    public Temperature(decimal temperature)
    {
        if (temperature < MinTempValue)
        {
            throw new ArgumentOutOfRangeException(
                string.Format("{0} is less than absolute zero.", temperature)
            );
        }

        _temp = temperature;
    }

    public decimal Celsius { get { return _temp; } }
    public decimal Fahrenheit { get { return _temp * 9 / 5 + 32; } }
    public decimal Kelvin { get { return _temp + MinTempValue; } }

    public override string ToString() { return string.Format("{0:F2} °C", _temp); }
}
```

假设，我们允许用户输入一个摄氏度的温度数值，然后存储进去。然后我们可以通过调用 `Fahrenheit` 属性获取华氏度，或者调用 `Kelvin` 属性获取开氏度。

思考一点。假设我想要通过 `ToString` 获取字符串结果，我们目前能够得到的只能是摄氏度的温度结果。但是，我想通过输出字符串的方式，不同的单位指示，可以有不同的字符串结果显示和输出。最简单的办法是使用枚举类型。

```csharp
public enum TemperatureUnit { Celsius, Fahrenheit, Kelvin }
```

这样就可以解决问题。那么使用的时候，我们只需要重载一个 `ToString` 方法，并多传入一个 `TemperatureUnit` 类型作为参数即可：

```csharp
public string ToString(TemperatureUnit unit)
{
    switch (unit)
    {
        case TemperatureUnit.Celsius:
            return string.Format("{0:F2} °C", _temp);
        case TemperatureUnit.Fahrenheit:
            return string.Format("{0:F2} °F", Fahrenheit);
        case TemperatureUnit.Kelvin:
            return string.Format("{0:F2} °K", Kelvin);
        default:
            throw new ArgumentException("The specified enumeration value is invalid.", unit);
    }
}
```

比如上面这样的代码，这种感觉。不过，这样的代码不够灵活，因为我们指定的枚举类型本身其实指代的是温度的单位，在这个例子貌似是奏效的；但是换一个例子的话，单独使用枚举来表达格式的话，就显得差点意思。所以，最实在的其实是字符串，这样也可以省略定义枚举类型的时间。

那么字符串我们可以这么做。

```csharp
public string ToString(string format)
{
    switch (format)
    {
        case "C":
        case "c":
            return string.Format("{0:F2} °C", _temp);
        case "F":
        case "f":
            return string.Format("{0:F2} °F", Fahrenheit);
        case "K":
        case "k":
            return string.Format("{0:F2} °K", Kelvin);
        default:
            throw new ArgumentException("The specified enumeration value is invalid.", "format");
    }
}
```

确实我们也不需要刻意去改变哪里。这样我们就可以通过这个方法来获取信息了：

```csharp
string s = temperature.ToString("F");
```

## Part 2 更进一步

这样可以解决很大一部分的问题，不过……按道理来说，既然有了这样的处理机制后，这个类型应该是有办法自己处理格式化字符串了，不过试试这个代码：

```csharp
Temperature temp = new Temperature(23M);

string s = string.Format("{0:F}", temp);
Console.WriteLine(s);
```

注意这里我们使用的是 `string.Format` 方法，传入的模式字符串是 `"{0:F}"`，其中的 `"F"` 就是我们在 `ToString` 重载方法里给出的这个处理了的格式化字符串（即 `case "F"` 里的这个 `"F"`）。按道理，因为我们自己实现了这个方法，应该是有办法处理格式化字符串了，但……实际上你在程序运行后看到的结果仍然是 23，而不是华氏度的结果 73.4（至于怎么得到的 73.4，最开始的那个类型设计里，`Fahrenheit` 属性是给出了公式的，这个就自己去算了）。

问题出在哪里呢？出在它并没有真正处理格式化字符串，而是从我们自身的角度出发的、得到的计算公式。因为我们知道调用这个重载方法就可以得到对应结果了，但机器本身是不知道的。

这可怎么办呢？别着急，C# 提供了一个手段可以解决这个问题，那就是 `IFormattable` 接口。如果任何一种数据类型能够实现这个接口，那么这个类型就可以这么使用代码，去得到正确结果。

不过，你会发现，`IFormattable` 接口要求你实现一个方法，也叫 `ToString`，但带有两个参数，一个还是 `string` 类型的格式化字符串（参数名是 `format`），而另外一个参数，却是一个新的接口类型的对象（参数名是 `formarProvider`）。这个第二个参数的类型是 `IFormatProvider`。看这个名字好像一点用都没有，我们也没有接触过这个接口类型。实际上，规范化的设计里，这个类型是用来表示一个专门的类型，这个类型用来专门提供和生成格式化字符串，并提供给别的类型使用的。

举个例子，假设我有一个 `Temperature` 类型表示温度，因为它的格式化字符串不同可以输出不同的结果，于是我们可能会考虑使用重载来搞定。不过，规范化的设计里我们是需要再单独给 `Temperature` 类型创建一个叫 `TemperatureFormatProvider` 的类型，这个类型专门用来生成和产生格式化字符串，以便和避免用户因为不懂格式化字符串而导致无法选择，进而产生调用的异常（格式化字符串错误之类的）。但是，从这个说法上我们可以看出，实际上 `Temperature` 类型完全不需要这个所谓的 `TemperatureFormatProvider` 类型，因为格式化字符串就只有 `"F`"、`"K"` 和 `"C"` 三种，就没有别的了。因此，用户自己去记住它们就行了，完全没有必要单独设计一个新的类型来帮助用户得到格式化字符串。

正是因为如此，我们完全可以不必管第二个参数 `IFormatProvier formatProvider`。但是，我们为了使用上上面我们的目标功能，我们可以考虑这么去实现：

```csharp
public string ToString(string format, IFormatProvider formatProvider)
{
    // Don't forget to handle 'null' value on reference types.
    if (format == null) format = "C";

    // Return the result without using the second argument.
    switch (format)
    {
        case "C":
        case "c":
            return string.Format("{0:F2} °C", _temp);
        case "F":
        case "f":
            return string.Format("{0:F2} °F", Fahrenheit);
        case "K":
        case "k":
            return string.Format("{0:F2} °K", Kelvin);
        default:
            throw new ArgumentException("The specified enumeration value is invalid.", "format");
    }
}
```

是的，代码直接从原来那个方法搬过来就行，而第二个参数我们直接不使用。

> 顺带一提。参数的参数名不需要和基类型或者接口里的这个方法完全一样。一般来说这个是必须要一样的，但是其实可以改名字的。

嗯，既然代码是复制粘贴过来的，那么原来的单参数的 `ToString` 方法我们需要怎么去改变呢？现在我们有三个 `ToString` 方法了，那么这三个方法的代码这样写比较合适：

```csharp
public override string ToString() { return ToString(null, default(IFormatProvider)); }
public string ToString(string format) { return ToString(format, default(IFormatProvider)); }
public string ToString(string format, IFormatProvider formatProvider)
{
    // Don't forget to handle 'null' value on reference types.
    if (format == null) format = "C";

    switch (format)
    {
        case "C":
        case "c":
            return string.Format("{0:F2} °C", _temp);
        case "F":
        case "f":
            return string.Format("{0:F2} °F", Fahrenheit);
        case "K":
        case "k":
            return string.Format("{0:F2} °K", Kelvin);
        default:
            throw new ArgumentException("The specified enumeration value is invalid.", "format");
    }
}
```

我们直接通过使用 `ToString(null, default(IFormatProvider))` 或 `ToString(fprmat, default(IFormatProvider))` 的方式全都去调用带两个参数的重载 `ToString` 方法就可以了。至于第二个参数，我们传什么数值进去其实都无所谓，因为方法里压根没用到。这个时候我们一般写成 `null` 或者 `default(IFormatProvider)`。

`null` 呢，是所有引用类型的默认值，但是万一我们这个参数是值类型的，习惯性地传入 `null` 可能会产生编译器错误，告诉你参数类型不匹配。所以得具体使用的时候要注意。

`default(IFormatProvider)` 呢，代码略长，但是更严谨一点。反正这个参数我们也没有用到，那干脆为了占一个参数的位置，总不能啥数据都不写吧。这里我们就写一个 `default` 表达式来表达这里参数我们是传的默认数值进去。这表示这个参数的数据本身是没有什么特殊意义的数据。

有了这样的实现后，我们就可以运行程序了。可以看到程序运行结果确实是从 `"23.00 °C"` 变成了 `"73.40 °F"` 了，任务我们就算完成了。

## Part 3 来看下完整的实现

下面我们来看下整个完整的实现吧。

```csharp
using System;

class Program
{
    static void Main()
    {
        var s = new Temperature(23M);
        Console.WriteLine(string.Format("{0:F}", s));
    }
}

public struct Temperature : IFormattable
{
    public static readonly Temperature MinValue = new Temperature(MinTempValue);

    private const decimal MinTempValue = -273.15M;
    private readonly decimal _temp;

    public Temperature(decimal temperature)
    {
        if (temperature < MinTempValue)
        {
            throw new ArgumentOutOfRangeException(
                string.Format("{0} is less than absolute zero.", temperature)
            );
        }

        _temp = temperature;
    }

    public decimal Celsius { get { return _temp; } }
    public decimal Fahrenheit { get { return _temp * 9 / 5 + 32; } }
    public decimal Kelvin { get { return _temp + MinTempValue; } }

    public override string ToString() { return ToString(null, null); }
    public string ToString(string format) { return ToString(format, null); }
    public string ToString(string format, IFormatProvider formatProvider)
    {
        if (format == null) format = "C";

        switch (format)
        {
            case "C":
            case "c":
                return string.Format("{0:F2} °C", _temp);
            case "F":
            case "f":
                return string.Format("{0:F2} °F", Fahrenheit);
            case "K":
            case "k":
                return string.Format("{0:F2} °K", Kelvin);
            default:
                throw new ArgumentException("The specified enumeration value is invalid.", "format");
        }
    }
}
```

