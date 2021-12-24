using System;

class Program
{
	static void Main()
	{
		// 循环结构（Looped Structure）。
		// while 循环。
		int result = 0;
		int i = 1;
		while (i <= 100)
		{
			result += i;
			i++;
		}

		Console.WriteLine(result);
	}
}
