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
    public partial class FrmDoktorPaneli : Form
    {
        public FrmDoktorPaneli()
        {
            InitializeComponent();
        }

        newsql nw = new newsql();

        private void FrmDoktorPaneli_Load(object sender, EventArgs e)
        {
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.MaximizeBox = false;
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter("Select * from Tbl_Doktorlar", nw.ConnSql());
            sda.Fill(dt);
            dataGridView1.DataSource = dt;

            // Branşları ComboBox'a Aktarma
            SqlCommand cmd2 = new SqlCommand("Select BransAd from Tbl_Branslar", nw.ConnSql());
            SqlDataReader sdr2 = cmd2.ExecuteReader();
            while (sdr2.Read())
            {
                cmbBrans.Items.Add(sdr2[0]);
            }
            nw.ConnSql().Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Insert into Tbl_Doktorlar(DoktorAd,DoktorSoyad,DoktorBrans,DoktorTC,DoktorSifre) values (@d1,@d2,@d3,@d4,@d5)", nw.ConnSql());
            cmd.Parameters.AddWithValue("@d1", TxtAd.Text);
            cmd.Parameters.AddWithValue("@d2", TxtSoyad.Text);
            cmd.Parameters.AddWithValue("@d3", cmbBrans.Text);
            cmd.Parameters.AddWithValue("@d4", MskTC.Text);
            cmd.Parameters.AddWithValue("@d5", TxtSifre.Text);
            cmd.ExecuteNonQuery();
            nw.ConnSql().Close();
            MessageBox.Show("Doktor kaydı gerçekleştirilmiştir", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            cmbBrans.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            MskTC.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();            
            TxtSifre.Text= dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Delete from Tbl_Doktorlar where DoktorTC=@p1", nw.ConnSql());
            cmd.Parameters.AddWithValue("@p1", MskTC.Text);
            cmd.ExecuteNonQuery();
            nw.ConnSql().Close();
            MessageBox.Show("Kayıt silindi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Update Tbl_Doktorlar set DoktorAd=@d1, DoktorSoyad=@d2, DoktorBrans=@d3, DoktorSifre=@d5 where DoktorTC=@d4", nw.ConnSql());
            cmd.Parameters.AddWithValue("@d1", TxtAd.Text);
            cmd.Parameters.AddWithValue("@d2", TxtSoyad.Text);
            cmd.Parameters.AddWithValue("@d3", cmbBrans.Text);
            cmd.Parameters.AddWithValue("@d4", MskTC.Text);
            cmd.Parameters.AddWithValue("@d5", TxtSifre.Text);
            cmd.ExecuteNonQuery();
            nw.ConnSql().Close();
            MessageBox.Show("Doktor kaydı güncellenmiştir", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
