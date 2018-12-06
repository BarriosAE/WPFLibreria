using System;
using System.Collections.Generic;
using System.Linq;
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
using WPFLibreria;

namespace WPFLibreria
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Consulta_Libros(object sender, RoutedEventArgs e)
        {
            //Window ventana = new Libros_Main();
            //ventana.Show();
        }
        private void Consulta_Genero(object sender, RoutedEventArgs e)
        {
            //MessageBoxResult result = MessageBox.Show("Proximamente en los mejores cines",
            //                                          "Confirmar",
            //                                          MessageBoxButton.OK,
            //                                          MessageBoxImage.Question);
        }
        private void Button_Close(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Realmente quiere salir del programa?",
                                                      "Confirmar",
                                                      MessageBoxButton.YesNo,
                                                      MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void LibroSeleccionView_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
