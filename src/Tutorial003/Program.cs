﻿// 命名空间的引用。
// 每一个不同的操作归类在不同的地方，需要使用这样的语句来引用，
// 达到正常使用对应操作的语句。
using System;

// 主类。该类型包裹了整个程序运行的初始方法。
// 类型将在面向对象里说到。
class Program
{
	// 主方法。程序的入口点。表示程序运行的时候第一个启动的方法。
	static void Main()
	{
		// 代码文字。
		Console.WriteLine("Hello, Jennifer!");

		// 视频内容的答案。
		// 既然要显示两行字，当然就需要写两行 Console.WriteLine 啊。
		Console.WriteLine("Hello, Tom!");
		Console.WriteLine("Hello, Jerry!");
	}
}