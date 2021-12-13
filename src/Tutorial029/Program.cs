using System;

class Program
{
	static void Main()
	{
		// 赋值运算符。
		int b = 30;
		b = b + 1; // b++; 或 ++b;
		b = (b = 30); // 可以这么写，但没有多大的意义。

		// 复合赋值运算符。
		// 解决的问题是累计。如果一个数据需要更新和累计，我们可以使用复合赋值运算符。
		// 写法是“op=”，其中的 op 指的是运算符符号本身。
		b += 1; // b = b + 1

		Console.WriteLine(b);

		// 支持绝大多数双目运算符，极少数运算符不可以这么简写，比如 && 和 ||。
		bool a = false;
		a &= true;

		Console.WriteLine(a);

		int x = 10, y = (x += 30);
		Console.WriteLine("x = {0}, y = {1}", x, y);

		// 复合赋值运算的操作可以写得很复杂，但是不建议这么写，因为它会影响代码的可读性。
		int w1 = 10, w2 = 20, w3 = 30, w4 = 40;
		int w5 = (w1 += (w2 -= (w3 *= (w4 /= 5))));
		Console.WriteLine("{0},{1},{2},{3},{4}", w1, w2, w3, w4, w5);
	}
}