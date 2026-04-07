using AccountingBook.Models;
using CSVLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using AccountingBook.Utility;

namespace AccountingBook.Views
{
    public partial class 記一筆 : Form
    {

        public 記一筆()
        {
            InitializeComponent();
        }

        private void 記一筆_Load(object sender, EventArgs e)
        {
            TypeComboBox.DataSource = DataModel.Category;
            TypeComboBox.SelectedIndexChanged += DetailComboBox_SelectedIndexChanged;
            var detailList = DataModel.Category[0];
            DetailComboBox.DataSource = DataModel.TypeDetails[detailList];
            TargetComboBox.DataSource = DataModel.Target;
            PaymentComboBox.DataSource = DataModel.Payment;

            pictureBox1.Image = Image.FromFile(@"C:\Users\bukut\Downloads\upload icon photo.png");
            pictureBox2.Image = Image.FromFile(@"C:\Users\bukut\Downloads\upload icon photo.png");
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

        }

        private void DetailComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string category = TypeComboBox.SelectedItem.ToString();
            DetailComboBox.DataSource = DataModel.TypeDetails[category];
        }


        private void AddRecord_Click(object sender, EventArgs e)
        {
            this.Debounce(() =>
            {
                //HW: 將資料的儲存改為日期劃分，將記帳的資料新增到指定日期的資料夾，若該日期不存在，則自動創建資料夾
                //每一個資料夾固定都有data.csv 以及所有當天日期的圖片

                string date = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                string rootFolder = @"C:\Users\bukut\Desktop\C#課程\記帳本資料";
                string dateFolder = rootFolder + "\\" + date;

                if (!Directory.Exists(dateFolder))
                {
                    Directory.CreateDirectory(dateFolder);
                }

                string saveImagePath1 = "";
                if (pictureBox1.Image != null)
                {
                    string fileName1 = Guid.NewGuid().ToString() + ".jpg";
                    string bitImagePath1 = dateFolder + "\\" + fileName1;
                    saveImagePath1 = dateFolder + "\\small_" + fileName1;

                    Bitmap bigBitmap1 = ImageCompress.CompressionJpg((Bitmap)pictureBox1.Image, 25L);
                    Bitmap smailBitmap1 = ImageCompress.ResizeImage((Bitmap)pictureBox1.Image, 40, 40);

                    bigBitmap1.Save(bitImagePath1, ImageFormat.Jpeg);
                    smailBitmap1.Save(saveImagePath1, ImageFormat.Jpeg);

                    bigBitmap1.Dispose();
                    smailBitmap1.Dispose();
                    bigBitmap1.Dispose();
                }

                string saveImagePath2 = "";
                if (pictureBox2.Image != null)
                {
                    string fileName2 = Guid.NewGuid().ToString() + ".jpg";

                    string bitImagePath2 = dateFolder + "\\" + fileName2;
                    saveImagePath2 = dateFolder + "\\small_" + fileName2;

                    Bitmap bigBitmap2 = ImageCompress.CompressionJpg((Bitmap)pictureBox2.Image, 25L);
                    Bitmap smailBitmap2 = ImageCompress.ResizeImage((Bitmap)pictureBox2.Image, 40, 40);
                    bigBitmap2.Save(bitImagePath2, ImageFormat.Jpeg);
                    smailBitmap2.Save(saveImagePath2, ImageFormat.Jpeg);

                    bigBitmap2.Dispose();
                    smailBitmap2.Dispose();
                    bigBitmap2.Dispose();
                }

                RecordModel recordModel = new RecordModel
                {
                    Date = date,
                    Amount = AmountTextBox.Text,
                    Category = TypeComboBox.SelectedItem.ToString(),
                    Detail = DetailComboBox.SelectedItem.ToString(),
                    Target = TargetComboBox.SelectedItem.ToString(),
                    Payment = PaymentComboBox.SelectedItem.ToString(),
                    ImagePath1 = saveImagePath1,
                    ImagePath2 = saveImagePath2,

                };

                CSVHelper.Write(dateFolder + "\\date.csv", recordModel);

                ResetForm();

            }, 400);
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "檔案圖片|*.jpg;*.jpeg;*.png";
            if (dialog.ShowDialog() != DialogResult.OK)
                return;
            PictureBox pictureBox = sender as PictureBox;
            pictureBox.Image.Dispose();
            pictureBox.Image = Image.FromFile(dialog.FileName);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void ResetForm()
        {
            dateTimePicker1.Value = DateTime.Now;
            AmountTextBox.Text = "";
            TypeComboBox.SelectedIndex = 0;
            string category = TypeComboBox.SelectedItem.ToString();
            DetailComboBox.DataSource = DataModel.TypeDetails[category];
            DetailComboBox.SelectedIndex = 0;
            TargetComboBox.SelectedIndex = 0;
            PaymentComboBox.SelectedIndex = 0;


            pictureBox1.Image.Dispose();

            pictureBox2.Image.Dispose();

            GC.Collect();
            pictureBox1.Image = Image.FromFile(@"C:\Users\bukut\Downloads\upload icon photo.png");
            pictureBox2.Image = Image.FromFile(@"C:\Users\bukut\Downloads\upload icon photo.png");

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        }

    }
}
