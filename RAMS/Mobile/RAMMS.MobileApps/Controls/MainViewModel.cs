using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace RAMMS.MobileApps.Controls
{
    public class Product
    {
        public string Name { get; set; }

        public bool IsVisible { get; set; }
    }
    class MainViewModel
    {
        private Product _oldProduct;

        public ObservableCollection<Product> Products { get; set; }

        public MainViewModel()
        {
            Products = new ObservableCollection<Product>
            {
                new Product
                {
                    Name = "Select Section",
                    IsVisible = false,
                },

            };
        }

        public void ShowOrHidePoducts(Product product)
        {
            if (_oldProduct == product)
            {
                // click twice on the same item will hide it
                product.IsVisible = !product.IsVisible;
                UpdateProducts(product);
            }
            else
            {
                if (_oldProduct != null)
                {
                    // hide previous selected item
                    _oldProduct.IsVisible = false;
                    UpdateProducts(_oldProduct);
                }
                // show selected item
                product.IsVisible = true;
                UpdateProducts(product);
            }

            _oldProduct = product;
        }

        private void UpdateProducts(Product product)
        {
            var index = Products.IndexOf(product);
            Products.Remove(product);
            Products.Insert(index, product);
        }
    }
}
