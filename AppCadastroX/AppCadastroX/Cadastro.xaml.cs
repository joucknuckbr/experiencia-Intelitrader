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
    public partial class Cadastro : ContentPage
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

        public Cadastro()
        {
            InitializeComponent();
        }

        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Add(object sender, EventArgs e)
        {
            // Cria uma instância de Post atribuindo um TimeStamp à propriedade Title
            Post post = new Post
            {
                Id = Guid.NewGuid(),
                FirstName = this.FirstName.Text,
                SurName = this.Surname.Text ,
                Age = Int32.Parse(this.Age.Text)
            };
            // Serializa ou converte o Post criado em uma string JSON
            string content = JsonConvert.SerializeObject(post);
            // Envia uma requisição POST para a Uri especificada em uma operação assíncrona
            // e com a codificação correta(utf8) e com o content type(application/json).
            await _client.PostAsync(Url, new StringContent(content, Encoding.UTF8, "application/json"));
            // Atualiza a UI inserindo um elemento na coleção

            await Navigation.PopAsync();
        }
    }
}