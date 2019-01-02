using System.Windows;

namespace WPFBookstore
{
    public partial class BookReturnResultWindow : Window
    {
        public BookReturnResultWindow(bool success)
        {
            InitializeComponent();
            DisplayMessage(success);
        }
    }
}
