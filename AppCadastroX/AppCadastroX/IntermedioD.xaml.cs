using AppCadastroX.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCadastroX
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IntermedioD : ContentPage
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

        //Cria uma instância de HttpClient (Microsoft.Net.Http)
        private readonly HttpClient _client = new HttpClient(insecureHandler);

        public IntermedioD()
        {
            InitializeComponent();
        }

        private async void FindUserD(object sender, EventArgs e)
        {

            string content = await _client.GetStringAsync(Url);
            // Deserializa ou converte a string JSON em uma coleção de Post
            List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(content);

            if (Find == null)
            {
                return;
            }

            foreach (Post post in posts)
            {
                if (this.Find.Text == post._firstName)
                {
                    await Navigation.PushAsync(new Deletar(post._id));
                }
            }

            return;

        }
    }
}