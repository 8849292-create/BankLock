using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BankLock
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var account = new BankAccount(1000);

            Thread t1 = new Thread(() => account.Operations(account, "Поток-1", 4,100,200));
            Thread t2 = new Thread(() => account.Operations(account, "Поток-2", 6, 150, 300));
            Thread t3 = new Thread(() => account.Operations(account, "Поток-3", 5, 50, 100));

            t1.Start();
            t2.Start();
            t3.Start();

            t1.Join();
            t2.Join();
            t3.Join();
            Console.WriteLine("--------------------------------");
            Console.WriteLine($"Итоговый баланс на счете: {account.GetBalance()}");
            Console.WriteLine("Потоки завершили работу");
        }        
    }
}
