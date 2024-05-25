using SAAUR.BLL.Interfaces.Services;
using SAAUR.DATA.Interfaces;
using SAAUR.MODELS.Entities;

namespace SAAUR.BLL.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;

        public ActivityService(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public ModelResponse Get_by_id(int item_id)
        {
            return _activityRepository.Get_by_id(item_id);
        }

        public ModelResponse Get_by_id_user(int user_id)
        {
            return _activityRepository.Get_by_id_user(user_id);
        }

        public ModelResponse Insert(ModelActivity model)
        {
            return _activityRepository.Insert(model);
        }

        public ModelResponse Complete(int act_id)
        {
            return _activityRepository.Complete(act_id);
        }

        public ModelResponse Delete(int act_id)
        {
            return _activityRepository.Delete(act_id);
        }
    }
}