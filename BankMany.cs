using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BankLock
{
    public class BankAccount
    {
        private int Balance;
        private readonly object lokerObj = new object();   

        public BankAccount(int balance)
        {
            Balance = balance;            
        }

        public void Deposit(int amount)
        {            
            lock (lokerObj)
            {
                Console.WriteLine("\n====== ПОПОЛНЕНИЕ БАЛАНСА ======");
                Console.WriteLine($"Текущий баланс: {Balance}");
                Console.WriteLine($"Пополнение на сумму: {amount}");
                Balance += amount;
                Console.WriteLine($"итоговый баланс: {Balance}");
            }
        }
        public bool Withdraw(int amount)
        {            
            lock (lokerObj)
            {
                Console.WriteLine("\n====== СНЯТИЕ ДЕНЕГ ======");
                if (Balance >= amount)
                {
                    Console.WriteLine($"Текущий баланс: {Balance}");
                    Console.WriteLine($"Снятие на сумму: {amount}");
                    Balance -= amount;
                    Console.WriteLine($"Итоговый баланс: {Balance}");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Снятие невозможно: {Balance} \nНедостаточно средств, сумма на счете: {amount}");
                    return false;
                }                
            }
        }
        public int GetBalance()
        {
            lock (lokerObj)
            {
                return Balance;
            }
        }
        public void Operations(BankAccount acc, string threadName, int iterations, int maxDeposit, int maxWithdraw)
        {
            Random rdn = new Random(Thread.CurrentThread.ManagedThreadId);
            for (int i = 0; i < iterations; i++)
            {
                int typeOper = (int)rdn.Next(0, 2);
                if (typeOper == 0)
                {
                    int amount = rdn.Next(10, maxDeposit);
                    acc.Deposit(amount);
                }
                else
                {
                    int amount = rdn.Next(10, maxWithdraw);
                    acc.Withdraw(amount);
                }
                Thread.Sleep(100);
            }
        }

    }
}
