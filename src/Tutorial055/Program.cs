using System;

class Program
{
	static void Main()
	{
		// goto case 语句和 goto default 语句。
		// 用于跳转到别的 case 和 default 标签上去执行别的标签上的代码。

		// 下面这个例子就用到了 goto case 和 goto default 语句。
		// 程序是为了计算当前输入月份到 1 月份期间一共多少天。
		int month = int.Parse(Console.ReadLine());
		int day = 0;
		switch (month)
		{
			case 12: day += 31; goto case 11;
			case 11: day += 30; goto case 10;
			case 10: day += 31; goto case 9;
			case 9: day += 30; goto case 8;
			case 8: day += 31; goto case 7;
			case 7: day += 31; goto case 6;
			case 6: day += 30; goto case 5;
			case 5: day += 31; goto case 4;
			case 4: day += 30; goto case 3;
			case 3: day += 31; goto case 2;
			case 2: day += 28; goto case 1;
			case 1: day += 30; goto default;
			default: Console.WriteLine(day); break;
		}
	}
}
