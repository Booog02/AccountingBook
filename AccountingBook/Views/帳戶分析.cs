using AccountingBook.Models.DTO;
using AccountingBook.Presenters;
using AccountingBook.Repositories;
using AccountingBook.Repositories.Models;
using AccountingBook.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AccountingBook.Contracts.AnalysisRecordContract;

namespace AccountingBook.Views
{
    public partial class 帳戶分析 : Form, IAnalysisRecordView
    {
        private AnalysisPresenter presenter;
        //private bool isUpdating = false;

        List<string> groups = new List<string>(); // 類型: 食衣住行育樂
        Dictionary<string, List<string>> filters = new Dictionary<string, List<string>>();
        // 篩選條件: 食, [午餐,晚餐]
        public 帳戶分析()
        {

            InitializeComponent();

            presenter = new AnalysisPresenter(this);
        }


        void IAnalysisRecordView.ShowRecords(List<RecordModelDAO> dAOs)
        {
            dataGridView1.DataSource = dAOs;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Debounce(() =>
            {

                presenter.SearchRecords(startDateTimePicker.Value.Date, endDateTimePicker.Value.Date);

            }, 400);
        }

        private void 帳戶分析_Load(object sender, EventArgs e)
        {
            //CreateChekcBoxes();
            //CreateFilterCheckBoxes("對象", presenter.GetTargets());
            //CreateFilterCheckBoxes("支付方式", presenter.GetPayments());

            flowLayoutPanel1.GroupCheckBoxGenerated(presenter);
            flowLayoutPanel2.CheckBoxGenerated();

        }





    }
}
