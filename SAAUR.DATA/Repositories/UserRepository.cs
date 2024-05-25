using Dapper;
using Newtonsoft.Json;
using SAAUR.DATA.DBContext;
using SAAUR.DATA.Interfaces;
using SAAUR.MODELS.Entities;
using System.Data;

namespace SAAUR.DATA.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbContext _db;

        public UserRepository(IDbContext db)
        {
            _db = db;
        }

        public ModelResponse Get()
        {
            ModelResponse result = new ModelResponse();
            IDbConnection cnn = _db.Get();

            try
            {
                var resultBD = Dapper.SqlMapper.Query<dynamic>(cnn, "users_list", null, commandType: CommandType.StoredProcedure).ToList();
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

		public ModelResponse Insert(ModelUser model)
		{
			ModelResponse result = new ModelResponse();
			IDbConnection cnn = _db.Get();

			try
			{
				var _params = new DynamicParameters();

				_params.Add("@rol_id", model.id_rol);
				_params.Add("@nombre", model.name.ToUpper());
				_params.Add("@paterno", model.p_last_name.ToUpper());
				_params.Add("@materno", model.m_last_name.ToUpper());
				_params.Add("@correo", model.email.ToLower());
				_params.Add("@pwd", model.password);
				_params.Add("@hashPass", model.hashPass);
				_params.Add("@salt", model.salt);

				var resultBD = Dapper.SqlMapper.Query<ModelResponse>(cnn, "user_ins", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();
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

		public ModelResponse UpdGeneralInfo(ModelUserEditGeneralInfo model)
		{
			ModelResponse result = new ModelResponse();
			IDbConnection cnn = _db.Get();

			try
			{
				var _params = new DynamicParameters();

				_params.Add("@user_id", model.user_id);
				_params.Add("@rol_id", model.id_rol);
				_params.Add("@nombre", model.name.ToUpper());
				_params.Add("@paterno", model.p_last_name.ToUpper());
				_params.Add("@materno", model.m_last_name.ToUpper());

				var resultBD = Dapper.SqlMapper.Query<ModelResponse>(cnn, "user_upd_general_info", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();
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

		public ModelResponse UpdEmail(ModelUserEditEmail model)
		{
			ModelResponse result = new ModelResponse();
			IDbConnection cnn = _db.Get();

			try
			{
				var _params = new DynamicParameters();

				_params.Add("@user_id", model.user_id);
				_params.Add("@correo", model.email.ToLower());

				var resultBD = Dapper.SqlMapper.Query<ModelResponse>(cnn, "user_upd_email", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();
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

		public ModelResponse UpdPassword(ModelUserEditPassword model)
		{
			ModelResponse result = new ModelResponse();
			IDbConnection cnn = _db.Get();

			try
			{
				var _params = new DynamicParameters();

				_params.Add("@user_id", model.user_id);
				_params.Add("@pwd", model.password);
				_params.Add("@hashPass", model.hashPass);
				_params.Add("@salt", model.salt);

				var resultBD = Dapper.SqlMapper.Query<ModelResponse>(cnn, "user_upd_password", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();
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

		public ModelResponse Delete(int id)
		{
			ModelResponse result = new ModelResponse();
			IDbConnection cnn = _db.Get();

			try
			{
				var _params = new DynamicParameters();

				_params.Add("@user_id", id);

				var resultBD = Dapper.SqlMapper.Query<ModelResponse>(cnn, "user_del", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();
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