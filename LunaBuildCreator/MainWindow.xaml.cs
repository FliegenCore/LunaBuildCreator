using System.Diagnostics;
using System.IO;
using System.Windows;
using LunaBuildCreator.Src;

namespace LunaBuildCreator
{
    public partial class MainWindow : Window
    {
        private string m_SavePath = "";
        private string m_HtmlPath = "";
        private HtmlAgilityPack.HtmlDocument m_BaseHtmlFile;
        private string m_ImagePath = "";

        public MainWindow()
        {
            InitializeComponent();
            OpenPathButton.Visibility = Visibility.Hidden;
        }

        private void Button_ChooseFile(object sender, RoutedEventArgs e)
        {
            ChooseHtmlFile();
        }

        private void ChooseHtmlFile()
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.Filter = "HTML Files (*.html)|*.html";
            fileDialog.Title = "Select html file(s)...";
            var success = fileDialog.ShowDialog();
            if (success == true)
            {
                var baseFile = fileDialog.FileName;

                try
                {
                    m_BaseHtmlFile = new HtmlAgilityPack.HtmlDocument();
                    m_BaseHtmlFile.Load(baseFile);
                    ChooseFileText.Text = baseFile;
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Ошибка при чтении файла: " + ex.Message);
                }
            }
        }
        private void Button_ChooseDirectory(object sender, RoutedEventArgs e)
        {
            ChooseSaveDirectory();
        }

        private void ChooseSaveDirectory()
        {
            FolderBrowserDialog fileDialog = new FolderBrowserDialog();

            var success = fileDialog.ShowDialog();

            if (success == System.Windows.Forms.DialogResult.OK)
            {
                var path = fileDialog.SelectedPath;
                try
                {
                    m_SavePath = path;
                    ChooseDirectoryText.Text = m_SavePath;
                    System.Windows.MessageBox.Show("Вы выбрали путь: " + path);
                    OpenPathButton.Visibility = Visibility.Visible;
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Ошибка при чтении файла: " + ex.Message);
                }
            }
        }

        private void Button_CreateBuilds(object sender, RoutedEventArgs e)
        {
            CreateBuilds();
        }

        private async void CreateBuilds()
        {
            if (m_BaseHtmlFile == null)
            {
                System.Windows.MessageBox.Show("Выберите html файл");
                return;
            }

            if (m_SavePath == string.Empty)
            {
                System.Windows.MessageBox.Show("Выберите путь для сохранения");
                return;
            }

            if (PlayableName.Text == string.Empty)
            {
                System.Windows.MessageBox.Show("Введите имя плеебла");
                return;
            }

            if (m_ImagePath == string.Empty)
            {
                System.Windows.MessageBox.Show("Добавьте иконку");
                return;
            }
            CreateBuildsButton.IsEnabled = false;
            Src.ImageConverter image = new Src.ImageConverter();
            string base64Image = await image.ConvertToBase64(m_ImagePath);

            Cleaner cleaner = new Cleaner();

            await cleaner.Clean(m_BaseHtmlFile);
            m_BaseHtmlFile.Save(m_SavePath + "\\index.html");
            m_HtmlPath = m_SavePath + "\\index.html";


            string workingDirectory = Directory.GetCurrentDirectory() + "/BuildRef/";
            string[] names = Directory.GetDirectories(workingDirectory);

            Creator creator = new Creator();

            foreach (string n in names)
            {
                var dir = new DirectoryInfo(n);
                if (n.Contains("AL"))
                {
                    await creator.CreateBuild(m_SavePath, m_HtmlPath, dir.Name, PlayableName.Text, base64Image, "AL");
                    await creator.CreateBuild(m_SavePath, m_HtmlPath, dir.Name, PlayableName.Text, base64Image, "UN");
                    await creator.CreateBuild(m_SavePath, m_HtmlPath, dir.Name, PlayableName.Text, base64Image, "VU");

                    continue;
                }

                await creator.CreateBuild(m_SavePath, m_HtmlPath, dir.Name, PlayableName.Text, base64Image);
            }

            System.Windows.MessageBox.Show("Билды созданы");
            CreateBuildsButton.IsEnabled = true;
        }

        private void Button_ChooseImage(object sender, RoutedEventArgs e)
        {
            ChooseImage();
        }

        private void ChooseImage()
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.Filter = "Image files|*.bmp;*.jpg;*.png;|All files|*.*";
            fileDialog.Title = "Select icon file(s)...";
            var success = fileDialog.ShowDialog();

            if (success == true)
            {
                var baseFile = fileDialog.FileName;
                try
                {
                    ImageDirection.Text = baseFile;
                    m_ImagePath = baseFile;
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Ошибка при чтении файла: " + ex.Message);
                }
            }
        }

        private void Button_OpenPath(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", m_SavePath);
        }

        private void Button_OpenAlUrl(object sender, RoutedEventArgs e)
        {
            OpenURL("https://p.applov.in/playablePreview?create=1&qr=1");
        }

        private void Button_OpenMvUrl(object sender, RoutedEventArgs e)
        {
            OpenURL("https://www.mindworks-creative.com/review/?testing_mod=ss&lang=en&timestamp=1689688211885&ext_data=%7B%22adv_id%22%3A112697%7D");
        }

        private void OpenURL(string url)
        {
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
    }
}