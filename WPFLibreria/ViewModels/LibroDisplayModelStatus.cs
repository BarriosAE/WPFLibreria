using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Media;

namespace WPFLibreria.ViewModels
{
    public class LibroDisplayModelStatus : INotifyPropertyChanged

    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        //Error status msg and field Brushes to indicate product field errors
        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged(new PropertyChangedEventArgs("Status")); }
        }
        private static SolidColorBrush errorBrush = new SolidColorBrush(Colors.Red);
        private static SolidColorBrush okBrush = new SolidColorBrush(Colors.Black);

        private SolidColorBrush modelNumberBrush = okBrush;
        public SolidColorBrush EditorialIDBrush
        {
            get { return modelNumberBrush; }
            set { modelNumberBrush = value; OnPropertyChanged(new PropertyChangedEventArgs("EditorialIDBrush")); }
        }

        private SolidColorBrush modelNameBrush = okBrush;
        public SolidColorBrush NombreAutorBrush
        {
            get { return modelNameBrush; }
            set { modelNameBrush = value; OnPropertyChanged(new PropertyChangedEventArgs("NombreAutorBrush")); }
        }

        private SolidColorBrush categoryNameBrush = okBrush;
        public SolidColorBrush DescricionBrush
        {
            get { return categoryNameBrush; }
            set { categoryNameBrush = value; OnPropertyChanged(new PropertyChangedEventArgs("DescricionBrush")); }
        }

        private SolidColorBrush precioUnitarioBrush = okBrush;
        public SolidColorBrush PrecioUnitarioBrush
        {
            get { return precioUnitarioBrush; }
            set { precioUnitarioBrush = value; OnPropertyChanged(new PropertyChangedEventArgs("precioUnitarioBrush")); }
        }
        private SolidColorBrush generoBrush = okBrush;
        public SolidColorBrush GeneroBrush
        {
            get { return generoBrush; }
            set { generoBrush = value; OnPropertyChanged(new PropertyChangedEventArgs("generoBrush")); }
        }


        //set error field brushes to OKBrush and status msg to OK
        public void NoError()
        {
            EditorialIDBrush = NombreAutorBrush = DescricionBrush = precioUnitarioBrush = okBrush;
            Status = "OK";
        } //NoError()


        public LibroDisplayModelStatus()
        {
            NoError();
        } //ctor


        //verify the Libros's unitcost is a decimal number > 0
        private bool ChkUnitCost(string costString)
        {
            if (String.IsNullOrEmpty(costString))
                return false;
            else
            {
                decimal precioUnitario;
                try
                {
                    precioUnitario = Decimal.Parse(costString);
                }
                catch
                {
                    return false;
                }
                if (precioUnitario < 0)
                    return false;
                else return true;
            }
        } //ChkUnitCost()


        //check all product fields for validity
        public bool ChkLibroForAdd(Libro p)
        {
            int errCnt = 0;
            if (string.IsNullOrEmpty(Convert.ToString( p.EditorialID)))
            { errCnt++; EditorialIDBrush = errorBrush; }
            else EditorialIDBrush = okBrush;
            if (String.IsNullOrEmpty(p.NombreAutor))
            { errCnt++; NombreAutorBrush = errorBrush; }
            else NombreAutorBrush = okBrush;
            if (String.IsNullOrEmpty(p.Descricion))
            { errCnt++; DescricionBrush = errorBrush; }
            else DescricionBrush = okBrush;
            if (String.IsNullOrEmpty(p.Genero))
            { errCnt++; GeneroBrush = errorBrush; }
            else GeneroBrush = okBrush;

            if (!ChkUnitCost(Convert.ToString(p.PrecioUnitario)))
            { errCnt++; PrecioUnitarioBrush = errorBrush; }

            if (errCnt == 0) { Status = "OK"; return true; }
            else { Status = "ADD, missing or invalid fields."; return false; }
        } //ChkLibroForAdd()




        //check all product fields for validity
        public bool ChkLibroForUpdate(Libro p)
        {
            int errCnt = 0;
            if (String.IsNullOrEmpty(Convert.ToString(p.EditorialID)))
            { errCnt++; EditorialIDBrush = errorBrush; }
            else EditorialIDBrush = okBrush;
            if (String.IsNullOrEmpty(p.NombreAutor))
            { errCnt++; NombreAutorBrush = errorBrush; }
            else NombreAutorBrush = okBrush;
            if (String.IsNullOrEmpty(p.Descricion))
            { errCnt++; DescricionBrush = errorBrush; }
            else DescricionBrush = okBrush;
            if (String.IsNullOrEmpty(p.Genero))
            { errCnt++; GeneroBrush = errorBrush; }
            else GeneroBrush = okBrush;

            if (!ChkUnitCost(Convert.ToString(p.PrecioUnitario)))
            { errCnt++; PrecioUnitarioBrush = errorBrush; }
            else PrecioUnitarioBrush = okBrush;

            if (errCnt == 0) { Status = "OK"; return true; }
            else { Status = "Update, missing or invalid fields."; return false; }
        } //ChkLibroForUpdate()
    }
}