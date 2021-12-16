using System;

class Program
{
	static void Main()
	{
		// 打出一行文字，提示用户按照规范输入数据。
		Console.WriteLine("请输入一个整数：");

		// 输入一行字符，然后将输入的数据从 string 类型解析成对应的数值信息。
		string str = Console.ReadLine();
		int number = int.Parse(str);

		// 计算输入的数值是不是奇数。
		// 两个表达式都可以。不过第一个要注意，因为 number 可以为负整数，因此
		// 余数是可以为 -1 的（因为取模运算看的是被除数的符号，而被除数刚好是这里
		// 我们用到的这个 number 变量）。
		// 所以表达式 number % 2 == -1 就意味着它是不是负奇数。
		// 当然，如果嫌麻烦的话可以用下面这个表达式：number % 2 != 0，就两个情况
		// 都判断到了。
		bool isOdd1 = number % 2 == 1 || number % 2 == -1;
		//bool isOdd2 = number % 2 != 0;

		// if 语句。
		// if 后紧跟一个小括号，里面包裹一个 bool 类型的表达式或 bool 字面量、变量等，
		// 我们把这个跟在 if 后小括号里的 bool 表达式（变量等等）称为条件（Condition）。
		// 后面紧跟一个大括号，里面写的是，如果 isOdd1 是 true 值的时候，要执行的代码；
		// 相反，如果 if 后给的这个条件的结果是 false 的话，后面紧跟的大括号的代码就不执行；
		// 意思就是说，这段代码会被直接跳过并且忽略掉。
		if (isOdd1)
		{
			Console.WriteLine("{0} 是一个奇数。", number);
		}

		// else 语句。
		// else 必须要和 if 成对出现；反之不然：有 if 不一定有 else。
		// else 语句表示当配对的 if 旁边给的这个条件为 false 的时候才会执行的语句。
		// 如果 if 条件为 true，那么 else 部分的代码是永远不会执行的。
		// 而且，if-else 语句是一个 if 和一个 else 成对出现的，因此只写 else 会产生编译器错误。
		else
		{
			Console.WriteLine("{0} 是一个偶数。", number);
		}
	}
}