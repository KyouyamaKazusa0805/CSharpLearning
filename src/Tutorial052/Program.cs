using System;

class Program
{
	static void Main()
	{
		// 获得第一个大于 50 的质数。
		// 普通写法。
		{
			int number = 50;
			for (; ; number++)
			{
				bool isPrime = true;
				for (int i = 2; i <= number - 1; i++)
				{
					if (number % i == 0)
					{
						isPrime = false;
						break;
					}
				}

				if (isPrime)
				{
					Console.WriteLine(number);
					break;
				}
			}
		}

		// 变量 isPrime 内联到 for 循环里。
		{
			int number = 50;
			for (bool isPrime = false; !isPrime; number++)
			{
				isPrime = true;
				for (int i = 2; i <= number - 1; i++)
				{
					if (number % i == 0)
					{
						isPrime = false;
						break;
					}
				}
			}

			Console.WriteLine(number - 1);
		}

		// 使用 goto 语句的写法。
		{
			int number = 50;
			for (; ; number++)
			{
				for (int i = 2; i <= number - 1; i++)
					if (number % i == 0)
						goto NextLoop;

				goto Output;

			NextLoop:
				;
			}

		Output:
			Console.WriteLine(number);
		}
	}
}
