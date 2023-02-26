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

namespace WeldScanApp
{
    public class DatabaseHelper
    {
        private static void Save(PartDTO p)
        {

            using (SqlDatabase db = new SqlDatabase())
            {
                db.OpenTr();

                if (p.Id == 0)
                {
                    #region row not saved in database - insert

                    string queryS_Insert =
                        "insert into dbo.SKAN_DETAL " +
                        "(detal, linia) " +
                        "values " +
                        "(@detal, @linia); " +
                        "select @Id = scope_identity();";

                    using (SqlCommand cmd = new SqlCommand(queryS_Insert, db.SqlCnn, db.Tr))
                    {
                        cmd.Parameters.Add("@detal", SqlDbType.VarChar).Value = p.PartNumber;
                        cmd.Parameters.Add("@linia", SqlDbType.VarChar).Value = Program.ProductionLine;

                        SqlParameter recordId = new SqlParameter("@Id", SqlDbType.Int);
                        recordId.Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(recordId);

                        cmd.ExecuteNonQuery();

                        p.Id = Convert.ToInt32(cmd.Parameters["@Id"].Value);
                    }

                    #endregion row not saved in database
                }
                else
                {
                    #region row saved in database - update

                    string queryS_Update =
                        "update dbo.SKAN_DETAL " +
                        "set decyzja = @decyzja " +
                        "where id = @id";

                    using (SqlCommand cmd = new SqlCommand(queryS_Update, db.SqlCnn, db.Tr))
                    {
                        cmd.Parameters.Add("@decyzja", SqlDbType.VarChar).Value = p.Result;

                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = p.Id;

                        cmd.ExecuteNonQuery();
                    }

                    #endregion row saved in database - update
                }

                db.CommitTr();
            }
        }

        public static PartDTO Get()
        {
            using (SqlDatabase db = new SqlDatabase())
            {
                db.Open();

                string queryS =
                    "select id, detal, decyzja, data_detal, linia " +
                    "from dbo.SKAN_DETAL";
                using (SqlCommand command = new SqlCommand(queryS, db.SqlCnn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            PartDTO part = new PartDTO();
                            part.Id = reader.GetInt32(reader.GetOrdinal("id"));
                            part.PartNumber = reader.GetString(reader.GetOrdinal("detal"));
                            part.Result = reader.GetString(reader.GetOrdinal("decyzja"));
                            part.Date = reader.GetDateTime(reader.GetOrdinal("data_detal"));
                            part.Line = reader.GetString(reader.GetOrdinal("linia"));

                            return part;
                        }
                    }
                }
            }

            return null;
        }
    }
}
