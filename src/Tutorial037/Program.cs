using System;

class Program
{
	static void Main()
	{
		// switch 语句。
		// 让用户输入一个月份（1-12），程序输出这个月有多少天。
		// 假设 2 月只有 28 天。
		int month = int.Parse(Console.ReadLine());

		// switch 语句的语法：
		// switch 关键字，然后是一对小括号，里面写变量名，然后和 if 类似，需要一对大括号。
		// 但请注意，switch 的大括号永远都不可以省略。
		// 里面列举变量的所有情况数值。写法是“case 数值:”。每一个处理过程写完后，
		// 一定要配套上一个 break 语句（写法是“break;”）。
		switch (month)
		{
			// 标签（Label）：用冒号结尾的部分。
			case 1: Console.WriteLine(31); break;
			case 2: Console.WriteLine(28); break;
			case 3: Console.WriteLine(31); break;
			case 4: Console.WriteLine(30); break;
			case 5: Console.WriteLine(31); break;
			case 6: Console.WriteLine(30); break;
			case 7: Console.WriteLine(31); break;
			case 8: Console.WriteLine(31); break;
			case 9: Console.WriteLine(30); break;
			case 10: Console.WriteLine(31); break;
			case 11: Console.WriteLine(30); break;
			case 12: Console.WriteLine(31); break;
		}
	}
}
