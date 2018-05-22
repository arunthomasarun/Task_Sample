using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_Sample
{
  class _04CompositeCancellation
  {

    public static void Main()
    {
      var plannedCancel = new CancellationTokenSource();
      var userCancelled = new CancellationTokenSource();
      var exceptionCancel = new CancellationTokenSource();

      var ctsCollection = CancellationTokenSource.CreateLinkedTokenSource(plannedCancel.Token, userCancelled.Token, exceptionCancel.Token);

      userCancelled.Token.Register(() =>
     {
       Console.WriteLine("User cancelled the action");
     });

       
      var t = new Task(() =>
      {
        int i = 0;
        while(true)
        {
          try
          { 
          if (ctsCollection.IsCancellationRequested)
          {
            ctsCollection.Token.ThrowIfCancellationRequested();
          }
          i++;
          Console.WriteLine(i.ToString());
          Thread.Sleep(300);
          }
          catch (OperationCanceledException opEx)
          {
            //Console.WriteLine("Exception :");
          }
        }

      }, ctsCollection.Token);
      t.Start();
      
      Console.ReadLine();
      try
      {
        userCancelled.Cancel();
        t.Wait(ctsCollection.Token);

      }
      catch (OperationCanceledException op)
      {

      }
      Console.WriteLine("Main program ended");
      Console.ReadLine();
      
    }
  }
}
