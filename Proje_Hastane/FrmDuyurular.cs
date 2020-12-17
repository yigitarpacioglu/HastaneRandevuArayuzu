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
    public partial class FrmDuyurular : Form
    {
        public FrmDuyurular()
        {
            InitializeComponent();
        }

        newsql nw = new newsql();
        private void FrmDuyurular_Load(object sender, EventArgs e)
        {
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.MaximizeBox = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter("Select * from Tbl_Duyurular",nw.ConnSql());
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
