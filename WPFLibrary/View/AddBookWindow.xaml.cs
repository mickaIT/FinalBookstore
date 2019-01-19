using System;
using System.Windows;
using BookstoreLogic.Services;

namespace WPFBookstore
{
    public partial class AddBookWindow : Window
    {

        public AddBookWindow(BookstoreDataService service, Action callback)
        {
            InitializeComponent();
        }
    }
}
