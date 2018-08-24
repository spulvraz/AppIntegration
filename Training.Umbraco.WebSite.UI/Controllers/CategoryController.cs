using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Training.Services.OnlineStore.Models;
using Training.Umbraco.WebSite.UI.Models;
using Training.Umbraco.WebSite.UI.ViewModels;
using Umbraco.Web.Models;

namespace Training.Umbraco.WebSite.UI.Controllers
{
    public class CategoryController : StoreRenderMvcController
    {
        public ActionResult ProductsByCategory(RenderModel model, int id, int page, ProductSortBy sortBy = ProductSortBy.RateHighest)
        {
            int totalItems;
            var category = this.CategoryService.GetById(id);
            var filter = new ProductSearchFilter() { CategoryId = id, SortBy = sortBy };
            var products = this.ProductService.GetAll(filter, out totalItems, page);

            var vm = new CategoryViewModel(model.Content);
            Mapper.Map(category, vm);

            var pager = new Pager<ProductPreviewViewModel>(
                Mapper.Map<IEnumerable<ProductPreviewViewModel>>(products),
                totalItems,
                page,
                //urlFormat: string.Format("/category/{0}/{0}", model.Content.Id));
                urlFormat: string.Format("/category/{0}", model.Content.Id));

            vm.Products = new ListOfProductsPreviewViewModel()
            {
                List = pager,
                ListTitle = model.Content.Name
            };

            return View(vm);
        }
    }
}