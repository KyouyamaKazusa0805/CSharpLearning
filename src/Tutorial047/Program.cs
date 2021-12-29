using System;

class Program
{
	static void Main()
	{
		// 1、迭代缺省/缺省迭代：允许 for 循环的每一个部件缺失。
		{
			int result = 0, i = 1;
			for (; i <= 100; i++)
				result += i;

			Console.WriteLine(result);
		}

		{
			int result = 0;
			for (int i = 1; i <= 100;)
				result += i++;

			Console.WriteLine(result);
		}

		{
			int result = 0, i = 1;
			for (; i <= 100;)
				result += i++;

			Console.WriteLine(result);
		}

		{
			int result = 0, i = 1;
			for (; i <= 100; result += i++)
				; // 空语句（Empty Statement）。

			Console.WriteLine(result);
		}

		// 2、复合迭代：可以允许初始化部分和增量部分放置多个赋值语句，中间用逗号隔开。
		{
			int result, i;
			for (result = 0, i = 1; i <= 100; result += i, i++)
				;

			Console.WriteLine(result);
		}
	}
}
