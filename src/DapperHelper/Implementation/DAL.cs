using DapperHelper.Contract;
using DapperHelper.Settings;
using System;
using System.Data;

namespace DapperHelper.Implementation
{
    public class DAL : IDAL
    {
        private IDbConnection connection;
        private IDbTransaction trans;
        private IDBFactory dbFactory;
        private bool _disposed;
        private string _ErrorMessage;
        private readonly ConnectionSettings connectionSettings;
        public DAL(ConnectionSettings connectionSettings)
        {
            this.connectionSettings = connectionSettings;
            dbFactory = new DBFactory(connectionSettings);
        }
        public DAL(string constr, string strprovider)
        {
            dbFactory = new DBFactory(constr, strprovider);
        }
        public IDbConnection CreateConnection()
        {
            if (connection == null)
            {
                connection = dbFactory.CreateConnection();
                return connection;
            }
            return connection;
        }

        /// <summary>
        /// Faris, create an transaction
        /// </summary>
        public void BeginTrans()
        {
            trans = connection.BeginTransaction();
        }
        /// <summary>
        /// commit trans
        /// </summary>
        public void Commit()
        {
            try
            {
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw new ApplicationException("error " + ex.Message);
                //Logger.Instance.AddLog(ex);
            }
            finally
            {
                trans.Dispose();
                trans = null;
            }
        }

        public void Rollback()
        {
            try
            {
                trans.Rollback();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("error " + ex.Message);
            }
            finally
            {
                trans.Dispose();
                trans = null;
            }
        }

        public int GetOffSet(int currentIndex = 0, int PageSize = 15)
        {
            int index = currentIndex == 0 ? currentIndex : currentIndex - 1;
            return(index * PageSize);
        }
        /// <summary>
        /// rollback if exceptions
        /// </summary>

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }
        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (trans != null)
                        trans.Dispose();
                    if (connection != null)
                        connection.Dispose();
                    dbFactory = null;
                    connection = null;
                    trans = null;
                }
                _disposed = true;
            }
        }
        /// <summary>
        /// faris ,destructor
        /// </summary>
        ~DAL()
        {
            dispose(false);
        }
    }
}
