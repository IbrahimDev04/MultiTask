using MultiShop.ViewModels.Products;

namespace MultiShop.SendModelView
{
    public class DetailData
    {
        public GetProductAdminVM GetProductVM { get; set; }

        public List<GetProductAdminVM> GetSameCategoryProductVM { get; set; }
    }
}
