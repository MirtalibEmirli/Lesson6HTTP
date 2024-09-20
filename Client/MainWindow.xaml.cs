using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client;


public partial class MainWindow : Window
{


    public MainWindow()
    {
        InitializeComponent();
    }

    private void Add_Click(object sender, RoutedEventArgs e)
    {
        Addfunc();
    }

    private void Get_Click(object sender, RoutedEventArgs e)
    {
        GetAllfunc();

    }

    private void Delete_Click(object sender, RoutedEventArgs e)
    {
        Deletefunc();
    }

    private void Addfunc()
    {
        Task.Run(() =>
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MessageBox.Show("A");
            });

        });
    }
    private void Deletefunc()
    {
        Task.Run(() =>
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MessageBox.Show("A");
            });

        });
    }
    private void GetAllfunc()
    {
        Task.Run(() =>
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MessageBox.Show("GetAllfunc");
            });

        });
    }

}