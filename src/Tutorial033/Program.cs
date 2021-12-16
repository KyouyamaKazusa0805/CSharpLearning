using System;

class Program
{
	static void Main()
	{
		// 调试（Debug）。
		// 断点（Breakpoint）。

		Console.WriteLine("请输入一个整数：");

		string str = Console.ReadLine();
		int number = int.Parse(str);

		// 模拟运行的错误（本来是 != 结果写成了 ==），以用调试操作来发现此行的错误。
		bool isOdd = number % 2 == 0;
		if (isOdd)
		{
			Console.WriteLine("{0} 是一个奇数。", number);
		}
		else
		{
			Console.WriteLine("{0} 是一个偶数。", number);
		}
	}
}