using AccountingBook.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountingBook.Repositories
{
    internal interface IDropdownRepository
    {
        List<string> GetCategories();
        List<String> GetDetails(string category);
        List<string> GetTargets();
        List<string> GetPayments();
    }
}
