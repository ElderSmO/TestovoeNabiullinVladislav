using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestovoeNabiullinVladislav
{
    internal class Transaction
    {
        int id {  get; set; }

        DateTime date;
        double amount { get; set; }

        Type type { get; set; }

        enum Type
        {
            Expense,
            Income
        }


    }
}