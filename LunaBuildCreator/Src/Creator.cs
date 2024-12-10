using HtmlAgilityPack;
using System.IO;
using System.IO.Compression;

namespace LunaBuildCreator.Src
{
    public class Creator
    {
        public async Task CreateBuild(string savePath, string baseHtmlPath, string buildName, string playableName, string base64Image, string realBuildName = "")
        {
            string splash = $"<img style=\"width: 30vw; position: absolute; top: 0; left: 0; right: 0; bottom: 0; margin: auto;\" src=\"{base64Image}\" alt=\"\">";
            string index = "index.html";
            if (realBuildName == "VU")
            {
                index = "ad.html";
            }

            await Task.Run(() =>
            {
                HtmlAgilityPack.HtmlDocument source = new HtmlAgilityPack.HtmlDocument();
                source.Load(baseHtmlPath);

                string workingDirectory = Environment.CurrentDirectory + "/BuildRef/" + buildName + "/index.html";
                string configDirectory = Environment.CurrentDirectory + "/BuildRef/config.json";
                string content = File.ReadAllText(workingDirectory);
                HtmlNode newScriptNode = HtmlNode.CreateNode(content);
                HtmlNode splachNode = HtmlNode.CreateNode(splash);

                string file;
                var bodyNode = source.DocumentNode.SelectSingleNode("//body");
                if (bodyNode != null)
                {
                    bodyNode.AppendChild(newScriptNode);
                    bodyNode.PrependChild(splachNode);
                }
                

                string folderName = "";
                if (realBuildName == string.Empty)
                {
                    folderName = savePath + "\\" + playableName + buildName;
                    file = folderName + "\\" + index;
                }
                else
                {
                    folderName = savePath + "\\" + playableName + realBuildName;
                    file = folderName + "\\" + index;
                }

                Directory.CreateDirectory(folderName);

                if (buildName == "BG")
                {
                    string headScript = "\n<script src=\"https://static-web.likeevideo.com/as/common-static/big-data/dsp-public/bgy-mraid-sdk.js\">";
                    var headNode = source.DocumentNode.SelectSingleNode("//head");
                    var headScriptNode = HtmlNode.CreateNode(headScript);
                    headNode.AppendChild(headScriptNode);
                    string destinationFile = Path.Combine(folderName, Path.GetFileName(configDirectory));
                    try
                    {
                        if (File.Exists(configDirectory))
                        {
                            // Копируем файл в новую папку
                            File.Copy(configDirectory, destinationFile, overwrite: true);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при копировании конфига " + ex);
                    }
                }

                source.Save(file);

                ZipFolder(folderName);
            });

        }

        private void ZipFolder(string folderToZip)
        {
            if (Directory.Exists(folderToZip))
            {
                string directoryName = Path.GetFileName(folderToZip);
                string zipFilePath = Path.Combine(Path.GetDirectoryName(folderToZip), directoryName + ".zip");

                try
                {
                    ZipFile.CreateFromDirectory(folderToZip, zipFilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при архивировании папки '{directoryName}': {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show($"Папка '{folderToZip}' не найдена.");
            }
        }
    }
}
