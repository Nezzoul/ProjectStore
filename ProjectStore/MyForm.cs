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

    class MyForm : Form
    {
        public ListBox listBox1;
        public ListBox listBox2;
        public Label ProductDescription;
        List<Product> products;
        List<Product> cartList;
        double sumOfAllProducts;
        TextBox CheckoutReceipts;
        PictureBox pictureBox1;

        public MyForm()
        {
            cartList = new List<Product>();
            TableLayoutPanel MainRootTable = new TableLayoutPanel
            {
                ColumnCount = 3,
                RowCount = 3,
                Dock = DockStyle.Fill
            };
            Controls.Add(MainRootTable);
            Size = new Size(800, 600);
            MainRootTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            MainRootTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            MainRootTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            MainRootTable.RowStyles.Add(new RowStyle(SizeType.Percent, 33));
            MainRootTable.RowStyles.Add(new RowStyle(SizeType.Percent, 33));
            MainRootTable.RowStyles.Add(new RowStyle(SizeType.Percent, 33));

            listBox1 = new ListBox
            {
                Dock = DockStyle.Fill,
            };
            MainRootTable.SetRowSpan(listBox1, 2);

            listBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;

            MainRootTable.Controls.Add(listBox1, 0, 0);

            listBox2 = new ListBox
            {
                Dock = DockStyle.Fill,
            };
            MainRootTable.SetRowSpan(listBox2, 2);
            MainRootTable.Controls.Add(listBox2, 2, 0);

            TableLayoutPanel TablePictureAndDescription = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 2,
                Dock = DockStyle.Fill
            };
            MainRootTable.Controls.Add(TablePictureAndDescription, 1, 0);

            TableLayoutPanel TableDiscount = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 2,
                Dock = DockStyle.Fill,
            };
            MainRootTable.Controls.Add(TableDiscount, 0, 2);

            TableLayoutPanel TableRemoveAddProductButtons = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 2,
                Dock = DockStyle.Fill,
            };
            MainRootTable.Controls.Add(TableRemoveAddProductButtons, 1, 2);

            TableLayoutPanel TableCheckoutButton = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 2,
                Dock = DockStyle.Fill,
            };
            MainRootTable.Controls.Add(TableCheckoutButton, 2, 2);

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
            MainRootTable.Controls.Add(ProductDescription, 1, 1);

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
            ButtAddProduct.Click += ButtAddProduct_click;

            Button SaveCart = new Button();
            {
                SaveCart.Text = "Save Cart";
            }
            TableRemoveAddProductButtons.Controls.Add(SaveCart, 0, 1);
            SaveCart.Click += SaveCart_click;

            Button LoadCart = new Button();
            {
                LoadCart.Text = "Load Cart";
            }
            TableRemoveAddProductButtons.Controls.Add(LoadCart, 1, 1);
            LoadCart.Click += LoadCart_click;

            Button ButtRemoveProduct = new Button();
            {
                ButtRemoveProduct.Text = "Remove product";

            }
            TableRemoveAddProductButtons.Controls.Add(ButtRemoveProduct, 1, 0);

            ButtRemoveProduct.Click += ButtRemoveProduct_click;

            Button ButtCheckout = new Button();
            {
                ButtCheckout.Text = "Checkout";

            }
            TableCheckoutButton.Controls.Add(ButtCheckout, 1, 1);

            ButtCheckout.Click += ButtCheckout_click;

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
            try

            {
                ProductDescription.Text = products[tmp.SelectedIndex].Description;

                pictureBox1.Load("Bilder/" + products[tmp.SelectedIndex].Image);
            }
            catch
            {
                MessageBox.Show("Select an item you are interested in!");

            }
        }

        private void ButtAddProduct_click(object sender, System.EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                int index = listBox1.SelectedIndex;
                listBox2.Items.Add(listBox1.SelectedItem);
                cartList.Add(products[index]);
                ProductDescription.Text = products[index].Description;
            }
        }

        private void ButtRemoveProduct_click(object sender, EventArgs e)
        {

            if (listBox2.SelectedItem != null)
            {
                try
                {
                    int index = listBox2.SelectedIndex;
                    listBox2.Items.RemoveAt(index);
                    cartList.RemoveAt(index);
                }
                catch { }
            }
        }
        private void SaveCart_click(object sender, EventArgs e)
        {
            string filename = @"C:\Windows\Temp\SavedCartsheet.csv";
            string listboxData = "";
            foreach (string str1 in listBox2.Items)
            {
                listboxData += str1 + Environment.NewLine;
            }
            File.WriteAllText(filename, listboxData);
        }
        private void LoadCart_click(object sender, EventArgs e)
        {
            string[] lines = File.ReadAllLines(@"C:\Windows\Temp\SavedCartsheet.csv");
            foreach (string line in lines)
            {
                string[] lineData = line.Split(',');
                var productFromLine = new Product()
                {
                    Name = lineData[0],
                    Price = double.Parse(lineData[1])
                };
                listBox2.Items.Add(productFromLine.Name + ", " + productFromLine.Price);
                cartList.Add(productFromLine);
            }
        }

        private IEnumerable<Product> MergProducts(IEnumerable<Product> products)
        {
            List<Product> result = new List<Product>();
            foreach (Product item in products)
            {
                var checkOutItem = result.FirstOrDefault(x => x.Name == item.Name);
                if (checkOutItem is null)
                {
                    result.Add(item);
                    item.amount = 1;
                }
                else
                {
                    checkOutItem.amount++;
                }
                sumOfAllProducts += item.Price;
            }
            return result;
        }
        
        private void ButtCheckout_click(object sender, EventArgs e)
        {
            StringBuilder str = new StringBuilder();
            var products = MergProducts(cartList);

            string[] line = File.ReadAllLines("rabatt.csv");
            foreach (string c in line)
            {
                if (CheckoutReceipts.Text.Contains(c))
                {
                    sumOfAllProducts = sumOfAllProducts * 0.75;
                    break;
                }
            }

            foreach (Product item in products)
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
                else if (input == "Growl")
                {
                    MessageBox.Show("denna koden är ogiltig");
                    break;
                }
            }
        }
    }
}