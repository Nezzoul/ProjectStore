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
    //interface IProduct
    //{
    //    string Name { get; set; }
    //    double Price { get; set; }
    //    void Enlarge(int steps);
    //    string Summary { get; }
    //}

    //class Computer : IProduct
    //{
    //    public string Name { get; set; }
    //    public string Model { get; set; }
    //    public double Price { get; set; }
    //    public double HardDrive { get; set; }

    //    public string Summary
    //    {
    //        get => "Laptop " + Name + " Model " + Model + " Price "
    //        + Price + " HardDrive " + HardDrive;
    //    }

    //    public void Enlarge(int steps)
    //    {
    //        HardDrive += 100 * steps;
    //    }
    //}

    class Product
    {
        public string Name;
        public string Description;
        public double Price;
    }


    class MyForm : Form
    {
        public ListBox listBox1;
        public ListBox listBox2;

        public MyForm()
        {

            TableLayoutPanel table = new TableLayoutPanel
            {
                ColumnCount = 3,
                RowCount = 3,
                Dock = DockStyle.Fill
            };
            Controls.Add(table);
            table.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset;

            TableLayoutPanel table2 = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 2,
                Dock = DockStyle.Fill
            };
            table.Controls.Add(table2, 1, 0);
            table2.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset;
            //===================================================

            listBox1 = new ListBox();
            {
                listBox1.Height = 400;
                listBox1.Width = 250;

            };
            table.Controls.Add(listBox1, 0, 0);

            //====================================================

            PictureBox pictureBox1 = new PictureBox();
            {
                pictureBox1.Height = 200;
                pictureBox1.Width = 300;
                Size = new Size(700, 550);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            };
            table2.Controls.Add(pictureBox1, 0, 0);
            pictureBox1.ImageLocation = "cat1.jpg";

            //listBox2 = new ListBox();
            //{
            //    listBox2.Height = 400;
            //    listBox2.Width = 250;
            //}
            //table.Controls.Add(listBox2, 4, 1);

            //TextBox Discount = new TextBox();
            //{
            //    Discount.Height = 100;
            //    Discount.Width = 250;
            //}

            //table.Controls.Add(Discount, 0, 4);

            TextBox Descript = new TextBox()
            {
                Multiline = true,
                Text = "Neque porro quisquam est qui dolorem ipsum quia " +
                        "dolor sit amet, consectetur, adipisci velit...",
                        Dock = DockStyle.Fill
            };
            table2.Controls.Add(Descript, 0, 1);



            //#region Buttons

            //Button Butt1 = new Button();
            //{
            //    Butt1.Text = "Add product";
            //}
            //table.Controls.Add(Butt1, 2, 2);

            //Butt1.Click += Butt1_click;

            //Button Butt2 = new Button();
            //{
            //    Butt2.Text = "remove product";
            //}
            //table.Controls.Add(Butt2, 2, 3);

            //Butt2.Click += Butt2_click;

            //Button butt3 = new Button();
            //{
            //    butt3.Text = "Checkout";
            //}
            //table.Controls.Add(butt3, 3, 3);

            //butt3.Click += Butt3_click;

            //Label label1 = new Label();
            //{
            //    label1.Text = "Discount code:";
            //}
            //table.Controls.Add(label1, 0, 3);
            //#endregion








            string[] lines = File.ReadAllLines("text1.csv");
            List<Product> products = new List<Product> { };
            foreach (string line in lines)
            {
                string[] values = line.Split(',');
                Product p = new Product
                {
                    Name = values[0],
                    Description = values[1],
                    Price = double.Parse(values[2])
                };
                listBox1.Items.Add(p.Name + ", " + p.Description + ", " + p.Price);
            }

        }

        //static double TotalPrice(IProduct[] products)
        //{
        //    double totalPrice = 0;
        //    foreach (var p in products)
        //    {
        //        totalPrice += p.Price;
        //    }
        //    return totalPrice;
        //}
        private void Butt1_click(object sender, System.EventArgs e)
        {
            listBox2.Items.Add(listBox1.SelectedItem);
        }

        private void Butt2_click(object sender, EventArgs e)
        {
            listBox2.Items.Remove(listBox2.SelectedItem);
        }

        private void Butt3_click(object sender, EventArgs e)
        {
            StringBuilder str = new StringBuilder();
            foreach (object selecteditem in listBox2.Items)
            {
                str.AppendLine(selecteditem.ToString());
            }
            MessageBox.Show("Din totala order innehåller:  " + str);
        }
    }
}