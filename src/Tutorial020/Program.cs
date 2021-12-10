using System;

class Program
{
	static void Main()
	{
		// 字符串的加法运算：字符串的加法相当于拼接字符串。
		// 字符串的拼接操作至少要求 + 运算符的左右两侧至少有一个是字符串类型，
		// 即 string 的变量或字面量。
		// 另外，字符串的拼接操作始终是不满足交换律的。
		string s = "Hello";
		string t = "world";
		char u = ',';
		string result = s + u + t;
		Console.WriteLine(result);

		// 不过，我们一般不建议使用者把 + 左侧的这个变量作为非字符串类型的变量
		// 来作为字符串拼接操作。
		// 因为，用户可能会因为 + 运算符而优先去以为是一个数字的基本操作（加减乘除运算），
		// 而忽略掉字符串的拼接操作。
		int i = 30;
		string z = "hello";
		string result2 = i + z;
		Console.WriteLine(result2);
	}
}
