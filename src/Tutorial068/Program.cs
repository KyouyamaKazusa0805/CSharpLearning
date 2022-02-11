using System;

class Program
{
	static void Main()
	{
		// 交错数组（锯齿数组，Jigsaw Array）。
		int[][] arr = new int[][]
		{
			new int[] { 1, 2 },
			new int[] { 3, 4, 5 },
			new int[] { 4, 5, 6, 7, 8 },
			new int[] { 9, 10, 11, 12 }
		};

		for (int i = 0; i < 4; i++)
		{
			// 将交错数组当成一个一维数组，每一个元素都是 int[] 类型的一维数组。
			// 因此，arr[i] 就相当于在获取整个交错数组的每一个元素。
			// 每一个元素都是 int[] 类型，所以 current 自然就是
			// int[] 类型的了。
			int[] current = arr[i];
			for (int j = 0; j < current.Length; j++)
			{
				// current[j] 和 arr[i][j] 是一样的。
				Console.Write("{0}, ", current[j]);
			}

			Console.WriteLine();
		}
	}
}
