using AccountingBook.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountingBook.Components
{
    public partial class Navbar : UserControl
    {
        public Navbar()
        {
            InitializeComponent();
            var types = Assembly.GetExecutingAssembly().DefinedTypes;
            var formTypes = types.Where(x => x.BaseType == (typeof(Form))).ToList();


            foreach (var type in formTypes)
            {
                Button btn = new Button();
                btn.Text = type.Name;
                btn.Width = formButtonContainer.Width / formTypes.Count;
                btn.Height = formButtonContainer.Height;
                btn.Margin = new Padding(0);
                btn.Click += NavButton_Click;
                formButtonContainer.Controls.Add(btn);
            }
        }
        //HW: 透過POS點餐機中的Assembly 去根據目前所擁有的Form動態生成Button到畫面上
        private void NavButton_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;
            if (btn == null)
                return;


            SingletonForm.GetForm(btn.Text).Show();

        }
        public void SetEnabled(string formName)
        {


            Button button = formButtonContainer.Controls.OfType<Button>().First(x => x.Text == formName);
            button.Enabled = false;
        }

        private void Navbar_SizeChanged(object sender, EventArgs e)
        {
            var buttons = formButtonContainer.Controls.OfType<Button>().ToList();
            if (buttons.Count == 0)
                return;
            int containerWidth = formButtonContainer.Width / buttons.Count;

            //int newContainer = containerWidth - 10;
            foreach (Button btn in buttons)
            {

                btn.Width = containerWidth;
                btn.Height = formButtonContainer.Height;
            }
        }
    }
}
