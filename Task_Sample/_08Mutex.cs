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


  class _08Mutex
  {       
    public static void Main()
    {
      BankAccount ba = new BankAccount();
      Mutex mtx = new Mutex();

      List<Task> tasks = new List<Task>();

      tasks.Add(Task.Factory.StartNew(() =>
      {
        for (int i = 0; i < 100; i++)
        {
          bool lockReceived = mtx.WaitOne();
          try
          {
            ba.DepositToAcc(i);
          }
          finally
          {
            if (lockReceived) mtx.ReleaseMutex();
          }
        }
      }));

      tasks.Add(Task.Factory.StartNew(() =>
      {
        for (int i = 0; i < 100; i++)
        {
          bool lockReceived = mtx.WaitOne();
          try
          {
            ba.WithdrawFromAcc(i);
          }
          finally
          {
            if (lockReceived) mtx.ReleaseMutex();
          }
        }
      }));
      Task.WaitAll(tasks.ToArray());

      //Task t1 = new Task(() =>
      // {
      //   for (int i = 0; i < 100; i++)
      //   {
      //     bool lockReceived = mtx.WaitOne();
      //     try
      //     {
      //       ba.DepositToAcc(i);
      //     }
      //     finally
      //     {
      //       if (lockReceived) mtx.ReleaseMutex();
      //     }
      //   }
      // });
      //t1.Start();

      //Task t2 = new Task(() =>
      //{
      //  for (int i = 0; i < 100; i++)
      //  {
      //    bool lockReceived = mtx.WaitOne();
      //    try
      //    {
      //      ba.WithdrawFromAcc(i);
      //    }
      //    finally
      //    {
      //      if (lockReceived) mtx.ReleaseMutex();
      //    }
      //  }
      //});
      //t2.Start();
      //Task.WaitAll(t1,t2);

      Console.WriteLine("Balance is: " + ba.Balance.ToString());
      Console.ReadLine();

    }
  }
}
