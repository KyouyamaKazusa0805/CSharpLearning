using System;

class Program
{
	static void Main()
	{
		int a = 10, b = 30;

		// 布尔表达式的简化逻辑。
		// 第一种简化类型：“条件 ? false : 另一个 bool 表达式”
		// 可简化为“!条件 && 另一个 bool 表达式”。
		bool condition1_1 = a == 10 ? false : b == 10;
		bool condition1_2 = a != 10 && b == 10;

		// 第二种简化类型：“条件 ? 另一个 bool 表达式 : true”
		// 可简化为“!条件 || 另一个 bool 表达式”。
		bool condition2_1 = a == 10 ? b == 10 : true;
		bool condition2_2 = a != 10 || b == 10;

		// 这两个也可以简化。不过怎么简化，需要你自己思考了。
		bool condition3 = a == 10 ? b == 10 : false;
		bool condition4 = a == 10 ? true : b == 10;

		Console.WriteLine(condition1_1);
		Console.WriteLine(condition1_2);
		Console.WriteLine(condition2_1);
		Console.WriteLine(condition2_2);
		Console.WriteLine(condition3);
		Console.WriteLine(condition4);
	}
}
