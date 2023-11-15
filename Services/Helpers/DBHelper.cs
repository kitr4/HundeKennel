using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Xml.Linq;
using HundeKennel.Models;
using OfficeOpenXml;
using System.Data;

namespace HundeKennel.Services.Helpers
{
    public class DBHelper
    {
        private static readonly string? connectionString = "Server=10.56.8.36;Database=DB_F23_32;User Id=DB_F23_USER_32;Password=OPENDB_32;";
        
        public static void ClearDB(SqlConnection connection)
        {
            string clear = "DELETE FROM dogs;"+"DELETE FROM owner_dog;"+"DELETE FROM owners;"+"DBCC CHECKIDENT('dogs', RESEED, 1);";
            using (SqlCommand ClearCommand = new SqlCommand(clear, connection))
            {
                connection.Open();
                ClearCommand.ExecuteNonQuery();
            }
        }

        public static async Task Import(Action<double> updateProgress)
        {
            await Task.Run(() =>
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // SSMS-path for used to establish connection with System.Data.SqlClient
                // string connectionString = "Server=10.56.8.36;Database=DB_F23_32;User Id=DB_F23_USER_32;Password=OPENDB_32;";

                // EXCEL-Filepath initiatialized as a string
                String excelFilePath = "C:\\Users\\jeppe\\source\\repos\\HundeKennel\\Resources\\HundeData.xlsx";

                // Establish connection to SSMS
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Drops tables if they exist (dogs, owner_dog, owners)
                    ClearDB(connection);


                    // Read data from Excel using EPPlus
                    using (ExcelPackage package = new ExcelPackage(new System.IO.FileInfo(excelFilePath)))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                        int rowCount = worksheet.Dimension.Rows;
                        int colCount = worksheet.Dimension.Columns; // mulig unødvendigt, da vi måske kører et loop afhængig af hvor mange columns der skal bruges

                        // Assuming your table in SSMS has columns with names similar to Excel columns

                        string sqlInsert = "INSERT INTO dogs("
                            + "Pedigree,"
                            + "Chip,"
                            + "Name,"
                            + "Breeder,"
                            + "DadId,"
                            + "MomId,"
                            + "DkkTitles,"
                            + "Titles,"
                            + "Count,"
                            + "Born,"
                            + "HD,"
                            + "AD,"
                            + "HZ,"
                            + "SP,"
                            + "Sex,"
                            + "Color,"
                            + "Dead,"
                            + "AK,"
                            + "BreedingStatus,"
                            + "MB,"
                            + "Image"
                            + ") "
                            + "VALUES ("
                            + "@Pedigree, @Chip,@Name,@Breeder,@Dad,@Mom,@DkkTitles,@Titles,@Count,@Born,@HD,@AD,@HZ,@SP,@Sex,@Color,@Dead,@AK,@BreedingStatus,@MB,@Image)";

                        // Prepare SQL command with parameters
                        using (SqlCommand cmd = new SqlCommand(sqlInsert, connection))
                        {
                            
                            for (int row = 2; row <= rowCount; row++) // Assuming data starts from row 2 (excluding header)
                            {
                                
                                // Set parameters based on your Excel columns
                                cmd.Parameters.Clear();
                                byte[] image = Convert.FromBase64String(worksheet.Cells[row, 25].Text);
                                DogHelper doghelper = new DogHelper(
                                    worksheet.Cells[row, 2].Text,
                                    worksheet.Cells[row, 3].Text,
                                    worksheet.Cells[row, 4].Text,
                                    worksheet.Cells[row, 5].Text,
                                    worksheet.Cells[row, 6].Text,
                                    worksheet.Cells[row, 7].Text,
                                    worksheet.Cells[row, 8].Text,
                                    worksheet.Cells[row, 9].Text,
                                    worksheet.Cells[row, 11].Text,
                                    worksheet.Cells[row, 12].Text,
                                    worksheet.Cells[row, 13].Text,
                                    worksheet.Cells[row, 14].Text,
                                    worksheet.Cells[row, 15].Text,
                                    worksheet.Cells[row, 16].Text,
                                    worksheet.Cells[row, 19].Text,
                                    worksheet.Cells[row, 20].Text,
                                    worksheet.Cells[row, 21].Text,
                                    worksheet.Cells[row, 22].Text,
                                    worksheet.Cells[row, 23].Text,
                                    worksheet.Cells[row, 24].Text,
                                    image);

                                // Data-integrity with Parameters
                                cmd.Parameters.AddWithValue("@Pedigree", doghelper.Pedigree);
                                cmd.Parameters.AddWithValue("@Chip", doghelper.Chip);
                                cmd.Parameters.AddWithValue("@Name", doghelper.Name);
                                cmd.Parameters.AddWithValue("@Breeder", doghelper.Breeder);
                                cmd.Parameters.AddWithValue("@Dad", doghelper.DadId);
                                cmd.Parameters.AddWithValue("@Mom", doghelper.MomId);
                                cmd.Parameters.AddWithValue("@DkkTitles", doghelper.DkkTitles);
                                cmd.Parameters.AddWithValue("@Titles", doghelper.Titles);
                                cmd.Parameters.AddWithValue("@Count", doghelper.Count);
                                cmd.Parameters.AddWithValue("@Born", doghelper.Born);
                                cmd.Parameters.AddWithValue("@HD", doghelper.HD);
                                cmd.Parameters.AddWithValue("@AD", doghelper.AD);
                                cmd.Parameters.AddWithValue("@HZ", doghelper.HZ);
                                cmd.Parameters.AddWithValue("@SP", doghelper.SP);
                                cmd.Parameters.AddWithValue("@Sex", doghelper.Sex);
                                cmd.Parameters.AddWithValue("@Color", doghelper.Color);
                                cmd.Parameters.AddWithValue("@Dead", doghelper.Dead);
                                cmd.Parameters.AddWithValue("@AK", doghelper.AK);
                                cmd.Parameters.AddWithValue("@BreedingStatus", doghelper.BreedingStatus);
                                cmd.Parameters.AddWithValue("@MB", doghelper.Mb);
                                cmd.Parameters.AddWithValue("@Image", doghelper.Image);

                                cmd.ExecuteNonQuery(); // 
                                double currentProgress = (row - 1) * 100 / (double) worksheet.Dimension.Rows;
                                updateProgress(currentProgress);
                            }
                        }
                    }
                }
            }); 
        } // END OF METHOD IMPORT

        public async Task<IEnumerable<T>> LoadData<T, U>(string sql, U parameters)
        {
            using IDbConnection conn = new SqlConnection(connectionString);
            return await conn.QueryAsync<T>(sql, parameters, commandType: CommandType.StoredProcedure);
        }

    }


}

