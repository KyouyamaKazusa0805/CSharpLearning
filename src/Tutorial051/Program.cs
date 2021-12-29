using System;

class Program
{
	static void Main()
	{
		// 循环的嵌套（Nested Loop）。
		// 案例：求一个数是否是质数/素数。
		int number = int.Parse(Console.ReadLine());
		for (int j = 2; j <= number; j++)
		{
			bool isPrime = true;
			for (int i = 2; i <= j - 1; i++)
			{
				if (j % i == 0)
				{
					isPrime = false;
					break;
				}
			}

			Console.WriteLine(isPrime ? "{0} 是质数" : "{0} 不是质数", j);
		}
	}
}
