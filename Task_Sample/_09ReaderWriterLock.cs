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

  class _09ReaderWriterLock
  {
    public static void Main()
    {
      ReaderWriterLockSlim slim = new ReaderWriterLockSlim();

      slim.EnterReadLock();

      slim.ExitReadLock();

      slim.EnterWriteLock();

      slim.ExitWriteLock();

    }
  }
}
