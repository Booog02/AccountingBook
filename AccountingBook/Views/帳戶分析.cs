using AccountingBook.Models.DTO;
using AccountingBook.Presenters;
using AccountingBook.Repositories;
using AccountingBook.Repositories.Models;
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
            CreateChekcBoxes();
            CreateFilterCheckBoxes("對象", presenter.GetTargets());
            CreateFilterCheckBoxes("支付方式", presenter.GetPayments());
        }



        private void CreateChekcBoxes()
        {
            flowLayoutPanel1.Controls.Clear();
            string[] groups =
            {
                "類型",
                "對象",
                "支付方式"
            };
            FlowLayoutPanel panel = new FlowLayoutPanel();
            panel.Width = flowLayoutPanel1.Width;
            panel.Height = 30;

            foreach (string group in groups)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Text = group;
                checkBox.Width = 75;

                checkBox.CheckedChanged += GroupCheckBox_CheckedChanged;
                panel.Controls.Add(checkBox);
            }
            flowLayoutPanel1.Controls.Add(panel);
        }

        private void GroupCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox groupCheckBbox = (CheckBox)sender;
            if (groupCheckBbox == null)
            {
                return;
            }

            if (groupCheckBbox.Text == "類型")
            {
                if (groupCheckBbox.Checked)
                {
                    CreateGroupCheckBoxes("類型", presenter.GetCategories());
                }
                else
                {
                    ClearGroupCheckBoxes("類型");
                    flowLayoutPanel2.Controls.Clear();
                }
            }
            else if (groupCheckBbox.Text == "對象")
            {
                if (groupCheckBbox.Checked)
                {
                    CreateGroupCheckBoxes("對象", presenter.GetTargets());
                }
                else
                {
                    ClearGroupCheckBoxes("對象");
                    flowLayoutPanel2.Controls.Clear();


                }
            }
            else if (groupCheckBbox.Text == "支付方式")
            {
                if (groupCheckBbox.Checked)
                {
                    CreateGroupCheckBoxes("支付方式", presenter.GetPayments());
                }
                else
                {
                    ClearGroupCheckBoxes("支付方式");
                    flowLayoutPanel2.Controls.Clear();


                }
            }


        }
        private void CreateGroupCheckBoxes(string groupName, List<string> items)
        {

            ClearGroupCheckBoxes(groupName);
            FlowLayoutPanel panel = new FlowLayoutPanel();
            panel.Name = groupName;
            panel.Width = flowLayoutPanel1.Width;
            panel.Height = 40;
            panel.FlowDirection = FlowDirection.LeftToRight;
            panel.BorderStyle = BorderStyle.FixedSingle;


            CheckBox selectAllCheckBox = new CheckBox();
            selectAllCheckBox.Text = "全選";
            selectAllCheckBox.AutoSize = true;
            selectAllCheckBox.Tag = false;
            selectAllCheckBox.CheckedChanged += SelectAllCheckBox_CheckedChange;
            panel.Controls.Add(selectAllCheckBox);

            foreach (string item in items)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Text = item;
                checkBox.AutoSize = true;
                checkBox.Tag = false;
                if (groupName == "類型")
                {
                    checkBox.CheckedChanged += TypeCategoryCheckBox_CheckedChanged;
                }
                checkBox.CheckedChanged += SelectAllCheckBox_CheckedChange;
                panel.Controls.Add(checkBox);
            }
            flowLayoutPanel1.Controls.Add(panel);

        }


        private void ClearGroupCheckBoxes(string groupName)
        {
            foreach (Control control in flowLayoutPanel1.Controls)
            {
                if (control.Name == groupName)
                {
                    flowLayoutPanel1.Controls.Remove(control);
                    break;
                }
            }
        }

        // HW: 將下面的細項每一個項目都加上全選的按鈕可以自動勾選
        // 如果取消任何一個項目，則全選按鈕要能自動取消全選
        // Tips: 可以使用 Tag 暫存資料
        private void TypeCategoryCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox categoryCheckBox = (CheckBox)sender;
            if (categoryCheckBox == null)
            {
                return;
            }
            if (categoryCheckBox.Checked)
            {
                CreateFilterCheckBoxes(categoryCheckBox.Text);
            }
            else
            {
                RemoveFilterCheckBoxes(categoryCheckBox.Text);

            }
        }

        private void RemoveFilterCheckBoxes(string category)
        {
            for (int i = 0; i < flowLayoutPanel2.Controls.Count; i++)
            {
                if (flowLayoutPanel2.Controls[i].Name == category)
                {
                    flowLayoutPanel2.Controls.RemoveAt(i);
                    break;
                }
            }

        }
        private void CreateFilterCheckBoxes(string category, List<string> items)
        {
            for (int i = 0; i < flowLayoutPanel2.Controls.Count; i++)
            {
                if (flowLayoutPanel2.Controls[i].Name == category)
                {
                    return;
                }
            }
            FlowLayoutPanel panel = new FlowLayoutPanel();
            panel.Name = category;
            panel.Width = flowLayoutPanel2.Width;
            panel.Height = 35;
            panel.FlowDirection = FlowDirection.LeftToRight;
            panel.BorderStyle = BorderStyle.FixedSingle;

            CheckBox selectAllCheckBox = new CheckBox();
            selectAllCheckBox.Text = "全選";
            selectAllCheckBox.AutoSize = true;
            selectAllCheckBox.Tag = false;

            selectAllCheckBox.CheckedChanged += SelectAllCheckBox_CheckedChange;
            panel.Controls.Add(selectAllCheckBox);


            foreach (string item in items)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Text = item;
                checkBox.AutoSize = true;
                checkBox.Tag = false;

                checkBox.CheckedChanged += DetailCheckBox_CheckedChanged;


                panel.Controls.Add(checkBox);
            }
            flowLayoutPanel2.Controls.Add(panel);
        }


        private void CreateFilterCheckBoxes(string category)
        {
            for (int i = 0; i < flowLayoutPanel2.Controls.Count; i++)
            {
                if (flowLayoutPanel2.Controls[i].Name == category)
                {
                    return;
                }
            }

            List<string> items = presenter.GetDetails(category);
            FlowLayoutPanel panel = new FlowLayoutPanel();
            panel.Name = category;
            panel.Width = flowLayoutPanel2.Width;
            panel.Height = 35;
            panel.FlowDirection = FlowDirection.LeftToRight;
            panel.BorderStyle = BorderStyle.FixedSingle;

            CheckBox selectAllCheckBox = new CheckBox();
            selectAllCheckBox.Text = "全選";
            selectAllCheckBox.AutoSize = true;
            selectAllCheckBox.Tag = false;

            selectAllCheckBox.CheckedChanged += SelectAllCheckBox_CheckedChange;
            panel.Controls.Add(selectAllCheckBox);


            foreach (string item in items)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Text = item;
                checkBox.AutoSize = true;
                checkBox.Tag = false;

                checkBox.CheckedChanged += DetailCheckBox_CheckedChanged;


                panel.Controls.Add(checkBox);
            }
            flowLayoutPanel2.Controls.Add(panel);

        }

        private void SelectAllCheckBox_CheckedChange(object sender, EventArgs e)
        {


            CheckBox selectAllCheckBox = (CheckBox)sender;
            if ((bool)selectAllCheckBox.Tag)
            {
                return;
            }
            for (int i = 0; i < flowLayoutPanel2.Controls.Count; i++)
            {
                FlowLayoutPanel panel = (FlowLayoutPanel)flowLayoutPanel2.Controls[i];
                bool isThisPanel = false;

                for (int j = 0; j < panel.Controls.Count; j++)
                {
                    if (panel.Controls[j] == selectAllCheckBox)
                    {
                        isThisPanel = true;
                        break;
                    }
                }

                if (isThisPanel)
                {
                    for (int j = 0; j < panel.Controls.Count; j++)
                    {
                        CheckBox checkBox = (CheckBox)panel.Controls[j];
                        if (checkBox.Text != "全選")
                        {
                            checkBox.Tag = true;
                            checkBox.Checked = selectAllCheckBox.Checked;
                            checkBox.Tag = false;
                        }
                    }
                    break;
                }

            }

        }
        private void DetailCheckBox_CheckedChanged(object sender, EventArgs e)
        {

            CheckBox detailCheckBox = (CheckBox)sender;
            if ((bool)detailCheckBox.Tag)
            {
                return;
            }

            for (int i = 0; i < flowLayoutPanel2.Controls.Count; i++)
            {
                FlowLayoutPanel panel = (FlowLayoutPanel)flowLayoutPanel2.Controls[i];
                bool isThisPanel = false;

                for (int j = 0; j < panel.Controls.Count; j++)
                {
                    if (panel.Controls[j] == detailCheckBox)
                    {
                        isThisPanel = true;
                        break;
                    }
                }

                if (isThisPanel)
                {
                    bool allChecked = true;
                    for (int j = 0; j < panel.Controls.Count; j++)
                    {
                        CheckBox checkBox = (CheckBox)panel.Controls[j];
                        if (checkBox.Text != "全選" && checkBox.Checked == false)
                        {
                            allChecked = false;
                            break;

                        }
                    }


                    for (int j = 0; j < panel.Controls.Count; j++)
                    {
                        CheckBox checkBox = (CheckBox)panel.Controls[j];
                        if (checkBox.Text == "全選")
                        {
                            checkBox.Tag = true;

                            checkBox.Checked = allChecked;

                            checkBox.Tag = false;
                            break;

                        }
                    }
                    break;

                }

            }
        }


    }
}
