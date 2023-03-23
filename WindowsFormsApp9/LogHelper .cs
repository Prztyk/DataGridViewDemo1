using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using Serilog;

namespace WeldScanApp
{
    public class LogHelper
    {
        public static void SaveLog(string message)
        {

            using (SqlDatabase db = new SqlDatabase())
            {
                db.Open();

                string queryS_Insert =
                    "insert into dbo.SKAN_ERR_LOG " +
                    "(opis) " +
                    "values " +
                    "(@opis)";

                using (SqlCommand cmd = new SqlCommand(queryS_Insert, db.SqlCnn, db.Tr))
                {
                    cmd.Parameters.Add("@opis", SqlDbType.VarChar).Value = message;

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
