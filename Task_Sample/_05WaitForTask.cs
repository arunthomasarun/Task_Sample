using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace Task_Sample
{
  class _05WaitForTask
  {
    public static void Main()
    {
      var cts = new CancellationTokenSource();
      var token = cts.Token;

      Stopwatch sw = Stopwatch.StartNew();

      Task t1 = new Task(() => {
        Thread.Sleep(7000);
      }, token);
      t1.Start();

      Task t2 = new Task(() => {
        Thread.Sleep(3000);
      }, token);
      t2.Start();

      Task.WaitAll(new[] { t1, t2 }, token );

      sw.Stop();
      Console.WriteLine("Elapsed time in ms: " + sw.ElapsedMilliseconds );
      Console.ReadLine();

    }
  }
}
