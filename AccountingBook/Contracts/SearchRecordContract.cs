using AccountingBook.Models.DTO;
using AccountingBook.Repositories;
using AccountingBook.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingBook.Contracts
{
    internal class SearchRecordContract
    {

        internal interface ISearchRecordView
        {
            void ShowRecords(List<SearchRecordModelDTO> records);

        }

        internal interface ISearchRecordPresenter
        {
            void SearchRecords(DateTime startDate, DateTime endDate);
            void UpdateRecord(SearchRecordModelDTO record);
            void DeleteRecord(SearchRecordModelDTO record);
        }
    }
}
