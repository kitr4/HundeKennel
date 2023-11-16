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
        // This query establishes the logic for a stored procedure-query
        public async Task<IEnumerable<Dog>> LoadDog(string pedigree)
        {
            return await DBHelper.LoadData<Dog, dynamic>("spSearchDog", new { Pedigree = pedigree });
        }
    }
}
