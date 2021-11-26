using System;

class Program
{
	static void Main()
	{
		// 变量定义。
		// dogName、age、price 都称为变量（Variable）。
		// "Shiny"、5、299.98D 都称为字面量（Literal）。
		// string、int、double 都称为变量的数据类型，或简称数据类型或类型（Data Type 或 Type）。

		// C# 变量必须先定义后使用。定义的方式是：
		//
		//    数据类型 变量名 = 字面量;
		//
		// 整个语句叫赋值语句（Assignment Statement），其中的“赋值”（Assign）表示
		// 把右侧的字面量信息给左边的变量存储起来。
		string dogName = "Shiny";
		int age = 5;
		double price = 299.98D;

		// {0} 和 {1} 叫占位符（Placeholder）。
		// 占位符里面的数字一定要从 0 开始。
		Console.WriteLine("The dog's name is {0}, {1} years old.", dogName, age);
		Console.WriteLine("When I bought her home, the price is {0}.", price);
	}
}