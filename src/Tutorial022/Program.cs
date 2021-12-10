using System;

class Program
{
	static void Main()
	{
		// 自增自减运算符的复杂情况。

		// 1. 自增自减运算可以单独成句。
		// 在单独成一个语句的情况下，a++ 和 ++a 是一个意思；--a 和 a-- 是一个意思，
		// 它们没有任何区别。
		int a = 10;
		a++;
		Console.WriteLine(a);

		// 2. 自增自减运算符的混用。
		// 不同变量的混用。
		int x = 10, y = 20, z = 30;
		int w = x++ + y++ + z++;
		Console.WriteLine(x); // 11
		Console.WriteLine(y); // 21
		Console.WriteLine(z); // 31
		Console.WriteLine(w); // 60

		// 同变量的混用。
		// 注意，同变量混用自增自减运算在 C# 里是允许的行为，因为
		// 它是良定义行为（Well-defined Behavior）；而在 C 和 C++ 里，
		// 它是未定义行为（Undefined Behavior）。
		// 处理过程和普通的处理机制是完全一样的，只是同变量一定要注意，改变了其中一个数值的地方，
		// 别的地方也都会跟着改变。
		int b = 10;
		int c = b++ + ++b;
		Console.WriteLine(b); // 12
		Console.WriteLine(c); // 22
	}
}
