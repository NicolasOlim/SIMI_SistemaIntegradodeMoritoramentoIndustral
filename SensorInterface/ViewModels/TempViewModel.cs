using SensorInterface.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SensorInterface.ViewModels
{
    internal class TempViewModel:BaseViewModel
    {
        public ICommand MudarTemperaturaMaxCommand { get; }

        private string _novaTemperaturaTexto;
        public string NovaTemperaturaTexto
        {
            get => _novaTemperaturaTexto; 
            set
            {
                _novaTemperaturaTexto = value;
                OnPropertyChanged(nameof(NovaTemperaturaTexto));
            }
        }
        public TempViewModel()
        {
            MudarTemperaturaMaxCommand = new RelayCommand(MudarTemperaturaMax);
        }
        private async void MudarTemperaturaMax()
        {
          
            string textoFormatado = NovaTemperaturaTexto.Replace(",", ".");

            if (double.TryParse(textoFormatado, System.Globalization.CultureInfo.InvariantCulture, out double valor))
            {
                try
                {
                    using var http = new HttpClient();


                    string urlDaApi = $"https://localhost:44379/api/v1/sensores/temperatura/{valor}";


                    var response = await http.PutAsync(urlDaApi, null);

                    if (response.IsSuccessStatusCode)
                    {
                        System.Windows.MessageBox.Show($"Sucesso! O appsettings.json foi atualizado para {valor}ºC.",
                                        "Sucesso", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);

                      
                        NovaTemperaturaTexto = string.Empty;
                    }
                    else
                    {
                        System.Windows.MessageBox.Show($"A API retornou um erro: {response.StatusCode}",
                                        "Erro", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"Erro de conexão com a API: {ex.Message}",
                                    "Erro", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Por favor, digite um valor numérico válido. Ex: 80",
                                "Valor Inválido", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
            }
        }
    }
}

