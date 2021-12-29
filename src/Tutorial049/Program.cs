using System;

class Program
{
	static void Main()
	{
		// 循环控制语句。
		// 1、break 语句
		{
			int result = 0, i = 1;
			while (i <= 10000)
			{
				if (i >= 2021 && i % 7 == 0)
				{
					break;
				}

				result += i;
				i += 3;
			}

			Console.WriteLine(result);
		}

		// 2、continue 语句
		{
			int result = 0;
			for (int i = 1; i <= 100; i++)
			{
				if (i % 2 != 0)
				{
					continue;
				}

				result += i;
			}

			Console.WriteLine(result);
		}
	}
}
