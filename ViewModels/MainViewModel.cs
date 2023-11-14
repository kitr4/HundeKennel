 using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HundeKennel.Models;

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

        }


    }
}
