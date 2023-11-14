using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundeKennel.Models
{
    public class Dog
    {
        // BACKINGFIELDS
        private int _dogId;
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
        private bool _dead;
        private bool _breedingStatus;
        private bool _mb;
        private byte[]? _image;
        private Owner _owner;
        private List<Dog> _pedigreeTree = new List<Dog>();

        
        // PROPERTIES
        public int DogId
        {
            get { return _dogId; }
            private set { _dogId = value; }
        }

        public string? Pedigree
        {
            get { return _pedigree; }
            private set { _pedigree = value; }
        }

        public string? Chip
        {
            get { return _chip; }
            private set { _chip = value; }
        }

        public string? Name
        {
            get { return _name; }
            private set { _name = value; }
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

        public bool Dead
        {
            get { return _dead; }
            private set { _dead = value; }
        }

        public bool BreedingStatus
        {
            get { return _breedingStatus; }
            private set { _breedingStatus = value; }
        }

        public bool Mb
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
        { get => _owner; set => _owner = value; 
        }

        public List<Dog> PedigreeTree
        {
            get { return _pedigreeTree; }
            set { _pedigreeTree = value; }
        }
    }
}
