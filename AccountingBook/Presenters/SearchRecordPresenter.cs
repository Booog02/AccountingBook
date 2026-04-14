using AccountingBook.Models.DTO;
using AccountingBook.Repositories;
using AccountingBook.Repositories.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static AccountingBook.Contracts.SearchRecordContract;

namespace AccountingBook.Contracts
{
    internal class SearchRecordPresenter : ISearchRecordPresenter
    {
        ISearchRecordView view;
        IRecordRepository recordRepository;

        public SearchRecordPresenter(ISearchRecordView view)
        {
            this.view = view;
            this.recordRepository = new RecordRepository();
        }
        public void DeleteRecord(SearchRecordModelDTO dto)
        {
            if (dto.ImagePath1 != "" && File.Exists(dto.ImagePath1))
            {
                File.Delete(dto.ImagePath1);
            }

            if (dto.ImagePath2 != "" && File.Exists(dto.ImagePath2))
            {
                File.Delete(dto.ImagePath2);
            }

            string bigFileName1 = dto.ImagePath1.Replace("small_", "");
            string bigFileName2 = dto.ImagePath2.Replace("small_", "");

            if (bigFileName1 != "" && File.Exists(bigFileName1))
            {
                File.Delete(bigFileName1);
            }

            if (bigFileName2 != "" && File.Exists(bigFileName2))
            {
                File.Delete(bigFileName2);
            }

            RecordModelDAO dao = new RecordModelDAO
            {
                Date = dto.Date,
                Amount = dto.Amount,
                Category = dto.Category,
                Detail = dto.Detail,
                Target = dto.Target,
                Payment = dto.Payment,
                ImagePath1 = dto.ImagePath1,
                ImagePath2 = dto.ImagePath2,

            };
            recordRepository.Delete(dao);

        }

        public void SearchRecords(DateTime startDate, DateTime endDate)
        {
            List<RecordModelDAO> dAOs = new List<RecordModelDAO>();
            List<SearchRecordModelDTO> dTOs = new List<SearchRecordModelDTO>();
            foreach (RecordModelDAO dAO in dAOs)
            {
                dTOs.Add(new SearchRecordModelDTO
                {
                    Date = dAO.Date,
                    Amount = dAO.Amount,
                    Category = dAO.Category,
                    Detail = dAO.Detail,
                    Target = dAO.Target,
                    Payment = dAO.Payment,
                    ImagePath1 = dAO.ImagePath1,
                    ImagePath2 = dAO.ImagePath2,
                });
            }
            view.ShowRecords(dTOs);
        }

        public void UpdateRecord(SearchRecordModelDTO record)
        {
            RecordModelDAO recordModel = new RecordModelDAO
            {
                Date = record.Date,
                Amount = record.Amount,
                Category = record.Category,
                Detail = record.Detail,
                Target = record.Target,
                Payment = record.Payment,
                ImagePath1 = record.ImagePath1,
                ImagePath2 = record.ImagePath2,

            };
            recordRepository.Update(recordModel);
        }
    }
}
