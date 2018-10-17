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


            TableLayoutPanel table = new TableLayoutPanel
            {

                ColumnCount = 3,
                RowCount = 1,
                Dock = DockStyle.Fill
            };
            Controls.Add(table);

            ListView listView1 = new ListView();
            {
                listView1.Width = 300;
               listView1.Columns.Add("Name", 100, HorizontalAlignment.Left);
                listView1.Columns.Add("Price", 100, HorizontalAlignment.Left);

                listView1.View = View.Details;
            };
            //ListViewItem listViewItem = new ListViewItem(merc[i]);
            List<string> lst = new List<string>();
            lst.Add("John");
            lst.Add("Smith");
            lst.Add("Cait");
            lst.Add("Irene");
            lst.Add("Ben");
            lst.Add("Deniel");
            listView1.View = View.Details;

            foreach (string pl in lst)
            {
                listView1.Items.Add(pl);

            }


            foreach (string pl in lst)
            {
                listView1.Items.Add(pl);

            }
            table.Controls.Add(listView1);


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

