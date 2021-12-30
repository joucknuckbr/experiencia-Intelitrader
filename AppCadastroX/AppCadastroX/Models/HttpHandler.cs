using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace AppCadastroX.Models
{
    public interface IHTTPClientHandlerCreationService
    {
        HttpClientHandler GetInsecureHandler();
    }
}