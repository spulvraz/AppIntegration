using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Training.Umbraco.WebSite.UI.ViewModels;
using Umbraco.Web.Models;

namespace Training.Umbraco.WebSite.UI.Controllers
{
    public class ProductController : StoreRenderMvcController
    {
        //
        public ActionResult Details(RenderModel model, int id)
        {
            if (model == null || model.Content == null || id < 0)
            {
                //never fires as a model is always passed
                return HttpNotFound();
            }

            onlineStoreDb.Product product = ProductService.GetById(id);
            ProductViewModel vmodel = new ProductViewModel(model.Content);

            //AutoMapper
            Mapper.Map(product, vmodel);
            return View(vmodel);
        }
    }
}