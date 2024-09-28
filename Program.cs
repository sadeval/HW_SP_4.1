using System;
using System.Threading;

class Program
{
    static int leftPosition = 0;
    static int rightPosition = Console.WindowWidth - 6;
    static string leftWord = "Left";
    static string rightWord = "Right";
    static bool isRunning = true;

    static void Main()
    {
        rightPosition = Console.WindowWidth - rightWord.Length;

        Thread leftThread = new Thread(MoveLeftWord);
        Thread rightThread = new Thread(MoveRightWord);

        leftThread.Start();
        rightThread.Start();

        leftThread.Join();
        rightThread.Join();

        Console.Clear();
        Console.WriteLine("Слова встретились, программа завершена!");
    }

    static void MoveLeftWord()
    {
        while (isRunning)
        {
            lock (typeof(Console))
            {
                Console.SetCursorPosition(0, 0);
                Console.Write(new string(' ', Console.WindowWidth));

                Console.SetCursorPosition(leftPosition, 0);
                Console.Write(leftWord);

                if (leftPosition >= rightPosition - leftWord.Length)
                {
                    isRunning = false;
                    break;
                }

                leftPosition++;
            }
            Thread.Sleep(1000);
        }
    }

    static void MoveRightWord()
    {
        while (isRunning)
        {
            lock (typeof(Console))
            {
                Console.SetCursorPosition(0, 1);
                Console.Write(new string(' ', Console.WindowWidth));

                Console.SetCursorPosition(rightPosition, 1);
                Console.Write(rightWord);

                if (rightPosition <= leftPosition + rightWord.Length)
                {
                    isRunning = false;
                    break;
                }

                rightPosition--;
            }
            Thread.Sleep(1000);
        }
    }
}
