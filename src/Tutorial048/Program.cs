using System;

class Program
{
	static void Main()
	{
		// 死循环（Dead Loop）：循环的条件是永真式的情况。
		while (true) ;
		do; while (true);
		for (; true;) ;

		//// 假死循环。
		int i = 0;
		while (i >= 0)
		{
			i++;
		}

		// 看起来像是会终止的假死循环，但实际上是真的死循环的循环。
		for (float f = .1F; f != 1; f += .1F)
		{
			Console.WriteLine(f);
		}
	}
}
