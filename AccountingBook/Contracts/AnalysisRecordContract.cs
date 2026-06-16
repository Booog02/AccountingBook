using AccountingBook.Models.DTO;
using AccountingBook.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingBook.Contracts
{
    internal class AnalysisRecordContract
    {
        public interface IAnalysisRecordView
        {

            void ShowRecords(List<RecordModelDAO> dAOs);
        }

        public interface IAnalysisRecordPresenter
        {
            void SearchRecords(DateTime startDate, DateTime endDate);
        }

    }
}
