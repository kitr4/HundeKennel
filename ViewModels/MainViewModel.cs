 using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HundeKennel.Models;
using System.Data.SqlClient;
using OfficeOpenXml;
using HundeKennel.Services.Helpers;

//tvivlsspørgsmål / diskussion
// Kunne være vi skulle lave checks for at strings også er i korrekte format når det er skrevet ind i excel arket? fx. ved Sex. at den er sat til Male/Female
// er IMage column 25 virkelig en int?

namespace HundeKennel.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private void LogParsingError
        private DogRepository dogRepo = new DogRepository();

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<DogViewModel> DogVM { get; set; } = new ObservableCollection<DogViewModel>();

        // CONSTRUCTORS
        public MainViewModel()
        {
            // Property LicenseContext is set to NonCommercial to make it eligible for use.
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            // SSMS-path for used to establish connection with System.Data.SqlClient
            string connectionString = "Server=10.56.8.36;Database=DB_F23_32;User Id=DB_F23_USER_32;Password=OPENDB_32;";

            // EXCEL-Filepath initiatialized as a string
            String excelFilePath = "C:\\Users\\jeppe\\source\\repos\\HundeKennel\\Resources\\HundeData.xlsx";

            // Establish connection to SSMS
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Read data from Excel using EPPlus
                using (ExcelPackage package = new ExcelPackage(new System.IO.FileInfo(excelFilePath)))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns; // mulig unødvendigt, da vi måske kører et loop afhængig af hvor mange columns der skal bruges

                    // Assuming your table in SSMS has columns with names similar to Excel columns

                    string sqlInsert = "INSERT INTO dogs("
                        + "@DogId,"
                        + "@Pedigree,"
                        + "@Chip,"
                        + "@Name,"
                        + "@Breeder,"
                        + "@DadId,"
                        + "@MomId,"
                        + "@DkkTitles,"
                        + "@Titles,"
                        + "@Count,"
                        + "@Born,"
                        + "@HD,"
                        + "@AD,"
                        + "@HZ,"
                        + "@SP,"
                        + "@Sex,"
                        + "@Color,"
                        + "@Dead,"
                        + "@AK,"
                        + "@BreedingStatus,"
                        + "@MB,"
                        + "@Image"
                        + ") "
                        + "VALUES ("
                        + "@DogId, @Pedigree, @Chip,@Name,@Breeder,@DadId,@MomId,@DkkTitles,@Titles,@Count,@Born,@HD,@AD,@HZ,@SP,@Sex,@Color,@Dead,@AK,@BreedingStatus,@MB,@Image)";

                    // Prepare SQL command with parameters
                    using (SqlCommand cmd = new SqlCommand(sqlInsert, connection))
                    {
                        for (int row = 2; row <= rowCount; row++) // Assuming data starts from row 2 (excluding header)
                        {
                            // Set parameters based on your Excel columns
                            cmd.Parameters.Clear();

                            if (int.TryParse(worksheet.Cells[row, 1].Text, out int dogId))
                            {
                                cmd.Parameters.AddWithValue("@DogId", dogId);
                            }
                            else
                            {
                                Console.WriteLine($"Invalid Dog ID format for Dog with pedigree: {worksheet.Cells[row, 2].Text}");
                                continue; // Skip this row
                            }
                            cmd.Parameters.AddWithValue("@Pedigree", worksheet.Cells[row, 2].Value?.ToString());
                            cmd.Parameters.AddWithValue("@Chip", worksheet.Cells[row, 3].Value?.ToString());
                            cmd.Parameters.AddWithValue("@Name", worksheet.Cells[row, 4].Value?.ToString());
                            cmd.Parameters.AddWithValue("@Breeder", worksheet.Cells[row, 5].Value?.ToString());
                            cmd.Parameters.AddWithValue("@DadId", worksheet.Cells[row, 6].Value?.ToString());
                            cmd.Parameters.AddWithValue("@MomId", worksheet.Cells[row, 7].Value?.ToString());
                            cmd.Parameters.AddWithValue("@DkkTitles", worksheet.Cells[row, 8].Value?.ToString());
                            cmd.Parameters.AddWithValue("@Titles", worksheet.Cells[row, 9].Value?.ToString());
                            if (int.TryParse(worksheet.Cells[row, 11].Text, out int Count))
                            {
                                cmd.Parameters.AddWithValue("@Count", Count);
                            }
                            else
                            {
                                string pedigree = worksheet.Cells[row, 11].Value?.ToString();
                                LoggingHelper.LogParsingError("Count", pedigree);
                            }

                            if (DateTime.TryParse(worksheet.Cells[row, 12].Text, out DateTime born))
                            {
                                cmd.Parameters.AddWithValue("@Born", born);
                            }
                            else
                            {
                                Console.WriteLine("Invalid date format for Born on Dog with pedigree: " + worksheet.Cells[row, 2].Text);
                            } //e

                                cmd.Parameters.AddWithValue("@HD", worksheet.Cells[row, 14].Value?.ToString());
                            if (int.TryParse(worksheet.Cells[row, 14].Text, out int AD))
                            {
                                cmd.Parameters.AddWithValue("@AD", AD);
                            }
                            else
                            {
                                Console.WriteLine("You have not passed a AD with a numberformat on Dog with pedigree:" + worksheet.Cells[row, 2].Text);
                            }
                            if (int.TryParse(worksheet.Cells[row, 15].Text, out int HZ))
                            {
                                cmd.Parameters.AddWithValue("@HZ", HZ);
                            }
                            else
                            {
                                Console.WriteLine("You have not passed a HZ with a numberformat on Dog with pedigree:" + worksheet.Cells[row, 2].Text);
                            }
                            if (int.TryParse(worksheet.Cells[row, 16].Text, out int SP))
                            {
                                cmd.Parameters.AddWithValue("@SP", SP);
                            }
                            else
                            {
                                Console.WriteLine("You have not passed a SP with a numberformat on Dog with pedigree:" + worksheet.Cells[row, 2].Text);
                            }

                                cmd.Parameters.AddWithValue("@Sex", worksheet.Cells[row, 19].Value?.ToString());
                                cmd.Parameters.AddWithValue("@Color", worksheet.Cells[row, 20].Value?.ToString());
                            if (int.TryParse(worksheet.Cells[row, 21].Text, out int Dead))
                            {
                                cmd.Parameters.AddWithValue("@Dead", Dead);
                            }
                            else
                            {
                                Console.WriteLine("You have not passed a Dead with a numberformat on Dog with pedigree:" + worksheet.Cells[row, 2].Text);
                            }
                            cmd.Parameters.AddWithValue("@AK", worksheet.Cells[row, 22].Value?.ToString());
                            if (int.TryParse(worksheet.Cells[row, 23].Text, out int BreedingStatus))
                            {
                                cmd.Parameters.AddWithValue("@BreedingStatus", BreedingStatus);
                            }
                            else
                            {
                                Console.WriteLine("You have not passed a BreedingStatus with a numberformat 0 or 1 on Dog with pedigree:" + worksheet.Cells[row, 2].Text);
                            }
                            if (int.TryParse(worksheet.Cells[row, 24].Text, out int MB))
                            {
                                cmd.Parameters.AddWithValue("@MB", MB);
                            }
                            else
                            {
                                Console.WriteLine("You have not passed a MB with a numberformat on Dog with pedigree:" + worksheet.Cells[row, 2].Text);
                            }
                            cmd.Parameters.AddWithValue("@Image", worksheet.Cells[row, 25].Value?.ToString()); // INT


                                // Add more parameters as needed

                                cmd.ExecuteNonQuery(); // Execute SQL command to insert data into SSMS
                            }
                        }
                    }
                }

                Console.WriteLine("Data imported from Excel to SSMS database.");
            }
        }

    }



