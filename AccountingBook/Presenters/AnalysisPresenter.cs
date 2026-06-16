using AccountingBook.Models.DTO;
using AccountingBook.Repositories;
using AccountingBook.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using static AccountingBook.Contracts.AnalysisRecordContract;

namespace AccountingBook.Presenters
{
    internal class AnalysisPresenter : IAnalysisRecordPresenter
    {
        IAnalysisRecordView view;
        IRecordRepository recordRepository;
        IDropdownRepository dropDownRepository;

        public AnalysisPresenter(IAnalysisRecordView view)
        {
            this.view = view;
            this.recordRepository = new RecordRepository();
            this.dropDownRepository = new DropdownRepository();
        }

        public void SearchRecords(DateTime startDate, DateTime endDate)
        {
            List<RecordModelDAO> dAOs = recordRepository.GetRecordsByDateRange(startDate, endDate);


            view.ShowRecords(dAOs);
        }

        public List<string> GetCategories()
        {
            return dropDownRepository.GetCategories();
        }
        public List<String> GetDetails(string category)
        {
            return dropDownRepository.GetDetails(category);
        }
        public List<string> GetTargets()
        {
            return dropDownRepository.GetTargets();
        }
        public List<string> GetPayments()
        {
            return dropDownRepository.GetPayments();
        }
    }
}
