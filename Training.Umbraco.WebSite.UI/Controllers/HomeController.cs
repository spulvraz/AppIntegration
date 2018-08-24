using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Training.Umbraco.WebSite.UI.ViewModels;

namespace Training.Umbraco.WebSite.UI.Controllers
{
    public class HomeController : StoreSurfaceController
    {
        //render album data - returned the partialview
        [ChildActionOnly]
        public ActionResult FeaturedProducts(int count = 3)
        {
            //this will be displayed only as a child 
            IEnumerable<onlineStoreDb.Product> featuresProducts = ProductService.GetFeatured(count);
            IEnumerable<ProductPreviewViewModel> items = Mapper.Map<IEnumerable<ProductPreviewViewModel>>(featuresProducts);
            return PartialView("_FeaturedProducts", items);         //defined in the view shared folder
        }

        //top selling albums
        [ChildActionOnly]
        public ActionResult TopSelling(int count = 6)
        {
            IEnumerable<onlineStoreDb.Product> topSelling = ProductService.GetTopSelling(count);
            IEnumerable<ProductPreviewViewModel> items = Mapper.Map<IEnumerable<ProductPreviewViewModel>>(topSelling);
            items = items.OrderByDescending(x => x.Rates);              //IOrderedIEnum...
            return PartialView("_HighlightProducts", items);            //defined in the view shared folder
        }
    }
}