using System;

class Program
{
	static void Main()
	{
		// 比较运算符。
		int a = 10, b = 7;
		bool result1 = a > b; // true
		bool result2 = a >= b; // true
		bool result3 = a < b; // false
		bool result4 = a <= b; // false
		bool result5 = a == b; // false
		bool result6 = a != b; // true

		Console.WriteLine(result1);
		Console.WriteLine(result2);
		Console.WriteLine(result3);
		Console.WriteLine(result4);
		Console.WriteLine(result5);
		Console.WriteLine(result6);
	}
}