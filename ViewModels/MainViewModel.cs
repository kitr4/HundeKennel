using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HundeKennel.Models;
using System.Data.SqlClient;
using OfficeOpenXml;
using HundeKennel.Services.Helpers;
using System.Windows.Data;
using System.Windows.Markup;


//tvivlsspørgsmål / diskussion
// Kunne være vi skulle lave checks for at strings også er i korrekte format når det er skrevet ind i excel arket? fx. ved Sex. at den er sat til Male/Female
// er IMage column 25 virkelig en int?

namespace HundeKennel.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public DataService data;
        private double _progress;
        public double dbProgress
        {
            get { return _progress; }
            set
            {
                if(_progress != value)
                {
                    _progress = value;
                    OnPropertyChanged(nameof(dbProgress));
                }
            }

        }
        private Dog? _selectedDog;
        public Dog SelectedDog
        {
            get { return _selectedDog; }
            set
            {
                _selectedDog = value;
                OnPropertyChanged(nameof(SelectedDog));
            }
        }


        //INotify interface: 
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        // Interface ended
        public ObservableCollection<Dog>? Dogs { get; private set; }
        

        // CONSTRUCTORS
        public MainViewModel()
        {
            DBHelper.Import(UpdateProgress);

        }
        private void UpdateProgress(double progress)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                dbProgress = progress;
            });
        }
        public async Task SearchDog(string pedigree)
        { 
            var DogFound = await data.LoadDog(pedigree);
            if (DogFound != null && DogFound.Any())
            {
                SelectedDog = DogFound.First(); // Accessing the first element                                  // Now you can use SelectedDog as needed
            }
            else
            {
                // Handle case when no dogs are found
            }
        }

    }
}
        
    


    




