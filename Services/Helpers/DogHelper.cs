using HundeKennel.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HundeKennel.Services.Helpers
{
    public class DogHelper
    {


        public DogHelper(string? pedigree, string? chip, string? name, string? breeder, string? dadId, string? momId, string? dkkTitles, string? titles, string? count, string? born, string? hD, string? aD, string? hZ, string? sP, string sex, string? color, string? dead, string? aK, string? breedingStatus, string? mb, byte[]? image)
        {
            Pedigree = pedigree;
            Chip = chip;
            Name = name;
            Breeder = breeder;
            DadId = dadId;
            MomId = momId;
            DkkTitles = dkkTitles;
            Titles = titles;
            Count = count;
            Born = born;
            HD = hD;
            AD = aD;
            HZ = hZ;
            SP = sP;
            Sex = sex;
            Color = color;
            Dead = dead;
            AK = aK;
            BreedingStatus = breedingStatus;
            Mb = mb;
            Image = image;


        }

        public string? Pedigree { get; set; }
        public string? Chip { get; set; }
        public string? Name { get; set; }
        public string? Breeder { get; set; }
        public string? DadId { get; set; }
        public string? MomId { get; set; }
        public string? DkkTitles { get; set; }
        public string? Titles { get; set; }
        public string? Count { get; set; }
        public string? Born { get; set; }
        public string? HD { get; set; }
        public string? AD { get; set; }
        public string? HZ { get; set; }
        public string? SP { get; set; }
        public string Sex { get; set; }
        public string? Color { get; set; }
        public string? Dead { get; set; }
        public string? AK { get; set; }
        public string? BreedingStatus { get; set; }
        public string? Mb { get; set; }
        public byte[]? Image { get; set; }



    }

}
