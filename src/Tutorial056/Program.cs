using System;

class Program
{
	static void Main()
	{
		// 异常结构（Exception-handling Structure）。
		try
		{
			int a;
			try
			{
				a = int.Parse(Console.ReadLine());
			}
			catch (FormatException)
			{
				throw new Exception("变量 a 不合法");
			}

			int b;
			try
			{
				b = int.Parse(Console.ReadLine());
			}
			catch (FormatException)
			{
				throw new Exception("变量 b 不合法");
			}

			int c = a / b;
			Console.WriteLine(c);
		}
		catch (DivideByZeroException)
		{
			Console.WriteLine("不要将 0 作为除数。0 作为除数没有意义。");
		}
		catch (Exception ex)
		{
			string message = ex.Message;
			if (message == "变量 a 不合法")
				Console.WriteLine("变量 a 必须是一个整数。");
			else if (message == "变量 b 不合法")
				Console.WriteLine("变量 b 必须是一个整数。");
			else
				Console.WriteLine(message);
		}
	}
}
