using AccountingBook.Models.DTO;
using AccountingBook.Repositories;
using AccountingBook.Repositories.Models;
using AccountingBook.Utility;
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

            RecordModelDAO dao = Mapper.Map<SearchRecordModelDTO, RecordModelDAO>(dto);
            recordRepository.Delete(dao);

        }

        public void SearchRecords(DateTime startDate, DateTime endDate)
        {
            List<RecordModelDAO> dAOs = recordRepository.GetRecordsByDateRange(startDate, endDate);

            List<SearchRecordModelDTO> dTOs = Mapper.Map<RecordModelDAO, SearchRecordModelDTO>(dAOs).ToList();

            view.ShowRecords(dTOs);
        }

        public void UpdateRecord(SearchRecordModelDTO record)
        {
            RecordModelDAO recordModel = Mapper.Map<SearchRecordModelDTO, RecordModelDAO>(record);
            recordRepository.Update(recordModel);
        }
    }
}
