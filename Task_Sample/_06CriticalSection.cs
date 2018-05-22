using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Sample
{
  class BankAccount
  {
    object _lock = new object();
    public int Balance { get; set; }

    public void DepositToAcc(int amount)
    {
      lock(_lock)
      {
        Balance += amount;

      }
    }

    public void WithdrawFromAcc(int amount)
    {
      lock (_lock)
      {
        Balance -= amount;
        
      }
    }
  }

  class _06CriticalSection
  {
    

    public static void Main()
    {
      BankAccount ba = new BankAccount();

      Task t1 = new Task(() =>
      {
        for (int i = 0; i < 100; i++)
        {
          ba.DepositToAcc(i);
        }
      }
      );
      t1.Start();

      Task t2 = new Task(() =>
      {
        for (int i = 0; i < 100; i++)
        {
          ba.WithdrawFromAcc(i);
        }
      }
      );
      t2.Start();

      Task.WaitAll(t1, t2);
      Console.WriteLine("Balance is: " + ba.Balance.ToString());
      Console.ReadLine();
    }


    

  }
}
