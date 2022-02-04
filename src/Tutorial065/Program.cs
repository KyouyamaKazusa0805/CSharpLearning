using System;

class Program
{
	static void Main()
	{
		// 高维数组（Multi-dimensional Array）。
		// 第一种赋值方式：使用数组初始化器赋值。
		int[,,] arr = new int[2, 3, 4]
		{
			{
				{ 1, 2, 3, 4 },
				{ 2, 3, 4, 5 },
				{ 3, 4, 5, 6 }
			},
			{
				{ 1, 2, 3, 4 },
				{ 2, 3, 4, 5 },
				{ 3, 4, 5, 6 }
			}
		};

		// 第二种赋值方式：使用循环。
		int[,,] brr = new int[2, 3, 4];
		for (int i = 0; i < 2; i++)
			for (int j = 0; j < 3; j++)
				for (int k = 0; k < 4; k++)
					arr[i, j, k] = 42;
	}
}
