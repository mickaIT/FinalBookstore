using System;
using System.Windows;
using LibraryLogic.Data;
using LibraryLogic.Services;

namespace WPFLibrary
{
    public partial class EditUserWindow : Window
    {
        ModificationResultWindow editResultWindow = null;

        LibraryDataService libService;
        Action windowCallback;

        public EditUserWindow(LibraryDataService service, User user, Action callback)
        {
            libService = service;
            windowCallback = callback;

            InitializeComponent();
            SetupWindow(user);
        }
    }
}
