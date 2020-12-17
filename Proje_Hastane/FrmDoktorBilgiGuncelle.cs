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
    public partial class FrmDoktorBilgiGuncelle : Form
    {
        public FrmDoktorBilgiGuncelle()
        {
            InitializeComponent();
        }

        newsql nw = new newsql();
        public string TCNO;
        private void FrmDoktorBilgiGuncelle_Load(object sender, EventArgs e)
        {
            MskTC.Text = TCNO;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.MaximizeBox = false;
            
            // TC yardımıyla SQL'den diğer bilgilerin çekilmesi
            SqlCommand cmd = new SqlCommand("Select * from Tbl_Doktorlar where DoktorTC=@p1", nw.ConnSql());
            cmd.Parameters.AddWithValue("@p1", MskTC.Text);
            SqlDataReader sdr = cmd.ExecuteReader();
            while(sdr.Read())
            {
                TxtAd.Text = sdr[1].ToString();
                TxtSoyad.Text = sdr[2].ToString();
                cmbBrans.Text = sdr[3].ToString();
                TxtSifre.Text = sdr[5].ToString();
            }
            nw.ConnSql().Close();
        }

        private void btnBilgiGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Update Tbl_Doktorlar set DoktorAd=@p1, DoktorSoyad=@p2, DoktorBrans=@p3, DoktorSifre=@p4 where DoktorTC=@p5", nw.ConnSql());
            cmd.Parameters.AddWithValue("@p1", TxtAd.Text);
            cmd.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            cmd.Parameters.AddWithValue("@p3", cmbBrans.Text);
            cmd.Parameters.AddWithValue("@p4", TxtSifre.Text);
            cmd.Parameters.AddWithValue("@p5", MskTC.Text);
            cmd.ExecuteNonQuery();
            nw.ConnSql().Close();
            MessageBox.Show("Kayıt güncellenmiştir", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
