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


//tvivlsspørgsmål / diskussion
// Kunne være vi skulle lave checks for at strings også er i korrekte format når det er skrevet ind i excel arket? fx. ved Sex. at den er sat til Male/Female
// er IMage column 25 virkelig en int?

namespace HundeKennel.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        
        private DogRepository dogRepo = new DogRepository();

        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<DogViewModel> DogVM { get; set; } = new ObservableCollection<DogViewModel>();

        // CONSTRUCTORS
        public MainViewModel()
        {
            DBHelper.Import();
            // Property LicenseContext is set to NonCommercial to make it eligible for use.
        }
        
    }

    
}



