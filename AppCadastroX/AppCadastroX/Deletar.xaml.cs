using AppCadastroX.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCadastroX
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Deletar : ContentPage
    {

        private const string Url = "http://172.29.144.1:8080/api/Users";

        public static HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }

        static HttpClientHandler insecureHandler = GetInsecureHandler();

        private ObservableCollection<Post> _posts;

        //Cria uma instância de HttpClient (Microsoft.Net.Http)
        private readonly HttpClient _client = new HttpClient(insecureHandler);

        Guid id;

        public Deletar(Guid ID)
        {
            id = ID;
            InitializeComponent();
        }

        private async void Delete(object sender, EventArgs e)
        {

            string context = await _client.GetStringAsync(Url);
            // Deserializa ou converte a string JSON em uma coleção de Post
            List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(context);
            // Convertendo a Lista para uma ObservalbleCollection de Post
            _posts = new ObservableCollection<Post>(posts);

            if (_posts.Count == 0)
            {
                return;
            }

            Post post = new Post();

            for (int i = 0; i < _posts.Count; i++)
            {
                if (id == _posts[i].Id)
                {
                    post = _posts[i];
                    Console.WriteLine(_posts[i].Id);
                    break;
                }
            }

            Console.WriteLine(post.Id);

            // Envia uma requisição DELETE para a Uri de forma assíncrona
            await _client.DeleteAsync(Url + "/" + post.Id);
            // Remove a primeira ocorrencia do objeto especificado na coleção Post
            _posts.Remove(post);

            await Navigation.PopAsync();

        }
    }
}