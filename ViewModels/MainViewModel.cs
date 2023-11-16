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
        public ICommand ChooseFileCommand { get; private set; }
        public ICommand CreateDogCommand { get; private set; }
        public ICommand SearchMateCommand { get; set; }


        // Backingfields
        private string searchText;
        private double _progress;
        private Dog? _selectedDog;
        public string? MateSex { get; set; }
        public string? MateColor { get; set; }
        public int? MateAge { get; set; }
        public string? MateAD { get; set; }
        public string? MateHD { get; set; }
        public string? MateSP { get; set; }
        public string? MateHZ { get; set; }

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

        List<Dog> Dogs = new List<Dog>();
        



        public Dog SelectedDog
        {
            get { return _selectedDog; }
            set
            {
                _selectedDog = value;
                OnPropertyChanged(nameof(SelectedDog));
            }
        }
        public Dog RightDog
        {
            get { return RightDog; }
            set
            {
                _selectedDog = value;
                OnPropertyChanged(nameof(RightDog));
            }
        }

        private string _selectedFilePath;

        public string SelectedFilePath
        {
            get { return _selectedFilePath; }
            set
            {
                if (_selectedFilePath != value)
                {
                    _selectedFilePath = value;
                    OnPropertyChanged(nameof(SelectedFilePath));
                }
            }
        }

        // public ObservableCollection<Dog> DogsRight { get; private set; }

        // Created Object-Properties
        public DataService data = new DataService();

        public readonly IFilePickerService _filePickerService;

        // CONSTRUCTORS
        public MainViewModel()
        {
            // DBHelper.Import(UpdateProgress);
            SearchCommand = new RelayCommand(ExecuteSearchCommand);
            ChooseFileCommand = new RelayCommand(async () => await ChooseFile());
            CreateDogCommand = new RelayCommand(ExecuteCreateDog);
            SearchMateCommand = new RelayCommand(ExecuteFindMate);
            // LOADER PIRAT Load();
    }

        private void ExecuteCreateDog()
        {
            createDog();
        }
        private void createDog()

        {
            Dogs.Add(SelectedDog);
            data.InsertDog(SelectedDog);
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

        // _________ PIRAT PEDITREE
        //public async void Load()
        //{
        //    var tempDogs = data.LoadAllDogs();
        //    foreach(var dog in tempDogs)
        //    {
        //        Dogs.Add(dog);
        //    }
        //}

        //    public async void Load()
        //{
        //    var treeDogs = await data.LoadAllDogs();
        //    int i = 0;
        //    bool treeMade = false;
        //    while (!treeMade)
        //        foreach (var parentDog in treeDogs)
        //        {
        //            if (i == 0)
        //            {
        //                if (parentDog.Pedigree == SelectedDog.DadId)
        //                {
        //                    SelectedDog.DadTree.Add(parentDog);
        //                    i++;
        //                }
        //            }

        //            if (i == 1 && SelectedDog.DadTree[0].Pedigree == parentDog.Pedigree)
        //            {
        //                SelectedDog.DadTree.Add(parentDog);
        //                i++;
        //            }
        //            if (i == 2 && SelectedDog.DadTree[1].Pedigree == parentDog.Pedigree)
        //            {
        //                SelectedDog.DadTree.Add(parentDog);
        //                i++;
        //            }
        //            if (i == 3 && SelectedDog.DadTree[2].Pedigree == parentDog.Pedigree)
        //            {
        //                SelectedDog.DadTree.Add(parentDog);
        //                i++;
        //            }
        //            if (i == 4 && SelectedDog.DadTree[3].Pedigree == parentDog.Pedigree)
        //            {
        //                SelectedDog.DadTree.Add(parentDog);
        //                treeMade = true;
        //            }
        //        }
        //}

        private async void ExecuteFindMate()
        {
            await FindMate(this.MateSex, this.MateColor, this.MateAge, this.MateAD, this.MateHD, this.MateSP, this.MateHZ);
        }
        
        public async Task FindMate(string? matesex, string? matecolor, int? mateage,string? matead, string? matehd, string? matesp, string? matehz)
        {
            var DogFound = await data.LoadMateDogs(matesex, matecolor, mateage, matead, matehd, matesp, matehz);
            if (DogFound != null && DogFound.Any())
            {
                ObservableCollection<Dog> DogsRight = new ObservableCollection<Dog>();
                foreach(Dog dog in DogFound)
                {
                    DogsRight.Add(dog);
                }
            }
        }

        
        // COMMANDS

        private async void ExecuteSearchCommand()
        {
             await SearchDog(SearchText);
            // Use the SearchText here (for example, just printing it for demonstration)
           
        }

        private async Task ChooseFile() 
            {
                IFilePickerService filePickerService = new FilePickerService(); // Instantiate the service here
                string filePath = await filePickerService.PickAFileAsync();
                SelectedFilePath = filePath;
                await DBHelper.ImportNew(filePath, UpdateProgress);
                SelectedFilePath = "HEY";

            // You can also assign the path to a string variable here or perform further operations
        }

    }

}


        
    


    




