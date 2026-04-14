using AccountingBook.Models;
using AccountingBook.Models.DTO;
using AccountingBook.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingBook.Contracts
{
    internal class AddRecordContract
    {
        internal interface IRecordView
        {
            void SetDorpdown(DropdownModel model);
            void SetDetails(List<string> details);

        }

        internal interface IRecordPresenter
        {
            void LoadDropdowns();
            void OnCategoryChanged(string category);
            void AddRecord(RecordModelDTO record);




        }
    }
}
