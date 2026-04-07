using AccountingBook.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountingBook.Views
{
    /// <summary>
    /// SingletonForm
    /// 
    /// 功能：
    /// 1.確保「每一種 Form 只會建立一次」
    /// 2.負責管理目前顯示中的視窗
    /// 3.切換畫面時，自動 Hide 舊畫面、Show 新畫面
    /// 
    /// 這個類別本身不顯示畫面，只負責「管理畫面」
    /// </summary>
    internal class SingletonForm
    {

        /// <summary>
        /// 紀錄目前顯示中的 Form
        /// 用來在切換時 Hide 掉
        /// </summary>
        private static Form currentForm = null;

        /// <summary>
        /// 儲存所有已建立過的 Form
        /// Key   : formName (例如 "記一筆")
        /// Value : 對應的 Form instance
        /// </summary>
        private static Dictionary<string, Form> forms = new Dictionary<string, Form>();

        /// <summary>
        /// 取得指定名稱的 Form
        /// - 若尚未建立，動態 new 一個
        /// - 若已存在，直接拿來用
        /// - 自動處理 Hide / Show
        /// </summary>
        public static Form GetForm(string formName)
        {

            if (currentForm != null)
            {
                currentForm.Hide();
            }

            if (!forms.ContainsKey(formName))
            {
                string typeName = $"AccountingBook.Views.{formName}";
                Type formType = Type.GetType(typeName);
                Form form = (Form)Activator.CreateInstance(formType) as Form;
                form.FormClosing += Form_FormClosing;
                forms[formName] = form;
            }
            currentForm = forms[formName];
            Type currentFormType = currentForm.GetType();
            FieldInfo[] props = currentFormType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            FieldInfo navbarType = props.FirstOrDefault(x => x.FieldType == typeof(Navbar));

            Navbar navbar = navbarType.GetValue(currentForm) as Navbar;
            navbar.SetEnabled(formName);
            return currentForm;
        }

        private static void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
