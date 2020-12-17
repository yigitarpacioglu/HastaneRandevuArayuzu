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
    public partial class FrmSekreterGiris : Form
    {
        public FrmSekreterGiris()
        {
            InitializeComponent();
        }

        newsql nw = new newsql();

        private void FrmSekreterGiris_Load(object sender, EventArgs e)
        {
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.MaximizeBox = false;
        }

        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Select * from Tbl_Sekreter where @p1=SekreterTC and @p2=SekreterSifre", nw.ConnSql());
            cmd.Parameters.AddWithValue("@p1", mskTC.Text);
            cmd.Parameters.AddWithValue("@p2", TxtSifre.Text);
            SqlDataReader sdr = cmd.ExecuteReader();
            if(sdr.Read())
            {
                FrmSekreterDetay fr = new FrmSekreterDetay();
                fr.sektcno = mskTC.Text;
                fr.Show();
                this.Hide();                    
            }
            else
            {
                MessageBox.Show("Hatalı 'TC' veya 'Şifre' girişi yapıldı");
            }
            nw.ConnSql().Close();
        }
    }
}
