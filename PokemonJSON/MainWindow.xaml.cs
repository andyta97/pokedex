using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PokemonJSON
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isFrontViewActive = true;
        PokeInfo Pokemon;
        public MainWindow()
        {
            InitializeComponent();

            PokemonAPI pokemonApiResults;

            string apiURL = @"https://pokeapi.co/api/v2/pokemon/?offset=0&limit=1100";

            using (var client = new HttpClient())
            {
                string jsonResults = client.GetStringAsync(apiURL).Result;

                pokemonApiResults = JsonConvert.DeserializeObject<PokemonAPI>(jsonResults);
            }
            foreach (var item in pokemonApiResults.results)
            {
                nameComBox.Items.Add(item);
            }
        }

        private void pokeInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            var pokemonResult = (AllResults) nameComBox.SelectedItem;
            string pokemonURL = pokemonResult.url;

            pokeNameTxtBlck.Text = pokemonResult.name.ToUpper();
            pokeNameTxtBlck.Visibility = Visibility.Visible;
            pokeNameRct.Visibility = Visibility.Visible;
            switchBtn.Visibility = Visibility.Visible;
            statsRct.Visibility = Visibility.Visible;
            pokeInfoTxtBlck.Visibility = Visibility.Visible;
            pokeImg.Visibility = Visibility.Visible;

            using (var client = new HttpClient())
            {
                string jsonResults = client.GetStringAsync(pokemonURL).Result;

                Pokemon = JsonConvert.DeserializeObject<PokeInfo>(jsonResults);
            }
            
            pokeImg.Source = new BitmapImage(new Uri(Pokemon.sprites.front_default));

            pokeInfoTxtBlck.Text = $"STATS:\n\nHEIGHT - {Pokemon.height}\n\nWEIGHT - {Pokemon.weight}";
        }

        private void switchBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isFrontViewActive == true)
            {
                isFrontViewActive = false;
                pokeImg.Source = new BitmapImage(new Uri(Pokemon.sprites.back_default));
            }
            else
            {
                isFrontViewActive = true;
                pokeImg.Source = new BitmapImage(new Uri(Pokemon.sprites.front_default));
            }
        }

        private void nameComBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (nameComBox.SelectedItem != null)
            {
                pokeInfoBtn.IsEnabled = true;
                switchBtn.IsEnabled = true;
            }
        }
    }
}
