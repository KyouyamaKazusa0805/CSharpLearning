using System;

class Program
{
    static void Main()
    {
        // 1. 标签的命名规则。
        // 必须满足标识符的基本规则。

        // 2. 标签的适用范围：可以看到它才行。

        // 3. 标签的骚操作：模拟循环。
        // for 改成标签写法的循环的时候：
        //    for 的条件降级为 if
        //    for 的初始化放在标签的前面
        //    for 的增量放在 if 的后面
        //    for 的循环体就是 if 的内容
        //    if 要记得配一个 else 用于跳出循环
        int number = 2, foundPrimes = 0;
    Loop:
        if (number <= 100)
        {
            bool isPrime = true;
            if (number % 2 == 0)
            {
                if (number != 2)
                    goto Increment;
                else
                    goto Determine;
            }

            int i = 3;
        NestedLoop:
            if (i * i <= number)
            {
                if (number % i == 0)
                {
                    isPrime = false;
                    goto Determine;
                }
            }
            else goto Determine;

            i += 2;
            goto NestedLoop;

        Determine:
            if (isPrime)
            {
                if (foundPrimes != 0 && foundPrimes % 5 == 0)
                    Console.WriteLine();

                foundPrimes++;
                Console.Write("{0,4}", number);
            }
        }
        else goto Returning;

        Increment:
        number++;
        goto Loop;

    Returning:
        ;
    }
}
