using System;
using System.Windows;
using BookstoreLogic.Data;
using BookstoreLogic.Services;

namespace WPFBookstore
{
    public partial class EditBookWindow : Window
    {
        ModificationResultWindow editResultWindow = null;

        BookstoreDataService libService;
        Action windowCallback;

        public EditBookWindow(BookstoreDataService service, Book book, Action callback)
        {
            libService = service;
            windowCallback = callback;

            InitializeComponent();
            SetupWindow(book);
        }
    }
}
