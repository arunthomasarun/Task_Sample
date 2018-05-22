using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Sample
{
  class _02ReturnValueFromTask
  {
    static void Main()
    {
      string s1 = "Arun", s2 = "Tresa";

      var v = new Task<int>(() => LengthOfString(s1));
      v.Start();

      Task<int> lenOfS2 =  Task.Factory.StartNew(() => LengthOfString(s2));

      Console.WriteLine($"Length of {s1} is {v.Result}");
      Console.WriteLine($"Length of {s2} is {lenOfS2.Result }");

      Console.ReadLine();
            
    }

    private static Int32 LengthOfString(string myString)
    { 
      Console.WriteLine($"Task with ID {Task.CurrentId} processing {myString}");
      return myString.Length;
    }
  }
}
