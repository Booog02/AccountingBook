using AccountingBook.Repositories.Models;
using CSVLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingBook.Repositories
{
    internal interface IRecordRepository
    {
        List<RecordModelDAO> GetRecordsByDateRange(DateTime startDate, DateTime endDate);
        List<RecordModelDAO> GetRecordsByDate(DateTime date);

        void Add(RecordModelDAO record);
        void Update(RecordModelDAO record);
        void Delete(RecordModelDAO record);

    }
}
