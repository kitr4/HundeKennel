using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundeKennel.Models 
{
    public class Dog : INotifyPropertyChanged
    {

        //INotify interface: 
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
        // BACKINGFIELDS
        private int? _dogId;
        private string? _pedigree;
        private string? _chip;
        private string? _name;
        private string? _dadId;
        private string? _momId;
        private string? _dkkTitles;
        private string? _titles;
        private DateTime _born;
        private string? _hd;
        private string? _ad;
        private string? _hz;
        private string? _sp;
        private char? _sex;
        private string? _color;
        private bool? _dead;
        private bool? _breedingStatus;
        private bool? _mb;
        private byte[]? _image;
        private Owner? _owner;


        public ObservableCollection<Dog> pedigreeTree { get; set; } = new ObservableCollection<Dog>()
        {
            new Dog { Name = "Buddy" },
            new Dog { Name = "Max" }
        };
        public ObservableCollection<Dog> PedigreeTree
        {
           get { return pedigreeTree; }
            set
            {
                pedigreeTree = value;
                OnPropertyChanged(nameof(PedigreeTree));
            }
        }
    


        // PROPERTIES
        public int? DogId
        {
            get { return _dogId; }
            private set { _dogId = value; }
        }

        public string? Pedigree
        {
            get { return _pedigree; }
            set { _pedigree = value; }
        }

        public string? Chip
        {
            get { return _chip; }
            private set { _chip = value; }
        }

        public string? Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string? DadId
        {
            get { return _dadId; }
            private set { _dadId = value; }
        }

        public string? MomId
        {
            get { return _momId; }
            private set { _momId = value; }
        }

        public string? DkkTitles
        {
            get { return _dkkTitles; }
            private set { _dkkTitles = value; }
        }

        public string? Titles
        {
            get { return _titles; }
            private set { _titles = value; }
        }

        public DateTime Born
        {
            get { return _born; }
            private set { _born = value; }
        }

        public string? HD
        {
            get { return _hd; }
            private set { _hd = value; }
        }

        public string? AD
        {
            get { return _ad; }
            private set { _ad = value; }
        }

        public string? HZ
        {
            get { return _hz; }
            private set { _hz = value; }
        }

        public string? SP
        {
            get { return _sp; }
            private set { _sp = value; }
        }

        public char? Sex
        {
            get { return _sex; }
            private set { _sex = value; }
        }

        public string? Color
        {
            get { return _color; }
            private set { _color = value; }
        }

        public bool? Dead
        {
            get { return _dead; }
            private set { _dead = value; }
        }

        public bool? BreedingStatus
        {
            get { return _breedingStatus; }
            private set { _breedingStatus = value; }
        }

        public bool? Mb
        {
            get { return _mb; }
            private set { _mb = value; }
        }

        public byte[]? Image
        {
            get { return _image; }
            private set { _image = value; }
        }

        public Owner owner
        {
            get => _owner; set => _owner = value;
        }
        public Dog()
        {

        }

        //    public Dog
        //        (
        //        string? pedigree,
        //        string? chip,
        //        string? name,
        //        string? dadId,
        //        string? momId,
        //        string? dkkTitles,
        //        string? titles,
        //        DateTime born,
        //        string? hd,
        //        string? ad,
        //        string? hz,
        //        string? sp,
        //        char? sex,
        //        string? color,
        //        bool dead,
        //        bool breedingStatus,
        //        bool mb,
        //        byte[]? image,
        //        Owner owner,
        //        List<Dog> pedigreeTree
        //        )
        //    {
        //        _pedigree = pedigree;
        //        _chip = chip;
        //        _name = name;
        //        _dadId = dadId;
        //        _momId = momId;
        //        _dkkTitles = dkkTitles;
        //        _titles = titles;
        //        _born = born;
        //        _hd = hd;
        //        _ad = ad;
        //        _hz = hz;
        //        _sp = sp;
        //        _sex = sex;
        //        _color = color;
        //        _dead = dead;
        //        _breedingStatus = breedingStatus;
        //        _mb = mb;
        //        _image = image;
        //        _owner = owner;
        //        _pedigreeTree = pedigreeTree;
        //    }

        //    public Dog(int dogId, string? pedigree, string? chip, string? name, string? dadId, string? momId, string? dkkTitles, string? titles, DateTime born, string? hD, string? aD, string? hZ, string? sP, char? sex, string? color, bool dead, bool breedingStatus, bool mb, byte[]? image)
        //    {
        //        Pedigree = pedigree;
        //        Chip = chip;
        //        Name = name;
        //        DadId = dadId;
        //        MomId = momId;
        //        DkkTitles = dkkTitles;
        //        Titles = titles;
        //        Born = born;
        //        HD = hD;
        //        AD = aD;
        //        HZ = hZ;
        //        SP = sP;
        //        Sex = sex;
        //        Color = color;
        //        Dead = dead;
        //        BreedingStatus = breedingStatus;
        //        Mb = mb;
        //        Image = image;
        //    }
        //}
    }
}
