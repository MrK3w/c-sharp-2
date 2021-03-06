using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Forms
{
    public partial class MainForm : Form
    {
        
        public BindingList<Customer> Customers = new BindingList<Customer>()
        {
            new Customer
            {
                FirstName = "Pavel",
                Surname = "Nevlud",
                Age = 60,
                Id = 1
            },
            new Customer
            {
                FirstName = "Karel",
                Surname = "Novak",
                Age = 25,
                Id = 2
            }
        };

        public MainForm()
        {
            InitializeComponent();
            RoundButton btn = new RoundButton()
            {
                Left = 10,
                Top = 415,
                Width = 30,
                Height = 30
            };
            btn.MyButtonClick += AddButton_Click;
            Controls.Add(btn);
            GenerateGrid();
            UpdateGrid();
        }


        private void GenerateGrid()
        {
            GridView.AutoGenerateColumns = false;
            GridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Surname",
                DataPropertyName = nameof(Customer.Surname)
            });
            GridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "FirstName",
                DataPropertyName = nameof(Customer.FirstName)
            });
            GridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Age",
                DataPropertyName = nameof(Customer.Age)
            });
            GridView.Columns.Add(new DataGridViewButtonColumn()
            {
                Text = "Edit",
                UseColumnTextForButtonValue = true,
                Name = "Edit"
            });
            GridView.Columns.Add(new DataGridViewButtonColumn()
            {
                Text = "Delete",
                UseColumnTextForButtonValue = true,
                Name = "Delete"
            });
        }

        private void UpdateGrid()
        {
            //GridView.Rows.Clear();
          
          
           
            GridView.DataSource = Customers;

            //GridView.ColumnCount = 4;
            //GridView.Columns[0].HeaderText = "Id";
            //GridView.Columns[1].HeaderText = "FirstName";
            //GridView.Columns[2].HeaderText = "Surname";
            //GridView.Columns[3].HeaderText = "Age";
            //foreach (var customer in Customers)
            //{
            //    GridView.Rows.Add(customer.Id, customer.FirstName, customer.Surname, customer.Age);
            //}
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            Customers.Add(customer);
            EditForm form = new EditForm(customer);
            form.ShowDialog();
            UpdateGrid();
        }

        private void GridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
            {
                return;
            }

            DataGridView grid = (DataGridView) sender;
            var col = grid.Columns[e.ColumnIndex];
            if (!(col is DataGridViewButtonColumn))
            {
                return;
            }

            Customer customer = Customers[e.RowIndex];
            switch (col.Name)
            {
                case "Edit": 
                    EditForm dialog = new EditForm(customer);
                    dialog.ShowDialog();
                    break;
                case "Delete":
                    Customers.Remove(customer);
                    break;
            }
        }
    }
}
