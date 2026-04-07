using AccountingBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountingBook
{
    internal static class Extension
    {
        private static System.Threading.Timer debounceTimer;
        private static Form callbackForm;
        private static Action callbackAction;
        private static string callbackText;

        //public static void Debounce(this Form1 form, Action value, int v)
        //{
        //    callbackForm = form;
        //    callbackAction = value;

        //    if (debounceTimer == null)
        //    {
        //        debounceTimer = new System.Threading.Timer(ThreadingTimerCallback2, null, 400, -1);

        //    }
        //    else
        //    {
        //        debounceTimer.Change(400, -1);

        //    }
        //}

        #region 帶參數callback
        //public static void Debounce(this Form form, Action<string> value, int v, string text)
        //{
        //    callbackForm = form;
        //    callbackAction = value;
        //    //callbackText = text;

        //    if (debounceTimer == null)
        //    {
        //        debounceTimer = new System.Threading.Timer(ThreadingTimerCallback2, text, 400, -1);

        //    }
        //    else
        //    {
        //        debounceTimer.Change(400, -1);

        //    }
        //}
        #endregion
        public static void Debounce(this Form form, Action value, int v)
        {
            callbackForm = form;
            callbackAction = value;

            if (debounceTimer == null)
            {
                debounceTimer = new System.Threading.Timer(ThreadingTimerCallback2, null, 400, -1);

            }
            else
            {
                debounceTimer.Change(400, -1);

            }
        }

        public static void CreateComboBoxColumn(this DataGridView dataGridView, string propName, string headerText, object dataSource)
        {
            DataGridViewComboBoxColumn column = new DataGridViewComboBoxColumn();

            column.Name = propName + "_ComboBox";
            column.HeaderText = headerText;
            column.DataPropertyName = propName;
            column.DataSource = dataSource;
            dataGridView.Columns.Add(column);
            dataGridView.Columns[propName].Visible = false;
        }

        public static void CreateImageColumn(this DataGridView dataGridView, string propName, string headerText)
        {
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();

            imageColumn.Name = propName + "_Image";
            imageColumn.HeaderText = headerText;
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;

            dataGridView.Columns.Add(imageColumn);
            dataGridView.Columns[propName].Visible = false;
        }

        private static void ThreadingTimerCallback2(object state)
        {
            //string text = state.ToString();
            callbackForm.Invoke(new Action(() =>
            {
                callbackAction();
            }));
        }

        //public static void Debounce(this Form1 form, Action value, int v)
        //{
        //    callbackForm = form;
        //    callbackAction = value;

        //    if (debounceTimer == null)
        //    {
        //        debounceTimer = new System.Threading.Timer((state) =>
        //        {
        //            callbackForm.Invoke(new Action(() =>
        //            {
        //                callbackAction();

        //            }));
        //        }, null, 400, -1);

        //    }
        //    else
        //    {
        //        debounceTimer.Change(400, -1);

        //    }
        //}

        //private static void ThreadingTimerCallback2(object state)
        //{
        //    callbackForm.Invoke(new Action(() =>
        //    {
        //        callbackAction();
        //    }));
        //}
    }
}
