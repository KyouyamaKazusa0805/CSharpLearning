using System;

class Program
{
	static void Main()
	{
		// 位运算符。
		// 1、位与运算/位且运算（&）；
		// 2、位或运算（|）；
		// 3、位异或运算（^）；
		// 4、位取反运算（~）；
		// 5、位右移运算（>>）；
		// 6、位左移运算（<<）。

		// 位运算只支持整数操作，而且没有 sbyte 这些数据类型的处理机制的；
		// 相反，我们必须要转为 int、uint、long、ulong 这些数据类型来进行处理。
		sbyte a = 5, b = -3;
		sbyte result1 = (sbyte)(a & b); // 5
		sbyte result2 = (sbyte)(a | b); // -3
		sbyte result3 = (sbyte)(a ^ b); // -8
		sbyte result4 = (sbyte)~a; // -6
		sbyte result5 = (sbyte)(a >> 1); // 2
		sbyte result6 = (sbyte)(a << 3); // 40

		Console.WriteLine(result1);
		Console.WriteLine(result2);
		Console.WriteLine(result3);
		Console.WriteLine(result4);
		Console.WriteLine(result5);
		Console.WriteLine(result6);
	}
}