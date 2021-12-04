using System;

class Program
{
	static void Main()
	{
		// 标识符（Identifier）。
		// 1、由数字、字母、下划线、@ 符号、中文字符、日语、韩语等常见语言字符构成。
		// 2、首字符不可以是数字，且如果出现 @ 符号的话，仅能把 @ 符号放在开头。
		// 3、如果要将关键字当成标识符用，必须带有 @ 符号；其余情况随意。
		// 4、大小写敏感。
		//int i = 30; // 重名变量。
		int @i = 30;
		int @int = 30;
		Console.WriteLine(i);
		Console.WriteLine(@int);

		int S = 60;
		int s = 70;
		Console.WriteLine(S);
		Console.WriteLine(s);

		// 标识符的命名规范。
		// 帕斯卡命名法（PascalCase）。
		// 驼峰命名法（camelCase）。
		// 蛇命名法（snake_case）。
		// 一般临时变量名称用的是驼峰命名法。
		int thePriceOfTheToyThatIBought = 50; // 驼峰命名法。
		int the_price_of_the_toy_that_i_bought = 50; // 蛇命名法。

		// 标识符的命名，是可以使用自然语言里的缩写词汇的，但不建议使用。
		int obj = 13;
		int qq = 10;
	}
}
