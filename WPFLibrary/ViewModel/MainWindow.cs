using System.Windows;
using System.Windows.Controls;
using WPFBookstore.ViewModel;

namespace WPFBookstore
{
    public partial class MainWindow : Window
    {
        /* Callback Actions */

        // obejście wywoływania eventów przez calbacki wpf 
        // event wywołuje command wprost w viewmodelu
        // jest teoretycznie callback , ale wywołuje metodę z viewmodelu 

        private void DataContextContainer_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (DataContext as MainViewModel).DataContextContainerSelectionChangedCommand();
        }
    }
}
