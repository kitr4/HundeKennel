﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundeKennel.Services.Helpers
{
public static class LoggingHelper
    {
        public static void LogParsingError(string columnName, string pedigree)
        {
            Console.WriteLine($"Invalid format for {columnName} on Dog with pedigree: {pedigree}");
        }
        public static void InvalidColumnFill()
        {

        }
    }
}
