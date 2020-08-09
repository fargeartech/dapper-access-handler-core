using System.Data;

namespace DapperHelper.Contract
{
    public interface IDBFactory
    {
        IDbConnection CreateConnection();
    }
}
