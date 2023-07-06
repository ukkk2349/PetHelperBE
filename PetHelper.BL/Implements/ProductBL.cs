using Newtonsoft.Json;
using PetHelper.BL.Interface;
using PetHelper.Core.Interfaces;
using PetHelper.Model;
using PetHelper.Model.Models;

namespace PetHelper.BL.Implements
{
    public class ProductBL : BaseBL, IProductBL
    {
        public ProductBL(IBaseService databaseService) : base(databaseService)
        {
        }

        public override async Task BeforeSaveAsync(BaseModel entity)
        {
            await base.BeforeSaveAsync(entity);
            var product = entity as Product;

            var fileName = convertToUnSign(product.ProductName);

            if (!string.IsNullOrEmpty(product.ProductAvatar))
            {
                var avtUrl = $@"C:\Users\uxumi\Documents\Code\Vue\DA\PetHelper\vueapp\src\assets\images\product\{string.Concat(fileName, "_avatar")}.png";
                System.IO.FileInfo file = new System.IO.FileInfo(avtUrl);
                file.Directory.Create();
                product.ProductAvatar = product.ProductAvatar.Replace("data:image/png;base64,", "");
                await File.WriteAllBytesAsync(avtUrl, Convert.FromBase64String(product.ProductAvatar));
                product.ProductAvatar = fileName + "_avatar.png";
            }

            if (!string.IsNullOrEmpty(product.Images))
            {
                var lstImg = product.Images.Split('\\', StringSplitOptions.RemoveEmptyEntries).ToList();
                product.Images = "";
                int i = 1;
                var lstImgName = new List<string>();
                foreach (var img in lstImg)
                {
                    var imgUrl = $@"C:\Users\uxumi\Documents\Code\Vue\DA\PetHelper\vueapp\src\assets\images\product\{string.Concat(fileName, "_", i)}.png";
                    System.IO.FileInfo file = new System.IO.FileInfo(imgUrl);
                    file.Directory.Create();
                    var imgBase64 = img.Replace("data:image/png;base64,", "");
                    imgBase64 = imgBase64.Replace("data:image/jpeg;base64,", "");
                    await File.WriteAllBytesAsync(imgUrl, Convert.FromBase64String(imgBase64));
                    lstImgName.Add($"{string.Concat(fileName, "_", i)}.png");
                    i++;
                }
                product.Images = string.Join(';', lstImgName);
            }
        }
    }
}
