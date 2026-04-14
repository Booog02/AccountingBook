using AccountingBook.Models;
using AccountingBook.Models.DTO;
using AccountingBook.Repositories;
using AccountingBook.Repositories.Models;
using AccountingBook.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AccountingBook.Contracts.AddRecordContract;

namespace AccountingBook.Contracts
{
    internal class AddRecordPresenter : IRecordPresenter
    {
        IRecordView view;
        IRecordRepository recordRepository;
        IDropdownRepository dropDownRepository;

        public AddRecordPresenter(IRecordView view)
        {
            this.view = view;
            this.recordRepository = new RecordRepository();
            this.dropDownRepository = new DropdownRepository();
        }

        public void AddRecord(RecordModelDTO record)
        {
            //HW: 將資料的儲存改為日期劃分，將記帳的資料新增到指定日期的資料夾，若該日期不存在，則自動創建資料夾
            //每一個資料夾固定都有data.csv 以及所有當天日期的圖片

            string date = record.Date;
            string rootFolder = @"C:\Users\bukut\Desktop\C#課程\記帳本資料";
            string dateFolder = rootFolder + "\\" + date;

            if (!Directory.Exists(dateFolder))
            {
                Directory.CreateDirectory(dateFolder);
            }

            string saveImagePath1 = "";
            if (record.ImagePath1 != null)
            {
                string fileName1 = Guid.NewGuid().ToString() + ".jpg";
                string bitImagePath1 = dateFolder + "\\" + fileName1;
                saveImagePath1 = dateFolder + "\\small_" + fileName1;

                Bitmap bigBitmap1 = ImageCompress.CompressionJpg((Bitmap)record.ImagePath1, 25L);
                Bitmap smailBitmap1 = ImageCompress.ResizeImage((Bitmap)record.ImagePath1, 40, 40);

                bigBitmap1.Save(bitImagePath1, ImageFormat.Jpeg);
                smailBitmap1.Save(saveImagePath1, ImageFormat.Jpeg);

                bigBitmap1.Dispose();
                smailBitmap1.Dispose();
                bigBitmap1.Dispose();
            }

            string saveImagePath2 = "";
            if (record.ImagePath2 != null)
            {
                string fileName2 = Guid.NewGuid().ToString() + ".jpg";

                string bitImagePath2 = dateFolder + "\\" + fileName2;
                saveImagePath2 = dateFolder + "\\small_" + fileName2;

                Bitmap bigBitmap2 = ImageCompress.CompressionJpg((Bitmap)record.ImagePath2, 25L);
                Bitmap smailBitmap2 = ImageCompress.ResizeImage((Bitmap)record.ImagePath2, 40, 40);
                bigBitmap2.Save(bitImagePath2, ImageFormat.Jpeg);
                smailBitmap2.Save(saveImagePath2, ImageFormat.Jpeg);

                bigBitmap2.Dispose();
                smailBitmap2.Dispose();
                bigBitmap2.Dispose();
            }

            RecordModelDAO recordModel = new RecordModelDAO
            {
                Date = record.Date,
                Amount = record.Amount,
                Category = record.Category,
                Detail = record.Detail,
                Target = record.Target,
                Payment = record.Payment,
                ImagePath1 = saveImagePath1,
                ImagePath2 = saveImagePath2,

            };
            recordRepository.Add(recordModel);
        }

        public void LoadDropdowns()
        {
            DropdownModel model = new DropdownModel
            {
                Categories = dropDownRepository.GetCategories(),
                Targets = dropDownRepository.GetTargets(),
                Payments = dropDownRepository.GetPayments(),
            };
            view.SetDorpdown(model);
            view.SetDetails(dropDownRepository.GetDetails(dropDownRepository.GetCategories()[0]));
        }

        public void OnCategoryChanged(string category)
        {
            view.SetDetails(dropDownRepository.GetDetails(category));
        }

    }
}
