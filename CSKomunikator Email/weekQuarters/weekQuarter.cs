using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace weekQuarters
{
    public partial class weekQuarter : UserControl
    {
        public weekQuarter()
        {
            InitializeComponent();
            InitializeComponentHere();
        }
        public bool[,,] getQuarters()
        {
            return quarters;
        }
        public void setQuarters(bool[,,] tempQuarters)
        {
            quarters = tempQuarters;
            for (int x = 0; x < 7; x++) for (int y = 0; y < 24; y++) for (int z = 0; z < 4; z++) quartersOriginal[x, y, z] = quarters[x, y, z];
        }
        public bool[,,] getMarkedQuarters()
        {
            return markedQuarters;
        }
        public void setMarkedQuarters(bool[,,] tempQuarters)
        {
            markedQuarters = tempQuarters;
        }
        private int Max(params float[] value)
        {
            return (int)value.Max(i => Math.Ceiling(i));
        }
        Font generalFont;
        int marginSize, headerSpacing, leftSpacing, cellWidth, cellHeight;
        SizeF monSize, tueSize, wedSize, thuSize, friSize, satSize, sunSize;
        private void weekQuarter_MouseUp(object sender, MouseEventArgs e)
        {
            int eX = eXinsideTable(e.X);
            int eY = eYinsideTable(e.Y);
            int whereX = (eX - (leftSpacing + marginSize)) / (cellWidth);
            int whereY = (eY - (marginSize + headerSpacing)) / cellHeight;
            if (e.Button == MouseButtons.Left && (origX != lastX || origY != lastY) && (e.X > leftSpacing + marginSize) && (e.X < marginSize + leftSpacing + 4 * 7 * cellWidth) && (e.Y > marginSize + headerSpacing) && (e.Y < marginSize + headerSpacing + 24 * cellHeight))
            {
                markSelectionOnQuarters(origX, origY, whereX, whereY);
                drawTable();
            }
            else if (e.Button == MouseButtons.Right && (markedOrigX != markedLastX || markedOrigY != markedLastY) && (e.X > leftSpacing + marginSize) && (e.X < marginSize + leftSpacing + 4 * 7 * cellWidth) && (e.Y > marginSize + headerSpacing) && (e.Y < marginSize + headerSpacing + 24 * cellHeight))
            {
                markSelectionOnMarkedQuarters(markedOrigX, markedOrigY, whereX, whereY);
                drawTable();
            }
            else
            {
                drawTable();
            }
        }
        int lastX, lastY, origX, origY, markedLastX, markedLastY, markedOrigX, markedOrigY;

        private void weekQuarter_Paint_1(object sender, PaintEventArgs e)
        {
            drawTable();
        }

        int eXinsideTable(int eX)
        {
            if (eX <= leftSpacing + marginSize) return leftSpacing + marginSize;
            if (eX >= marginSize + leftSpacing + 4 * 7 * cellWidth - 1) return marginSize + leftSpacing + 4 * 7 * cellWidth - 1;
            return eX;
        }

        private void weekQuarter_Resize(object sender, EventArgs e)
        {
            InitializeComponentHere();
            Graphics g = Graphics.FromHwnd(Handle);
            g.Clear(Color.White);
            drawTable();
        }

        int eYinsideTable(int eY)
        {
            if (eY <= marginSize + headerSpacing) return marginSize + headerSpacing;
            if (eY >= marginSize + headerSpacing + 24 * cellHeight - 1) return marginSize + headerSpacing + 24 * cellHeight - 1;
            return eY;
        }
        bool origValue, markedOrigValue;

        private void weekQuarter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Home) {
                for (int x = 0; x < 7; x++) for (int y = 0; y < 24; y++) for (int z = 0; z < 4; z++) quarters[x, y, z]= quartersOriginal[x, y, z];
                drawTable();
            }
        }

        private void weekQuarter_MouseDown(object sender, MouseEventArgs e)
        {
            int eX = eXinsideTable(e.X);
            int eY = eYinsideTable(e.Y);
            if (e.Button == MouseButtons.Left)
            {
                origX = lastX = (eX - (leftSpacing + marginSize)) / (cellWidth);
                origY = lastY = (eY - (marginSize + headerSpacing)) / cellHeight;
                origValue = quarters[origX / 4, origY, origX % 4];
            }
            else if (e.Button == MouseButtons.Right)
            {
                markedOrigX = markedLastX = (eX - (leftSpacing + marginSize)) / (cellWidth);
                markedOrigY = markedLastY = (eY - (marginSize + headerSpacing)) / cellHeight;
                markedOrigValue = markedQuarters[markedOrigX / 4, markedOrigY, markedOrigX % 4];
            }
        }
        void markSelectionOnQuarters(int origX, int origY, int whereX, int whereY)
        {
            int fromX, toX, fromY, toY;
            if (origX < whereX)
            {
                fromX = origX;
                toX = whereX;
            }
            else
            {
                fromX = whereX;
                toX = origX;
            }
            if (origY < whereY)
            {
                fromY = origY;
                toY = whereY;
            }
            else
            {
                fromY = whereY;
                toY = origY;
            }
            for (int x = fromX; x <= toX; x++) for (int y = fromY; y <= toY; y++) quarters[x / 4, y, x % 4] = !origValue;
        }
        void markSelectionOnMarkedQuarters(int origX, int origY, int whereX, int whereY)
        {
            int fromX, toX, fromY, toY;
            if (origX < whereX)
            {
                fromX = origX;
                toX = whereX;
            }
            else
            {
                fromX = whereX;
                toX = origX;
            }
            if (origY < whereY)
            {
                fromY = origY;
                toY = whereY;
            }
            else
            {
                fromY = whereY;
                toY = origY;
            }
            for (int x = fromX; x <= toX; x++) for (int y = fromY; y <= toY; y++) markedQuarters[x / 4, y, x % 4] = !markedOrigValue;
        }
        bool[,,] tempQuarters = new bool[7, 24, 4];
        private void weekQuarter_MouseMove(object sender, MouseEventArgs e)
        {
            int eX = eXinsideTable(e.X);
            int eY = eYinsideTable(e.Y);
            int whereX = (eX - (leftSpacing + marginSize)) / (cellWidth);
            int whereY = (eY - (marginSize + headerSpacing)) / cellHeight;
            if (e.Button == MouseButtons.Left && (lastX != whereX || lastY != whereY) && (e.X > leftSpacing + marginSize) && (e.X < marginSize + leftSpacing + 4 * 7 * cellWidth) && (e.Y > marginSize + headerSpacing) && (e.Y < marginSize + headerSpacing + 24 * cellHeight))
            {
                lastX = whereX;
                lastY = whereY;
                bool[,,] lastQuarters = quarters;
                for (int x = 0; x < 7; x++) for (int y = 0; y < 24; y++) for (int z = 0; z < 4; z++) tempQuarters[x, y, z] = quarters[x, y, z];
                quarters = tempQuarters;
                markSelectionOnQuarters(origX, origY, whereX, whereY);
                drawTable();
                quarters = lastQuarters;
            }
            else if (e.Button == MouseButtons.Right && (markedLastX != whereX || markedLastY != whereY) && (e.X > leftSpacing + marginSize) && (e.X < marginSize + leftSpacing + 4 * 7 * cellWidth) && (e.Y > marginSize + headerSpacing) && (e.Y < marginSize + headerSpacing + 24 * cellHeight))
            {
                markedLastX = whereX;
                markedLastY = whereY;
                bool[,,] lastQuarters = markedQuarters;
                for (int x = 0; x < 7; x++) for (int y = 0; y < 24; y++) for (int z = 0; z < 4; z++) tempQuarters[x, y, z] = markedQuarters[x, y, z];
                markedQuarters = tempQuarters;
                markSelectionOnMarkedQuarters(markedOrigX, markedOrigY, whereX, whereY);
                drawTable();
                markedQuarters = lastQuarters;
            }
        }
        void recalculateDrawingSizes() {
            Graphics g = Graphics.FromHwnd(Handle);
            generalFont = new Font("Sergoe Script", ClientSize.Height / 45, FontStyle.Regular);
            monSize = g.MeasureString("Mon", generalFont);
            tueSize = g.MeasureString("Tue", generalFont);
            wedSize = g.MeasureString("Wed", generalFont);
            thuSize = g.MeasureString("Thu", generalFont);
            friSize = g.MeasureString("Fri", generalFont);
            satSize = g.MeasureString("Sat", generalFont);
            sunSize = g.MeasureString("Sun", generalFont);
            SizeF lefSize = g.MeasureString("00:00", generalFont);
            headerSpacing = Max(sunSize.Height, monSize.Height, tueSize.Height, wedSize.Height, thuSize.Height, friSize.Height, satSize.Height) + 5;
            leftSpacing = (int)Math.Ceiling(lefSize.Width) + 5;
            cellWidth = (ClientSize.Width - marginSize * 2 - leftSpacing) / (7 * 4);
            cellHeight = (ClientSize.Height - marginSize * 2 - headerSpacing) / 24;
            g.Dispose();
        }
        void InitializeComponentHere()
        {
            marginSize = 2;
            recalculateDrawingSizes();
        }
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);

        private const int WM_SETREDRAW = 11;

        public void SuspendDrawing(Control parent)
        {
            SendMessage(parent.Handle, WM_SETREDRAW, false, 0);
        }

        public void ResumeDrawing(Control parent)
        {
            SendMessage(parent.Handle, WM_SETREDRAW, true, 0);
        }
        void drawTable()
        {
            Graphics g = Graphics.FromHwnd(Handle);
            int xStart, yStart;
            xStart = marginSize;
            yStart = marginSize + headerSpacing;
            for (int y = 0; y < 24; y++)
            {
                string quarterText = (y.ToString("D2") + ":00").ToString();
                SizeF quarterSize = g.MeasureString(quarterText, generalFont);
                g.DrawString(quarterText, generalFont, Brushes.Black, xStart + ((leftSpacing - (int)quarterSize.Width) / 2), yStart);
                xStart += leftSpacing;
                for (int x = 0; x < 7; x++)
                {
                    for (int z = 0; z < 4; z++)
                    {
                        if (quarters[x, y, z] == false)
                        {
                            if (y % 2 == 0) g.FillRectangle(new SolidBrush(Color.FromArgb(234, 234, 234)), xStart, yStart, cellWidth, cellHeight);
                            else g.FillRectangle(new SolidBrush(Color.FromArgb(200, 200, 200)), xStart, yStart, cellWidth, cellHeight);
                        }
                        else
                        {
                            g.FillRectangle(new SolidBrush(Color.FromArgb(200, 200, 50)), xStart, yStart, cellWidth, cellHeight);
                        }
                        if (markedQuarters[x, y, z] == true) g.FillRectangle(new SolidBrush(Color.FromArgb(250, 50, 50)), xStart, yStart, cellWidth, cellHeight);
                        g.DrawRectangle(new Pen(Color.FromArgb(100, 100, 100)), xStart, yStart, cellWidth, cellHeight);
                        xStart += cellWidth;
                    }
                    g.DrawRectangle(new Pen(Color.FromArgb(0, 0, 0)), xStart - cellWidth * 4, yStart, cellWidth * 4, cellHeight);
                }
                xStart = marginSize;
                yStart += cellHeight;
            }
            yStart = marginSize;
            xStart = leftSpacing + marginSize + ((cellWidth - (int)sunSize.Width) / 2);
            g.DrawString("Mon", generalFont, Brushes.Black, xStart, yStart);
            xStart = leftSpacing + marginSize + ((cellWidth - (int)monSize.Width) / 2) + cellWidth * 4;
            g.DrawString("Tue", generalFont, Brushes.Black, xStart, yStart);
            xStart = leftSpacing + marginSize + ((cellWidth - (int)tueSize.Width) / 2) + cellWidth * 2 * 4;
            g.DrawString("Wed", generalFont, Brushes.Black, xStart, yStart);
            xStart = leftSpacing + marginSize + ((cellWidth - (int)wedSize.Width) / 2) + cellWidth * 3 * 4;
            g.DrawString("Thu", generalFont, Brushes.Black, xStart, yStart);
            xStart = leftSpacing + marginSize + ((cellWidth - (int)thuSize.Width) / 2) + cellWidth * 4 * 4;
            g.DrawString("Fri", generalFont, Brushes.Black, xStart, yStart);
            xStart = leftSpacing + marginSize + ((cellWidth - (int)friSize.Width) / 2) + cellWidth * 5 * 4;
            g.DrawString("Sat", generalFont, Brushes.Black, xStart, yStart);
            xStart = leftSpacing + marginSize + ((cellWidth - (int)satSize.Width) / 2) + cellWidth * 6 * 4;
            g.DrawString("Sun", generalFont, Brushes.Black, xStart, yStart);
            g.Dispose();
        }
        void clearAllMarkedQuarters()
        {
            for (int x = 0; x < 7; x++) for (int y = 0; y < 24; y++) for (int z = 0; z < 4; z++) markedQuarters[x, y, z] = false;
            drawTable();
        }
        void clearAllQuarters()
        {
            for (int x = 0; x < 7; x++) for (int y = 0; y < 24; y++) for (int z = 0; z < 4; z++) quarters[x, y, z] = false;
            drawTable();
        }
        bool[,,] quarters = new bool[7, 24, 4];
        bool[,,] quartersOriginal = new bool[7, 24, 4];
        bool[,,] markedQuarters = new bool[7, 24, 4];
        private void weekQuarter_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && (e.X > leftSpacing + marginSize) && (e.X < marginSize + leftSpacing + 4 * 7 * cellWidth) && (e.Y > marginSize + headerSpacing) && (e.Y < marginSize + headerSpacing + 24 * cellHeight))
            {
                if (origX != lastX || origY != lastY) return;
                int whereX = (e.X - (leftSpacing + marginSize)) / (cellWidth);
                int whereY = (e.Y - (marginSize + headerSpacing)) / cellHeight;
                if (quarters[whereX / 4, whereY, whereX % 4] == false) quarters[whereX / 4, whereY, whereX % 4] = true;
                else quarters[whereX / 4, whereY, whereX % 4] = false;
                drawTable();
            }
            else if (e.Button == MouseButtons.Left)
            {
                clearAllQuarters();
            }
            if (e.Button == MouseButtons.Right && (e.X > leftSpacing + marginSize) && (e.X < marginSize + leftSpacing + 4 * 7 * cellWidth) && (e.Y > marginSize + headerSpacing) && (e.Y < marginSize + headerSpacing + 24 * cellHeight))
            {
                if (markedOrigX != markedLastX || markedOrigY != markedLastY) return;
                int whereX = (e.X - (leftSpacing + marginSize)) / (cellWidth);
                int whereY = (e.Y - (marginSize + headerSpacing)) / cellHeight;
                if (markedQuarters[whereX / 4, whereY, whereX % 4] == false) markedQuarters[whereX / 4, whereY, whereX % 4] = true;
                else markedQuarters[whereX / 4, whereY, whereX % 4] = false;
                drawTable();
            }
            else if (e.Button == MouseButtons.Right)
            {
                clearAllMarkedQuarters();
            }
        }
    }
}