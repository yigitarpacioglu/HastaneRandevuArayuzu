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
    public partial class FrmDoktorGiris : Form
    {
        public FrmDoktorGiris()
        {
            InitializeComponent();
        }

        newsql nw = new newsql();
             
        private void FrmDoktorGiris_Load(object sender, EventArgs e)
        {
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.MaximizeBox = false;
            TxtSifre.UseSystemPasswordChar = true;
            this.AcceptButton = BtnGirisYap;
        }

        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Select * from Tbl_Doktorlar where DoktorTC=@p1 and DoktorSifre=@p2", nw.ConnSql());
            cmd.Parameters.AddWithValue("@p1", mskTC.Text);
            cmd.Parameters.AddWithValue("@p2", TxtSifre.Text);
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.Read())
            {
                FrmDoktorDetay frd = new FrmDoktorDetay();
                frd.TC = mskTC.Text;
                frd.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı TC veya şifre girdiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            nw.ConnSql().Close();

        }
    }
}
