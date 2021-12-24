using System;

class Program
{
	static void Main()
	{
		// do-while 循环的使用场合：验证输入是否合法。
		// 伪代码如下：
		//
		// do
		// {
		//     用户输入;
		// }
		// while (!输入内容是否合法);
		//

		// 用户输入一个整数，如果整数不在 0 到 100 之间（包含边界），
		// 就反复让用户重新输入。
		int result, trialTimes = 0;
		do
		{
			string s = trialTimes++ == 0
				? "请输入一个 1-100 的整数："
				: "你输入的数字不在合适的范围。请重新输入。";

			Console.WriteLine(s);
			result = int.Parse(Console.ReadLine());
		}
		while (!(result >= 0 && result <= 100));

		Console.WriteLine("result = {0}", result);
	}
}
