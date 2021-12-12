using System;

class Program
{
	static void Main()
	{
		// 逻辑运算符。
		// 这些运算符都只针对于布尔类型的变量（或布尔类型的表达式）。
		// 1、逻辑与/逻辑且运算（&&）；
		// 2、逻辑或运算（||）；
		// 3、逻辑异或运算（^）；
		// 4、逻辑取反运算（!）；
		// 5、贪婪逻辑与/贪婪逻辑且运算/非短路逻辑且运算/非短路逻辑与运算（&）；
		// 6、贪婪逻辑或/非短路逻辑或运算（|）；
		// 7、逻辑与元运算/逻辑且元运算（false）；
		// 8、逻辑或元运算（true）。

		bool a = true, b = false;
		bool result1 = a && b; // false
		bool result2 = a || b; // true
		Console.WriteLine(result1);
		Console.WriteLine(result2);

		bool result3 = a ^ b; // true
		bool result4 = !a; // false
		Console.WriteLine(result3);
		Console.WriteLine(result4);

		// 短路现象。
		int x1 = 10, y1 = 1;
		bool result5 = ++x1 > 0 && --y1 > 0; // 11, 0, false
		Console.WriteLine("x = {0}, y = {1}, result = {2}", x1, y1, result5);

		// 短路：
		// 如果“expr1 && expr2”的 expr1 是 false，由于 expr2 不论 true 还是 false，都
		// 无法改变整个“expr1 && expr2”的结果（因为有一个 false 结果就是 false），因此，
		// expr2 就不用算了。
		int x2 = 10, y2 = 1;
		bool result6 = --y2 > 0 && ++x2 > 0; // 10, 0, false
		Console.WriteLine("x = {0}, y = {1}, result = {2}", x2, y2, result6);

		// 如果“expr1 || expr2”的 expr1 是 true，由于 expr2 不论 true 还是 false，都
		// 无法改变整个表达式“expr1 || expr2”的结果（因为有一个 true 结果就是 true），因此，
		// expr2 就不用算了。
		int x3 = 10, y3 = 1;
		bool result7 = ++x3 > 0 || --y3 != 0; // 11, 1, true
		Console.WriteLine("x = {0}, y = {1}, result = {2}", x3, y3, result7);

		// 贪婪运算（非短路运算）。
		int xx = 10, yy = 1;
		bool result8 = --yy > 0 & ++xx > 0; // 11, 0, false
		Console.WriteLine("x = {0}, y = {1}, result = {2}", xx, yy, result8);

		int xxx = 10, yyy = 1;
		bool result9 = ++xxx > 0 | --yyy != 0; // 11, 0, true
		Console.WriteLine("x = {0}, y = {1}, result = {2}", xxx, yyy, result9);
	}
}