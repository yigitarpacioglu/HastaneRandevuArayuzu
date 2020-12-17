using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Proje_Hastane
{
    public partial class FrmDoktorDetay : Form
    {
        public FrmDoktorDetay()
        {
            InitializeComponent();
        }

        newsql nw = new newsql();
        public string TC;
        private void FrmDoktorDetay_Load(object sender, EventArgs e)
        {
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.MaximizeBox = false;
            LblTC.Text = TC;

            // Doktor Ad-Soyad
            SqlCommand cmd = new SqlCommand("Select DoktorAd,DoktorSoyad from Tbl_Doktorlar where DoktorTC=@p1", nw.ConnSql());
            cmd.Parameters.AddWithValue("@p1", LblTC.Text);
            SqlDataReader sdr = cmd.ExecuteReader();
            while(sdr.Read())
            {
                LblAdSoyad.Text = sdr[0] + " " + sdr[1];
            }
            nw.ConnSql().Close();

            //Randevular
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter("Select * from Tbl_Randevular where RandevuDoktor='" + LblAdSoyad.Text + "'", nw.ConnSql());
            sda.Fill(dt);
            dataGridView1.DataSource = dt;


        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            FrmDoktorBilgiGuncelle fr = new FrmDoktorBilgiGuncelle();
            fr.TCNO=LblTC.Text;
            fr.Show();
        }

        private void btnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular fr = new FrmDuyurular();
            fr.Show();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            rchSikayet.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
        }
    }
}
