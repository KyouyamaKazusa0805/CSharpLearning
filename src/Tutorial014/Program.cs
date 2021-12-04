using System;

class Program
{
	static void Main()
	{
		// 类型转换（Type Conversion 或 Type Cast）。

		// 1、显式转换（强制转换，Explicitly Cast）。
		// 用于一个范围较大的数据类型的数值赋值给较小的的情况。
		decimal m = (decimal)30D;
		double d = 40D;
		int i = (int)d;
		//char c = (char)"1434"; // 不可以。因为字符串的长度不定，可以超过字符表达的总数量。

		// 2、隐式转换（Implicitly Cast）。
		// 用于一个较小范围的数据类型的数值赋值给较大的的情况。
		int i2 = 40;
		double d2 = i2;
		//string s = 'h'; // 不可以。因为处理机制不一样。

		// 3、字符串转其它数据类型（以及反过来）。
		// 一般被翻译成“解析”（Parse），即把字符串里有意义的数据部分提取出来。
		string str = "13.4";
		double targetValue = double.Parse(str);
		Console.WriteLine(targetValue);

		// 反过来。
		Console.WriteLine(13.56);
	}
}
