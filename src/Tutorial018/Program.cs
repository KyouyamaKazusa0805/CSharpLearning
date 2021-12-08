using System;

class Program
{
	static void Main()
	{
		// 算术运算符。
		// 加减乘和数学上的 +、-、* 是一致的操作和行为。
		{
			int a = 10;
			int b = 60;
			int c = a * b;
			Console.WriteLine(c);
		}

		// 除法运算符“/”。
		{
			// 除法运算是不会变更数据类型的。
			// 除法运算由于不改变计算类型，因此两个整数的除法运算相当于整除运算。
			// 这个整除运算和 Visual Basic 语言的 \ 符号是一个意思。
			int a = 10;
			int b = 3;
			int c = a / b;
			Console.WriteLine(c);

			// 两个小数（浮点数）的除法运算就和数学上的除法（➗）是类似的了。
			float f = 10;
			float g = 3;
			float h = f / g;
			Console.WriteLine(h);
		}

		// 取模运算符“%”。
		{
			// 整数的取模运算：表示获取整数除法下的余数部分。
			int a = 10;
			int b = 3;
			int c = a % b;
			Console.WriteLine(c);

			// 如果被除数和除数里被除数是负数，结果才是负数；否则的所有情况都是正的。
			int d = 10;
			int d2 = -10;
			int e = -3;

			//    被除数
			//      ↓  除数
			//      |   ↓
			int f = d % e;
			Console.WriteLine(f);

			int f2 = d2 % e;
			Console.WriteLine(f2);

			// 浮点数的取模运算。
			// 假设运算 x / y 得到表达式：x / y = z...w，并且 z 是整数。
			// 则 x % y 的结果为 w。
			float x = 13.3F;
			float y = 6.3F;
			float z = x % y; // 13.3 / 6.3 = 2...0.7，所以 z = 0.7
			Console.WriteLine(z);
		}
	}
}
