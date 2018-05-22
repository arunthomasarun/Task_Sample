using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_Sample
{
  class _03CancellingATask
  {

    static void Main()
    {
      var cts = new CancellationTokenSource();
      var token = cts.Token;

      token.Register(() => 
        {
          Console.WriteLine("Task was cancelled by user!!!");
        });

      Task t = new Task(() =>
       {       
          try
         {         
           int i = 0;
           while (true)
           {
             if (cts.IsCancellationRequested)
             {
               //throw new OperationCanceledException();
               token.ThrowIfCancellationRequested();
             }
             else
             {
               i++;
               Console.WriteLine(i.ToString());
               Thread.Sleep(500);
             }
           }
         }
         catch (OperationCanceledException Ex)
         {
           Console.WriteLine("Exception");
         }
       },token);
      t.Start();

      try
      {
        Console.ReadKey();
        cts.Cancel();
        t.Wait(token);

      }
      catch (OperationCanceledException AggEx)
      {

      }
      Console.WriteLine("Main Program Ended");
      Console.ReadLine();
    }
  }
}
