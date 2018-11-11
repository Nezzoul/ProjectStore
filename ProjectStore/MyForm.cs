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
        public string Image;
    }


    class MyForm : Form
    {
        public ListBox listBox1;
        public ListBox listBox2;
        public Label ProductDescription;
        List<Product> products;
        List<Product> cartList = new List<Product>();
        double sumOfAllProducts;
        TextBox CheckoutReceipts;
        PictureBox pictureBox1;


        public MyForm()
        {

            TableLayoutPanel MainTable = new TableLayoutPanel
            {
                ColumnCount = 3,
                RowCount = 3,
                Dock = DockStyle.Fill
            };
            Controls.Add(MainTable);
            Size = new Size(800, 600);
            MainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            MainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            MainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            MainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 33));
            MainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 33));
            MainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 33));

            listBox1 = new ListBox
            {
                Dock = DockStyle.Fill,
            };
            MainTable.SetRowSpan(listBox1, 2);

            listBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;

            MainTable.Controls.Add(listBox1, 0, 0);


            listBox2 = new ListBox
            {
                Dock = DockStyle.Fill,
            };
            MainTable.SetRowSpan(listBox2, 2);
            MainTable.Controls.Add(listBox2, 2, 0);


            TableLayoutPanel TablePictureAndDescription = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 2,
                Dock = DockStyle.Fill
            };
            MainTable.Controls.Add(TablePictureAndDescription, 1, 0);



            TableLayoutPanel TableDiscount = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 2,
                Dock = DockStyle.Fill,
            };
            MainTable.Controls.Add(TableDiscount, 0, 2);

            TableLayoutPanel TableRemoveAddProductButtons = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 2,
                Dock = DockStyle.Fill,
            };
            MainTable.Controls.Add(TableRemoveAddProductButtons, 1, 2);

            TableLayoutPanel TableCheckoutButton = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 2,
                Dock = DockStyle.Fill,
            };
            MainTable.Controls.Add(TableCheckoutButton, 2, 2);


            pictureBox1 = new PictureBox();
            {
                pictureBox1.Height = 200;
                pictureBox1.Width = 300;

                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            };
            TablePictureAndDescription.Controls.Add(pictureBox1, 0, 0);


            ProductDescription = new Label()
            {
                Text = "",
                Dock = DockStyle.Fill,

            };
            MainTable.Controls.Add(ProductDescription, 1, 1);

            CheckoutReceipts = new TextBox();
            {
                CheckoutReceipts.Height = 100;
                CheckoutReceipts.Width = 250;
                this.Controls.Add(CheckoutReceipts);
                CheckoutReceipts.KeyPress += new KeyPressEventHandler(Keypressed);
            }
            TableDiscount.Controls.Add(CheckoutReceipts, 0, 1);

            Button ButtAddProduct = new Button();
            {

                ButtAddProduct.Text = "Add product";
            }
            TableRemoveAddProductButtons.Controls.Add(ButtAddProduct, 0, 0);


            ButtAddProduct.Click += Butt1_click;

            Button ButtRemoveProduct = new Button();
            {
                ButtRemoveProduct.Text = "Remove product";

            }
            TableRemoveAddProductButtons.Controls.Add(ButtRemoveProduct, 1, 0);

            ButtRemoveProduct.Click += Butt2_click;

            Button ButtCheckout = new Button();
            {
                ButtCheckout.Text = "Checkout";

            }
            TableCheckoutButton.Controls.Add(ButtCheckout, 1, 1);

            ButtCheckout.Click += Butt3_click;


            Label label1 = new Label();
            {
                label1.Text = "Discount Code: ";

            }
            TableDiscount.Controls.Add(label1, 0, 0);

            string[] lines = File.ReadAllLines("PetSheet.csv");
            products = new List<Product>();
            foreach (string line in lines)
            {
                string[] values = line.Split(',');
                Product p = new Product
                {
                    Name = values[0],
                    Description = values[1],
                    Price = double.Parse(values[2]),
                    Image = values[3].Trim()

                };
                listBox1.Items.Add(p.Name + ", " + p.Price);

                products.Add(p);
            }

        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox tmp = (ListBox)sender;

            ProductDescription.Text = products[tmp.SelectedIndex].Description;
            //pictureBox1.ImageLocation = "Bilder/" + products[tmp.SelectedIndex].Image;
            pictureBox1.Load("Bilder/" + products[tmp.SelectedIndex].Image);
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
                ProductDescription.Text = products[index].Description;
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
                if (CheckoutReceipts.Text.Contains(c))
                {
                    sumOfAllProducts = sumOfAllProducts * 0.75;
                    break;
                }
            }

            foreach (Product item in checkOut)
            {
                str.AppendLine(item.amount + "st, Namn: " + item.Name + ", Pris:" + item.Price + ":-");
            }
            MessageBox.Show(str.ToString() + "\nTotal kostnad: " + sumOfAllProducts + ":-");
            sumOfAllProducts = 0;
            listBox2.Items.Clear();
            cartList.Clear();
        }
        private void Keypressed(object o, KeyPressEventArgs e)
        {
            String input = CheckoutReceipts.Text;
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