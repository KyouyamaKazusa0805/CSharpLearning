# C# 2 之允许 getter 和 setter 访问级别不同

今天我们继续来看语法新拓展。

## Part 1 引例

尽管目前 C# 的封装机制做得已经很优秀了，但仍然有极少数情况下，目前的语法做不到。

假设我有一个形状的抽象类型 `Shape`，然后派生出 `Circle` 圆形、`Rectangle` 矩形等等形状的类型。

```csharp
public abstract class Shape
{
}

public sealed class Circle : Shape
{
}

public sealed class Rectangle : Shape
{
}
```

假设我有一个属性 `Area` 表示面积，用来计算面积数值。从另外一个层面，我们可以通过构造器初始化 `Area` 来达到赋值的过程，就不用运行时使用属性的时候才来计算了：

```csharp
public abstract class Shape
{
    private double _area;

    public double Area
    {
        get { return _area; }
        protected set { _area = value; } // Here.
    }
}

public sealed class Circle : Shape
{
    public Circle(int radius)
    {
        Area = Math.PI * radius * radius;
    }
}

public sealed class Rectangle : Shape
{
    public Rectangle(int a, int b) { Area = a * b; }
}
```

可以看到，这样的实现机制可以更加灵活地使用属性：我们为 `Area` 属性的 setter 设置了 `protected` 访问修饰符后，这个方法就只能用在当前类和它的派生类型里使用赋值操作了。

在原来，我们如果这么书写代码，就非常不合理：因为 setter 是 `public` 的，所以你完全可以随便篡改数值。