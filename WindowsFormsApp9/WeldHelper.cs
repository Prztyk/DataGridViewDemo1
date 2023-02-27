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
    public class WeldHelper
    {
        public static void SaveWeld(WeldDTO weld)
        {

            using (SqlDatabase db = new SqlDatabase())
            {
                db.OpenTr();

                int updated = 0;

                #region update

                string queryS_Update =
                    "update dbo.SKAN_POLA " +
                    "set rqty = rqty + 1 " +
                    "where skan_detal_id = @skan_detal_id " +
                    "and pole = @pole";

                using (SqlCommand cmd = new SqlCommand(queryS_Update, db.SqlCnn, db.Tr))
                {
                    cmd.Parameters.Add("@skan_detal_id", SqlDbType.Int).Value = weld.PartId;
                    cmd.Parameters.Add("@pole", SqlDbType.VarChar).Value = weld.WeldCode;
                    updated = cmd.ExecuteNonQuery();
                }

                #endregion update

                if (updated == 0)
                {
                    #region insert

                    Log.Information($"Weld insert [{weld.PartId}] [{weld.WeldCode}]");

                    string queryS_Insert =
                        "insert into dbo.SKAN_POLA " +
                        "(skan_detal_id, pole) " +
                        "values " +
                        "(@skan_detal_id, @pole)";

                    using (SqlCommand cmd = new SqlCommand(queryS_Insert, db.SqlCnn, db.Tr))
                    {
                        cmd.Parameters.Add("@skan_detal_id", SqlDbType.Int).Value = weld.PartId;
                        cmd.Parameters.Add("@pole", SqlDbType.VarChar).Value = weld.WeldCode;

                        cmd.ExecuteNonQuery();
                    }

                    #endregion insert
                }

                db.CommitTr();
            }
        }

        public static List<string> GetWeldsByPartId(int partId)
        {
            var list = new List<string>();

            using (SqlDatabase db = new SqlDatabase())
            {
                db.Open();

                string queryS =
                    "select skan_detal_id, pole, data " +
                    "from dbo.SKAN_POLA " +
                    "where skan_detal_id = @skan_detal_id " +
                    "order by data asc";
                using (SqlCommand command = new SqlCommand(queryS, db.SqlCnn))
                {
                    command.Parameters.Add("@skan_detal_id", SqlDbType.Int).Value = partId;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string weld = reader.GetString(reader.GetOrdinal("pole"));

                            list.Add(weld);
                        }
                    }
                }
            }

            return list;
        }
    }
}
