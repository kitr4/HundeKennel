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
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;


//tvivlsspørgsmål / diskussion
// Kunne være vi skulle lave checks for at strings også er i korrekte format når det er skrevet ind i excel arket? fx. ved Sex. at den er sat til Male/Female
// er IMage column 25 virkelig en int?

namespace HundeKennel.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        
        public ICommand ImportCommand { get; private set; } //for importing

        private DatabaseService _databaseService;
        private int _progress;
        public int Progress
        {
            get { return _progress; }
            set
            {
                if(_progress != value)
                {
                    _progress = value;
                    OnPropertyChanged(nameof(Progress));
                }
            }

        }

        //INotify interface: 
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        // Interface ended


        private DogRepository dogRepo = new DogRepository();

        
        public ObservableCollection<DogViewModel> DogVM { get; set; } = new ObservableCollection<DogViewModel>();
       

        // CONSTRUCTORS
        public MainViewModel()
        {
            DBHelper.Import(UpdateProgress);
            string connectionString = "Server=10.56.8.36;Database=DB_F23_32;User Id=DB_F23_USER_32;Password=OPENDB_32;";
            _databaseService = new DatabaseService(connectionString);
            ImportCommand = new RelayCommand(ExecuteImportCommand);


            // Property LicenseContext is set to NonCommercial to make it eligible for use.
        }
        private void UpdateProgress(int progress)
        {
            Progress = progress;
        }

        private async void ExecuteImportCommand()
        {
            var openFileDialog = new OpenFileDialog(); 
            if (openFileDialog.ShowDialog() == true)
            {
                await ImportDogsAsync(openFileDialog.FileName);
            }
        }

        public async Task ImportDogsAsync(string filePath)
        {
            var dogs = await DBHelper.Import(filePath, UpdateProgress); // Reads and parses the Excel file
            List<string> failedDogPedigrees = new List<string>();

            using (var connection = await _databaseService.GetDatabaseConnectionAsync())
            {
                foreach (var dog in dogs)
                {
                    bool isSuccess = ExcelImportHelper.InsertDogIntoDatabase(dog, connection);
                    if (!isSuccess)
                    {
                        failedDogPedigrees.Add(dog.Pedigree); // Collect failed pedigrees
                    }
                }
            }

            // Provide feedback based on the success or failure of individual dog imports
            if (failedDogPedigrees.Count == 0)
            {
                MessageBox.Show("All dogs imported successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                string failedMessage = "Failed to import dogs with the following pedigrees:\n" + string.Join("\n", failedDogPedigrees);
                MessageBox.Show(failedMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }



}
        
    


    




