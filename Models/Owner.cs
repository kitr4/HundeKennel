using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundeKennel.Models
{
    public class Owner
    {
        private int _ownerId;
        private string? _name;
        private string? _address;
        private int _zipcode;
        private string? _city;
        private string? _email;
        private string? _phone;
        private string? _username;
        private string? _password;
        private string? _url;
        private List<Dog> _ownedDogs = new List<Dog>();

        public int OwnerId
        {
            get { return _ownerId; }
            private set { _ownerId = value; }
        }

        public string? Name
        {
            get { return _name; }
            private set { _name = value; }
        }

        public string? Address
        {
            get { return _address; }
            private set { _address = value; }
        }

        public int Zipcode
        {
            get { return _zipcode; }
            private set { _zipcode = value; }
        }

        public string? City
        {
            get { return _city; }
            private set { _city = value; }
        }

        public string? Email
        {
            get { return _email; }
            private set { _email = value; }
        }

        public string? Phone
        {
            get { return _phone; }
            private set { _phone = value; }
        }

        public string? Url
        {
            get { return _url; }
            private set { _url = value; }
        }

        public List<Dog> OwnedDogs
        {
            get { return _ownedDogs; }
            private set { _ownedDogs = value; }

        }
        public Owner(int ownerId, string? name, string? address, int zipcode, string? city, string? email, string? phone, string? url)
        {
            _ownerId = ownerId;
            _name = name;
            _address = address;
            _zipcode = zipcode;
            _city = city;
            _email = email;
            _phone = phone;
            _url = url;
        }
    }
}
