using System;
using System.Windows;
using BookstoreLogic.Services;

namespace WPFBookstore
{
    public partial class AddBookWindow : Window
    {
        ModificationResultWindow addResultWindow = null;

        BookstoreDataService libService;
        Action windowCallback;

        public AddBookWindow(BookstoreDataService service, Action callback)
        {
            libService = service;
            windowCallback = callback;

            InitializeComponent();
            SetupWindow();
        }
    }
}
