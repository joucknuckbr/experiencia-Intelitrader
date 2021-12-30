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
    public partial class Atualizar : ContentPage
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

        private readonly HttpClient _client = new HttpClient(insecureHandler);

        Guid id;

        private ObservableCollection<Post> _posts;

        public Atualizar(Guid ID)
        {
            id = ID;
            InitializeComponent();
        }

        private async void Put(object sender, EventArgs e)
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
                    break;
                }
            }

            // Atribui o segundo objeto Post da Coleção para uma nova instância de Post
            // Anexa a string [atualizado] ao valor atual da propriedade FirstName
            post.FirstName = this.FirstName.Text;
            post.SurName = this.Surname.Text;
            post.Age = Int32.Parse(this.Age.Text);
            // Serializa ou converte o post criado em uma string JSON
            string content = JsonConvert.SerializeObject(post);
            // Envia uma requisição PUT para a uri definida como uma operação assincrona
            await _client.PutAsync(Url + "/" + post.Id, new StringContent(content, Encoding.UTF8, "application/json"));

            await Navigation.PopAsync();

        }


    }
}