using System;

class Program
{
	static void Main()
	{
		// 格式化（Formatting）。

		// 说明符（Specifier）。
		int price = 12;
		Console.WriteLine("{0:C}", price);
		double population = 7800000000D;
		Console.WriteLine("{0:E}", population);

		// 填充（Padding）。
		string name1 = "Sunnie";
		string name2 = "Tom";
		string name3 = "Jerry";
		double score1 = 80;
		double score2 = 64;
		double score3 = 100;
		Console.WriteLine("{0,-8}|{1}", "Name", "Score");
		Console.WriteLine("--------+-----");
		Console.WriteLine("{0,-8}|{1}", name1, score1);
		Console.WriteLine("{0,-8}|{1}", name2, score2);
		Console.WriteLine("{0,-8}|{1}", name3, score3);

		// 混用一下。
		Console.WriteLine("|{0,16:E}|", population);
		Console.WriteLine("|{0,-16:E}|", population);
	}
}
