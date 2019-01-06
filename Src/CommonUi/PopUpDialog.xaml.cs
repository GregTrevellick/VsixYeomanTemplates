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
        private string _generatorName;
        private YoProcessor _yoProcessor;
        private FileSystemDto _dto;

        public PopUpDialog(string generatorName, Dictionary<string, string> replacementsDictionary)
        {
            _generatorName = generatorName;
            _yoProcessor = new YoProcessor(_generatorName);
            _dto = _yoProcessor.Initialise(replacementsDictionary);

            InitializeComponent();
            InitializePopUpDialog();
        }

        private void InitializePopUpDialog()
        {
            AppTextBlock.Text = DialogHelper.GetLabelText(_dto.SolutionDirectory, _dto.TempDirectory, _generatorName, _dto.RegularProjectName);
            BtnOkay.Focus();
            HasMaximizeButton = true;
            HasMinimizeButton = true;
            ResizeMode = ResizeMode.CanResize;
            SizeToContent = SizeToContent.WidthAndHeight;
            Title = _generatorName;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void BtnOkay_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _yoProcessor.Generate();
                //as soon as new project dialog 'go' clicked the wizard is invoked which does zero substitution & creates the NA1 folder & simultaneously opens the yo dialog. when user clicks 'ok' on my yo dialog we get here, and the yo.bat is run. whilst yo.bat is running async we continue to call Close() and can be all but certain the NA1 folder exists  
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            Close();
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)//corner x to also do archgive
        {
            Close(); 
        }

        /// <summary>
        /// Invoked by Close();
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
