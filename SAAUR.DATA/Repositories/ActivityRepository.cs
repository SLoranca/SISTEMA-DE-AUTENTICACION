using Dapper;
using Newtonsoft.Json;
using SAAUR.DATA.DBContext;
using SAAUR.DATA.Interfaces;
using SAAUR.MODELS.Entities;
using System.Data;

namespace SAAUR.DATA.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly IDbContext _db;

        public ActivityRepository(IDbContext db)
        {
            _db = db;
        }

        public ModelResponse Get_by_id(int item_id)
        {
            ModelResponse result = new ModelResponse();
            IDbConnection cnn = _db.Get();

            try
            {
                var _params = new DynamicParameters();

                _params.Add("@item_id", item_id);

                var resultBD = Dapper.SqlMapper.Query<dynamic>(cnn, "act_get_by_id", _params, commandType: CommandType.StoredProcedure).ToList();
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

        public ModelResponse Get_by_id_user(int user_id)
        {
            ModelResponse result = new ModelResponse();
            IDbConnection cnn = _db.Get();

            try
            {
                var _params = new DynamicParameters();

                _params.Add("@user_id", user_id);

                var resultBD = Dapper.SqlMapper.Query<dynamic>(cnn, "act_get_by_id_user", _params, commandType: CommandType.StoredProcedure).ToList();
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

        public ModelResponse Insert(ModelActivity model)
        {
            ModelResponse result = new ModelResponse();
            IDbConnection cnn = _db.Get();

            try
            {
                var _params = new DynamicParameters();

                _params.Add("@item_id", model.item_id);
                _params.Add("@user_id", model.user_id);
                _params.Add("@titulo", model.title_act);
                _params.Add("@descripcion", model.desc_act);
                _params.Add("@fecha_asignada", model.date_assigned);

                var resultBD = Dapper.SqlMapper.Query<ModelResponse>(cnn, "act_ins", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();
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

        public ModelResponse Complete(int act_id)
        {
            ModelResponse result = new ModelResponse();
            IDbConnection cnn = _db.Get();

            try
            {
                var _params = new DynamicParameters();

                _params.Add("@act_id", act_id);
                
                var resultBD = Dapper.SqlMapper.Query<ModelResponse>(cnn, "act_comp", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();
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

        public ModelResponse Delete(int act_id)
        {
            ModelResponse result = new ModelResponse();
            IDbConnection cnn = _db.Get();

            try
            {
                var _params = new DynamicParameters();

                _params.Add("@act_id", act_id);

                var resultBD = Dapper.SqlMapper.Query<ModelResponse>(cnn, "act_del", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();
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