using System;
using System.Windows;
using LibraryLogic.Data;
using LibraryLogic.Services;

namespace WPFLibrary
{
    public partial class BookRentingWindow : Window
    {
        LibraryDataService libService;
        Action<bool> windowCallback;

        public BookRentingWindow(LibraryDataService service, Book book, Action<bool> callback)
        {
            libService = service;
            windowCallback = callback;

            InitializeComponent();
            SetupWindow(book);
        }
    }
}
