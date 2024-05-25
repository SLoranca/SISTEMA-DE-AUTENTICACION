using Dapper;
using Newtonsoft.Json;
using SAAUR.DATA.DBContext;
using SAAUR.DATA.Interfaces;
using SAAUR.MODELS.Entities;
using System.Data;

namespace SAAUR.DATA.Repositories
{
    public class GraphicRepository : IGraphicRepository
    {
        private readonly IDbContext _db;

        public GraphicRepository(IDbContext db)
        {
            _db = db;
        }

        public ModelResponse GraphicsPerfomanceGeneral(string status)
        {
            ModelResponse result = new ModelResponse();
            IDbConnection cnn = _db.Get();

            try
            {
                var _params = new DynamicParameters();

                _params.Add("@status", status);

                var resultBD = Dapper.SqlMapper.Query<dynamic>(cnn, "performance_general", _params, commandType: CommandType.StoredProcedure).ToList();
                result.status = "OK";
                result.data = JsonConvert.SerializeObject(resultBD);
            }
            catch (Exception e)
            {
                result.status = "ERROR";
                result.message = e.Message.ToString();
            }
            finally
            {
                cnn.Close();
            }
            return result;
        }
    }
}