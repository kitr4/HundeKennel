using HundeKennel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HundeKennel.Services.Helpers
{
    public class ExcelDogHelper
    {
        
            public string? DogId { get; set; }
            public string? Pedigree { get; set; }
            public string? Chip { get; set; }
            public string? Name { get; set; }
            public string? DadId { get; set; }
            public string? MomId { get; set; }
            public string? DkkTitles { get; set; }
            public string? Titles { get; set; }
            public DateTime? Born { get; set; }
            public string? HD { get; set; }
            public string? AD { get; set; }
            public string? HZ { get; set; }
            public string? SP { get; set; }
            public char? Sex { get; set; }
            public string? Color { get; set; }
            public string? Dead { get; set; }
            public string? BreedingStatus { get; set; }
            public string? Mb { get; set; }
            public byte[]? Image { get; set; }
        }
}
