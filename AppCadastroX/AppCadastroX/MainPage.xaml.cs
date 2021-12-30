using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;
using AppCadastroX.Model;

namespace AppCadastroX
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        //Esta é a url publica que oferece o serviço
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

        //Cria uma instância de HttpClient (Microsoft.Net.Http)
        private readonly HttpClient _client = new HttpClient(insecureHandler);
        //Definimos uma coleção do tipo ObservableCollection que é coleção de dados dinâmica
        //e que permite atualizar o estado da UI em temporeal quando os dados forem alterados
        public ObservableCollection<Post> _posts;

        public static string alfanumericoAleatorio(int tamanho)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, tamanho)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        protected override async void OnAppearing()
        {
            // Envia uma requisição GET para a url especificada e retorna
            // o Response (Body) como uma string em uma operação assíncrona
            string content = await _client.GetStringAsync(Url);
            // Deserializa ou converte a string JSON em uma coleção de Post
            List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(content);
            // Convertendo a Lista para uma ObservalbleCollection de Post
            _posts = new ObservableCollection<Post>(posts);
            //Atribui a ObservableCollection ao Listview lvwDados 
            lvwDados.ItemsSource = _posts;
            base.OnAppearing();
        }

        /// <summary>
        /// Evento Click do botão Incluir. 
        /// Envia uma requisição POST incluindo um objeto Post no servidor e na coleção
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnAdd(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Cadastro());
        }

        /// <summary>
        /// Evento Click do botão Atualizar
        /// Envia uma requisição PUT para atualizar o primieiro objeto Post
        /// no servidor e na coleção
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnUpdate(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Intermedio());
        }

        /// <summary>
        /// Evento Click do botão Deletar
        /// Envia uma requisição DELETE removendo o primeiro objeto post
        /// no servidor e na coleção
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnDelete(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new IntermedioD());
        }

    }
}
