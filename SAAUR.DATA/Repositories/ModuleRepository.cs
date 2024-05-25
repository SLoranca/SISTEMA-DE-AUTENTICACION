using Dapper;
using Newtonsoft.Json;
using SAAUR.DATA.DBContext;
using SAAUR.DATA.Interfaces;
using SAAUR.MODELS.Entities;
using System.Data;

namespace SAAUR.DATA.Repositories
{
	public class ModuleRepository : IModuleRepository
	{
		private readonly IDbContext _db;

		public ModuleRepository(IDbContext db)
		{
			_db = db;
		}

		public ModelResponse Get(int app_id)
		{
			ModelResponse result = new ModelResponse();
			IDbConnection cnn = _db.Get();

			try
			{
				var _params = new DynamicParameters();

				_params.Add("@app_id", app_id);

				var resultBD = Dapper.SqlMapper.Query<dynamic>(cnn, "module_list", _params, commandType: CommandType.StoredProcedure).ToList();
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

		public ModelResponse Insert(ModelModule model)
		{
			ModelResponse result = new ModelResponse();
			IDbConnection cnn = _db.Get();

			try
			{
				var _params = new DynamicParameters();

				_params.Add("@app_id", model.app_id);
				_params.Add("@titulo", model.title.ToUpper());
				_params.Add("@accion", model.action);
				_params.Add("@controlador", model.controller);

				var resultBD = Dapper.SqlMapper.Query<ModelResponse>(cnn, "module_app_ins", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();
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

		public ModelResponse Update(ModelModule model)
		{
			ModelResponse result = new ModelResponse();
			IDbConnection cnn = _db.Get();

			try
			{
				var _params = new DynamicParameters();

				_params.Add("@id", model.id);
				_params.Add("@app_id", model.app_id);
				_params.Add("@titulo", model.title.ToUpper());
				_params.Add("@accion", model.action);
				_params.Add("@controlador", model.controller);

				var resultBD = Dapper.SqlMapper.Query<ModelResponse>(cnn, "module_app_upd", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();
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

        public ModelResponse Enable(int id)
        {
            ModelResponse result = new ModelResponse();
            IDbConnection cnn = _db.Get();

            try
            {
                var _params = new DynamicParameters();

                _params.Add("@module_id", id);

                var resultBD = Dapper.SqlMapper.Query<ModelResponse>(cnn, "module_app_enable", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();
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

        public ModelResponse Disable(int id)
		{
			ModelResponse result = new ModelResponse();
			IDbConnection cnn = _db.Get();

			try
			{
				var _params = new DynamicParameters();

				_params.Add("@module_id", id);

				var resultBD = Dapper.SqlMapper.Query<ModelResponse>(cnn, "module_app_disable", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();
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
