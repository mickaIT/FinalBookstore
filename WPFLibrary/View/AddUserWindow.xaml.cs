using System;
using System.Windows;
using LibraryLogic.Services;

namespace WPFLibrary
{
    public partial class AddUserWindow : Window
    {
        ModificationResultWindow addResultWindow = null;

        LibraryDataService libService;
        Action windowCallback;

        public AddUserWindow(LibraryDataService service, Action callback)
        {
            libService = service;
            windowCallback = callback;

            InitializeComponent();
            SetupWindow();
        }
    }
}
