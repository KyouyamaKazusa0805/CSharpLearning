using System;

class Program
{
	static void Main()
	{
		// 让死循环变“活”。
		int i = 1;
		for (; ; i++)
		{
			if (i >= 100)
				break;
		}

		Console.WriteLine(i);
	}
}
