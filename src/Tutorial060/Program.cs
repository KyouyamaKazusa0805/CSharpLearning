using System;

class Program
{
	static void Main()
	{
		// 异常结构语法的一些细节：
		//     1、catch 后是可以不跟具体的异常类型的，
		//     即直接写一个 catch 而不是原来的 catch (...) 语法。
		//     但是要注意，如果还有别的 catch 块捕获了别的具体的异常类型的话，
		//     不写异常类型的这个 catch 语法只能放在最后一个位置来处理。
		//
		//     2、即使只有一句话，try、catch 和 finally 里的任何一个部分
		//     都不能省略大括号。
		//
		//     3、catch 是可以没有的。但是在这种情况下，
		//     必须有 try 和 finally。
		//     所以，可以形成组合的情况只有如下的一些：
		//         try-catch
		//         try-catch-finally
		//         try-finally
		//     其中的 catch 块可以有多个，不过每一个 catch 块的捕获异常的类型
		//     都得不同。
		//
		//     4、try、catch、finally 块里面是可以不写代码的，即保持为空。

		//int a = int.Parse(Console.ReadLine());
		//int b = int.Parse(Console.ReadLine());

		//int result = a / b;
		//Console.WriteLine(result);

		//Console.WriteLine("请输入任意按键以继续。");
		//Console.ReadKey();
		try
		{
			int a = int.Parse(Console.ReadLine());
			int b = int.Parse(Console.ReadLine());

			int result = a / b;
			Console.WriteLine(result);
		}
		finally
		{
			Console.WriteLine("请输入任意按键以继续。");
			Console.ReadKey();
		}
	}
}
