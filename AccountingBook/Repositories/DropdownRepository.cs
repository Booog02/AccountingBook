using AccountingBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingBook.Repositories
{
    internal class DropdownRepository : IDropdownRepository
    {
        public List<string> GetCategories()
        {
            return DataModel.Category;
        }

        public List<string> GetDetails(string category)
        {
            return DataModel.TypeDetails[category];
        }

        public List<string> GetPayments()
        {
            return DataModel.Payment;
        }

        public List<string> GetTargets()
        {
            return DataModel.Target;
        }
    }
}
