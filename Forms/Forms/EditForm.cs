using System;
using System.Windows.Forms;

namespace Forms
{
    public partial class EditForm : Form
    {
        public EditForm(Customer customer)
        {
            InitializeComponent();
            NameTextBox.DataBindings.Add(new Binding(nameof(TextBox.Text), customer, 
                nameof(Customer.FirstName),true,DataSourceUpdateMode.OnPropertyChanged));
            SurnameTextBox.DataBindings.Add(new Binding(nameof(TextBox.Text), customer,
                nameof(Customer.Surname), true, DataSourceUpdateMode.OnPropertyChanged));
        }

        private void SaveBtn(object sender, EventArgs e)
        {
            Close();
        }
    }
}
