using Dapper;
using Newtonsoft.Json;
using SAAUR.DATA.DBContext;
using SAAUR.DATA.Interfaces;
using SAAUR.MODELS.Entities;
using System.Data;

namespace SAAUR.DATA.Repositories
{
    public class UploadImgRepository : IUploadImgRepository
    {
        private readonly IDbContext _db;

        public UploadImgRepository(IDbContext db)
        {
            _db = db;
        }

        public ModelResponse UploadImg(ModelImgUpload model)
        {
            ModelResponse result = new ModelResponse();
            IDbConnection cnn = _db.Get();

            try
            {
                var _params = new DynamicParameters();

                _params.Add("@user_id", model.user_id);
                _params.Add("@img", model.image);

                var resultBD = Dapper.SqlMapper.Query<ModelResponse>(cnn, "upload_img", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();
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