using System.Data;

namespace SAAUR.DATA.DBContext
{
    public interface IDbContext
    {
        IDbConnection Get();
    }
}