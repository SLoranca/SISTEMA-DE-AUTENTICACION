using Dapper;
using Newtonsoft.Json;
using SAAUR.DATA.DBContext;
using SAAUR.DATA.Interfaces;
using SAAUR.MODELS.Entities;
using System.Data;

namespace SAAUR.DATA.Repositories
{
    public class AccountRepository :IAccountRepository
    {
        private readonly IDbContext _db;

		public AccountRepository(IDbContext db)
		{
			_db = db;
		}

        public ModelResponse Authentication(string email, string password)
		{
			ModelResponse result = new ModelResponse();
			IDbConnection cnn = _db.Get();

			try
			{
				var _params = new DynamicParameters();

				_params.Add("@correo", email);
				_params.Add("@pwd", password);

				var resultBD = Dapper.SqlMapper.Query<ModelTwoFactorInteger>(cnn, "account_auth", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();
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

        public ModelResponse Create(ModelAccountCreate model)
        {
            ModelResponse result = new ModelResponse();
            IDbConnection cnn = _db.Get();

            try
            {
                var _params = new DynamicParameters();

                _params.Add("@nombre", model.name.ToUpper());
                _params.Add("@materno", model.m_last_name.ToUpper());
                _params.Add("@paterno", model.p_last_name.ToUpper());
                _params.Add("@correo", model.email.ToLower());
                _params.Add("@pwd", model.password);
                _params.Add("@salt", model.salt);
				_params.Add("@hashPass", model.hashPass);

				var resultBD = Dapper.SqlMapper.Query<ModelResponse>(cnn, "account_create", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();
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

        public ModelResponse Recovery(string email, string password, string hashPass, string salt)
        {
            ModelResponse result = new ModelResponse();
            IDbConnection cnn = _db.Get();

            try
            {
                var _params = new DynamicParameters();

                _params.Add("@email", email);
                _params.Add("@pwd", password);
                _params.Add("@hashPass", hashPass);
                _params.Add("@salt", salt);

                var resultBD = Dapper.SqlMapper.Query<ModelResponse>(cnn, "account_recovery", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();
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