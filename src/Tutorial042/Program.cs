using System;

class Program
{
	static void Main()
	{
		{
			int result = 0, i = 1;
			while (i <= 100)
			{
				result += i;
				i++;
			}

			Console.WriteLine("result = {0}", result);
		}

		// while 循环的本质：条件 -> 循环体 -> 条件 -> 循环体 -> 条件 -> ...

		{
			int result = 0, i = 1;
			while (i <= 100)
				result += i++;

			Console.WriteLine("result = {0}", result);
		}

		{
			int result = 0, i = 0;
			while (++i <= 100)
				result += i;

			Console.WriteLine("result = {0}", result);
		}

		// 循环体是可能一次都遇不到的。
		//int result = 0, i = 101;
		//while (i <= 100)
		//{
		//	result += i;
		//	i++;
		//}

		{
			// 练习题：请问这段代码表示什么内容？
			int result = 0, i = 1;
			while (result <= 1000)
			{
				result += i;
				i++;
			}

			Console.WriteLine("result = {0}", result);
		}
	}
}
