using SensorInterface.Commands;
using Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SensorInterface.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        public ObservableCollection<double> Temperaturas { get; set; }
        public ObservableCollection<double> Umidades { get; set; }

        public ICommand CarregarSensoresCommand { get; }

        public TempViewModel TempVM { get; }
   
        public MainViewModel()
        {
            Temperaturas = new ObservableCollection<double>();
            Umidades = new ObservableCollection<double>();

            CarregarSensoresCommand = new RelayCommand(CarregarSensores);
            TempVM = new TempViewModel();
        }

        private async void CarregarSensores()
        {
            var http = new HttpClient();
            List<SensorData>? dados = await http.GetFromJsonAsync<List<SensorData>>(
                "https://localhost:44379/api/v1/sensores");

            Temperaturas.Clear();
            Umidades.Clear();

            foreach (var valor in dados)
            {

                Temperaturas.Add(valor.Temperatura);
                Umidades.Add(valor.Umidade);
            }
        }

    }
}
