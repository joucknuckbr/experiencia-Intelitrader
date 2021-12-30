using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppCadastroX.Model
{
    public class Post : INotifyPropertyChanged
    {
        public Guid _id;
        public string _firstName;
        public string _surName;
        public int _age;
        public DateTime CreationDate { get; set; }

        [JsonProperty("id")]
        public Guid Id
        {
            get => _id;
            set
            {
                _id = value;
                //Notifica a sua View ou ViewModel que o valor que a propriedade
                //no modelo mudou e a view precisa ser atualizada
                OnPropertyChanged();
            }
        }

        //Mapeia o elemento firstName do serviço rest para o modelo
        [JsonProperty("firstName")]
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                //Notifica a sua View ou ViewModel que o valor que a propriedade
                //no modelo mudou e a view precisa ser atualizada
                OnPropertyChanged();
            }
        }

        [JsonProperty("surName")]
        public string SurName
        {
            get => _surName;
            set
            {
                _surName = value;
                OnPropertyChanged();
            }
        }

        [JsonProperty("age")]
        public int Age
        {
            get => _age;
            set
            {
                _age = value;
                OnPropertyChanged();
            }
        }

        //Método OnPropertyChanged()
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}