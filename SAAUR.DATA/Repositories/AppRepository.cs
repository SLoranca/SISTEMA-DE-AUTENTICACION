using Dapper;
using Newtonsoft.Json;
using SAAUR.DATA.DBContext;
using SAAUR.DATA.Interfaces;
using SAAUR.MODELS.Entities;
using System.Data;

namespace SAAUR.DATA.Repositories
{
    public class AppRepository : IAppRepository
    {
        private readonly IDbContext _db;

		public AppRepository(IDbContext db)
		{
			_db = db;
		}

		public ModelResponse Get()
		{
			ModelResponse result = new ModelResponse();
			IDbConnection cnn = _db.Get();

			try
			{
				var resultBD = Dapper.SqlMapper.Query<dynamic>(cnn, "app_list", null, commandType: CommandType.StoredProcedure).ToList();
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

		public ModelResponse Insert(ModelApplication model)
		{
			ModelResponse result = new ModelResponse();
			IDbConnection cnn = _db.Get();

			try
			{
				var _params = new DynamicParameters();

				_params.Add("@nombre", model.name.ToUpper());
				_params.Add("@descripcion", model.description.ToUpper());
				_params.Add("@vinculo", model.link);

				var resultBD = Dapper.SqlMapper.Query<ModelResponse>(cnn, "app_ins", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();
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

		public ModelResponse Update(ModelApplication model)
		{
			ModelResponse result = new ModelResponse();
			IDbConnection cnn = _db.Get();

			try
			{
				var _params = new DynamicParameters();

				_params.Add("@id", model.id);
				_params.Add("@nombre", model.name.ToUpper());
				_params.Add("@descripcion", model.description.ToUpper());
				_params.Add("@vinculo", model.link);

				var resultBD = Dapper.SqlMapper.Query<ModelResponse>(cnn, "app_upd", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();
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

				_params.Add("@id", id);

				var resultBD = Dapper.SqlMapper.Query<ModelResponse>(cnn, "app_del", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();
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