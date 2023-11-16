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
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;


//tvivlsspørgsmål / diskussion
// Kunne være vi skulle lave checks for at strings også er i korrekte format når det er skrevet ind i excel arket? fx. ved Sex. at den er sat til Male/Female
// er IMage column 25 virkelig en int?

namespace HundeKennel.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {

        //INotify interface: 
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        // ICommands
        public ICommand SearchCommand { get; private set; }


        // Backingfields
        private string searchText;
        private double _progress;
        private Dog? _selectedDog;

        // Properties with NotifyProperty on set
        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                // Notify property change to update binding in the TextBox
                // This assumes you have implemented INotifyPropertyChanged
                OnPropertyChanged(nameof(SearchText));
            }
        }
        public double dbProgress
        {
            get { return _progress; }
            set
            {
                if (_progress != value)
                {
                    _progress = value;
                    OnPropertyChanged(nameof(dbProgress));
                }
            }

        }
        public Dog SelectedDog
        {
            get { return _selectedDog; }
            set
            {
                _selectedDog = value;
                OnPropertyChanged(nameof(SelectedDog));
            }
        }

        public ObservableCollection<Dog>? Dogs { get; private set; }

        // Created Object-Properties
        public DataService data = new DataService();


        // CONSTRUCTORS
        public MainViewModel()
        {
            DBHelper.Import(UpdateProgress);
            SearchCommand = new RelayCommand(ExecuteSearchCommand);

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


        // COMMANDS

        private async void ExecuteSearchCommand()
        {
             await SearchDog(SearchText);
            // Use the SearchText here (for example, just printing it for demonstration)
           
        }
       



    }
}
        
    


    




