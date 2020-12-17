using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace Proje_Hastane
{
    public partial class FrmHastaBilgiGuncelle : Form
    {
        public FrmHastaBilgiGuncelle()
        {
            InitializeComponent();
        }

        
        public string TCno;
        newsql nw = new newsql();
        private void FrmHastaBilgiGuncelle_Load(object sender, EventArgs e)
        {
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.MaximizeBox = false;
            MskTC.Text = TCno;
            SqlCommand cmd = new SqlCommand("Select * from Tbl_Hastalar where @p1=HastaTC", nw.ConnSql());
            cmd.Parameters.AddWithValue("@p1", MskTC.Text);
            SqlDataReader sdr = cmd.ExecuteReader();
            while(sdr.Read())
            {
                TxtAd.Text = sdr[1].ToString();
                TxtSoyad.Text = sdr[2].ToString();
                MskTelefon.Text = sdr[4].ToString();
                TxtSifre.Text = sdr[5].ToString();
                cmbCinsiyet.Text = sdr[6].ToString();
            }
            nw.ConnSql().Close();
        }

        private void btnBilgiGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Update Tbl_Hastalar set HastaAd=@p1,HastaSoyad=@p2, HastaTelefon=@p3, HastaSifre=@p4, HastaCinsiyet=@p5 where HastaTC=@p6", nw.ConnSql());
            cmd.Parameters.AddWithValue("@p1", TxtAd.Text);
            cmd.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            cmd.Parameters.AddWithValue("@p3", MskTelefon.Text);
            cmd.Parameters.AddWithValue("@p4", TxtSifre.Text);
            cmd.Parameters.AddWithValue("@p5", cmbCinsiyet.Text);
            cmd.Parameters.AddWithValue("@p6", MskTC.Text);
            cmd.ExecuteNonQuery();
            nw.ConnSql().Close();
            MessageBox.Show("Bilgileriniz güncellenmiştir", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
