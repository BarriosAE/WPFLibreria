using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPFLibreria;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
using WPFLibreria.Foundation;
using WPFLibreria.ViewModels;


namespace WPFLibreria.ViewModels
{
    public class LibroDisplayModel : INotifyPropertyChanged
    {
        private bool isSelected = false;


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
        //data checks and status indicators done in another class
        private readonly LibroDisplayModelStatus stat = new LibroDisplayModelStatus();
        public LibroDisplayModelStatus Stat { get { return stat; } }

        private Libro displayedLibro = new Libro();
        public Libro DisplayedLibro
        {
            get { return displayedLibro; }
            set { displayedLibro = value; OnPropertyChanged(new PropertyChangedEventArgs("DisplayedLibro")); }
        }


        private RelayCommand getLibrosCommand;
        public ICommand GetLibroCommand
        {
            get { return getLibrosCommand ?? (getLibrosCommand = new RelayCommand(() => GetLibros())); }
        }

        private void GetLibros()
        {
            isSelected = false;
            stat.NoError();
            DisplayedLibro = new Libro();
            App.Messenger.NotifyColleagues("GetLibros");
        }


        private RelayCommand clearCommand;
        public ICommand ClearCommand
        {
            get { return clearCommand ?? (clearCommand = new RelayCommand(() => ClearLibroDisplay()/*, ()=>isSelected*/)); }
        }

        private void ClearLibroDisplay()
        {
            isSelected = false;
            stat.NoError();
            DisplayedLibro = new Libro();
            App.Messenger.NotifyColleagues("LibroCleared");
        } //ClearLibroDisplay()


        private RelayCommand updateCommand;
        public ICommand UpdateCommand
        {
            get { return updateCommand ?? (updateCommand = new RelayCommand(() => UpdateLibro(), () => isSelected)); }
        }

        private void UpdateLibro()
        {
            if (!stat.ChkLibroForUpdate(DisplayedLibro)) return;
            if (!App.LinqSQL.UpdateLibros(DisplayedLibro))
            {
                stat.Status = App.LinqSQL.errorMessage;
                return;
            }
            App.Messenger.NotifyColleagues("UpdateLibro", DisplayedLibro);
        } //UpdateLibro()


        private RelayCommand deleteCommand;
        public ICommand DeleteCommand
        {
            get { return deleteCommand ?? (deleteCommand = new RelayCommand(() => DeleteLibro(), () => isSelected)); }
        }


        private void DeleteLibro()
        {
            if (!App.LinqSQL.DeleteLibro(DisplayedLibro._LibroID))
            {
                stat.Status = App.LinqSQL.errorMessage;
                return;
            }
            isSelected = false;
            App.Messenger.NotifyColleagues("DeleteLibro");
        } //DeleteLibro


        private RelayCommand addCommand;
        public ICommand AddCommand
        {
            get { return addCommand ?? (addCommand = new RelayCommand(() => AddLibro(), () => !isSelected)); }
        }


        private void AddLibro()
        {
            if (!stat.ChkLibroForAdd(DisplayedLibro)) return;
            if (!App.LinqSQL.AddLibro(DisplayedLibro))
            {
                stat.Status = App.LinqSQL.errorMessage;
                return;
            }
            App.Messenger.NotifyColleagues("AddLibro", DisplayedLibro);
        } //AddLibro()


        public LibroDisplayModel()
        {
            Messenger messenger = App.Messenger;
            messenger.Register("LibroSelectionChanged", (Action<Libro>)(param => ProcessLibro(param)));
            messenger.Register("SetStatus", (Action<String>)(param => stat.Status = param));
        } //ctor

        public void ProcessLibro(Libro p)
        {
            if (p == null) { /*DisplayedLibro = null;*/  isSelected = false; return; }
            Libro temp = new Libro();
            temp.CopyLibro(p);
            DisplayedLibro = temp;
            isSelected = true;
            stat.NoError();
        } // ProcessLibro(
    }
}
