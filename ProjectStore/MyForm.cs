using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace ProjectStore
{
    interface IProduct
    {
        string Name { get; set; }
        double Price { get; set; }
    }


    class Product
    {
        public string Name;
        public string Description;
        public double Price;
        public int amount;
    }


    class MyForm : Form
    {
        public ListBox listBox1;
        public ListBox listBox2;
      
        List<Product> products;
        List<Product> cartList = new List<Product>();
        double sumOfAllProducts;
        
        TextBox text1;

        public MyForm()
        {

            TableLayoutPanel table = new TableLayoutPanel
            {
                ColumnCount = 3,
                RowCount = 3,
                Dock = DockStyle.Fill
            };
            Controls.Add(table);
            Size = new Size(800, 600);
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 33));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 33));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 33));
            //table.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset;

            listBox1 = new ListBox
            {
                Dock = DockStyle.Fill,
            };
            table.SetRowSpan(listBox1, 2);
            table.Controls.Add(listBox1, 0, 0);

            listBox2 = new ListBox
            {
                Dock = DockStyle.Fill,
            };
            table.SetRowSpan(listBox2, 2);
            table.Controls.Add(listBox2, 2, 0);


            TableLayoutPanel table2 = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 2,
                Dock = DockStyle.Fill
            };
            table.Controls.Add(table2, 1, 0);



            TableLayoutPanel table3 = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 2,
                Dock = DockStyle.Fill,
            };
            table.Controls.Add(table3, 0, 2);

            TableLayoutPanel table4 = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 2,
                Dock = DockStyle.Fill,
            };
            table.Controls.Add(table4, 1, 2);

            TableLayoutPanel table5 = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 2,
                Dock = DockStyle.Fill,
            };
            table.Controls.Add(table5, 2, 2);


            PictureBox pictureBox1 = new PictureBox();
            {
                pictureBox1.Height = 200;
                pictureBox1.Width = 300;

                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            };
            table2.Controls.Add(pictureBox1, 0, 0);
            pictureBox1.ImageLocation = "cat1.jpg";


            Label Descript = new Label()
            {
                Text = "Neque porro quisquam est qui dolorem ipsum quia" +
                        "dolor sit amet, consectetur, adipisci velit...",
                Dock = DockStyle.Fill,

            };
            table.Controls.Add(Descript, 1, 1);

            text1 = new TextBox();
            {
                text1.Height = 100;
                text1.Width = 250;
                this.Controls.Add(text1);
                text1.KeyPress += new KeyPressEventHandler(Keypressed);
            }
            table3.Controls.Add(text1, 0, 1);

            Button Butt1 = new Button();
            {

                Butt1.Text = "Add product";
            }
            table4.Controls.Add(Butt1, 0, 0);


            Butt1.Click += Butt1_click;

            Button Butt2 = new Button();
            {
                Butt2.Text = "Remove product";

            }
            table4.Controls.Add(Butt2, 1, 0);

            Butt2.Click += Butt2_click;

            Button butt3 = new Button();
            {
                butt3.Text = "Checkout";

            }
            table5.Controls.Add(butt3, 1, 1);

            butt3.Click += Butt3_click;


            Label label1 = new Label();
            {
                label1.Text = "Discount Code: ";

            }
            table3.Controls.Add(label1, 0, 0);

            string[] lines = File.ReadAllLines("text1.csv");
            products = new List<Product>();
            foreach (string line in lines)
            {
                string[] values = line.Split(',');
                Product p = new Product
                {
                    Name = values[0],
                    Description = values[1],
                    Price = double.Parse(values[2])
                };
                // listBox1.Items.Add(p.Name + ", " + p.Description + ", " + p.Price);
                listBox1.Items.Add(p.Name + ", " + p.Price);
                

                products.Add(p);
            }

        }

        static double TotalPrice(IProduct[] products)
        {
            double totalPrice = 0;
            foreach (IProduct p in products)
            {
                totalPrice += p.Price;
            }
            return totalPrice;
        }
        private void Butt1_click(object sender, System.EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                int index = listBox1.SelectedIndex;
                listBox2.Items.Add(listBox1.SelectedItem);
                cartList.Add(products[index]);
            }
        }

        private void Butt2_click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                int index = listBox2.SelectedIndex;
                listBox2.Items.RemoveAt(index);
                cartList.RemoveAt(index);
            }
        }

        private void Butt3_click(object sender, EventArgs e)
        {

            StringBuilder str = new StringBuilder();
            List<Product> checkOut = new List<Product>();
            foreach (Product item in cartList)
            {
                if (!checkOut.Contains(item))
                {
                    checkOut.Add(item);
                    item.amount = 1;
                }
                else
                {
                    item.amount++;
                }
                sumOfAllProducts += item.Price;
            }

            string[] line = File.ReadAllLines("rabatt.csv");
            foreach (string c in line)
            {
                if (text1.Text.Contains(c))
                {
                    sumOfAllProducts = sumOfAllProducts * 0.75;
                    break;
                }
            }

            foreach (Product item in checkOut)
            {
                str.AppendLine(item.amount + "st, Namn: " + item.Name + ", Pris:" + item.Price);
            }
            MessageBox.Show(str.ToString() + "\nTotal kostnad: " + sumOfAllProducts);
            sumOfAllProducts = 0;
            listBox2.Items.Clear();
            cartList.Clear();
        }
        private void Keypressed(object o, KeyPressEventArgs e)
        {
            String input = text1.Text;
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
            }

            string[] line = File.ReadAllLines("rabatt.csv");
            foreach (string c in line)
            {
                if (input == c)
                {
                    MessageBox.Show("Din totala rabatt är: 25%");
                }
                else if (input == "julklapp")
                {
                    MessageBox.Show("denna koden är ogiltig");
                    break;
                }
            }
        }
    }
}