using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundeKennel.Models
{
    public class OwnerRepository
    {
        ObservableCollection<Dog> dogs = new ObservableCollection<Dog>();
    }
}
