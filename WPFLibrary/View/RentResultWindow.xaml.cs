using System.Windows;

namespace WPFBookstore
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
        CannotSell,
    }
}
