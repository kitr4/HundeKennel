using HundeKennel.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public async Task<IEnumerable<Dog>> LoadAllDogs()
        {
            return await DBHelper.LoadData<Dog, dynamic>("spGetAllDogs", new { });
        }


        public static bool InsertDogIntoDatabase(Dog dog, SqlConnection connection)
        {
            SqlTransaction transaction = null;
            // Check if the dog already exists in the database
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();

                string checkCommand = "SELECT COUNT(*) FROM Dogs WHERE Pedigree = @Pedigree";
                using (SqlCommand checkCmd = new SqlCommand(checkCommand, connection, transaction))
                {
                    checkCmd.Parameters.AddWithValue("@Pedigree", dog.Pedigree ?? (object)DBNull.Value);

                    if (checkCmd.ExecuteScalar() == null)
                    {
                        // Dog does not exist, insert new record
                        string insertCommand =
                        "INSERT INTO Dogs (Pedigree, Chip, Name, DadId, MomId, DkkTitles, Titles, Born, HD, AD, HZ, SP, Sex, Color, Dead, BreedingStatus, Mb, Image)"
                        + "VALUES (@Pedigree, @Chip, @Name, @DadId, @MomId, @DkkTitles, @Titles, @Born, @HD, @AD, @HZ, @SP, @Sex, @Color, @Dead, @BreedingStatus, @Mb, @Image)";
                        using (SqlCommand insertCmd = new SqlCommand(insertCommand, connection, transaction))
                        {
                            // Add parameters from the Dog object
                            insertCmd.Parameters.AddWithValue("@Pedigree", dog.Pedigree);
                            insertCmd.Parameters.AddWithValue("@Chip", dog.Chip);
                            insertCmd.Parameters.AddWithValue("@Name", dog.Name);
                            insertCmd.Parameters.AddWithValue("@DadId", dog.DadId);
                            insertCmd.Parameters.AddWithValue("@MomId", dog.MomId);
                            insertCmd.Parameters.AddWithValue("@DkkTitles", dog.DkkTitles);
                            insertCmd.Parameters.AddWithValue("@Titles", dog.Titles);
                            insertCmd.Parameters.AddWithValue("@Born", dog.Born);
                            insertCmd.Parameters.AddWithValue("@HD", dog.HD);
                            insertCmd.Parameters.AddWithValue("@AD", dog.AD);
                            insertCmd.Parameters.AddWithValue("@HZ", dog.HZ);
                            insertCmd.Parameters.AddWithValue("@SP", dog.SP);
                            insertCmd.Parameters.AddWithValue("@Sex", dog.Sex);
                            insertCmd.Parameters.AddWithValue("@Color", dog.Color);
                            insertCmd.Parameters.AddWithValue("@Dead", dog.Dead);
                            insertCmd.Parameters.AddWithValue("@BreedingStatus", dog.BreedingStatus);
                            insertCmd.Parameters.AddWithValue("@Mb", dog.Mb);
                            if (dog.Image == null)
                            {
                                insertCmd.Parameters.AddWithValue("@Image", DBNull.Value);
                            }
                            else
                            {
                                insertCmd.Parameters.AddWithValue("@Image", dog.Image);
                            }

                            insertCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                    string updateCommand = @"
                    UPDATE Dogs SET 
                    Chip = @Chip, 
                    Name = @Name, 
                    DadId = @DadId, 
                    MomId = @MomId, 
                    DkkTitles = @DkkTitles, 
                    Titles = @Titles, 
                    Born = @Born, 
                    HD = @HD, 
                    AD = @AD, 
                    HZ = @HZ, 
                    SP = @SP, 
                    Sex = @Sex, 
                    Color = @Color, 
                    Dead = @Dead, 
                    BreedingStatus = @BreedingStatus, 
                    Mb = @Mb, 
                    Image = @Image
                    WHERE Pedigree = @Pedigree";
                    using (SqlCommand updateCmd = new SqlCommand(updateCommand, connection, transaction))
                    {
                            // Add parameters from the Dog object
                            updateCmd.Parameters.AddWithValue("@Pedigree", dog.Pedigree ?? (object)DBNull.Value);
                            updateCmd.Parameters.AddWithValue("@Chip", dog.Chip ?? (object)DBNull.Value);
                            updateCmd.Parameters.AddWithValue("@Name", dog.Name ?? (object)DBNull.Value);
                            updateCmd.Parameters.AddWithValue("@DadId", dog.DadId ?? (object)DBNull.Value);
                            updateCmd.Parameters.AddWithValue("@MomId", dog.MomId ?? (object)DBNull.Value);
                            updateCmd.Parameters.AddWithValue("@DkkTitles", dog.DkkTitles ?? (object)DBNull.Value);
                            updateCmd.Parameters.AddWithValue("@Titles", dog.Titles ?? (object)DBNull.Value);
                            updateCmd.Parameters.AddWithValue("@Born", dog.Born);
                            updateCmd.Parameters.AddWithValue("@HD", dog.HD ?? (object)DBNull.Value);
                            updateCmd.Parameters.AddWithValue("@AD", dog.AD ?? (object)DBNull.Value);
                            updateCmd.Parameters.AddWithValue("@HZ", dog.HZ ?? (object)DBNull.Value);
                            updateCmd.Parameters.AddWithValue("@SP", dog.SP ?? (object)DBNull.Value);
                            updateCmd.Parameters.AddWithValue("@Sex", dog.Sex ?? (object)DBNull.Value);
                            updateCmd.Parameters.AddWithValue("@Color", dog.Color ?? (object)DBNull.Value);
                            updateCmd.Parameters.AddWithValue("@Dead", dog.Dead);
                            updateCmd.Parameters.AddWithValue("@BreedingStatus", dog.BreedingStatus);
                            updateCmd.Parameters.AddWithValue("@Mb", dog.Mb);
                            if (dog.Image == null)
                            {
                                updateCmd.Parameters.AddWithValue("@Image", DBNull.Value);
                            }
                            else
                            {
                                updateCmd.Parameters.AddWithValue("@Image", dog.Image);
                            }
                            updateCmd.ExecuteNonQuery();
                    }
                    }
                }
                transaction.Commit();
                return true;
            }
            catch (SqlException ex)
            {
                //log and handle sql exceptions
                transaction?.Rollback();
                return false;
            }
            catch (Exception ex)
            {
                //log and handle non-SQL exceptions
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

    }
}
