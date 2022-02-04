using System;

class Program
{
	static void Main()
	{
		// 代码片段（Code Snippet）。
		// 用法：输入代码片段的快捷方式（比如 cw、for 等等），
		// 然后按下两次 Tab 按键。
		// 期间按下 Tab 按键可以切换不同的输入部分，
		// 最后按下 Enter 按键确认代码片段输入完成。
		int[] arr = new int[10];
		for (int i = 0; i < 10; i++)
			arr[i] = int.Parse(Console.ReadLine());

		int sum = 0;
		for (int i = 0; i < 10; i++)
			sum += arr[i];

		float average = sum / 10F;
		Console.WriteLine(average);
	}
}
