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
    class Products
    {
        public string name;
        public int price;

    }

    class MyForm : Form
    {



        public Products[] merc;

        public MyForm()
        {
            string[] items = File.ReadAllLines("Text1.csv");

            merc = new Products[items.Length];


            for (int i = 0; i < items.Length; i++)
            {
                string[] stock = items[i].Split(',');
                merc[i] = new Products { name = stock[0], price = int.Parse(stock[1]), };

            }

            Size = new Size(800, 600);


            TableLayoutPanel table = new TableLayoutPanel
            {
                ColumnCount = 4,
                RowCount = 4,
                Dock = DockStyle.Fill
            };
            Controls.Add(table);

            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            table.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            ListView listView1 = new ListView();
            {
                listView1.Height = 400;
                listView1.Width = 250;
            };
            table.Controls.Add(listView1, 0,1);

            ListView listview2 = new ListView();
            {
                listview2.Height = 400;
                listview2.Width = 250;
            }
            table.Controls.Add(listview2, 3, 1);

            TextBox rabatt = new TextBox();
            {
                rabatt.Height = 100;
                rabatt.Width = 300;
            }
            table.Controls.Add(rabatt, 0,3);

            //PictureBox box1 = new PictureBox();
            //{
            //    box1.Height = 250;
            //    box1.Width = 250;
            //}
            //table.Controls.Add(box1, , 3);

            //DataGrid MyGrid = new DataGrid();
            //{
            //    MyGrid.Width = 250;
            //    MyGrid.Height = 100;
            //}
            //Controls.Add(MyGrid);

            //ListView listView2 = new ListView();
            //{

            //}
            //table.Controls.Add(listView2);

        }
        private void CreatePicture(string path)
        {
            PictureBox box1 = new PictureBox
            {
                Image = Image.FromFile(path),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Width = 150,
                Height = 150
            };

        }

    }
}

