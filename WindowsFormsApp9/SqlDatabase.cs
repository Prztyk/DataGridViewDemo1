using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeldScanApp
{
    public class SqlDatabase : IDisposable
    {
        private SqlConnection __db_cnn = null;
        private SqlTransaction __db_tr = null;
        private SqlConnectionStringBuilder __db_cnn_string = null;
        private Boolean __is_trans = false;

        public void Dispose()
        {
            if (__is_trans)
            {
                RollbckTr();
            }
            if (__db_cnn == null) { return; }
            __db_cnn.Close();
            __db_cnn = null;
        }

        public SqlDatabase()
        {
            string cnnString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;

            __db_cnn_string = new SqlConnectionStringBuilder(cnnString);

            __db_cnn = new SqlConnection();
        }

        #region Properties

        public SqlConnection SqlCnn
        {
            get { return __db_cnn; }
        }

        public SqlTransaction Tr
        {
            get { return __db_tr; }
        }

        public Boolean InTrans
        {
            get { return __is_trans; }
        }

        #endregion Properties

        #region Public methods

        public void Open()
        {
            if (__db_cnn.State == System.Data.ConnectionState.Closed)
            {
                __db_cnn.ConnectionString = __db_cnn_string.ToString();
                __db_cnn.Open();
            }
        }

        public void Close()
        {
            __db_cnn.Close();
            __db_cnn = null;
        }

        public void OpenTr()
        {
            Open();
            __db_tr = __db_cnn.BeginTransaction();
            __is_trans = true;
        }

        public void CommitTr()
        {
            if (__is_trans)
            {
                __db_tr.Commit();
                __db_tr = null;
                __is_trans = false;
            }
        }

        public void RollbckTr()
        {
            if (__is_trans)
            {
                __db_tr.Rollback();
                __db_tr = null;
                __is_trans = false;
            }
        }

        #endregion Public methods
    }
}
