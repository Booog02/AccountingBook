using AccountingBook.Attributes;
using AccountingBook.Models;
using CSVLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountingBook.Views
{
    public partial class 記帳本 : Form
    {
        public 記帳本()
        {
            InitializeComponent();

        }

        private void 記帳本_Load(object sender, EventArgs e)
        {
            //DataGridView構成:   dataGridView1.DataSource = recordList;
            //DataGridViewColumn(行) =>  先透過反射，抓取目標物件的所有公開屬性來創建Column, 創建的Column為 DataGridViewTextboxColumn
            //DataGridViewRow(列) => 跑迴圈將每一筆資料都建立DataGridViewRow
            //DataGridViewCell(欄) => 建立在Row底下的每一格Cell,每一格的Cell都會有OwningColumn來去表示他所對應的Column,如果是TextboxColumn,那就會創建對應的 TextboxCell

            dataGridView1.CellEndEdit += DataGridView1_CellEndEdit;
        }

        List<RecordModel> recordList;

        private void LoadOldData()
        {


            for (int i = 0; i < recordList.Count; i++)
            {
                //dataGridView1.Rows[i].Cells["Date"].Value = recordList[i].Date;
                //dataGridView1.Rows[i].Cells["Amount"].Value = recordList[i].Amount;
                //dataGridView1.Rows[i].Cells["Category_ComboBox"].Value = recordList[i].Category;
                DataGridViewComboBoxCell detailCell = (DataGridViewComboBoxCell)dataGridView1.Rows[i].Cells["Detail_ComboBox"];
                detailCell.DataSource = DataModel.TypeDetails[recordList[i].Category];
                detailCell.Value = recordList[i].Detail;
                //dataGridView1.Rows[i].Cells["Target_ComboBox"].Value = recordList[i].Target;
                //dataGridView1.Rows[i].Cells["Payment_ComboBox"].Value = recordList[i].Payment;

                //File.ReadAllBytes()
                //MemoryStream
                //Bitmap

                dataGridView1.Rows[i].Cells.OfType<DataGridViewImageCell>()
                    .Where(x => x.OwningColumn.Name != "ImagePath3_Image").ToList().ForEach(cell =>
                    {
                        string propName = cell.OwningColumn.Name.Replace("_Image", "");
                        string imagePath = (string)typeof(RecordModel).GetProperty(propName).GetValue(recordList[i]);

                        if (imagePath != "")
                        {
                            byte[] byte1 = File.ReadAllBytes(imagePath);
                            MemoryStream memStream = new MemoryStream(byte1);
                            Bitmap bitmap = new Bitmap(memStream);
                            cell.Value = bitmap;
                        }
                    });
            }
        }

        private void BuildColumns()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = recordList;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //0331HW: 使用擴充方法，把創建ComboBoxColumn & ImageColumn 的創建過程，用擴充方法封裝起來
            //dataGridView.CreateComboBoxColumn(?);
            var props = typeof(RecordModel).GetProperties();
            foreach (var prop in props)
            {
                string headerText = prop.GetCustomAttribute<DisplayNameAttribute>().DisplayName;

                if (prop.GetCustomAttribute<ComboBoxColumnAttribute>() is ComboBoxColumnAttribute)
                {
                    var dataModelType = typeof(DataModel);
                    var field = dataModelType.GetField(prop.Name);
                    object dataSource = field?.GetValue(null);
                    dataGridView1.CreateComboBoxColumn(prop.Name, headerText, dataSource);

                }


                if (prop.GetCustomAttribute<ImageColumnAttribute>() is ImageColumnAttribute)
                {

                    dataGridView1.CreateImageColumn(prop.Name, headerText);

                }
            }
        }


        //HW 完成查詢按鈕,點選後 記一筆資料會顯示在 dataGridView 上,
        //   RecordModel.cs 紀錄記一筆的資料,以便切割使用  
        private void SearchButton_Click(object sender, EventArgs e)
        {
            this.Debounce(() =>
            {
                DisposeGridImages();

                DateTime startDate = startDateTimePicker.Value.Date;
                DateTime endDate = endDateTimePicker.Value.Date;

                TimeSpan timeSpan = endDate - startDate;
                int days = timeSpan.Days;
                string rootFolder = @"C:\\Users\\bukut\\Desktop\\C#課程\\記帳本資料";

                recordList = new List<RecordModel>();

                for (int i = 0; i <= days; i++)
                {
                    string dateFolder = rootFolder + "\\" + startDate.AddDays(i).ToString("yyyy-MM-dd");
                    string filePath = dateFolder + "\\date.csv";

                    if (File.Exists(filePath))
                    {
                        List<RecordModel> oneDayList = CSVHelper.Read<RecordModel>(filePath);
                        recordList.AddRange(oneDayList);
                    }
                }

                BuildColumns();
                LoadOldData();

            }, 400);

        }


        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string editedValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            if (dataGridView1.Columns[e.ColumnIndex].Name == "Category_ComboBox")
            {
                DataGridViewComboBoxCell detailCell = (DataGridViewComboBoxCell)dataGridView1.Rows[e.RowIndex].Cells["Detail_ComboBox"];

                var detailList = DataModel.TypeDetails[editedValue];
                detailCell.DataSource = detailList;
                detailCell.Value = detailList[0];
                recordList[e.RowIndex].Detail = detailList[0];

            }
            string oneDate = recordList[e.RowIndex].Date;

            List<RecordModel> oneDayList = recordList.Where(x => x.Date == oneDate).ToList();

            string rootFolder = @"C:\\Users\\bukut\\Desktop\\C#課程\\記帳本資料";
            string dateFolder = rootFolder + "\\" + oneDate;
            string filePath = dateFolder + "\\date.csv";


            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            CSVHelper.Write<RecordModel>(filePath, oneDayList);

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            RecordModel model = recordList[e.RowIndex];

            if (dataGridView1.Columns[e.ColumnIndex].Name == "ImagePath1_Image")
            {
                string bigFileName1 = model.ImagePath1.Replace("small_", "");
                if (File.Exists(bigFileName1))
                {
                    Imageform imageform = new Imageform(bigFileName1);
                    imageform.ShowDialog();
                }
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "ImagePath2_Image")
            {
                string bigFileName2 = model.ImagePath2.Replace("small_", "");
                if (File.Exists(bigFileName2))
                {
                    Imageform imageform = new Imageform(bigFileName2);
                    imageform.ShowDialog();
                }

            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "ImagePath3_Image")
            {

                if (model.ImagePath1 != "" && File.Exists(model.ImagePath1))
                {
                    File.Delete(model.ImagePath1);
                }
                if (model.ImagePath2 != "" && File.Exists(model.ImagePath2))
                {
                    File.Delete(model.ImagePath2);
                }

                string bigFileName1 = model.ImagePath1.Replace("small_", "");
                string bigFileName2 = model.ImagePath2.Replace("small_", "");

                if (bigFileName1 != "" && File.Exists(bigFileName1))
                {
                    File.Delete(bigFileName1);
                }

                if (bigFileName2 != "" && File.Exists(bigFileName2))
                {
                    File.Delete(bigFileName2);
                }

                string oneDate = model.Date;
                recordList.RemoveAt(e.RowIndex);

                List<RecordModel> oneDayList = recordList.Where(x => x.Date == oneDate).ToList();

                string rootFolder = @"C:\\Users\\bukut\\Desktop\\C#課程\\記帳本資料";
                string dateFolder = rootFolder + "\\" + oneDate;
                string filePath = dateFolder + "\\date.csv";

                if (oneDayList.Count > 0)
                {
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    CSVHelper.Write<RecordModel>(filePath, oneDayList);
                }
                else
                {

                    Directory.Delete(dateFolder, true);
                }

                //HW: 刪除資料的時候，同時也要將圖片進行移除, 提示: MemoryStream
                BuildColumns();
                LoadOldData();
            }

        }

        private void DisposeGridImages()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                dataGridView1.Rows[i].Cells.OfType<DataGridViewImageCell>()
                    .Where(x => x.Value != null).ToList().ForEach(cell =>
                    {
                        Image image = (Image)cell.Value;
                        image.Dispose();
                    });

            }
        }
    }
}
