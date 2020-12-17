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
    public partial class FrmBransPaneli : Form
    {
        public FrmBransPaneli()
        {
            InitializeComponent();
        }

        newsql nw = new newsql();
        private void FrmBransPaneli_Load(object sender, EventArgs e)
        {
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.MaximizeBox = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter("Select * from Tbl_Branslar", nw.ConnSql());
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Insert into Tbl_Branslar(BransAd)  values (@b1)", nw.ConnSql());
            cmd.Parameters.AddWithValue("@b1", TxtBransAd.Text);
            cmd.ExecuteNonQuery();
            nw.ConnSql().Close();
            MessageBox.Show("Branş eklenmiştir", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            TxtBransID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtBransAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Delete from Tbl_Branslar where BransID=@b1", nw.ConnSql());
            cmd.Parameters.AddWithValue("@b1", TxtBransID.Text);
            cmd.ExecuteNonQuery();
            nw.ConnSql().Close();
            MessageBox.Show("Branş silinmiştir", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Update Tbl_Branslar set bransad=@b1 where bransID=@b2", nw.ConnSql());
            cmd.Parameters.AddWithValue("@b2", TxtBransID.Text);
            cmd.Parameters.AddWithValue("@b1", TxtBransAd.Text);
            cmd.ExecuteNonQuery();
            nw.ConnSql().Close();
            MessageBox.Show("Kayıt güncellenmiştir", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
