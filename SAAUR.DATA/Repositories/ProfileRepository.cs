using Dapper;
using Newtonsoft.Json;
using SAAUR.DATA.DBContext;
using SAAUR.DATA.Interfaces;
using SAAUR.MODELS.Entities;
using System.Data;

namespace SAAUR.DATA.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly IDbContext _db;

		public ProfileRepository(IDbContext db)
		{
			_db = db;
		}

		public ModelResponse UpdProfile(ModelProfile model)
		{
			ModelResponse result = new ModelResponse();
			IDbConnection cnn = _db.Get();

			try
			{
				var _params = new DynamicParameters();

				_params.Add("@user_id", model.user_id);
				_params.Add("@nombre", model.name);
				_params.Add("@paterno", model.p_last_name);
				_params.Add("@materno", model.m_last_name);
				_params.Add("@correo", model.email);

				var resultBD = Dapper.SqlMapper.Query<ModelResponse>(cnn, "profile_upd", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();
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

		public ModelResponse UpdPassword(ModelProfilePassword model)
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

				var resultBD = Dapper.SqlMapper.Query<ModelResponse>(cnn, "profile_upd_pass", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();
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

		public ModelResponse UpdEmail(ModelProfileEmail model)
        {
            ModelResponse result = new ModelResponse();
            IDbConnection cnn = _db.Get();

            try
            {
                var _params = new DynamicParameters();

                _params.Add("@id_user", model.user_id);
                _params.Add("@confirm_pass", model.confirm_password);
                _params.Add("@new_email", model.new_email);

                var resultBD = Dapper.SqlMapper.Query<ModelResponse>(cnn, "profile_upd_email", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();
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

		public ModelResponse EnableTwoFactor(int user_id)
        {
            ModelResponse result = new ModelResponse();
            IDbConnection cnn = _db.Get();

            try
            {
                var _params = new DynamicParameters();

                _params.Add("@user_id", user_id);

                var resultBD = Dapper.SqlMapper.Query<ModelResponse>(cnn, "account_auth2factor_enable", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();
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

		public ModelResponse DisableTwoFactor(int user_id)
        {
            ModelResponse result = new ModelResponse();
            IDbConnection cnn = _db.Get();

            try
            {
                var _params = new DynamicParameters();

                _params.Add("@user_id", user_id);

                var resultBD = Dapper.SqlMapper.Query<ModelResponse>(cnn, "account_auth2factor_disable", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();
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