using System;

class Program
{
	static void Main()
	{
		int[] arr = { 3, 8, 1, 6, 5, 4, 7, 2, 9 };
		for (int i = 0; i < arr.Length; i++)
		{
			// 获取当前变量。
			int element = arr[i];

			// 确定是否当前元素是一个奇数。
			if ((element & 1) != 0)
			{
				Console.WriteLine(element);
			}
		}
	}
}

// 推荐字体：
// 1. Cascadia Code (Cascadia Mono)
// 2. Code New Roman
// 3. Consolas
// 4. JetBrains Mono
// 5. Ubuntu Mono

// 带渲染的编程字体的渲染效果：
// !=
// ==
// ===
// <>
// ~~
// ^=
// /=
// ##
// ~@
// =>
// </>