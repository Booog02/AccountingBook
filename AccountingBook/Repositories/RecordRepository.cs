using AccountingBook.Repositories.Models;
using CSVLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountingBook.Repositories
{
    internal class RecordRepository : IRecordRepository
    {
        private string rootFolder = @"C:\\Users\\bukut\\Desktop\\C#課程\\記帳本資料";
        public void Add(RecordModelDAO record)
        {
            string filePath = Path.Combine(rootFolder, record.Date, "date.csv");
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            CSVHelper.Write(filePath + "\\date.csv", record);
        }

        public void Delete(RecordModelDAO record)
        {


            string filePath = Path.Combine(rootFolder, record.Date, "date.csv");
            List<RecordModelDAO> oneDayList = GetRecordsByDate(DateTime.Parse(record.Date));
            oneDayList.RemoveAll(x => x.ImagePath1 == record.ImagePath1);

            if (oneDayList.Count > 0)
            {
                File.Delete(filePath);
                CSVHelper.Write<RecordModelDAO>(filePath, oneDayList);
            }
            else
            {
                Directory.Delete(filePath, true);
            }

        }

        public List<RecordModelDAO> GetRecordsByDate(DateTime date)
        {
            string filePath = Path.Combine(rootFolder, date.ToString("yyyy-MM-dd"), "date.csv");
            if (File.Exists(filePath))
            {
                return CSVHelper.Read<RecordModelDAO>(filePath);
            }
            return new List<RecordModelDAO>();
        }
        public List<RecordModelDAO> GetRecordsByDateRange(DateTime startDate, DateTime endDate)
        {
            List<RecordModelDAO> result = new List<RecordModelDAO>();
            int days = (endDate - startDate).Days;
            for (int i = 0; i <= days; i++)
            {
                List<RecordModelDAO> oneDayList = GetRecordsByDate(startDate.AddDays(i));
                result.AddRange(oneDayList);
            }
            return result;
        }

        public void Update(RecordModelDAO record)
        {

            string filePath = Path.Combine(rootFolder, record.Date, "date.csv");// rootFolder + "\\" + record.Date + "\\date.csv";
            List<RecordModelDAO> oneDayList = GetRecordsByDate(DateTime.Parse(record.Date));

            for (int i = 0; i < oneDayList.Count; i++)
            {
                if (oneDayList[i].ImagePath1 == record.ImagePath1)
                {
                    oneDayList[i] = record;
                    break;
                }
            }
            File.Delete(filePath);
            CSVHelper.Write<RecordModelDAO>(filePath, oneDayList);
        }
    }
}
