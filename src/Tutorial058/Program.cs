using System;

class Program
{
	static void Main()
	{
		// 另一个异常的例子。
		// 假设用户输入两个数，然后计算两个数字的商。
		// 为了简化题目计算规则，我们只要求整数的除法，
		// 并且不考虑整数除法导致的取整的情况。
		try
		{
			int a = int.Parse(Console.ReadLine());
			int b = int.Parse(Console.ReadLine());

			int result = a / b;
			Console.WriteLine(result);
		}
		catch (FormatException)
		{
			Console.WriteLine("您输入的不是一个合适的整数信息。");

			Console.WriteLine("请输入任意按键以继续。");
			Console.ReadKey();
		}
		catch (DivideByZeroException)
		{
			Console.WriteLine("请不要将 0 作为除数使用。");

			Console.WriteLine("请输入任意按键以继续。");
			Console.ReadKey();
		}
	}
}
