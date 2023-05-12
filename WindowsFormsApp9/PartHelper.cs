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
using System.Security.Cryptography;

namespace WeldScanApp
{
    public class PartHelper
    {

        private static List<string> notFixableWelds = new List<string>() { "W35", "W36", "W37", "W44.1", "W45.1", "W82", "W83", "W84", "W91.1", "W92.1", "W19", "W66", "W22", "W69", "W11.2", "W58.2", "W4", "W51",
            "W44.2", "W45.2", "W91.2", "W92.2", "W11.3", "W58.3" };


        public static void SavePart(PartDTO part)
        {

            using (SqlDatabase db = new SqlDatabase())
            {
                db.OpenTr();

                if (part.Id == 0)
                {
                    #region row not saved in database - insert

                    Log.Information($"Part insert [{part.PartCode}]");

                    string queryS_Insert =
                        "insert into dbo.SKAN_DETAL " +
                        "(detal, linia) " +
                        "values " +
                        "(@detal, @linia); " +
                        "select @Id = scope_identity();";

                    using (SqlCommand cmd = new SqlCommand(queryS_Insert, db.SqlCnn, db.Tr))
                    {
                        cmd.Parameters.Add("@detal", SqlDbType.VarChar).Value = part.PartCode;
                        cmd.Parameters.Add("@linia", SqlDbType.VarChar).Value = part.Line;

                        SqlParameter recordId = new SqlParameter("@Id", SqlDbType.Int);
                        recordId.Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(recordId);

                        cmd.ExecuteNonQuery();

                        part.Id = Convert.ToInt32(cmd.Parameters["@Id"].Value);
                    }

                    #endregion row not saved in database
                }
                else
                {
                    #region row saved in database - update

                    Log.Information($"Part rqty udate [{part.PartCode}]");

                    string queryS_Update =
                        "update dbo.SKAN_DETAL " +
                        "set rqty = rqty + 1 " +
                        "where id = @id";

                    using (SqlCommand cmd = new SqlCommand(queryS_Update, db.SqlCnn, db.Tr))
                    {
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = part.Id;

                        cmd.ExecuteNonQuery();
                    }

                    #endregion row saved in database - update
                }

                db.CommitTr();
            }
        }

        public static void UpdateResult(PartDTO part)
        {
            Log.Information($"Part status update [{part.PartCode}] [{part.Result}]");

            using (SqlDatabase db = new SqlDatabase())
            {
                db.OpenTr();

                string queryS_Update =
                    "update dbo.SKAN_DETAL " +
                    "set decyzja = @decyzja " +
                    "where id = @id";

                // Removed as user should have last word part is fixable or not
                //if(IsPartNotFixable(part)) { part.Result = "NOK"; }

                using (SqlCommand cmd = new SqlCommand(queryS_Update, db.SqlCnn, db.Tr))
                {
                    cmd.Parameters.Add("@decyzja", SqlDbType.VarChar).Value = part.Result;
                    
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = part.Id;

                    cmd.ExecuteNonQuery();
                }

                db.CommitTr();
            }
        }

        public static PartDTO GetPartByCode(string partCode)
        {
            using (SqlDatabase db = new SqlDatabase())
            {
                db.Open();

                string queryS =
                    "select id, detal, decyzja, data_detal, linia " +
                    "from dbo.SKAN_DETAL " +
                    "where detal = @detal " +
                    "and linia = @line";
                using (SqlCommand cmd = new SqlCommand(queryS, db.SqlCnn))
                {
                    cmd.Parameters.Add("@detal", SqlDbType.VarChar).Value = partCode;
                    cmd.Parameters.Add("@line", SqlDbType.VarChar).Value = Program.ProductionLine;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            PartDTO part = new PartDTO();
                            part.Id = reader.GetInt32(reader.GetOrdinal("id"));
                            part.PartCode = reader.GetString(reader.GetOrdinal("detal"));
                            part.Result = reader.IsDBNull(reader.GetOrdinal("decyzja")) ? string.Empty : reader.GetString(reader.GetOrdinal("decyzja"));
                            part.Date = reader.GetDateTime(reader.GetOrdinal("data_detal"));
                            part.Line = reader.GetString(reader.GetOrdinal("linia"));

                            part.Welds = WeldHelper.GetWeldsByPartId(part.Id);

                            return part;
                        }
                    }
                }
            }

            return null;
        }

        public static bool IsPartNotFixable(PartDTO part)
        {
            foreach (var notFixableWeld in notFixableWelds)
            {
                if(part.Welds.Contains(notFixableWeld))
                { return true; }
            }
            return false;
        }

        public static bool IsWeldNotFixable(string weld)
        {
            return notFixableWelds.Contains(weld);
        }

    }
}
