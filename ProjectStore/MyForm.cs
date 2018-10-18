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

       public MyForm()
        {

            TableLayoutPanel table = new TableLayoutPanel
            {
                ColumnCount = 4,
                RowCount = 4,
                Dock = DockStyle.Fill
            };
            Controls.Add(table);
            for (int i = 0; i < 4; i++)
            {
                table.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            }

            ListView listView1 = new ListView();
            {
                listView1.Height = 400;
                listView1.Width = 250;
            };
            table.Controls.Add(listView1, 0, 1);

            ListView listview2 = new ListView();
            {
                listview2.Height = 400;
                listview2.Width = 250;
            }
            table.Controls.Add(listview2, 4, 1);

            TextBox rabatt = new TextBox();
            {
                rabatt.Height = 100;
                rabatt.Width = 250;
            }

            table.Controls.Add(rabatt, 0, 4);

            Button butt1 = new Button();
            {
                butt1.Text = "Add product";
            }
            table.Controls.Add(butt1, 2, 2);

            Button butt2 = new Button();
            {
                butt2.Text = "remove product";
            }
            table.Controls.Add(butt2, 2, 3);

            Button butt3 = new Button();
            {
                butt3.Text = "Checkout";
            }
            table.Controls.Add(butt3, 3, 3);

            Label label1 = new Label();
            {
                label1.Text = "Discount code:";
            }
            table.Controls.Add(label1, 0, 3);

            RichTextBox box1 = new RichTextBox();
            {
                box1.Height = 150;
                box1.Width = 150;
            }

            table.Controls.Add(box1, 2, 1);

            PictureBox pics1 = new PictureBox();
            {
                pics1.Height = 250;
                pics1.Width = 250;
            }
            //table.Controls.Add(pics1, 2, 1);

        }

    }
}