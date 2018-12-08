using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace WPFLibreria.ViewModels
{
    public class Libro
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        //For DB use only!
        private int _libroID;
        public int _LibroID { get { return _libroID; } }

        private int editorialID;
        public int EditorialID
        {
            get { return editorialID; }
            set
            {
                editorialID = value; OnPropertyChanged(new PropertyChangedEventArgs("EditorialID"));
            }
        }


        private string nombreAutor;
        public string NombreAutor
        {
            get { return nombreAutor; }
            set { nombreAutor = value; OnPropertyChanged(new PropertyChangedEventArgs("NombreAutor")); }
        }

        private string precioUnitario;
        public string PrecioUnitario
        {
            get { return precioUnitario; }
            set { precioUnitario = value; OnPropertyChanged(new PropertyChangedEventArgs("PrecioUnitario")); }
        }

        private string descricion;
        public string Descricion
        {
            get { return descricion; }
            set { descricion = value; OnPropertyChanged(new PropertyChangedEventArgs("Descricion")); }
        }


        private string genero;
        public string Genero
        {
            get { return genero; }
            set { genero = value; OnPropertyChanged(new PropertyChangedEventArgs("Genero")); }
        }
       

        public Libro()
        {
        }

        public Libro(int libroID, int editorialID, string nombreAutor,
                       string precioUnitario, string descricion, string categoryName)
        {
            this._libroID = libroID;
            EditorialID = editorialID;
            NombreAutor = nombreAutor;
            PrecioUnitario = precioUnitario;
            Descricion = descricion;
            Genero = genero;
        }

        public void CopyLibro(Libro p)
        {
            this._libroID = p._LibroID;
            this.EditorialID = p.EditorialID;
            this.NombreAutor = p.NombreAutor;
            this.PrecioUnitario = p.PrecioUnitario;
            this.Genero = p.Genero;
            this.Descricion = p.Descricion;
        }


        public void LibroAdded2DB(SqlLibro sqlLibro)
        {
            this._libroID = sqlLibro.LibroID;
        }

    } //class Libro


    public class SqlLibro
    {
        public int LibroID { get; set; }
        public int EditorialID { get; set; }
        public string NombreAutor { get; set; }
        public decimal PrecioUnitario { get; set; }
        public string Descricion { get; set; }
        public string Genero { get; set; }

        public SqlLibro() { }

        public SqlLibro(int libroID, int editorialID, string nombreAutor,
                       decimal precioUnitario, string descricion, string genero)
        {
            LibroID = libroID;
            EditorialID = editorialID;
            NombreAutor = nombreAutor;
            PrecioUnitario = precioUnitario;
            Descricion = descricion;
            Genero = genero;
        }

        public SqlLibro(Libro p)
        {
            LibroID = p._LibroID;
            EditorialID = p.EditorialID;
            NombreAutor = p.NombreAutor;
            PrecioUnitario = Convert.ToDecimal(p.PrecioUnitario);
            Descricion = p.Descricion;
            Genero = p.Genero;
        }

        public Libro SqlLibro2Libro()
        {
            string precioUnitario = PrecioUnitario.ToString();
            return new Libro(LibroID, EditorialID, NombreAutor, precioUnitario, Descricion, Genero);
        } 
    }

}
