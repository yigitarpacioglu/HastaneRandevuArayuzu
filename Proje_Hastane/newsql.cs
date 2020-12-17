using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Proje_Hastane
{
    class newsql
    {
        public SqlConnection ConnSql()
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-G2SNSLM\\SQLEXPRESS;Initial Catalog=HastaneProje;Integrated Security=True");
            conn.Open();
            return conn;
        }
    }
}
