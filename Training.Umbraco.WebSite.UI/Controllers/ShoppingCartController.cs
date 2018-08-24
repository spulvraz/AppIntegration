using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using onlineStoreDb;
using Training.Umbraco.WebSite.UI.Models;
using Training.Umbraco.WebSite.UI.ViewModels;

namespace Training.Umbraco.WebSite.UI.Controllers
{
    public class ShoppingCartController : StoreSurfaceController
    {
        #region Methods

        public ActionResult Index(int page = 1)
        {
            var products = GetCurrentShoppingCartItems<ShoppingCartProductPreviewViewModel>();
            var list = new Pager<ShoppingCartProductPreviewViewModel>(products.Skip((page - 1) * 20).Take(20), products.Count(), page);

            return View(new ShoppingCartViewModel() { List = list });
        }

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            return PartialView("_CartSummary", new NavShoppingCartViewModel
            {
                Products = GetCurrentShoppingCartItems<ProductBasicViewModel>(),
                CartTotal = ShoppingCartContext.Current.ShoppingTotal.ToString("N"),
                TotalItems = ShoppingCartContext.Current.NumberOfProducts
            });
        }

        public ActionResult AddToCart(int productId, int quantity)
        {
            AddProductToCart(productId, quantity);
            return RedirectToUmbracoPage(1058);             //was return RedirectToAction("Index", "Home");
        }

        public ActionResult ChangeQuantity(int productId, int quantity)
        {
            ShoppingCartContext.Current.ChangeQuantity(productId, quantity);
            return CurrentUmbracoPage();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveFromCart(int productId, string returnUrl)
        {
            ShoppingCartContext.Current.RemoveItem(productId);
            return CurrentUmbracoPage();
        }

        #endregion

        #region Helpers

        private void AddProductToCart(int productId, int quantity)
        {
            var product = ProductService.GetById(productId);

            if (product != null)
            {
                var cartItem = Mapper.Map<CartItem>(product);
                cartItem.Quantity = quantity;
                ShoppingCartContext.Current.AddItemToCart(cartItem);
            }
        }

        private IEnumerable<T> GetCurrentShoppingCartItems<T>()
        {
            var products = Mapper.Map<IEnumerable<T>>(GetProductsInCart()).ToList();

            if ((typeof(T).IsAssignableFrom(typeof(ShoppingCartProductPreviewViewModel))))
            {
                products.ForEach(x =>
                {
                    var shoppingCartProductPreviewViewModel = x as ShoppingCartProductPreviewViewModel;
                    if (shoppingCartProductPreviewViewModel != null)
                    {
                        shoppingCartProductPreviewViewModel.Quantity =
                            ShoppingCartContext.Current.CartItems.Single(
                                y => y.ProductId == shoppingCartProductPreviewViewModel.Id).Quantity;
                    }

                });
            }

            return products;
        }

        private IEnumerable<Product> GetProductsInCart()
        {
            return ShoppingCartContext.Current
                .CartItems
                .Select(x => x.ProductId)
                .Select(y => ProductService.GetById(y))
                .ToList();
        }
        
        #endregion
    }
}