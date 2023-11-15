using HundeKennel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundeKennel.Services.Helpers
{
    public class DataService
    {
        public async Task<IEnumerable<Dog>> LoadDog(string pedigree)
        {
            return await DBHelper.LoadData<Dog, dynamic>("spRetrieveDog", new { Pedigree = pedigree });
        }
    }
}
