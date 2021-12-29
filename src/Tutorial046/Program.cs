using System;

class Program
{
	static void Main()
	{
		// for 循环。
		// for (初始量①; 条件②; 增量③)
		// {
		//     循环体④
		// }
		// 执行顺序：①②④③②④③②④③...
		// for 循环使用的变量（即这里的 i）叫做迭代变量（Iteration Variable）。
		int result = 0;
		for (int i = 1; i <= 100; i++)
		{
			result += i;
		}

		Console.WriteLine(result);
	}
}
