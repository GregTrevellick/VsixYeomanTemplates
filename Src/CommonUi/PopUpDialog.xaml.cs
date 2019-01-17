using CommonYo;
using CommonYo.Dtos;
using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace CommonUi
{
    public partial class PopUpDialog : DialogWindow
    {
        private string _extensionName;
        private string _generatorName;
        private YoProcessor _yoProcessor;
        private FileSystemDto _dto;
       
        public PopUpDialog(string extensionName, string generatorName, Dictionary<string, string> replacementsDictionary)
        {
            _extensionName = extensionName;
            _generatorName = generatorName;
            _yoProcessor = new YoProcessor(_generatorName);
            _dto = _yoProcessor.Initialise(replacementsDictionary);

            InitializeComponent();
            InitializePopUpDialog();
        }

        private void InitializePopUpDialog()
        {
            AppTextBlock.Text = DialogHelper.GetLabelText(_dto.SolutionDirectory, _dto.TempDirectory, _generatorName, _dto.RegularProjectName);
            BtnOkay.Content = DialogHelper.Okay;
            BtnOkay.Focus();
            HasMaximizeButton = true;
            HasMinimizeButton = true;
            ResizeMode = ResizeMode.CanResize;
            SizeToContent = SizeToContent.WidthAndHeight;
            Title = _extensionName;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void BtnOkay_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                using (_yoProcessor)
                {
                    _yoProcessor.Generate();
                }
            }
            catch (Exception ex)
            {
                _yoProcessor.ShowMessageBoxError($"{_yoProcessor.ProjectNotCreated}{_yoProcessor.LineBreak}{ex.ToString()}.");
            }

            Close();
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            Close(); 
        }

        /// <summary>
        /// Invoked by Close(); (i.e Ok or Cancel clicked) or when top RH corner 'X' clicked
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            try
            {
                _yoProcessor.ArchiveRegularProject(_dto.SolutionDirectory, _dto.TempDirectory);
            }
            catch (Exception ex)
            {
                // Most likely (although unlikely) is that the solution directory we're trying to archive doesn't yet exist, or not enough disc space
                MessageBox.Show(ex.ToString());
            }
        }
    }
}

//using System;
//using System.Windows.Media.Imaging;
//private void SetWindowIcon(DialogWindow window)
//{
//    var iconUri = GetIconUri();
//    window.Icon = new BitmapImage(iconUri);
//}
//private Uri GetIconUri()
//{
//    //var assemblyInfo = System.Reflection.Assembly.GetExecutingAssembly();
//    //var assemblyName = assemblyInfo.FullName;
//    var assemblyName = "CommonUi";
//    var packUri = $"pack://application:,,,/{assemblyName};component/vsixextensionicon_90x90_resource_bb6_icon.ico";
//    return new Uri(packUri, UriKind.RelativeOrAbsolute);
//}
