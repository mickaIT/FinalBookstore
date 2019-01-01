using System;
using System.Windows;
using LibraryLogic.Data;
using LibraryLogic.Services;

namespace WPFLibrary
{
    public partial class EditBookWindow : Window
    {
        ModificationResultWindow editResultWindow = null;

        LibraryDataService libService;
        Action windowCallback;

        public EditBookWindow(LibraryDataService service, Book book, Action callback)
        {
            libService = service;
            windowCallback = callback;

            InitializeComponent();
            SetupWindow(book);
        }
    }
}
