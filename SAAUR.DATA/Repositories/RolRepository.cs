using Dapper;
using Newtonsoft.Json;
using SAAUR.DATA.DBContext;
using SAAUR.DATA.Interfaces;
using SAAUR.MODELS.Entities;
using System.Data;

namespace SAAUR.DATA.Repositories
{
    public class RolRepository : IRolRepository
    {
        private readonly IDbContext _db;

		public RolRepository(IDbContext db)
		{
			_db = db;
		}

		public ModelResponse Get()
		{
			ModelResponse result = new ModelResponse();
			IDbConnection cnn = _db.Get();

			try
			{
				var resultBD = Dapper.SqlMapper.Query<dynamic>(cnn, "roles_list", null, commandType: CommandType.StoredProcedure).ToList();
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

		public ModelResponse Insert(ModelRol model)
		{
			ModelResponse result = new ModelResponse();
			IDbConnection cnn = _db.Get();

			try
			{
				var _params = new DynamicParameters();

				_params.Add("@rol", model.rol);

				var resultBD = Dapper.SqlMapper.Query<ModelResponse>(cnn, "roles_ins", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();
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

		public ModelResponse Update(ModelRol model)
		{
			ModelResponse result = new ModelResponse();
			IDbConnection cnn = _db.Get();

			try
			{
				var _params = new DynamicParameters();

				_params.Add("@id", model.id);
				_params.Add("@rol", model.rol);

				var resultBD = Dapper.SqlMapper.Query<ModelResponse>(cnn, "roles_upd", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();
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

				var resultBD = Dapper.SqlMapper.Query<ModelResponse>(cnn, "roles_del", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();
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

        
		#region CONFIGURACIÓN DE PERMISOS A ROLES
		public ModelResponse ListRolApp()
        {
            ModelResponse result = new ModelResponse();
            IDbConnection cnn = _db.Get();

            try
            {
                var resultBD = Dapper.SqlMapper.Query<dynamic>(cnn, "roles_apps_list", null, commandType: CommandType.StoredProcedure).ToList();
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

        public ModelResponse ListRolModules(int app_id)
        {
            ModelResponse result = new ModelResponse();
            IDbConnection cnn = _db.Get();

            try
            {
                var _params = new DynamicParameters();

                _params.Add("@app_id", app_id);

                var resultBD = Dapper.SqlMapper.Query<dynamic>(cnn, "roles_apps_modules_get", _params, commandType: CommandType.StoredProcedure).ToList();
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

        public ModelResponse InsertRolAppModules(ModelRolApp model)
        {
            ModelResponse result = new();
            IDbConnection cnn = _db.Get();

            try
            {
                DataTable dt = new DataTable();
                DataRow row;

                dt.Columns.Add(new DataColumn() { DataType = Type.GetType("System.Int32"), ColumnName = "module_id" });

                foreach (ModelRolModuleList opt in model.modules)
                {
                    row = dt.NewRow();
                    row[0] = opt.module_id;
                    dt.Rows.Add(row);
                }

                var _params = new DynamicParameters();

                _params.Add("@rol_id", model.rol_id);
                _params.Add("@app_id", model.app_id);
                _params.Add("@modules", dt, DbType.Object);

                var resultBD = Dapper.SqlMapper.Query<ModelResponse>(cnn, "roles_apps_ins", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();
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
		#endregion
    }
}