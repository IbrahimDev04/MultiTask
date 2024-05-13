﻿using MultiShop.ViewModels.Categories;
using MultiShop.ViewModels.Products;
using MultiShop.ViewModels.Sliders;

namespace MultiShop.SendModelView
{
    public class HomeData
    {
        public List<GetProductAdminVM> getProductVM {  get; set; }
        public List<GetSliderVM> getSliderVM { get; set; }

        public List<GetCategoryVM> getCategoryVM { get; set; }
    }
}
