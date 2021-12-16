using System;

class Program
{
	static void Main()
	{
		// if 语句的嵌套。
		int a = 30, b = 60;
		if (a > b)
		{
			Console.WriteLine("a 比 b 大。");
		}
		else
		{
			if (a < b)
			{
				Console.WriteLine("a 比 b 小。");
			}
			else
			{
				Console.WriteLine("a 和 b 一样大。");
			}
		}

		// if 语句和 else 的大括号缺省。
		// 如果大括号里只包含一个分号，就说明这段只有一个语句，所以可以不写大括号。
		if (a > b)
			Console.WriteLine("a 比 b 大。"); // 大括号没有了。
		else if (a < b)
			Console.WriteLine("a 比 b 小。");
		else
			Console.WriteLine("a 和 b 一样大。");

		// 就近配套原则。
		// 如果有 else 的话，else 会和它上面代码里的第一个 if 配对。
		int score = 65;
		if (score >= 91 && score <= 100)
			Console.WriteLine("优");
		else if (score >= 81 && score <= 90)
			Console.WriteLine("良");
		else if (score >= 71 && score <= 80)
			Console.WriteLine("中");
		else if (score >= 60 && score <= 70)
			Console.WriteLine("差");
		else if (score >= 0 && score < 60)
			Console.WriteLine("不及格");
		else
			Console.WriteLine("数据不合适");
	}
}