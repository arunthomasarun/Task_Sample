using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Task_Sample
{
  class _01SimpleTask
  {
    static void Main(string[] args)
    {
      //PrintCharacters("a");

      var t = new Task(() => PrintCharacters("a"));
      t.Start();

      Thread.MemoryBarrier();
      var s = new Task(() => PrintCharacters("?"));
      s.Start();

      
      Task.Factory.StartNew(() => PrintCharacters("-"));

      Console.WriteLine("Main Program Finished!!!");
      Console.ReadLine();
    }

    private static void PrintCharacters(string ch)
    {
      for (int i = 0; i < 3000; i++)
      {
        Console.Write(ch);
      }

    }
  }
}
