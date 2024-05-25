using Dapper;
using Newtonsoft.Json;
using SAAUR.DATA.DBContext;
using SAAUR.DATA.Interfaces;
using SAAUR.MODELS.Entities;
using System.Data;

namespace SAAUR.DATA.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly IDbContext _db;

        public ItemRepository(IDbContext db)
        {
            _db = db;
        }

        public ModelResponse Get(int user_id)
        {
            ModelResponse result = new ModelResponse();
            IDbConnection cnn = _db.Get();

            try
            {
                var _params = new DynamicParameters();

                _params.Add("@user_id", user_id);

                var resultBD = Dapper.SqlMapper.Query<dynamic>(cnn, "item_get_by_id", _params, commandType: CommandType.StoredProcedure).ToList();
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

        public ModelResponse Insert(ModelItem model)
        {
            ModelResponse result = new ModelResponse();
            IDbConnection cnn = _db.Get();

            try
            {
                var _params = new DynamicParameters();

                _params.Add("@user_id", model.user_id);
                _params.Add("@titulo", model.title);

                var resultBD = Dapper.SqlMapper.Query<ModelResponse>(cnn, "item_ins", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();
                result.status = resultBD.status;
                result.message = resultBD.message;
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