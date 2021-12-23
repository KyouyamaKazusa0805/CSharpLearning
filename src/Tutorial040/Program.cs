using System;

class Program
{
	static void Main()
	{
		// switch 语句的细节。

		// 1、switch 语句不能用于 bool 和浮点数变量。
		// bool 不能用的原因在于破坏了严谨性；
		// 浮点数不能用的原因则在于精度的问题导致数据存储不精确，进而判断失效。
		//bool variable = false;
		//switch (variable)
		//{
		//	case false:
		//		// ...
		//		break;
		//	case true:
		//		// ...
		//		break;
		//	default:
		//		// ...
		//		break;
		//}

		// 2、嵌套。
		//int year = int.Parse(Console.ReadLine());
		//int month = int.Parse(Console.ReadLine());
		//switch (month)
		//{
		//	case 1: case 3: case 5: case 7: case 8: case 10: case 12:
		//		Console.WriteLine(31);
		//		break;
		//	case 4: case 6: case 9: case 11:
		//		Console.WriteLine(30);
		//		break;
		//	case 2:
		//		// 可以嵌套 if。
		//		//if (year % 400 == 0 || year % 4 == 0 && year % 100 != 0)
		//		//	Console.WriteLine(29);
		//		//else
		//		//	Console.WriteLine(28);
		//
		//		// 也可以嵌套变量的定义过程。
		//		bool isLeapYear = year % 400 == 0 || year % 4 == 0 && year % 100 != 0;
		//		Console.WriteLine(isLeapYear ? 29 : 28);
		//		break;
		//	default:
		//		Console.WriteLine("你输入的数字不合法。必须是 1-12 的数字。");
		//		break;
		//}

		// 3、变量定义冲突的问题。
		//switch (month)
		//{
		//	case 1: case 3: case 5: case 7: case 8: case 10: case 12:
		//		//bool isLeapYear = false; // 不能这么用，因为别处已经有了同名变量了。
		//		Console.WriteLine(31);
		//		break;
		//	case 4: case 6: case 9: case 11:
		//		Console.WriteLine(30);
		//		break;
		//	case 2:
		//		bool isLeapYear = year % 400 == 0 || year % 4 == 0 && year % 100 != 0;
		//		Console.WriteLine(isLeapYear ? 29 : 28);
		//		break;
		//	default:
		//		Console.WriteLine("你输入的数字不合法。必须是 1-12 的数字。");
		//		break;
		//}

		// 4、字符串和字符的 switch。
		// 字符串的 switch 是逐字符比较，并且大小写全部一致和位置、长度全部都一致。
		string myFavoriteFruit = "apple";
		switch (myFavoriteFruit)
		{
			case "Apple": case "apple":
				Console.WriteLine("So sweet.");
				break;
			case "Banana": case "banana":
				Console.WriteLine("Very delicious.");
				break;
			case "Pear": case "pear":
				Console.WriteLine("A cute shape.");
				break;
			default:
				Console.WriteLine("I don't know your input.");
				break;
		}

		char c = 'a';
		switch (c)
		{
			case 'a': case 'A':
				// ...
				break;
			case 'b': case 'B':
				// ...
				break;
			default:
				// ...
				break;
		}
	}
}
