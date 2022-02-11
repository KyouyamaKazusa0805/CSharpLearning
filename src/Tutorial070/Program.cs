using System;

class Program
{
	static void Main()
	{
		// foreach 循环和数组的用法。
		// 语法：foreach (数据类型 变量名 in 数组) { }
		int[] arr = { 3, 8, 1, 6, 5, 4, 7, 2, 9 };
		int[,] brr = { { 1, 2, 3 }, { 3, 4, 5 } };
		int[,,] crr = { { { 1, 2 }, { 2, 3 } }, { { 3, 4 }, { 5, 6 } } };
		int[][] drr = { new int[] { 1, 2 }, new int[] { 3, 4, 5 } };

		// 迭代数组（Iterates on each element）。
		// 遍历数组（Traverses the array）。
		foreach (int current in arr)
			Console.Write("{0}, ", current);
		Console.WriteLine();

		foreach (int current in brr)
			Console.Write("{0}, ", current);
		Console.WriteLine();

		foreach (int current in crr)
			Console.Write("{0}, ", current);
		Console.WriteLine();

		foreach (int[] current in drr)
		{
			foreach (int c in current)
				Console.Write("{0}, ", c);
			Console.Write("|");
		}
		Console.WriteLine();
	}
}
