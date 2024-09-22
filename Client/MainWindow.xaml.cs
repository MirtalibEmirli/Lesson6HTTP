using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
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


public partial class MainWindow : Window, INotifyPropertyChanged
{
    HttpClient httpClient;

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string ?
        name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
    private User _selectedUser;
    public User selectedUser
    {
        get { return _selectedUser; }
        set
        {
            _selectedUser = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<User> _userList;
    public ObservableCollection<User> userList
    {
        get { return _userList; }
        set
        {
            _userList = value;
            OnPropertyChanged();
        }
    }
    public MainWindow()
    {
        InitializeComponent();
        httpClient = new HttpClient();
        selectedUser = new User();
        userList = new ObservableCollection<User>();
        DataContext = this;
        GetAllfunc();
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
        Task.Run(async () =>
        {
            try
            {
                int id = 0, age = 0;
                string name = "", surname = "";


                Application.Current.Dispatcher.Invoke(() =>
                {
                    id = int.Parse(Id.Text);
                    age = int.Parse(Age.Text);
                    name = Name.Text;
                    surname = Surname.Text;
                });


                var user = new User
                {
                    Id = id,
                    Name = name,
                    SurName = surname,
                    Age = age
                };


                using var json = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");


                var response = await httpClient.PostAsync($"http://localhost:27001/add", json);
                response.EnsureSuccessStatusCode();
                var content = response.Content;
                var responsecontent = await content.ReadAsStringAsync();

                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageBox.Show(responsecontent);
                });
            }
            catch (Exception ex)
            {

                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                });
            }
        });
    }

    private void Deletefunc()
    {

        string id = Id.Text;
        Task.Run(async () =>
        {
            var response = await httpClient.DeleteAsync($"http://localhost:27001/delete/{id}");
            var content = response.Content;
            var js = await content.ReadAsStringAsync();
            Application.Current.Dispatcher.Invoke(() => {   MessageBox.Show(js); });    

        });
    }
    private void GetAllfunc()
    {
        Task.Run(async () =>
        {
            try
            {
                var response = await httpClient.GetAsync($"http://localhost:27001/get");
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();
                using (var reader = new StreamReader(stream, encoding: Encoding.UTF8))
                {
                    var data = await reader.ReadToEndAsync();
                    if (!string.IsNullOrEmpty(data))
                    {
                        var users = JsonSerializer.Deserialize<List<User>>(data);
                        if (users is not null)
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                userList = new ObservableCollection<User>(users);
                            });
                        }

                    }
                }



            }
            catch (Exception ex)
            {

                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageBox.Show(ex.Message);
                });
            }


        });
    }



}