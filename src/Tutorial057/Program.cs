using System;

class Program
{
	static void Main()
	{
		// 异常（Exception）：
		// 程序不期望达到的地方；换句话说就是，我们程序设计的过程期间，
		// 本应该这么去做，但在执行期间因为我们考虑的不周全，
		// 导致了程序“偏离了航线”，出现了和预期不同的结果。
		// 这样的情况叫异常情况；而异常情况在代码里的实体表现就叫异常。
		// 这里的“异常”因为指的是一个实体数据，所以它是一个名词，而不是形容词。

		// 在这里，用户如果输入的东西不是可以转为整数表达的字符串的话，
		// 就必然会导致程序闪退。
		try
		{
			string s = Console.ReadLine();
			int i = int.Parse(s);
			if (i > 0)
				Console.WriteLine("i 是大于 0 的数字。");
			else if (i < 0)
				Console.WriteLine("i 是小于 0 的数字。");
			else
				Console.WriteLine("i 就是 0。");
		}
		catch (FormatException)
		{
			Console.WriteLine("你输入的不是一个合适的整数。");
		}
	}
}
