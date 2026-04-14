using AccountingBook.Contracts;
using AccountingBook.Models;
using AccountingBook.Models.DTO;
using AccountingBook.Repositories;
using AccountingBook.Repositories.Models;
using AccountingBook.Utility;
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
using static AccountingBook.Contracts.AddRecordContract;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace AccountingBook.Views
{
    public partial class 記一筆 : Form, IRecordView
    {
        private AddRecordPresenter presenter;
        public 記一筆()
        {
            InitializeComponent();

        }

        private void 記一筆_Load(object sender, EventArgs e)
        {
            presenter = new AddRecordPresenter(this);
            presenter.LoadDropdowns();

            TypeComboBox.SelectedIndexChanged += DetailComboBox_SelectedIndexChanged;


            pictureBox1.Image = Image.FromFile(@"C:\Users\bukut\Downloads\upload icon photo.png");
            pictureBox2.Image = Image.FromFile(@"C:\Users\bukut\Downloads\upload icon photo.png");
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

        }

        private void DetailComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            presenter.OnCategoryChanged(TypeComboBox.Text);
        }


        private void AddRecord_Click(object sender, EventArgs e)
        {
            this.Debounce(() =>
            {

                RecordModelDTO recordModel = new RecordModelDTO
                {
                    Date = dateTimePicker1.Value.ToString("yyyy-MM-dd"),
                    Amount = AmountTextBox.Text,
                    Category = TypeComboBox.SelectedItem.ToString(),
                    Detail = DetailComboBox.SelectedItem.ToString(),
                    Target = TargetComboBox.SelectedItem.ToString(),
                    Payment = PaymentComboBox.SelectedItem.ToString(),
                    ImagePath1 = pictureBox1.Image,
                    ImagePath2 = pictureBox2.Image,

                };
                presenter.AddRecord(recordModel);

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

        void IRecordView.SetDorpdown(DropdownModel model)
        {
            TypeComboBox.DataSource = model.Categories;
            TargetComboBox.DataSource = model.Targets;
            PaymentComboBox.DataSource = model.Payments;
        }

        public void SetDetails(List<string> details)
        {
            DetailComboBox.DataSource = details;
        }


    }
}
