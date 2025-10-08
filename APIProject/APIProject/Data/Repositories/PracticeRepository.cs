using APIProject.Data.Interfaces;
using APIProject.Dtos;
using APIProject.Models;
using System.Data;
using System.Data.SqlClient;

namespace APIProject.Data.Repositories
{
    public class PracticeRepository:IPracticeRepository
    {
        private readonly IConfiguration _configuration;
        private static string _connStr = "";
        public PracticeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connStr = _configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<List<Practice>> GetAllPracticesAsync()
        {
            List<Practice> practices=new List<Practice>();
            using(SqlConnection connection=new SqlConnection(_connStr))
            {
                await connection.OpenAsync();
                using(SqlCommand command=new SqlCommand("getAllPractices", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = await command.ExecuteReaderAsync(); 
                    while(await reader.ReadAsync())
                    {
                        Practice practice = new Practice();
                        practice.Id=reader.GetInt32(0);
                        practice.Name=reader.GetString(5);
                        practice.TaxId =reader.GetString(6);
                        practices.Add(practice);
                    }
                }
                await connection.CloseAsync();
            }
            return practices;
        }

        public async Task<Practice> GetPracticeAsync(int practiceId)
        {
            Practice practice=new Practice();
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("getPracticeById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@practiceID", practiceId);
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        
                        practice.Id = reader.GetInt32(0);
                        practice.Name = reader.GetString(5);
                        practice.TaxId = reader.GetString(6);
                        
                    }
                }
                await connection.CloseAsync();
            }
            return practice;
        }
        public async Task AddPracticeAsync(CreatePracticeDto practice)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("createNewPractice", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@name", practice.Name);
                    command.Parameters.AddWithValue("@TID", practice.TaxId);
                    await command.ExecuteNonQueryAsync();
                }
                await connection.CloseAsync();
            }
        }
        public async Task DeletePracticeAsync(int practiceId)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("deletePracticeById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@practiceID", practiceId);
                    await command.ExecuteNonQueryAsync();
                }
                await connection.CloseAsync();
            }
        }


    }
}
