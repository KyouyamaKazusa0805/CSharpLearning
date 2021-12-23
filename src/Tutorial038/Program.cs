using System;

class Program
{
	static void Main()
	{
		// switch 语句的简化。
		// 如果说 case 对应的执行逻辑是完全一致的，我们可以把它们放在一起，
		// 然后去掉前面多余的相同部分，只保留最后一个。
		int month = int.Parse(Console.ReadLine());
		switch (month)
		{
			case 1: case 3: case 5: case 7: case 8: case 10: case 12:
				Console.WriteLine(31);
				break;
			case 4: case 6: case 9: case 11:
				Console.WriteLine(30);
				break;
			case 2:
				Console.WriteLine(28);
				break;
		}
	}
}
