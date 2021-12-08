using System;

class Program
{
	static void Main()
	{
		// 数据类型不同的时候的运算符运算机制和行为。
		// 整数：sbyte, byte, short, ushort, int, uint, long, ulong
		// 浮点数：float, double, decimal
		{
			// 如果两个数据类型不同的数进行运算，结果取的是
			// 这两个数据类型里表示数据较为宽泛的那一个。
			int a = 10;
			double b = 20;
			double c = a * b;
			Console.WriteLine(c);
		}

		{
			// 类型提升：如果两个数据的类型不一样，但表示的数据宽度一样，
			// 这样两种数据类型的数字参与运算，结果就会提升它自己原本的数据类型。
			// 提升规则：
			// sbyte 和 byte：int
			// short 和 ushort：int
			// int 和 uint：long
			// long 和 ulong：编译器报错
			uint a = 10;
			int b = 20;
			Console.WriteLine(a + b);
		}
	}
}
