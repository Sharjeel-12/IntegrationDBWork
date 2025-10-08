using APIProject.Data.Interfaces;
using APIProject.Dtos;
using System.Runtime.CompilerServices;
using System.Data.SqlClient;
using System.Data;
using APIProject.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.Mail;
using System.Reflection;

namespace APIProject.Data.Repositories
{
    public class PatientRepository:IPatientRepository
    {
        private readonly IConfiguration _config;
        private static string _connectionString=string.Empty;
        public PatientRepository(IConfiguration configuration) 
        { 
            _config = configuration;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }
        public async Task AddPatientAsync(CreatePatientDto patient)
        {
            using(SqlConnection connection=new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using(SqlCommand command=new SqlCommand("addNewPatient",connection)) 
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Title", patient.Title);
                    command.Parameters.AddWithValue("@FirstName", patient.FirstName);
                    command.Parameters.AddWithValue("@LastName", patient.LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", patient.DOB.ToDateTime(TimeOnly.MinValue)); // Convert DateOnly → DateTime
                    command.Parameters.AddWithValue("@Gender", patient.Gender);
                    command.Parameters.AddWithValue("@EmailAddress", patient.Email);
                    command.Parameters.AddWithValue("@ContactNumber", patient.PhoneNumber);
                    await command.ExecuteNonQueryAsync();
                
                }
                await connection.CloseAsync();
            }
        }
        public async Task UpdatePatientAsync(UpdatePatientDto patient)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("updatePatientRecord", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@patientID", patient.Id);
                    command.Parameters.AddWithValue("@Title", patient.Title);
                    command.Parameters.AddWithValue("@FirstName", patient.FirstName);
                    command.Parameters.AddWithValue("@LastName", patient.LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", patient.DOB.ToDateTime(TimeOnly.MinValue)); // Convert DateOnly to DateTime
                    command.Parameters.AddWithValue("@Gender", patient.Gender);
                    command.Parameters.AddWithValue("@EmailAddress", patient.Email);
                    command.Parameters.AddWithValue("@ContactNumber", patient.PhoneNumber);
                    await command.ExecuteNonQueryAsync();

                }
                await connection.CloseAsync();
            }
        }
        public async Task DeletePatientAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("deletePatientRecord", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@patientID", id);
                    await command.ExecuteNonQueryAsync();

                }
                await connection.CloseAsync();
            }
        }

        public async Task<List<Patient>> GetAllPatientsAsync()
        {
            List<Patient> patients = new List<Patient>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("getAllPatients", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    
                    SqlDataReader reader= await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync()) { 
                    Patient RetrievedPatient= new Patient();
                        RetrievedPatient.Id = reader.GetInt32(0);
                        RetrievedPatient.Title= reader.GetString(1);
                        RetrievedPatient.FirstName= reader.GetString(2);
                        RetrievedPatient.LastName= reader.GetString(3);
                        RetrievedPatient.DOB=DateOnly.FromDateTime(reader.GetDateTime(4));
                        RetrievedPatient.Gender= reader.GetString(5);
                        RetrievedPatient.Email=reader.GetString(6);
                        RetrievedPatient.PhoneNumber= reader.GetString(7);
                        patients.Add( RetrievedPatient );
                    }

                }
                await connection.CloseAsync();

            }
            return patients;
        }

        public async Task<Patient> GetPatientAsync(int id)
        {
            Patient RetrievedPatient=new Patient();
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("getPatientById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@patientID", id);
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync()) 
                    {
                        RetrievedPatient.Id = reader.GetInt32(0);
                        RetrievedPatient.Title = reader.GetString(1);
                        RetrievedPatient.FirstName = reader.GetString(2);
                        RetrievedPatient.LastName = reader.GetString(3);
                        RetrievedPatient.DOB = DateOnly.FromDateTime(reader.GetDateTime(4));
                        RetrievedPatient.Gender = reader.GetString(5);
                        RetrievedPatient.Email = reader.GetString(6);
                        RetrievedPatient.PhoneNumber = reader.GetString(7);
                    }
                }
                await connection.CloseAsync();
            }
            return RetrievedPatient;

        }






    }
}
