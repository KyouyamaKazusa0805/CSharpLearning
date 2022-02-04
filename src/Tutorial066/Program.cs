using System;

class Program
{
	static void Main()
	{
		// 数组的长度的获取。
		// 1、arr.Length，直接把“.Length”语法放在数组的变量名后面。
		// 表示获取这个数组的总元素个数。
		// 2、arr.GetLength(维度编号)，表示获取指定数组在维度下的元素个数。
		// 维度编号是从 0 开始的，比如 0 表示第一个维度，1 表示第二个维度，
		// 以此类推。
		int[,,] threeDimension = new int[2, 3, 4];
		Console.WriteLine(threeDimension.Length); // 24
		Console.WriteLine(threeDimension.GetLength(0)); // 2
		Console.WriteLine(threeDimension.GetLength(1)); // 3
		Console.WriteLine(threeDimension.GetLength(2)); // 4

		// 赋值，并使用上这里的语法。
		for (int i = 0; i < threeDimension.GetLength(0); i++)
			for (int j = 0; j < threeDimension.GetLength(1); j++)
				for (int k = 0; k < threeDimension.GetLength(2); k++)
					threeDimension[i, j, k] = 42;
	}
}
