using Blazored.LocalStorage;
using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Services
{
    public class ManageCartItemsLocalStorageService : IManageCartItemsLocalStorageService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IShoppingCartService _shoppingCartService;

        const string Key = "CartItemCollection";

        public ManageCartItemsLocalStorageService(ILocalStorageService localStorageService,
                                                  IShoppingCartService shoppingCartService)
        {
            this._localStorageService = localStorageService;
            this._shoppingCartService = shoppingCartService;
        }

        public async Task<List<CartItemDto>> GetCollection()
        {
            return await this._localStorageService.GetItemAsync<List<CartItemDto>>(Key)
                    ?? await AddCollection();
        }

        public async Task RemoveCollection()
        {
           await this._localStorageService.RemoveItemAsync(Key);
        }

        public async Task SaveCollection(List<CartItemDto> cartItemDtos)
        {
            await this._localStorageService.SetItemAsync(Key,cartItemDtos);
        }

        private async Task<List<CartItemDto>> AddCollection()
        {
            var shoppingCartCollection = await this._shoppingCartService.GetItems(HardCoded.UserId);

            if(shoppingCartCollection != null)
            {
                await this._localStorageService.SetItemAsync(Key, shoppingCartCollection);
            }
            
            return shoppingCartCollection;

        }

    }
}
