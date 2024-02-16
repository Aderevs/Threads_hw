namespace Task4
{
    internal class Program
    {

        static int counter = 0;

        static object block = new object(); // block - не повинен бути структурним.

        static void Function()
        {
            for (int i = 0; i < 50; ++i)
            {
                // Встановлюється блокування кожен (50!) разів у новий object (boxing).
                Monitor.Enter(block); // boxing створює новий об'єкт (50 об'єктів).

                // Виконання деякої роботи потоком ...
                Console.WriteLine(++counter);

                // Спроба зняти блокування з об'єкта, який не є об'єктом блокування.
                Monitor.Exit(block); // boxing створює абсолютно новий об'єкт.
            }
        }

        static void Main()
        {
            Thread[] threads = { new Thread(Function), new Thread(Function), new Thread(Function) };

            foreach (Thread thread in threads)
                thread.Start();

            // Delay
            Console.ReadKey();
        }
    }
}
