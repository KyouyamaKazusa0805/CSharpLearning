using System;

class Program
{
	static void Main()
	{
		// 数组（Array）。

		// 引例：求 10 个学生学习成绩的平均值。
		// 第一步：录入 10 个学生的学习成绩，假设学习成绩使用 int 表示。
		// new 表达式也叫实例化表达式（Initialization Expression）。
		// [] 的语义：
		//    1、跟在 new T 的后面（T 是一个占位用的类型，具体情况替代这儿），
		//    表示数组创建期间规划的总元素个数（或者叫总大小、总长度）。
		//    2、写在具体的一个数组类型的变量的后面，表示获取这个数组第几个元素。
		//    注意 [] 里的数字从 0 开始，表示第一个；1 表示第 2 个，以此类推。
		int[] array = new int[10];
		for (int i = 0; i < 10; i++)
			array[i] = int.Parse(Console.ReadLine());

		// 第二步：获取总分。
		int sum = 0;
		for (int i = 0; i < 10; i++)
			sum += array[i];

		// 第三步：求平均值并输出。
		float average = sum / 10F;
		Console.WriteLine("{0:0.00}", average);
	}
}
