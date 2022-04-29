using Blazored.LocalStorage;
using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Services
{
    public class ManageProductsLocalStorageService : IManageProductsLocalStorageService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IProductService _productService;

        private const string Key = "ProductCollection";

        public ManageProductsLocalStorageService(ILocalStorageService localStorageService,
                                                 IProductService productService)
        {
            this._localStorageService = localStorageService;
            this._productService = productService;
        }

        public async Task<IEnumerable<ProductDto>> GetCollection()
        {
            return await this._localStorageService.GetItemAsync<IEnumerable<ProductDto>>(Key)
                    ?? await AddCollection();
        }

        public async Task RemoveCollection()
        {
           await this._localStorageService.RemoveItemAsync(Key);
        }

        private async Task<IEnumerable<ProductDto>> AddCollection()
        {
            var productCollection = await this._productService.GetItems();

            if (productCollection != null)
            {
                await this._localStorageService.SetItemAsync(Key, productCollection);
            }

            return productCollection;

        }

    }
}
