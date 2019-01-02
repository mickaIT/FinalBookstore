using System.Windows;
using LibraryLogic.Services;

namespace WPFLibrary
{
    public partial class MainWindow : Window
    {
        public LibraryDataService libService { get; set; }
        LibraryListSelection currentLLSelection = LibraryListSelection.None;

        AddBookWindow addBookWindow = null;
        EditBookWindow editBookWindow = null;

        CannotRemoveErrorWindow removalErrorWindow = null;
        BookReturnResultWindow bookReturnResultWindow = null;
        BookRentingWindow bookRentingWindow = null;
        RentResultWindow rentResultWindow = null;


        public MainWindow()
        {
            libService = new LibraryDataService();

            InitializeComponent();
            UpdateButtons();
            ShowList();
        }
    }



    public enum LibraryListSelection
    {
        Books,
        Rentals,
        None,
    }
}
