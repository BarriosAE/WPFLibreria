using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPFLibreria.Foundation;
using WPFLibreria.Model;

namespace WPFLibreria
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static LinqSQL linqSQL = new LinqSQL();
        public static LinqSQL LinqSQL { get { return linqSQL; } }
        internal static Messenger Messenger
        {
            get { return _messenger; }
        }

        readonly static Messenger _messenger = new Messenger();
    }
}
