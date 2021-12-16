using System;

class Program
{
	static void Main()
	{
		// 条件运算符。
		// 布尔表达式 ? t : f
		// 两个要求：
		// 1、t 和 f 的数据类型一样。
		// 2、布尔表达式的地方只能写布尔类型的变量、字面量、表达式等等，不能是别的数据类型。
		// 它表示布尔表达式结果为 true 的时候，t 是表达式的结果；
		// 而如果为 false 的时候，f 是表达式的结果。
		Console.WriteLine("请输入一个整数：");

		string str = Console.ReadLine();
		int number = int.Parse(str);

		string output = number % 2 != 0 ? "{0} 是一个奇数。" : "{0} 是一个偶数。";
		Console.WriteLine(output, number);

		// 条件运算符的嵌套使用。
		int score = 65;
		string level = score > 90 && score <= 100
			? "优"
			: score > 80 && score <= 90
				? "良"
				: score > 70 && score <= 80
					? "中"
					: score >= 60 && score <= 70
						? "差"
						: score >= 0 && score < 60 ? "不及格" : "数据不合适";
	}
}