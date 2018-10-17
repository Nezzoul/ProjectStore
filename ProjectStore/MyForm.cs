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

            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

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
                rabatt.Width = 250;
            }
            table.Controls.Add(rabatt, 0,3);

            Button butt1 = new Button();
            {
                Text = "Add";
            }
        }   

    }
}

