using System.Windows;
using LibraryLogic.Data;

namespace WPFLibrary
{
    public partial class CannotRemoveErrorWindow : Window
    {
        public CannotRemoveErrorWindow(Book book)
        {
            InitializeComponent();
            DisplayErrorMessage(book);
        }

        public CannotRemoveErrorWindow(User user)
        {
            InitializeComponent();
            DisplayErrorMessage(user);
        }
    }
}
