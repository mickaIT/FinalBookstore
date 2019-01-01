using System.Windows;

namespace WPFLibrary
{
    public partial class RentResultWindow : Window
    {
        public RentResultWindow(RentResultType type)
        {
            InitializeComponent();
            DisplayMessage(type);
        }
    }


    public enum RentResultType
    {
        RentSuccess,
        UserNotFound,
        CannotBorrow,
    }
}
