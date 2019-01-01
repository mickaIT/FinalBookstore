using System.Windows;

namespace WPFLibrary
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
