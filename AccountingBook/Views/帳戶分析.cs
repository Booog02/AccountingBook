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
                presenter.SearchRecords(startDateTimePicker.Value.Date, endDateTimePicker.Value.Date, groups, filters);

            }, 400);
        }

        private void 帳戶分析_Load(object sender, EventArgs e)
        {
            //CreateChekcBoxes();
            //CreateFilterCheckBoxes("對象", presenter.GetTargets());
            //CreateFilterCheckBoxes("支付方式", presenter.GetPayments());

            flowLayoutPanel1.GroupCheckBoxGenerated(presenter, OnGroupCheckedChanged);
            flowLayoutPanel2.CheckBoxGenerated(OnFilterCheckedChanged);

        }

        //0627HW: presenter篩選
        //0630HW: 修好 篩選條件

        private void OnGroupCheckedChanged(object sender, EventArgs e)
        {

            CheckBox checkBox = sender as CheckBox;
            if (checkBox == null)
            {
                return;
            }
            if (checkBox.Checked)
            {
                if (groups.Contains(checkBox.Text) == false)
                {
                    groups.Add(checkBox.Text);

                }
            }
            else
            {
                groups.Remove(checkBox.Text);
                if (filters.ContainsKey(checkBox.Text))
                {
                    filters.Remove(checkBox.Text);

                }

            }
            Console.WriteLine(checkBox.Text);

        }
        private void OnFilterCheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            string category = "";
            if (checkBox == null)
            {
                return;
            }
            if (checkBox.Text == "全選")
            {
                return;
            }

            for (int i = 0; i < flowLayoutPanel2.Controls.Count; i++)
            {
                FlowLayoutPanel panel = (FlowLayoutPanel)flowLayoutPanel2.Controls[i];
                for (int j = 0; j < panel.Controls.Count; j++)
                {
                    if (panel.Controls[j] == checkBox)
                    {
                        category = panel.Name;
                        break;
                    }
                }
                if (category != "")
                {
                    break;
                }
            }
            if (category == "")
            {
                return;
            }


            if (checkBox.Checked)
            {
                if (filters.ContainsKey(category) == false)
                {
                    filters.Add(category, new List<string>());
                }
                if (filters[category].Contains(checkBox.Text) == false)
                {
                    filters[category].Add(checkBox.Text);
                }

            }
            else
            {

                if (filters.ContainsKey(category))
                {
                    filters[category].Remove(checkBox.Text);
                }
                if (filters[category].Count == 0)
                {
                    filters.Remove(category);
                }

            }




            Console.WriteLine(checkBox.Text);
        }





    }
}
