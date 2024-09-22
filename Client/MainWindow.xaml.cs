using System.Net.Http;
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
    HttpClient httpClient;

    public MainWindow()
    {
        InitializeComponent();
        httpClient = new HttpClient();
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

        string id = Id.Text;
        Task.Run(async () =>
        {
            var response = await httpClient.DeleteAsync($"http://localhost:27001/delete/{id}");//isdyr

        });
    }
    private void GetAllfunc()
    {
        Task.Run(() =>
        {

           
        });
    }



}