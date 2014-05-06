using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Awesomium;
using Awesomium.Core;

namespace TheE_Parser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AwesomiumBrowserTools awesomeTools = new AwesomiumBrowserTools();
        HAPTools hapTools;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            hapTools = new HAPTools(awwebDisplay);
            awesomeTools.Client = awwebPicker;
            awesomeTools.Client.DocumentReady += Client_DocumentReady;
            awesomeTools.Callback += JSCallback_Handler;
            hapTools.displayTools.Callback += JSCallback_Handler;
            awesomeTools.Client.Source = new Uri("http://google.com/");
        }

        void Client_DocumentReady(object sender, UrlEventArgs e)
        {
            tbxUrl.Text = awesomeTools.Client.Source.ToString();
        }

        void JSCallback_Handler(object source, JSCallbackArgs e)
        {
            tbxXPath.Text = e.TryGetResult;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            awwebPicker.Dispose();
            WebCore.Shutdown();
        }

        private void tbxUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
                btnNavigate_Click(sender, null);
        }

        private void btnNavigate_Click(object sender, RoutedEventArgs e)
        {
            if (!tbxUrl.Text.StartsWith("http://") || !tbxUrl.Text.StartsWith("https://"))
                tbxUrl.Text = "http://" + tbxUrl.Text;

            awwebPicker.Source = new Uri(tbxUrl.Text);
        }

        private void tbxXPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            int count = hapTools.TestXPath(awesomeTools.OuterHtml, tbxXPath.Text);
            
            if (count != -1)
            {
                cmbbxXPathIndex.Visibility = Visibility.Visible;
               XPathIndicatorImage.Source = new BitmapImage(new Uri("/Resources/icon_bad2.png", UriKind.Relative));
                cmbbxXPathIndex.Items.Clear();
                for (int i = 0; i < count; i++)
                    cmbbxXPathIndex.Items.Add(i + 1);
                cmbbxXPathIndex.SelectedIndex = 0;
            }
            else
            {
                XPathIndicatorImage.Source = new BitmapImage(new Uri("/Resources/icon_bad.png", UriKind.Relative));
                cmbbxXPathIndex.Visibility = Visibility.Hidden;
            }
        }

        private void cmbbxXPathIndex_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            hapTools.DisplayXPath(cmbbxXPathIndex.SelectedIndex);
            rtbxSourceCode.Document.Blocks.Clear();
            rtbxSourceCode.AppendText(hapTools.NodeOuterHtml(cmbbxXPathIndex.SelectedIndex));
        }

        private void btnGetParent_Click(object sender, RoutedEventArgs e)
        {
            awesomeTools.ParentXPath();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            awesomeTools.Client.GoBack();
        }

       
       
    }
}
