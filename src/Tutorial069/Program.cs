using System;

class Program
{
	static void Main()
	{
		// .Length 取数组总元素个数。
		// .GetLength(维度) 取当前维度的元素个数。

		int[,] arr = new int[2, 4];
		int r = arr.Length; // 8
		int s = arr.GetLength(0); // 2
		int t = arr.GetLength(1); // 4
		Console.WriteLine("{0}, {1}, {2}", r, s, t);

		int[][] brr = new int[2][];
		int u = brr.Length; // 2
		int v = brr.GetLength(0); // 2
		//int w = brr.GetLength(1); // 程序出错，原因是这个数组没有第二个维度。
		Console.WriteLine("{0}, {1}", u, v);
	}
}
