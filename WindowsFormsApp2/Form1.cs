using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Diagnostics;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        int cozum = 0;
        string FilePath = "";
        string Loglar1 = "";
        string Loglar2 = "";
        string Loglar3 = "";
        ArrayList AryKayit1 = new ArrayList();
        ArrayList AryKayit2 = new ArrayList();
        ArrayList AryKayit3 = new ArrayList();
        Thread Th1;
        Thread Th2;
        Thread Th3;
        Stopwatch stopwatch1 = new Stopwatch();
        Stopwatch stopwatch2 = new Stopwatch();
        Stopwatch stopwatch3 = new Stopwatch();

        public Form1()
        {
            InitializeComponent();
            this.Text = "Sudoku";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Build();

        }

        public void Build()
        {
            DataGrid.AllowUserToAddRows = false;
            DataGrid.AllowUserToDeleteRows = false;
            DataGrid.AllowUserToResizeColumns = false;
            DataGrid.AllowUserToResizeRows = false;
            DataGrid.RowHeadersVisible = false;
            DataGrid.ColumnHeadersVisible = false;
            DataGrid.ScrollBars = ScrollBars.None;
            DataGrid.GridColor = Color.White;
            DataGrid.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            DataGrid.AlternatingRowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGrid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGrid.ColumnCount = 9;
            DataGrid.Rows.Add(9);
            DataGrid.MultiSelect = false;

            for (int i = 0; i < 9; i++)
            {
                DataGridViewColumn column = DataGrid.Columns[i];
                column.Width = (int)(DataGrid.Width / 9f);
                DataGridViewRow row = DataGrid.Rows[i];
                row.Height = (int)(DataGrid.Height / 9f);
            }
            DataGrid.Width = DataGrid.Columns[1].Width * 9;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    DataGrid.Rows[i].Cells[j].Style.BackColor = Color.DeepPink;
                }

            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 3; j < 6; j++)
                {
                    DataGrid.Rows[i].Cells[j].Style.BackColor = Color.DeepSkyBlue;
                    DataGrid.Rows[j].Cells[i].Style.BackColor = Color.DeepSkyBlue;
                }

            }

            for (int i = 3; i < 6; i++)
            {
                for (int j = 3; j < 6; j++)
                {
                    DataGrid.Rows[i].Cells[j].Style.BackColor = Color.DeepPink;
                    
                }
            }
        }

        private void open_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(DosyaSec);
            th.ApartmentState = ApartmentState.STA;
            th.Start();
        }

        private void DosyaSec()
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Filter = "Metin Belgesi |*.txt";
            if (OFD.ShowDialog() != null)
                FilePath = OFD.FileName;
            string[] lines = System.IO.File.ReadAllLines(FilePath);
            int RowCountFromPath = 0;
            int CellCountFromPath = 0;
            foreach (string line in lines)
            {
                string Satir = line;
                foreach (char c in Satir)
                {
                    if (c == '*')
                    {
                        DataGridViewRow row = DataGrid.Rows[RowCountFromPath];
                        DataGridViewCell cell = row.Cells[CellCountFromPath];
                        cell.Value = null;
                    }
                    else if (c == ' ')
                    { CellCountFromPath--; }
                    else
                    {
                        DataGridViewRow row = DataGrid.Rows[RowCountFromPath];
                        DataGridViewCell cell = row.Cells[CellCountFromPath];
                        cell.Value = c.ToString();
                    }
                    CellCountFromPath++;
                    if (CellCountFromPath == 9)
                    {
                        CellCountFromPath = 0;
                        RowCountFromPath++;
                    }
                    if (RowCountFromPath == 9)
                        break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Th1 = new Thread(DenetleyiciVeCozumleyici1);
            Th2 = new Thread(DenetleyiciVeCozumleyici2);
            Th3 = new Thread(DenetleyiciVeCozumleyici3);

            stopwatch1.Reset();
            stopwatch1.Start();
            Th1.Start();

            stopwatch2.Reset();
            stopwatch2.Start();
            Th2.Start();

            stopwatch3.Reset();
            stopwatch3.Start();
            Th3.Start();

            Thread.Sleep(1000);

            if (TimeSpan.Compare(stopwatch1.Elapsed, stopwatch2.Elapsed) == -1 && TimeSpan.Compare(stopwatch1.Elapsed, stopwatch3.Elapsed) == -1)
            {
                cozum =1;
            }
            else if (TimeSpan.Compare(stopwatch2.Elapsed, stopwatch1.Elapsed) == -1 && TimeSpan.Compare(stopwatch2.Elapsed, stopwatch3.Elapsed) == -1)
            {
                cozum = 2;
            }
            else if (TimeSpan.Compare(stopwatch3.Elapsed, stopwatch2.Elapsed) == -1 && TimeSpan.Compare(stopwatch3.Elapsed, stopwatch1.Elapsed) == -1)
            {
                cozum = 3;
            }

                if (cozum == 1)
            {
                listBox1.DataSource = null;
                listBox1.Items.Clear();
                listBox1.DataSource = AryKayit1;
                LblThread.Text = "Thread 1";
                Form2 fm2 = new Form2();
                fm2.AryListe = AryKayit2;
                fm2.Text = "Thread 2";
                fm2.Show();
                Form2 fm3 = new Form2();
                fm3.AryListe = AryKayit3;
                fm3.Text = "Thread 3";
                fm3.Show();
            }
            else if (cozum == 2)
            {
                listBox1.DataSource = null;
                listBox1.Items.Clear();
                listBox1.DataSource = AryKayit2;
                LblThread.Text = "Thread 2";
                Form2 fm2 = new Form2();
                fm2.AryListe = AryKayit1;
                fm2.Text = "Thread 1";
                fm2.Show();
                Form2 fm3 = new Form2();
                fm3.AryListe = AryKayit3;
                fm3.Text = "Thread 3";
                fm3.Show();
            }
            else if (cozum == 3)
            {
                listBox1.DataSource = null;
                listBox1.Items.Clear();
                listBox1.DataSource = AryKayit3;
                LblThread.Text = "Thread 3";
                Form2 fm2 = new Form2();
                fm2.AryListe = AryKayit2;
                fm2.Text = "Thread 2";
                fm2.Show();
                Form2 fm3 = new Form2();
                fm3.AryListe = AryKayit1;
                fm3.Text = "Thread 1";
                fm3.Show();
            }

            SaveLog();
        }

        private void AbortThread(int th)
        {
            stopwatch1.Stop();
            stopwatch2.Stop();
            stopwatch3.Stop();

            switch (th)
            {
                case 1:
                    Th2.Abort();
                    Th3.Abort();
                    break;
                case 2:
                   
                    Th1.Abort();
                    Th3.Abort();
                    break;

                case 3:
                    Th2.Abort();
                    Th1.Abort();
                    break;
            }
            Invoke(new Action(delegate
            {
                LblSure1.Text = "Süre 1 :" + stopwatch1.Elapsed;
                LblSure2.Text = "Süre 2 :" + stopwatch2.Elapsed;
                LblSure3.Text = "Süre 3 :" + stopwatch3.Elapsed;
            }));

        }

        private void DenetleyiciVeCozumleyici1()
        {
            ArrayList AryOlasilik = new ArrayList();
            ArrayList AryBulunanlar = new ArrayList();
            for (int a = 0; a <= 80; a++)
            {
                AryOlasilik.Clear();
                int RowCount = 0;
                int CellCount = 0;
                ArrayList Rakamlar = new ArrayList();
                for (int main = 0; main <= 80; main++)
                {
                    string olasilik = "";
                    DataGridViewRow row = DataGrid.Rows[RowCount];
                    DataGridViewCell cell = row.Cells[CellCount];

                    if (cell.Value == null)
                    {

                        #region BOLMELER
                        #region SOL BOLMELER
                        if (CellCount == 0 || CellCount == 1 || CellCount == 2)
                        {
                            if (RowCount == 0 || RowCount == 1 || RowCount == 2)
                            {
                                int RowBolmeCount = 0;
                                int CellBolmeCount = 0;
                                DataGridViewRow RowBolme = DataGrid.Rows[RowBolmeCount];
                                DataGridViewCell CellBolme = RowBolme.Cells[CellBolmeCount];
                                for (int i = 0; i <= 8; i++)
                                {
                                    CellBolme = RowBolme.Cells[CellBolmeCount];
                                    if (CellBolme.Value != null)
                                        Rakamlar.Add(CellBolme.Value.ToString());
                                    CellBolmeCount++;

                                    if (i == 2 || i == 5)
                                    {
                                        RowBolmeCount++;
                                        RowBolme = DataGrid.Rows[RowBolmeCount];
                                        CellBolmeCount = 0;
                                    }
                                }
                            }
                            else if (RowCount == 3 || RowCount == 4 || RowCount == 5)
                            {
                                int RowBolmeCount = 3;
                                int CellBolmeCount = 0;
                                DataGridViewRow RowBolme = DataGrid.Rows[RowBolmeCount];
                                DataGridViewCell CellBolme = RowBolme.Cells[CellBolmeCount];
                                for (int i = 0; i <= 8; i++)
                                {
                                    CellBolme = RowBolme.Cells[CellBolmeCount];
                                    if (CellBolme.Value != null)
                                        Rakamlar.Add(CellBolme.Value.ToString());
                                    CellBolmeCount++;

                                    if (i == 2 || i == 5)
                                    {
                                        RowBolmeCount++;
                                        RowBolme = DataGrid.Rows[RowBolmeCount];
                                        CellBolmeCount = 0;
                                    }
                                }
                            }
                            else if (RowCount == 6 || RowCount == 7 || RowCount == 8)
                            {
                                int RowBolmeCount = 6;
                                int CellBolmeCount = 0;
                                DataGridViewRow RowBolme = DataGrid.Rows[RowBolmeCount];
                                DataGridViewCell CellBolme = RowBolme.Cells[CellBolmeCount];
                                for (int i = 0; i <= 8; i++)
                                {
                                    CellBolme = RowBolme.Cells[CellBolmeCount];
                                    if (CellBolme.Value != null)
                                        Rakamlar.Add(CellBolme.Value.ToString());
                                    CellBolmeCount++;

                                    if (i == 2 || i == 5)
                                    {
                                        RowBolmeCount++;
                                        RowBolme = DataGrid.Rows[RowBolmeCount];
                                        CellBolmeCount = 0;
                                    }
                                }
                            }
                        }
                        #endregion
                        #region ORTA BOLMELER
                        else if (CellCount == 3 || CellCount == 4 || CellCount == 5)
                        {
                            if (RowCount == 0 || RowCount == 1 || RowCount == 2)
                            {
                                int RowBolmeCount = 0;
                                int CellBolmeCount = 3;
                                DataGridViewRow RowBolme = DataGrid.Rows[RowBolmeCount];
                                DataGridViewCell CellBolme = RowBolme.Cells[CellBolmeCount];
                                for (int i = 0; i <= 8; i++)
                                {
                                    CellBolme = RowBolme.Cells[CellBolmeCount];
                                    if (CellBolme.Value != null)
                                        Rakamlar.Add(CellBolme.Value.ToString());
                                    CellBolmeCount++;

                                    if (i == 2 || i == 5)
                                    {
                                        RowBolmeCount++;
                                        RowBolme = DataGrid.Rows[RowBolmeCount];
                                        CellBolmeCount = 3;
                                    }
                                }
                            }
                            else if (RowCount == 3 || RowCount == 4 || RowCount == 5)
                            {
                                int RowBolmeCount = 3;
                                int CellBolmeCount = 3;
                                DataGridViewRow RowBolme = DataGrid.Rows[RowBolmeCount];
                                DataGridViewCell CellBolme = RowBolme.Cells[CellBolmeCount];
                                for (int i = 0; i <= 8; i++)
                                {
                                    CellBolme = RowBolme.Cells[CellBolmeCount];
                                    if (CellBolme.Value != null)
                                        Rakamlar.Add(CellBolme.Value.ToString());
                                    CellBolmeCount++;

                                    if (i == 2 || i == 5)
                                    {
                                        RowBolmeCount++;
                                        RowBolme = DataGrid.Rows[RowBolmeCount];
                                        CellBolmeCount = 3;
                                    }
                                }
                            }
                            else if (RowCount == 6 || RowCount == 7 || RowCount == 8)
                            {
                                int RowBolmeCount = 6;
                                int CellBolmeCount = 3;
                                DataGridViewRow RowBolme = DataGrid.Rows[RowBolmeCount];
                                DataGridViewCell CellBolme = RowBolme.Cells[CellBolmeCount];
                                for (int i = 0; i <= 8; i++)
                                {
                                    CellBolme = RowBolme.Cells[CellBolmeCount];
                                    if (CellBolme.Value != null)
                                        Rakamlar.Add(CellBolme.Value.ToString());
                                    CellBolmeCount++;

                                    if (i == 2 || i == 5)
                                    {
                                        RowBolmeCount++;
                                        RowBolme = DataGrid.Rows[RowBolmeCount];
                                        CellBolmeCount = 3;
                                    }
                                }
                            }
                        }
                        #endregion
                        #region SAG BOLMELER
                        else if (CellCount == 6 || CellCount == 7 || CellCount == 8)
                        {
                            if (RowCount == 0 || RowCount == 1 || RowCount == 2)
                            {
                                int RowBolmeCount = 0;
                                int CellBolmeCount = 6;
                                DataGridViewRow RowBolme = DataGrid.Rows[RowBolmeCount];
                                DataGridViewCell CellBolme = RowBolme.Cells[CellBolmeCount];
                                for (int i = 0; i <= 8; i++)
                                {
                                    CellBolme = RowBolme.Cells[CellBolmeCount];
                                    if (CellBolme.Value != null)
                                        Rakamlar.Add(CellBolme.Value.ToString());
                                    CellBolmeCount++;

                                    if (i == 2 || i == 5)
                                    {
                                        RowBolmeCount++;
                                        RowBolme = DataGrid.Rows[RowBolmeCount];
                                        CellBolmeCount = 6;
                                    }
                                }
                            }
                            else if (RowCount == 3 || RowCount == 4 || RowCount == 5)
                            {
                                int RowBolmeCount = 3;
                                int CellBolmeCount = 6;
                                DataGridViewRow RowBolme = DataGrid.Rows[RowBolmeCount];
                                DataGridViewCell CellBolme = RowBolme.Cells[CellBolmeCount];
                                for (int i = 0; i <= 8; i++)
                                {
                                    CellBolme = RowBolme.Cells[CellBolmeCount];
                                    if (CellBolme.Value != null)
                                        Rakamlar.Add(CellBolme.Value.ToString());
                                    CellBolmeCount++;

                                    if (i == 2 || i == 5)
                                    {
                                        RowBolmeCount++;
                                        RowBolme = DataGrid.Rows[RowBolmeCount];
                                        CellBolmeCount = 6;
                                    }
                                }
                            }
                            else if (RowCount == 6 || RowCount == 7 || RowCount == 8)
                            {
                                int RowBolmeCount = 6;
                                int CellBolmeCount = 6;
                                DataGridViewRow RowBolme = DataGrid.Rows[RowBolmeCount];
                                DataGridViewCell CellBolme = RowBolme.Cells[CellBolmeCount];
                                for (int i = 0; i <= 8; i++)
                                {
                                    CellBolme = RowBolme.Cells[CellBolmeCount];
                                    if (CellBolme.Value != null)
                                        Rakamlar.Add(CellBolme.Value.ToString());
                                    CellBolmeCount++;

                                    if (i == 2 || i == 5)
                                    {
                                        RowBolmeCount++;
                                        RowBolme = DataGrid.Rows[RowBolmeCount];
                                        CellBolmeCount = 6;
                                    }
                                }
                            }
                        }
                        #endregion
                        #endregion

                        #region Sol
                        for (int i = CellCount - 1; i > -1; i--)
                        {
                            DataGridViewCell cell2 = row.Cells[i];
                            if (cell2.Value != null)
                            {
                                Rakamlar.Add(cell2.Value.ToString());
                            }
                        }
                        #endregion

                        #region Sağ
                        for (int i = CellCount + 1; i <= 8; i++)
                        {
                            DataGridViewCell cell2 = row.Cells[i];
                            if (cell2.Value != null)
                            {
                                Rakamlar.Add(cell2.Value.ToString());
                            }
                        }
                        #endregion

                        #region Alt
                        for (int i = RowCount + 1; i <= 8; i++)
                        {
                            DataGridViewRow row2 = DataGrid.Rows[i];
                            DataGridViewCell cell2 = row2.Cells[CellCount];
                            if (cell2.Value != null)
                            {
                                Rakamlar.Add(cell2.Value.ToString());
                            }
                        }
                        #endregion

                        #region Üst
                        for (int i = RowCount - 1; i > -1; i--)
                        {
                            DataGridViewRow row2 = DataGrid.Rows[i];
                            DataGridViewCell cell2 = row2.Cells[CellCount];
                            if (cell2.Value != null)
                            {
                                Rakamlar.Add(cell2.Value.ToString());
                            }
                        }
                        #endregion


                        for (int i = 1; i <= 9; i++)
                        {
                            if (!Rakamlar.Contains(i.ToString()))
                            {
                                olasilik += i + "-";
                            }
                        }
                        Rakamlar.Clear();
                    }
                    else olasilik = "DOLU";
                    AryOlasilik.Add(olasilik);
                    CellCount++;
                    if (CellCount == 9)
                    {
                        CellCount = 0;
                        RowCount++;
                    }
                    if (RowCount == 9)
                    {
                        break;
                    }
                }

                #region Çözümleyici
                int kutucuk = 0, count = 0;
                int enkucuk = 20;

                foreach (string sayılar in AryOlasilik)
                {
                    if (sayılar.Length < enkucuk)
                    {
                        if (!AryBulunanlar.Contains(count))
                        {
                            enkucuk = sayılar.Length;
                            kutucuk = count;
                        }
                    }
                    count++;
                }

                int RowCount1 = 0;
                int CellCount1 = 0;
                DataGridViewRow row1 = DataGrid.Rows[RowCount1];
                DataGridViewCell cell1 = row1.Cells[CellCount1];
                for (int main = 0; main <= kutucuk; main++)
                {
                    row1 = DataGrid.Rows[RowCount1];
                    cell1 = row1.Cells[CellCount1];

                    CellCount1++;
                    if (CellCount1 == 9)
                    {
                        CellCount1 = 0;
                        RowCount1++;
                    }
                    if (RowCount1 == 9)
                    {
                        break;
                    }
                }

                ArrayList AryRakamlar = new ArrayList();
                string olasilik1 = AryOlasilik[kutucuk].ToString();
                foreach (char i in olasilik1)
                {
                    if (i != '-')
                        AryRakamlar.Add(i.ToString());
                }

                for (int i = 1; i <= 9; i++)
                {
                    if (AryRakamlar.Contains(i.ToString()))
                    {
                        try
                        {
                            AddLog1("Thread 1 - " + (row1.Index+1) + ". Satır, " + (cell1.ColumnIndex+1) + ". Sütun " + i + " olarak değiştirildi.");
                            AryKayit1.Add((row1.Index+1) + ". Satır, " + (cell1.ColumnIndex+1) + ". Sütun " + i + " olarak değiştirildi.");
                            cell1.Value = i;
                            break;
                        }
                        catch { }
                    }
                }
                #endregion
            }
            AbortThread(1);
        }

        private void DenetleyiciVeCozumleyici2()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            ArrayList AryOlasilik = new ArrayList();
            ArrayList AryBulunanlar = new ArrayList();
            for (int a = 0; a <= 80; a++)
            {
                AryOlasilik.Clear();
                int RowCount = 0;
                int CellCount = 0;
                ArrayList Rakamlar = new ArrayList();
                for (int main = 0; main <= 80; main++)
                {
                    string olasilik = "";
                    DataGridViewRow row = DataGrid.Rows[RowCount];
                    DataGridViewCell cell = row.Cells[CellCount];

                    if (cell.Value == null)
                    {

                        #region BOLMELER
                        #region SOL BOLMELER
                        if (CellCount == 0 || CellCount == 1 || CellCount == 2)
                        {
                            if (RowCount == 0 || RowCount == 1 || RowCount == 2)
                            {
                                int RowBolmeCount = 0;
                                int CellBolmeCount = 0;
                                DataGridViewRow RowBolme = DataGrid.Rows[RowBolmeCount];
                                DataGridViewCell CellBolme = RowBolme.Cells[CellBolmeCount];
                                for (int i = 0; i <= 8; i++)
                                {
                                    CellBolme = RowBolme.Cells[CellBolmeCount];
                                    if (CellBolme.Value != null)
                                        Rakamlar.Add(CellBolme.Value.ToString());
                                    CellBolmeCount++;

                                    if (i == 2 || i == 5)
                                    {
                                        RowBolmeCount++;
                                        RowBolme = DataGrid.Rows[RowBolmeCount];
                                        CellBolmeCount = 0;
                                    }
                                }
                            }
                            else if (RowCount == 3 || RowCount == 4 || RowCount == 5)
                            {
                                int RowBolmeCount = 3;
                                int CellBolmeCount = 0;
                                DataGridViewRow RowBolme = DataGrid.Rows[RowBolmeCount];
                                DataGridViewCell CellBolme = RowBolme.Cells[CellBolmeCount];
                                for (int i = 0; i <= 8; i++)
                                {
                                    CellBolme = RowBolme.Cells[CellBolmeCount];
                                    if (CellBolme.Value != null)
                                        Rakamlar.Add(CellBolme.Value.ToString());
                                    CellBolmeCount++;

                                    if (i == 2 || i == 5)
                                    {
                                        RowBolmeCount++;
                                        RowBolme = DataGrid.Rows[RowBolmeCount];
                                        CellBolmeCount = 0;
                                    }
                                }
                            }
                            else if (RowCount == 6 || RowCount == 7 || RowCount == 8)
                            {
                                int RowBolmeCount = 6;
                                int CellBolmeCount = 0;
                                DataGridViewRow RowBolme = DataGrid.Rows[RowBolmeCount];
                                DataGridViewCell CellBolme = RowBolme.Cells[CellBolmeCount];
                                for (int i = 0; i <= 8; i++)
                                {
                                    CellBolme = RowBolme.Cells[CellBolmeCount];
                                    if (CellBolme.Value != null)
                                        Rakamlar.Add(CellBolme.Value.ToString());
                                    CellBolmeCount++;

                                    if (i == 2 || i == 5)
                                    {
                                        RowBolmeCount++;
                                        RowBolme = DataGrid.Rows[RowBolmeCount];
                                        CellBolmeCount = 0;
                                    }
                                }
                            }
                        }
                        #endregion
                        #region ORTA BOLMELER
                        else if (CellCount == 3 || CellCount == 4 || CellCount == 5)
                        {
                            if (RowCount == 0 || RowCount == 1 || RowCount == 2)
                            {
                                int RowBolmeCount = 0;
                                int CellBolmeCount = 3;
                                DataGridViewRow RowBolme = DataGrid.Rows[RowBolmeCount];
                                DataGridViewCell CellBolme = RowBolme.Cells[CellBolmeCount];
                                for (int i = 0; i <= 8; i++)
                                {
                                    CellBolme = RowBolme.Cells[CellBolmeCount];
                                    if (CellBolme.Value != null)
                                        Rakamlar.Add(CellBolme.Value.ToString());
                                    CellBolmeCount++;

                                    if (i == 2 || i == 5)
                                    {
                                        RowBolmeCount++;
                                        RowBolme = DataGrid.Rows[RowBolmeCount];
                                        CellBolmeCount = 3;
                                    }
                                }
                            }
                            else if (RowCount == 3 || RowCount == 4 || RowCount == 5)
                            {
                                int RowBolmeCount = 3;
                                int CellBolmeCount = 3;
                                DataGridViewRow RowBolme = DataGrid.Rows[RowBolmeCount];
                                DataGridViewCell CellBolme = RowBolme.Cells[CellBolmeCount];
                                for (int i = 0; i <= 8; i++)
                                {
                                    CellBolme = RowBolme.Cells[CellBolmeCount];
                                    if (CellBolme.Value != null)
                                        Rakamlar.Add(CellBolme.Value.ToString());
                                    CellBolmeCount++;

                                    if (i == 2 || i == 5)
                                    {
                                        RowBolmeCount++;
                                        RowBolme = DataGrid.Rows[RowBolmeCount];
                                        CellBolmeCount = 3;
                                    }
                                }
                            }
                            else if (RowCount == 6 || RowCount == 7 || RowCount == 8)
                            {
                                int RowBolmeCount = 6;
                                int CellBolmeCount = 3;
                                DataGridViewRow RowBolme = DataGrid.Rows[RowBolmeCount];
                                DataGridViewCell CellBolme = RowBolme.Cells[CellBolmeCount];
                                for (int i = 0; i <= 8; i++)
                                {
                                    CellBolme = RowBolme.Cells[CellBolmeCount];
                                    if (CellBolme.Value != null)
                                        Rakamlar.Add(CellBolme.Value.ToString());
                                    CellBolmeCount++;

                                    if (i == 2 || i == 5)
                                    {
                                        RowBolmeCount++;
                                        RowBolme = DataGrid.Rows[RowBolmeCount];
                                        CellBolmeCount = 3;
                                    }
                                }
                            }
                        }
                        #endregion
                        #region SAG BOLMELER
                        else if (CellCount == 6 || CellCount == 7 || CellCount == 8)
                        {
                            if (RowCount == 0 || RowCount == 1 || RowCount == 2)
                            {
                                int RowBolmeCount = 0;
                                int CellBolmeCount = 6;
                                DataGridViewRow RowBolme = DataGrid.Rows[RowBolmeCount];
                                DataGridViewCell CellBolme = RowBolme.Cells[CellBolmeCount];
                                for (int i = 0; i <= 8; i++)
                                {
                                    CellBolme = RowBolme.Cells[CellBolmeCount];
                                    if (CellBolme.Value != null)
                                        Rakamlar.Add(CellBolme.Value.ToString());
                                    CellBolmeCount++;

                                    if (i == 2 || i == 5)
                                    {
                                        RowBolmeCount++;
                                        RowBolme = DataGrid.Rows[RowBolmeCount];
                                        CellBolmeCount = 6;
                                    }
                                }
                            }
                            else if (RowCount == 3 || RowCount == 4 || RowCount == 5)
                            {
                                int RowBolmeCount = 3;
                                int CellBolmeCount = 6;
                                DataGridViewRow RowBolme = DataGrid.Rows[RowBolmeCount];
                                DataGridViewCell CellBolme = RowBolme.Cells[CellBolmeCount];
                                for (int i = 0; i <= 8; i++)
                                {
                                    CellBolme = RowBolme.Cells[CellBolmeCount];
                                    if (CellBolme.Value != null)
                                        Rakamlar.Add(CellBolme.Value.ToString());
                                    CellBolmeCount++;

                                    if (i == 2 || i == 5)
                                    {
                                        RowBolmeCount++;
                                        RowBolme = DataGrid.Rows[RowBolmeCount];
                                        CellBolmeCount = 6;
                                    }
                                }
                            }
                            else if (RowCount == 6 || RowCount == 7 || RowCount == 8)
                            {
                                int RowBolmeCount = 6;
                                int CellBolmeCount = 6;
                                DataGridViewRow RowBolme = DataGrid.Rows[RowBolmeCount];
                                DataGridViewCell CellBolme = RowBolme.Cells[CellBolmeCount];
                                for (int i = 0; i <= 8; i++)
                                {
                                    CellBolme = RowBolme.Cells[CellBolmeCount];
                                    if (CellBolme.Value != null)
                                        Rakamlar.Add(CellBolme.Value.ToString());
                                    CellBolmeCount++;

                                    if (i == 2 || i == 5)
                                    {
                                        RowBolmeCount++;
                                        RowBolme = DataGrid.Rows[RowBolmeCount];
                                        CellBolmeCount = 6;
                                    }
                                }
                            }
                        }
                        #endregion
                        #endregion

                        #region Sol
                        for (int i = CellCount - 1; i > -1; i--)
                        {
                            DataGridViewCell cell2 = row.Cells[i];
                            if (cell2.Value != null)
                            {
                                Rakamlar.Add(cell2.Value.ToString());
                            }
                        }
                        #endregion

                        #region Sağ
                        for (int i = CellCount + 1; i <= 8; i++)
                        {
                            DataGridViewCell cell2 = row.Cells[i];
                            if (cell2.Value != null)
                            {
                                Rakamlar.Add(cell2.Value.ToString());
                            }
                        }
                        #endregion

                        #region Alt
                        for (int i = RowCount + 1; i <= 8; i++)
                        {
                            DataGridViewRow row2 = DataGrid.Rows[i];
                            DataGridViewCell cell2 = row2.Cells[CellCount];
                            if (cell2.Value != null)
                            {
                                Rakamlar.Add(cell2.Value.ToString());
                            }
                        }
                        #endregion

                        #region Üst
                        for (int i = RowCount - 1; i > -1; i--)
                        {
                            DataGridViewRow row2 = DataGrid.Rows[i];
                            DataGridViewCell cell2 = row2.Cells[CellCount];
                            if (cell2.Value != null)
                            {
                                Rakamlar.Add(cell2.Value.ToString());
                            }
                        }
                        #endregion


                        for (int i = 1; i <= 9; i++)
                        {
                            if (!Rakamlar.Contains(i.ToString()))
                            {
                                olasilik += i + "-";
                            }
                        }
                        Rakamlar.Clear();
                    }
                    else olasilik = "DOLU";
                    AryOlasilik.Add(olasilik);
                    CellCount++;
                    if (CellCount == 9)
                    {
                        CellCount = 0;
                        RowCount++;
                    }
                    if (RowCount == 9)
                    {
                        break;
                    }
                }

                #region Çözümleyici
                int kutucuk = 0, count = 0;
                int enkucuk = 20;

                foreach (string sayılar in AryOlasilik)
                {
                    if (sayılar.Length < enkucuk)
                    {
                        if (!AryBulunanlar.Contains(count))
                        {
                            enkucuk = sayılar.Length;
                            kutucuk = count;
                        }
                    }
                    count++;
                }

                int RowCount1 = 0;
                int CellCount1 = 0;
                DataGridViewRow row1 = DataGrid.Rows[RowCount1];
                DataGridViewCell cell1 = row1.Cells[CellCount1];
                for (int main = 0; main <= kutucuk; main++)
                {
                    row1 = DataGrid.Rows[RowCount1];
                    cell1 = row1.Cells[CellCount1];

                    CellCount1++;
                    if (CellCount1 == 9)
                    {
                        CellCount1 = 0;
                        RowCount1++;
                    }
                    if (RowCount1 == 9)
                    {
                        break;
                    }
                }

                ArrayList AryRakamlar = new ArrayList();
                string olasilik1 = AryOlasilik[kutucuk].ToString();
                foreach (char i in olasilik1)
                {
                    if (i != '-')
                        AryRakamlar.Add(i.ToString());
                }

                for (int i = 1; i <= 9; i++)
                {
                    if (AryRakamlar.Contains(i.ToString()))
                    {
                        try
                        {
                            AddLog2("Thread 2 - " + (row1.Index+1) + ". Satır, " + (cell1.ColumnIndex+1) + ". Sütun " + i + " olarak değiştirildi.");
                            AryKayit2.Add((row1.Index+1) + ". Satır, " + (cell1.ColumnIndex+1) + ". Sütun " + i + " olarak değiştirildi.");

                            cell1.Value = i;
                            break;
                        }
                        catch { }
                    }
                }
                #endregion
            }
            AbortThread(2);
        }

        private void DenetleyiciVeCozumleyici3()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            ArrayList AryOlasilik = new ArrayList();
            ArrayList AryBulunanlar = new ArrayList();
            for (int a = 0; a <= 80; a++)
            {
                AryOlasilik.Clear();
                int RowCount = 0;
                int CellCount = 0;
                ArrayList Rakamlar = new ArrayList();
                for (int main = 0; main <= 80; main++)
                {
                    string olasilik = "";
                    DataGridViewRow row = DataGrid.Rows[RowCount];
                    DataGridViewCell cell = row.Cells[CellCount];

                    if (cell.Value == null)
                    {

                        #region BOLMELER
                        #region SOL BOLMELER
                        if (CellCount == 0 || CellCount == 1 || CellCount == 2)
                        {
                            if (RowCount == 0 || RowCount == 1 || RowCount == 2)
                            {
                                int RowBolmeCount = 0;
                                int CellBolmeCount = 0;
                                DataGridViewRow RowBolme = DataGrid.Rows[RowBolmeCount];
                                DataGridViewCell CellBolme = RowBolme.Cells[CellBolmeCount];
                                for (int i = 0; i <= 8; i++)
                                {
                                    CellBolme = RowBolme.Cells[CellBolmeCount];
                                    if (CellBolme.Value != null)
                                        Rakamlar.Add(CellBolme.Value.ToString());
                                    CellBolmeCount++;

                                    if (i == 2 || i == 5)
                                    {
                                        RowBolmeCount++;
                                        RowBolme = DataGrid.Rows[RowBolmeCount];
                                        CellBolmeCount = 0;
                                    }
                                }
                            }
                            else if (RowCount == 3 || RowCount == 4 || RowCount == 5)
                            {
                                int RowBolmeCount = 3;
                                int CellBolmeCount = 0;
                                DataGridViewRow RowBolme = DataGrid.Rows[RowBolmeCount];
                                DataGridViewCell CellBolme = RowBolme.Cells[CellBolmeCount];
                                for (int i = 0; i <= 8; i++)
                                {
                                    CellBolme = RowBolme.Cells[CellBolmeCount];
                                    if (CellBolme.Value != null)
                                        Rakamlar.Add(CellBolme.Value.ToString());
                                    CellBolmeCount++;

                                    if (i == 2 || i == 5)
                                    {
                                        RowBolmeCount++;
                                        RowBolme = DataGrid.Rows[RowBolmeCount];
                                        CellBolmeCount = 0;
                                    }
                                }
                            }
                            else if (RowCount == 6 || RowCount == 7 || RowCount == 8)
                            {
                                int RowBolmeCount = 6;
                                int CellBolmeCount = 0;
                                DataGridViewRow RowBolme = DataGrid.Rows[RowBolmeCount];
                                DataGridViewCell CellBolme = RowBolme.Cells[CellBolmeCount];
                                for (int i = 0; i <= 8; i++)
                                {
                                    CellBolme = RowBolme.Cells[CellBolmeCount];
                                    if (CellBolme.Value != null)
                                        Rakamlar.Add(CellBolme.Value.ToString());
                                    CellBolmeCount++;

                                    if (i == 2 || i == 5)
                                    {
                                        RowBolmeCount++;
                                        RowBolme = DataGrid.Rows[RowBolmeCount];
                                        CellBolmeCount = 0;
                                    }
                                }
                            }
                        }
                        #endregion
                        #region ORTA BOLMELER
                        else if (CellCount == 3 || CellCount == 4 || CellCount == 5)
                        {
                            if (RowCount == 0 || RowCount == 1 || RowCount == 2)
                            {
                                int RowBolmeCount = 0;
                                int CellBolmeCount = 3;
                                DataGridViewRow RowBolme = DataGrid.Rows[RowBolmeCount];
                                DataGridViewCell CellBolme = RowBolme.Cells[CellBolmeCount];
                                for (int i = 0; i <= 8; i++)
                                {
                                    CellBolme = RowBolme.Cells[CellBolmeCount];
                                    if (CellBolme.Value != null)
                                        Rakamlar.Add(CellBolme.Value.ToString());
                                    CellBolmeCount++;

                                    if (i == 2 || i == 5)
                                    {
                                        RowBolmeCount++;
                                        RowBolme = DataGrid.Rows[RowBolmeCount];
                                        CellBolmeCount = 3;
                                    }
                                }
                            }
                            else if (RowCount == 3 || RowCount == 4 || RowCount == 5)
                            {
                                int RowBolmeCount = 3;
                                int CellBolmeCount = 3;
                                DataGridViewRow RowBolme = DataGrid.Rows[RowBolmeCount];
                                DataGridViewCell CellBolme = RowBolme.Cells[CellBolmeCount];
                                for (int i = 0; i <= 8; i++)
                                {
                                    CellBolme = RowBolme.Cells[CellBolmeCount];
                                    if (CellBolme.Value != null)
                                        Rakamlar.Add(CellBolme.Value.ToString());
                                    CellBolmeCount++;

                                    if (i == 2 || i == 5)
                                    {
                                        RowBolmeCount++;
                                        RowBolme = DataGrid.Rows[RowBolmeCount];
                                        CellBolmeCount = 3;
                                    }
                                }
                            }
                            else if (RowCount == 6 || RowCount == 7 || RowCount == 8)
                            {
                                int RowBolmeCount = 6;
                                int CellBolmeCount = 3;
                                DataGridViewRow RowBolme = DataGrid.Rows[RowBolmeCount];
                                DataGridViewCell CellBolme = RowBolme.Cells[CellBolmeCount];
                                for (int i = 0; i <= 8; i++)
                                {
                                    CellBolme = RowBolme.Cells[CellBolmeCount];
                                    if (CellBolme.Value != null)
                                        Rakamlar.Add(CellBolme.Value.ToString());
                                    CellBolmeCount++;

                                    if (i == 2 || i == 5)
                                    {
                                        RowBolmeCount++;
                                        RowBolme = DataGrid.Rows[RowBolmeCount];
                                        CellBolmeCount = 3;
                                    }
                                }
                            }
                        }
                        #endregion
                        #region SAG BOLMELER
                        else if (CellCount == 6 || CellCount == 7 || CellCount == 8)
                        {
                            if (RowCount == 0 || RowCount == 1 || RowCount == 2)
                            {
                                int RowBolmeCount = 0;
                                int CellBolmeCount = 6;
                                DataGridViewRow RowBolme = DataGrid.Rows[RowBolmeCount];
                                DataGridViewCell CellBolme = RowBolme.Cells[CellBolmeCount];
                                for (int i = 0; i <= 8; i++)
                                {
                                    CellBolme = RowBolme.Cells[CellBolmeCount];
                                    if (CellBolme.Value != null)
                                        Rakamlar.Add(CellBolme.Value.ToString());
                                    CellBolmeCount++;

                                    if (i == 2 || i == 5)
                                    {
                                        RowBolmeCount++;
                                        RowBolme = DataGrid.Rows[RowBolmeCount];
                                        CellBolmeCount = 6;
                                    }
                                }
                            }
                            else if (RowCount == 3 || RowCount == 4 || RowCount == 5)
                            {
                                int RowBolmeCount = 3;
                                int CellBolmeCount = 6;
                                DataGridViewRow RowBolme = DataGrid.Rows[RowBolmeCount];
                                DataGridViewCell CellBolme = RowBolme.Cells[CellBolmeCount];
                                for (int i = 0; i <= 8; i++)
                                {
                                    CellBolme = RowBolme.Cells[CellBolmeCount];
                                    if (CellBolme.Value != null)
                                        Rakamlar.Add(CellBolme.Value.ToString());
                                    CellBolmeCount++;

                                    if (i == 2 || i == 5)
                                    {
                                        RowBolmeCount++;
                                        RowBolme = DataGrid.Rows[RowBolmeCount];
                                        CellBolmeCount = 6;
                                    }
                                }
                            }
                            else if (RowCount == 6 || RowCount == 7 || RowCount == 8)
                            {
                                int RowBolmeCount = 6;
                                int CellBolmeCount = 6;
                                DataGridViewRow RowBolme = DataGrid.Rows[RowBolmeCount];
                                DataGridViewCell CellBolme = RowBolme.Cells[CellBolmeCount];
                                for (int i = 0; i <= 8; i++)
                                {
                                    CellBolme = RowBolme.Cells[CellBolmeCount];
                                    if (CellBolme.Value != null)
                                        Rakamlar.Add(CellBolme.Value.ToString());
                                    CellBolmeCount++;

                                    if (i == 2 || i == 5)
                                    {
                                        RowBolmeCount++;
                                        RowBolme = DataGrid.Rows[RowBolmeCount];
                                        CellBolmeCount = 6;
                                    }
                                }
                            }
                        }
                        #endregion
                        #endregion

                        #region Sol
                        for (int i = CellCount - 1; i > -1; i--)
                        {
                            DataGridViewCell cell2 = row.Cells[i];
                            if (cell2.Value != null)
                            {
                                Rakamlar.Add(cell2.Value.ToString());
                            }
                        }
                        #endregion

                        #region Sağ
                        for (int i = CellCount + 1; i <= 8; i++)
                        {
                            DataGridViewCell cell2 = row.Cells[i];
                            if (cell2.Value != null)
                            {
                                Rakamlar.Add(cell2.Value.ToString());
                            }
                        }
                        #endregion

                        #region Alt
                        for (int i = RowCount + 1; i <= 8; i++)
                        {
                            DataGridViewRow row2 = DataGrid.Rows[i];
                            DataGridViewCell cell2 = row2.Cells[CellCount];
                            if (cell2.Value != null)
                            {
                                Rakamlar.Add(cell2.Value.ToString());
                            }
                        }
                        #endregion

                        #region Üst
                        for (int i = RowCount - 1; i > -1; i--)
                        {
                            DataGridViewRow row2 = DataGrid.Rows[i];
                            DataGridViewCell cell2 = row2.Cells[CellCount];
                            if (cell2.Value != null)
                            {
                                Rakamlar.Add(cell2.Value.ToString());
                            }
                        }
                        #endregion


                        for (int i = 1; i <= 9; i++)
                        {
                            if (!Rakamlar.Contains(i.ToString()))
                            {
                                olasilik += i + "-";
                            }
                        }
                        Rakamlar.Clear();
                    }
                    else olasilik = "DOLU";
                    AryOlasilik.Add(olasilik);
                    CellCount++;
                    if (CellCount == 9)
                    {
                        CellCount = 0;
                        RowCount++;
                    }
                    if (RowCount == 9)
                    {
                        break;
                    }
                }

                #region Çözümleyici
                int kutucuk = 0, count = 0;
                int enkucuk = 20;

                foreach (string sayılar in AryOlasilik)
                {
                    if (sayılar.Length < enkucuk)
                    {
                        if (!AryBulunanlar.Contains(count))
                        {
                            enkucuk = sayılar.Length;
                            kutucuk = count;
                        }
                    }
                    count++;
                }

                int RowCount1 = 0;
                int CellCount1 = 0;
                DataGridViewRow row1 = DataGrid.Rows[RowCount1];
                DataGridViewCell cell1 = row1.Cells[CellCount1];
                for (int main = 0; main <= kutucuk; main++)
                {
                    row1 = DataGrid.Rows[RowCount1];
                    cell1 = row1.Cells[CellCount1];

                    CellCount1++;
                    if (CellCount1 == 9)
                    {
                        CellCount1 = 0;
                        RowCount1++;
                    }
                    if (RowCount1 == 9)
                    {
                        break;
                    }
                }

                ArrayList AryRakamlar = new ArrayList();
                string olasilik1 = AryOlasilik[kutucuk].ToString();
                foreach (char i in olasilik1)
                {
                    if (i != '-')
                        AryRakamlar.Add(i.ToString());
                }

                for (int i = 1; i <= 9; i++)
                {
                    if (AryRakamlar.Contains(i.ToString()))
                    {
                        try
                        {
                            AddLog3("Thread 3 - " + (row1.Index+1) + ". Satır, " + (cell1.ColumnIndex+1) + ". Sütun " + i + " olarak değiştirildi.");
                            AryKayit3.Add((row1.Index+1) + ". Satır, " + (cell1.ColumnIndex+1) + ". Sütun " + i + " olarak değiştirildi.");
                            cell1.Value = i;
                            break;
                        }
                        catch { }
                    }
                }
                #endregion
            }
            AbortThread(3);
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            SaveFileDialog od = new SaveFileDialog();
            od.Filter = "Text olarak kaydet | *.txt";
            od.ShowDialog();
            string konum = od.FileName;
            if (!File.Exists(konum))
            {
                int RowCount = 0;
                int CellCount = 0;
                using (StreamWriter dosya = new StreamWriter(konum))
                    for (int main = 0; main <= 80; main++)
                    {
                        DataGridViewRow row = DataGrid.Rows[RowCount];
                        DataGridViewCell cell = row.Cells[CellCount];
                        if (cell.Value == null)
                            dosya.Write("*");
                        else
                            dosya.Write(cell.Value);
                        CellCount++;
                        if (CellCount == 9)
                        {
                            CellCount = 0;
                            dosya.WriteLine("");
                            RowCount++;
                        }
                        if (RowCount == 9)
                        {
                            break;
                        }
                    }
            }
            else MessageBox.Show("Bu dosya mevcut!", "HATA");
        }

        private void AddLog1(string Log)
        {
            Loglar1 += Environment.NewLine + Log;
        }

        private void AddLog2(string Log)
        {
            Loglar2 += Environment.NewLine + Log;
        }

        private void AddLog3(string Log)
        {
            Loglar3 += Environment.NewLine + Log;
        }

        private void SaveLog()
        {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Log.txt"))
            {
                using (StreamWriter dosya = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Log.txt"))
                {
                    dosya.WriteLine(Loglar1);
                    dosya.WriteLine(Loglar2);
                    dosya.WriteLine(Loglar3);
                }
            }
            else
            {
                using (StreamWriter dosya = File.AppendText(AppDomain.CurrentDomain.BaseDirectory + "Log.txt"))
                {
                    dosya.WriteLine(Loglar1);
                    dosya.WriteLine(Loglar2);
                    dosya.WriteLine(Loglar3);
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
