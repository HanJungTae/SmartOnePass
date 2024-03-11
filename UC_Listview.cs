using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SmartOnePass
{
    public partial class UC_ListView : UserControl
    {
        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2, int cx, int cy);

        [DllImport("user32.dll")]
        private static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw);

        public List<string> mListColumns = new List<string>();
        ColumnHeader[] mColumnHeaders = null;
        Color m_headerBgColor;

        public UC_ListView()
        {
            InitializeComponent();

        }

        public void SetColumns(List<string> a_listColumns)
        {
            mListColumns = a_listColumns;
        }

        public void SetColumns(ColumnHeader[] a_columnHeaders)
        {
            mColumnHeaders = a_columnHeaders;

            if (mColumnHeaders != null)
            {
                ColumnHeader[] headers = new ColumnHeader[mListColumns.Count];
                int i = 0;
                foreach (ColumnHeader _header in mColumnHeaders)
                {
                    listView1.Columns.Add(_header);
                    i++;
                }
            }
        }

        public void SetTitle(string a_strTitle)
        {
            label1.Text = a_strTitle;
        }

        public void SetTitle(string a_strTitle, float a_fEmSize, System.Drawing.FontStyle a_style)
        {
            label1.Text = a_strTitle;
            label1.Font = new System.Drawing.Font("돋움", a_fEmSize, a_style, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
        }

        private void UC_ListView_Load(object sender, EventArgs e)
        {
            label1.BackColor = Color.Transparent;
            label1.Parent = this;
        }

        public void doListClear()
        {
            listView1.Items.Clear();
        }

        public void SetListData(List<ListViewItem> a_listViewItems)
        {
            listView1.Invoke(new MethodInvoker(delegate
            {
                foreach (ListViewItem _lvi in a_listViewItems)
                {
                    listView1.Items.Add(_lvi);
                }
                if(listView1.Items.Count > 0)
                    listView1.TopItem = listView1.Items[listView1.Items.Count - 1];
            }));

            /*
            foreach (ListViewItem _lvi in a_listViewItems)
            {
                listView1.Items.Add(_lvi);
            }
            listView1.TopItem = listView1.Items[listView1.Items.Count - 1];
            */
        }

        public void SetBackground(string a_strBgFileName)
        {
            this.BackgroundImage = System.Drawing.Image.FromFile(a_strBgFileName);
        }

        public void SetBackground(System.Drawing.Bitmap a_bitmap)
        {
            this.BackgroundImage = a_bitmap;
        }

        public void SetHeaderBackColor(Color a_rgb)
        {
            listView1.OwnerDraw = true;
            m_headerBgColor = a_rgb;
        }
        

        public void SetListDock()
        {
            listView1.Dock = DockStyle.Fill;
            this.Padding = new Padding(10);
            label1.Hide();
        }

        private void listView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            if (e.ColumnIndex == 0)
                return;

            using (StringFormat sf = new StringFormat())
            {

                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                e.DrawBackground();
                // Draw the header text.
                using (Font headerFont =
                            new Font("돋움", 11))
                {
                    //Color bgColor = Color.FromArgb(239, 249, 254);
                    e.Graphics.FillRectangle(new SolidBrush(m_headerBgColor), e.Bounds);
                    e.Graphics.DrawString(e.Header.Text, headerFont, Brushes.Black, e.Bounds, sf);

                    e.Graphics.DrawLine(new Pen(Color.FromArgb(210, 210, 210)), new Point(e.Bounds.X, 0), new Point(e.Bounds.X, e.Bounds.Height));  // Header 세로 라인
                    
                }
            }
            return;
        }


        private void listView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void listView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }
        

    }
}
