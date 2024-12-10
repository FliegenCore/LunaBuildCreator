using System.IO;

namespace LunaBuildCreator.Src
{
    public class ImageConverter
    {
        public async Task<string> ConvertToBase64(string path)
        {
            string code = "";
            await Task.Run(() =>
            {
                byte[] imageArray = File.ReadAllBytes(path);
                code = "data:image/png;base64," + Convert.ToBase64String(imageArray);
            });

            return code;
        }
    }
}
