using HtmlAgilityPack;
using System.Text.RegularExpressions;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace LunaBuildCreator.Src
{
    public class Cleaner
    {
        public async Task Clean(HtmlAgilityPack.HtmlDocument source)
        {
            await Task.Run(() =>
            {
                var scriptNodes = source.DocumentNode.SelectNodes("//script");
                if (scriptNodes != null)
                {
                    scriptNodes[0].Remove();
                }
                if (scriptNodes != null)
                {
                    scriptNodes[1].Remove();
                }

                var styleNodes = source.DocumentNode.SelectNodes("//link[@rel='stylesheet'] | //style");
                if (styleNodes != null)
                {
                    styleNodes[0].Remove();
                }

                string classToRemove = "preloader";
                var divNodes = source.DocumentNode.SelectNodes("//div[contains(@class, '" + classToRemove + "')]");

                if (divNodes != null)
                {
                    divNodes[0].Remove();
                }

                if (scriptNodes != null)
                {
                    scriptNodes[scriptNodes.Count - 2].Remove();
                    scriptNodes[scriptNodes.Count - 1].Remove();
                }

            });
            await RemoveHttpsLinks(source);

        }

        private async Task RemoveHttpsLinks(HtmlDocument document)
        {
            await Task.Run(() =>
            {
                var regex = new Regex(@"https://[^\s'""<>()]*", RegexOptions.Compiled);

                var textNodes = document.DocumentNode.Descendants()
                    .Where(n => n.NodeType == HtmlNodeType.Text).ToList();

                foreach (var textNode in textNodes)
                {
                    if (textNode.InnerText.Contains("iosLink:") || textNode.InnerText.Contains("androidLink:"))
                    {
                        continue;
                    }

                    // Заменяем все другие ссылки
                    string cleanedText = regex.Replace(textNode.InnerText, string.Empty);
                    textNode.InnerHtml = cleanedText;
                }
            });
        }
    }
}
