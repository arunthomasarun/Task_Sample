using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Task_Sample
{

  class BankAccount
  {
        public int Balance { get; set; }

    public void DepositToAcc(int amount)
    {
      
        Balance += amount;
            
    }

    public void WithdrawFromAcc(int amount)
    {
        Balance -= amount;
    }
  }

  class _07SpinLock
  {


    public static void Main()
    {
      BankAccount ba = new BankAccount();
      SpinLock sl = new SpinLock();

      Task t1 = new Task(() =>
      {
        for (int i = 0; i < 100; i++)
        {
          bool lockTaken = false;

          try
          {
            sl.Enter(ref lockTaken);
            ba.DepositToAcc(i);
          }
          finally
          {
            if(lockTaken)
            {
              sl.Exit();
            }
          }
        }
      }
      );
      t1.Start();

      Task t2 = new Task(() =>
      {
        for (int i = 0; i < 100; i++)
        {
          bool lockTaken = false;

          try
          {
            sl.Enter(ref lockTaken);
            ba.WithdrawFromAcc(i);
          }
          finally
          {
            if (lockTaken)
            {
              sl.Exit();
            }
          }
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
