using Umbraco.Core.Models;
using Umbraco.Web.Models;

namespace Training.Umbraco.WebSite.UI.ViewModels
{
    //public class ProductViewModel :  RenderModel           //ProductPreviewViewModel
    //{
    //    //implementation for inheritance RenderModel
    //    public ProductViewModel(IPublishedContent content) : base(content)
    //    {
    //    }

    //    //add properties from product basic view model
    //    public int Id { get; set; }
    //    public string Artist { get; set; }
    //    public string Name { get; set; }
    //    public decimal Price { get; set; }

    //    //from Product View Model
    //    public string Description { get; set; }
    //    public string CategoryName { get; set; }

    //    //from Product Preview View Model
    //    public string Summary { get; set; }
    //    public int Rates { get; set; }

    //    //new added - for Umbraco Image cropping and uses the path url
    //    public string ImageUrl { get; set; }
    //}


    //this would be a better solution then the above
    ////added IRenderModel interface to add Content without partial view
    public class ProductViewModel : ProductPreviewViewModel, IRenderModel
    {
        public IPublishedContent Content { get; }

        public ProductViewModel(IPublishedContent content)
        {
            Content = content;
        }
        public string Description { get; set; }

        public string CategoryName { get; set; }

        public string DetailsImageUrl { get; set; }

        public string ImageUrl { get; set; }
    }
}