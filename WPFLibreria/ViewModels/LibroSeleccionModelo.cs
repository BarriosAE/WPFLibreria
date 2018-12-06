using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using System.Linq;
using System.Text;
using System.ComponentModel;
using WPFLibreria;
using WPFLibreria.Foundation;

namespace WPFLibreria.ViewModels
{
    public class LibroSeleccionModelo : INotifyPropertyChanged
    {

        public LibroSeleccionModelo()
        {
            dataItems = new MyObservableCollection<Libro>();
            DataItems = App.LinqSQL.GetLibros();
            listBoxCommand = new RelayCommand(() => SelectionHasChanged());
            App.Messenger.Register("ProductCleared", (Action)(() => selectedLibro = null));
            App.Messenger.Register("GetLibro", (Action)(() => GetLibro()));
            App.Messenger.Register("UpdateLibro", (Action<Libro>)(param => UpdateLibro(param)));
            App.Messenger.Register("DeleteLibro", (Action)(() => DeleteLibro()));
            App.Messenger.Register("AddLibro", (Action<Libro>)(param => AddLibro(param)));
        }


        private void GetLibro()
        {
            DataItems = App.LinqSQL.GetLibros();
            if (App.LinqSQL.hasError)
                App.Messenger.NotifyColleagues("SetStatus", App.LinqSQL.errorMessage);
        }


        private void AddLibro(Libro p)
        {
            dataItems.Add(p);
        }


        private void UpdateLibro(Libro p)
        {
            int index = dataItems.IndexOf(selectedLibro);
            dataItems.ReplaceItem(index, p);
            selectedLibro = p;
        }


        private void DeleteLibro()
        {
            dataItems.Remove(selectedLibro);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        private MyObservableCollection<Libro> dataItems;
        public MyObservableCollection<Libro> DataItems
        {
            get { return dataItems; }
            //If dataItems replaced by new collection, WPF must be told
            set { dataItems = value; OnPropertyChanged(new PropertyChangedEventArgs("DataItems")); }
        }

        private Libro selectedLibro;
        public Libro SelectedLibro
        {
            get { return selectedLibro; }
            set { selectedLibro = value; OnPropertyChanged(new PropertyChangedEventArgs("SelectedLibro")); }
        }

        private RelayCommand listBoxCommand;
        public ICommand ListBoxCommand
        {
            get { return listBoxCommand; }
        }

        private void SelectionHasChanged()
        {
            Messenger messenger = App.Messenger;
            messenger.NotifyColleagues("LibroSelectionChanged", selectedLibro);
        }
    }//class ProductSelectionModel



    public class MyObservableCollection<Libro> : ObservableCollection<Libro>
    {
        public void UpdateCollection()
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                                NotifyCollectionChangedAction.Reset));
        }


        public void ReplaceItem(int index, Libro item)
        {
            base.SetItem(index, item);
        }

    } 
}

