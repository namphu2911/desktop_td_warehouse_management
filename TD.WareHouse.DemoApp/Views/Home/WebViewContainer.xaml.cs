using IdentityModel.OidcClient.Browser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TD.WareHouse.DemoApp.Views.Home
{
    /// <summary>
    /// Interaction logic for WebViewContainer.xaml
    /// </summary>
    public partial class WebViewContainer : UserControl
    {
        public static readonly DependencyProperty BrowserPropertyProperty = DependencyProperty
            .Register("Browser", typeof(IBrowser), typeof(WebViewContainer), new PropertyMetadata());

        public IBrowser Browser
        {
            get
            {
                return (IBrowser)GetValue(BrowserPropertyProperty);
            }
            set
            {
                SetValue(BrowserPropertyProperty, value);
            }
        }

        public WebViewContainer()
        {
            InitializeComponent();
        }

        public void InitializeBrowser()
        {
            Browser = new WpfEmbeddedBrowser(webView);
        }
    }
}
