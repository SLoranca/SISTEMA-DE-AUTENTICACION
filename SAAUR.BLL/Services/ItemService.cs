using SAAUR.BLL.Interfaces.Services;
using SAAUR.DATA.Interfaces;
using SAAUR.MODELS.Entities;

namespace SSOLogin.BLL.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public ModelResponse Get(int user_id)
        {
            return _itemRepository.Get(user_id);
        }

        public ModelResponse Insert(ModelItem model)
        {
            return _itemRepository.Insert(model);
        }
    }
}