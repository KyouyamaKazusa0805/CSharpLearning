using System;

class Program
{
	static void Main()
	{
		// 位运算优化基本运算过程。
		// 1、乘除法部分可以用位运算代替。
		int x1 = 10;
		int y1 = x1 / 2; // x1 >> 1
		Console.WriteLine(y1);

		int x2 = 6;
		int y2 = x2 * 16; // x2 << 4
		Console.WriteLine(y2);

		// 2、取模部分可以用位运算代替。
		// 如果可以把 x % y 的 y 看成 2 的 n 次方的话：
		// 就可以代替为 x & (y - 1)。
		int x3 = 17;
		int y3 = x3 % 8; // x3 & 7
		Console.WriteLine(y3);

		// 问题。
		// 如果把一个数字的二进制旋转一下。
		// 00111011
		// 旋转右移 3 个单位：
		// 01100111
		// 假设记作 x >>> y
	}
}