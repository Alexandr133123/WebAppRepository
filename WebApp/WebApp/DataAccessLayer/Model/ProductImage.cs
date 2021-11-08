using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.DataAccessLayer.Model
{
    public class ProductImage
    {
        public ProductImage(int pK_ProductId, string fileName)
        {
            PFK_ImageId = pK_ProductId;
            ImageRelativePath = fileName;
        }
        public ProductImage()
        {
        }

        public int PFK_ImageId { get; set; }
        public string ImageRelativePath { get; set; }
        public Product Product { get; set; }
    }
}
