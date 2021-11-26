using System;

class Program
{
	static void Main()
	{
		// 字面量。
		// 字面量本身是有数据类型的“归属”的。
		{
			// 整数类型的字面量。
			// 整数类型的字面量默认是 int 类型的（System.Int32）。
			// 带后缀 U 或 u 的整数类型字面量默认是 uint 类型的（System.UInt32）。
			// 带后缀 L 或 l 的整数类型字面量默认是 long 类型的（System.Int64）。
			// 同时带有 UL 或 ul 的整数类型字面量默认是 ulong 类型的（System.UInt64）。
			int i = 60; // 成功。
			sbyte s = 60; // 成功。字面量的类型和数值兼容。
			long l = 60; // 成功。
			int j = +60; // +60 和 60 是一个东西。
			sbyte t = -60; // 成功。
			uint k = 60U; // 60U 和 60u 是一个东西。

			//int p = 60U; // 失败。字面量的类型不兼容。
			//int q = 60L; // 失败。字面量的类型不兼容。
			long m = 60L; // 成功。
			ulong u = 60UL; // 成功。1UL、1Ul、1uL、1ul、1LU、1Lu、1lU、1lu 都是一样的。
		}

		{
			// 浮点数字面量。
			// 浮点数字面量的默认类型是 double。
			// 用 F 或 f 后缀表示这个浮点数字面量的默认类型是 float（System.Single）。
			// 用 D 或 d 后缀表示这个浮点数字面量的默认类型是 double（System.Double）。
			// 用 M 或 m 后缀表示这个浮点数字面量的默认类型是 decimal（System.Decimal）。
			float f = 40F;
			double d = 40D;
			decimal m = 40M;
			//float g = 40D; // 失败。类型不兼容。
			//float h = 40M; // 失败。类型不兼容。
			double e = 40.0;
			double n = .7; // 成功。整数部分是 0 的时候是可以不写的。

			//double e2 = 40.; // 失败。
			double q = -40.5; // 成功。
		}

		// 科学计数法字面量。
		// 格式 aEb、aE+b 或 aE-b，其中的 E 可以大写也可以小写（aeb 之类）。
		// 其中的 a 和 b 表示一个数，表示 a 乘以 10 的 b 次方，所以叫它科学计数法。
		// 变量的默认类型是 double 类型（System.Double）。
		{
			double d = 1E10; // 1 * 10 的 10 次方。
			double e = 1E+10; // 1 * 10 的 +10 次方（10 次方）。
			double f = 1E-10; // 1 * 10 的 -10 次方。
			double g = 1e10;
			//float h = 1e10; // 失败。因为类型不兼容。
		}
	}
}