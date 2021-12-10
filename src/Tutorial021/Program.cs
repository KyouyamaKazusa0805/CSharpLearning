using System;

class Program
{
	static void Main()
	{
		// 自增自减运算符。
		// 自增运算符：++。
		// 自减运算符：--。
		// 用于变量上的；也就是说，你不能把 ++ 和 -- 用于一个字面量。

		// 自增自减运算符可以作用于一个变量，而它可以直接写在变量的左侧，也可以
		// 写在变量的右侧。
		// 其中，写在左边的情况，我们叫前缀自增（或自减）运算；
		// 而写在右边的情况，我们叫后缀自增（或自减）运算。
		int a = 10;
		int b = a++; // b = 10, a = 11
		int c = ++a; // a = 12, c = 12
		int d = a--; // d = 12, a = 11
		int e = --a; // a = 10, e = 10
		Console.WriteLine(a); // 10
		Console.WriteLine(b); // 10
		Console.WriteLine(c); // 12
		Console.WriteLine(d); // 12
		Console.WriteLine(e); // 10

		//int a1 = 10, a2 = 10;
		//int b2 = a1++;
		//int c2 = ++a2;
		//Console.WriteLine(a1); // 11
		//Console.WriteLine(a2); // 11
		//Console.WriteLine(b2); // 10
		//Console.WriteLine(c2); // 11
	}
}
