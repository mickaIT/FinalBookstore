using System;
using System.Windows;
using BookstoreLogic.Data;
using BookstoreLogic.Services;

namespace WPFBookstore
{
    public partial class BookSellingWindow : Window
    {
        BookstoreDataService libService;
        Action<bool> windowCallback;

        public BookSellingWindow(BookstoreDataService service, Book book, Action<bool> callback)
        {
            libService = service;
            windowCallback = callback;

            InitializeComponent();
            SetupWindow(book);
        }
    }
}
