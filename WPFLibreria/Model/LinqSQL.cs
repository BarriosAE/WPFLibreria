using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using WPFLibreria;
using WPFLibreria.Model;
using WPFLibreria.ViewModels;

namespace WPFLibreria.Model
{
    public class LinqSQL
    {
        public bool hasError = false;
        public string errorMessage;

        public MyObservableCollection<Libro> GetLibros()
        {
            hasError = false;
            MyObservableCollection<Libro> libros = new MyObservableCollection<Libro>();
            try
            {
                EnlaceLibreriaDataContext dc = new EnlaceLibreriaDataContext();
                var query = from q in dc.Libros
                            select new SqlLibro
                            {
                                LibroID = q.LibroID,
                                EditorialID = q.EditorialID,
                                NombreAutor = q.NombreAutor,
                                Genero = q.Genero,
                                PrecioUnitario = Convert.ToDecimal(q.PrecioUnitario),
                                Descricion = q.Descricion,
                              
                              
                            };
                foreach (SqlLibro sp in query)
                    libros.Add(sp.SqlLibro2Libro());
            } //try
            catch (Exception ex)
            {
                errorMessage = "GetLibros() error, " + ex.Message;
                hasError = true;
            }
            return libros;
        } 

        public bool UpdateLibros(Libro displayP)
        {
            try
            {
                SqlLibro p = new SqlLibro(displayP);
                EnlaceLibreriaDataContext dc = new EnlaceLibreriaDataContext();
                dc.UpdateLibro(p.LibroID, p.EditorialID, p.NombreAutor, p.Genero, p.PrecioUnitario, p.Descricion);
            }
            catch (Exception ex)
            {
                errorMessage = "Update error, " + ex.Message;
                hasError = true;
            }
            return (!hasError);
        } 
        public bool DeleteLibro(int libroID)
        {
            hasError = false;
            try
            {
                EnlaceLibreriaDataContext dc = new EnlaceLibreriaDataContext();
                dc.DeleteLibro(libroID);
            }
            catch (Exception ex)
            {
                errorMessage = "Delete error, " + ex.Message;
                hasError = true;
            }
            return !hasError;
        }
        public bool AddLibro(Libro displayP)
        {
            hasError = false;
            try
            {
                SqlLibro p = new SqlLibro(displayP);
                EnlaceLibreriaDataContext dc = new EnlaceLibreriaDataContext();
                int?  newlibroID = 0;
                dc.AddLibro(p.EditorialID, p.NombreAutor, p.Genero, p.PrecioUnitario,p.Descricion, ref newlibroID);
                p.LibroID = (int)newlibroID;
                displayP.LibroAdded2DB(p);    //update corresponding Libro ProductId using SqlLibro
            }
            catch (Exception ex)
            {
                errorMessage = "Add error, " + ex.Message;
                hasError = true;
            }
            return !hasError;
        } 
    } 
}
