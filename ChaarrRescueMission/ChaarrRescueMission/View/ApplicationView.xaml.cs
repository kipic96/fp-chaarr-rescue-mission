using System.Windows;

namespace ChaarrRescueMission
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ApplicationView : Window
    {
        public ApplicationView()
        {
            InitializeComponent();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ActionsComboBox.SelectedIndex = 0;
            OrdersComboBox.SelectedIndex = 0;
            PlacesComboBox.SelectedIndex = 0;
            RepairingComboBox.SelectedIndex = 0;
            ProductionComboBox.SelectedIndex = 0;
        }
    }
}
