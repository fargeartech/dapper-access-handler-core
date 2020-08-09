using System;
using System.Data;

namespace DapperHelper.Contract
{
    public interface IDAL : IDisposable
    {
        //IDbConnection GetConnection { get; }
        IDbConnection CreateConnection();
        void BeginTrans();
        void Commit();
        void Rollback();
    }
}
