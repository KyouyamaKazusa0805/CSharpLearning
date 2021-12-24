using System;

class Program
{
	static void Main()
	{
		// do-while 循环。
		{
			int result = 0, i = 1;
			do
			{
				result += i;
				i++;
			} while (i <= 100);

			Console.WriteLine(result);
		}

		// 等价写法。
		{
			int result = 0, i = 1;
			do
			{
				result += i;
			} while (++i <= 100);

			Console.WriteLine(result);
		}

		// 可省略大括号（如果大括号里只有一个语句）。
		{
			int result = 0, i = 1;
			do result += i; while (++i <= 100);

			Console.WriteLine(result);
		}
	}
}
