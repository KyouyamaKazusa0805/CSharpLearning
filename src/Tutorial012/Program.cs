using System;

class Program
{
	static void Main()
	{
		// 字符字面量（Character Literal）和字符串字面量（String Literal）。
		char c = '0';
		int i = 0;
		Console.WriteLine(c);
		Console.WriteLine(i);
		string s = "今天吃什么？";
		Console.WriteLine(s);

		// 转义字符（Escape Character）。
		string quote = "He said, \"I'm tired now.\"";
		Console.WriteLine(quote);
		char c2 = '\'';
		Console.WriteLine(c2);
		string twoLinesString = "The first-line sentence.\nThe second-line sentence.";
		Console.WriteLine(twoLinesString);
		string s2 = "abc\\def";
		Console.WriteLine(s2);
		//string wrongString = "\%";

		// 原义字符串（Verbatim String）。
		string verbatimString = @"The first-line sentence.
The second-line sentence.";
		Console.WriteLine(verbatimString);
	}
}
