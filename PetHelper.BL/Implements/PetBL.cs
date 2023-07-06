using Newtonsoft.Json;
using PetHelper.BL.Interface;
using PetHelper.Core.Interfaces;
using PetHelper.Model;
using System.Buffers.Text;
using System.Net.WebSockets;

namespace PetHelper.BL.Implements
{
    public class PetBL : BaseBL, IPetBL
    {
        public PetBL(IBaseService databaseService) : base(databaseService)
        {
        }

        public override async Task BeforeSaveAsync(BaseModel entity)
        {
            await base.BeforeSaveAsync(entity);
            var pet = entity as Pet;

            var fileName = convertToUnSign(pet.PetName);

            if (!string.IsNullOrEmpty(pet.PetAvatar))
            {
                var avtUrl = $@"C:\Users\uxumi\Documents\Code\Vue\DA\PetHelper\vueapp\src\assets\images\{string.Concat(fileName, "_avatar")}.png";
                System.IO.FileInfo file = new System.IO.FileInfo(avtUrl);
                file.Directory.Create();
                pet.PetAvatar = pet.PetAvatar.Replace("data:image/png;base64,", "");
                await File.WriteAllBytesAsync(avtUrl, Convert.FromBase64String(pet.PetAvatar));
                pet.PetAvatar = fileName + "_avatar.png";
            }

            if (!string.IsNullOrEmpty(pet.Images))
            {
                var lstImg = pet.Images.Split('\\', StringSplitOptions.RemoveEmptyEntries).ToList();
                pet.Images = "";
                int i = 1;
                var lstImgName = new List<string>();
                foreach (var img in lstImg)
                {
                    var imgUrl = $@"C:\Users\uxumi\Documents\Code\Vue\DA\PetHelper\vueapp\src\assets\images\{string.Concat(fileName, "_", i)}.png";
                    System.IO.FileInfo file = new System.IO.FileInfo(imgUrl);
                    file.Directory.Create();
                    var imgBase64 = img.Replace("data:image/png;base64,", "");
                    imgBase64 = imgBase64.Replace("data:image/jpeg;base64,", "");
                    await File.WriteAllBytesAsync(imgUrl, Convert.FromBase64String(imgBase64));
                    lstImgName.Add($"{string.Concat(fileName, "_", i)}.png");
                    i++;
                }
                pet.Images = string.Join(';', lstImgName);
            }
        }
    }
}
