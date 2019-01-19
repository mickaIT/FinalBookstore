using System;
using System.Windows;
using BookstoreLogic.Data;
using BookstoreLogic.Services;

namespace WPFBookstore
{
    public partial class EditBookWindow : Window
    {


        public EditBookWindow(BookstoreDataService service, Book book, Action callback)
        {


            InitializeComponent();
        }
    }
}
