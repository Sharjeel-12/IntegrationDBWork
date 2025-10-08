using APIProject.Data.Interfaces;
using APIProject.Models;
using System.Data.SqlClient;
using System.Data;
using APIProject.Dtos;

namespace APIProject.Data.Repositories
{
    public class PracticeLocationRepository:IPracticeLocationRepository
    {   
        
        private readonly IConfiguration _configuration;
        private static string _connStr = "";
        public PracticeLocationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connStr=_configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<PracticeLocation> GetPracticeLocationAsync(int LocationId)
        {
            PracticeLocation RetrievedLocation=new PracticeLocation();
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("exec getPracticeLocation @LocationID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LocationID", LocationId);
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        RetrievedLocation.PracticeId = reader.GetInt32(0);
                        RetrievedLocation.LocationId = reader.GetInt32(1);
                        RetrievedLocation.Address = reader.GetString(2);
                        RetrievedLocation.ContactEmail = reader.GetString(3);
                        RetrievedLocation.POS = reader.GetString(4); 

                    }
                }
                await connection.CloseAsync();
            }
            return RetrievedLocation;
        }
        public async Task<List<PracticeLocation>> GetAllPracticeLocationsAsync()
        {
            List<PracticeLocation> PracticeLocations=new List<PracticeLocation>();
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("getAllPractices", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        PracticeLocation RetrievedLocation = new PracticeLocation();

                        RetrievedLocation.PracticeId = reader.GetInt32(0);
                        RetrievedLocation.LocationId = reader.GetInt32(1);
                        RetrievedLocation.Address = reader.GetString(2);
                        RetrievedLocation.ContactEmail = reader.GetString(3);
                        RetrievedLocation.POS = reader.GetString(4);
                        PracticeLocations.Add(RetrievedLocation);
                    }
                }
                await connection.CloseAsync();
                return PracticeLocations;
            }
        }

        public async Task DeletePracticeLocationAsync(int LocationId)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("deletePracticeLocation", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@locationID", LocationId);
                    await command.ExecuteNonQueryAsync();

                }
                await connection.CloseAsync();
            }
        }

        public async Task AddPracticeLocationAsync(CreatePracticeLocationDto practiceLocationDto)
        {
           using (SqlConnection connection = new SqlConnection(_connStr))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("addNewLocation", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@practiceID", practiceLocationDto.PracticeId);
                    command.Parameters.AddWithValue("@practiceAddress", practiceLocationDto.Address);
                    command.Parameters.AddWithValue("@contactEmail", practiceLocationDto.ContactEmail);
                    command.Parameters.AddWithValue("@POS", practiceLocationDto.POS);

                    await command.ExecuteNonQueryAsync();

                }
                await connection.CloseAsync();
            }
        }
        
        public async Task UpdatePracticeLocationAsync(UpdatePracticeLocationDto updateLocationDto)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("updateLocation", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@locationID", updateLocationDto.LocationId);
                    command.Parameters.AddWithValue("@practiceID", updateLocationDto.PracticeId);
                    command.Parameters.AddWithValue("@practiceAddress", updateLocationDto.Address);
                    command.Parameters.AddWithValue("@contactEmail", updateLocationDto.ContactEmail);
                    command.Parameters.AddWithValue("@POS", updateLocationDto.POS);

                    await command.ExecuteNonQueryAsync();

                }
                await connection.CloseAsync();
            }
        }






    }
}
