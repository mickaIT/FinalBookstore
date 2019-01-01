using System;
using System.Windows;
using LibraryLogic.Services;

namespace WPFLibrary
{
    public partial class AddBookWindow : Window
    {
        ModificationResultWindow addResultWindow = null;

        LibraryDataService libService;
        Action windowCallback;

        public AddBookWindow(LibraryDataService service, Action callback)
        {
            libService = service;
            windowCallback = callback;

            InitializeComponent();
            SetupWindow();
        }
    }
}
