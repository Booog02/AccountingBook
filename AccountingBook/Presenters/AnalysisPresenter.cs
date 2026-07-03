using AccountingBook.Models.DTO;
using AccountingBook.Repositories;
using AccountingBook.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using static AccountingBook.Contracts.AnalysisRecordContract;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

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

        public void SearchRecords(DateTime startDate, DateTime endDate, List<string> groups, Dictionary<string, List<string>> filters)
        {
            List<RecordModelDAO> dAOs = recordRepository.GetRecordsByDateRange(startDate, endDate);
            List<string> categories = GetCategories();
            List<string> selectedDetails = new List<string>();

            foreach (string category in categories)
            {
                if (filters.ContainsKey(category))
                {
                    selectedDetails.AddRange(filters[category]);

                }
            }
            if (selectedDetails.Count > 0)
            {
                dAOs = dAOs.Where(x => selectedDetails.Contains(x.Detail)).ToList();
            }

            if (filters.ContainsKey("對象"))
            {
                List<string> targets = filters["對象"];
                dAOs = dAOs.Where(x => targets.Contains(x.Target)).ToList();
            }
            if (filters.ContainsKey("支付方式"))
            {
                List<string> payments = filters["支付方式"];
                dAOs = dAOs.Where(x => payments.Contains(x.Payment)).ToList();
            }
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
