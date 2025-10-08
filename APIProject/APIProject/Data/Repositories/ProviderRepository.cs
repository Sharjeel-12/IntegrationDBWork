
    using APIProject.Data.Interfaces;
    using APIProject.Models;
    using APIProject.Dtos;
    using System.Data;
    using System.Data.SqlClient;


    namespace APIProject.Data.Repositories
    {
        public class ProviderRepository : IProviderRepository
        {
            private readonly IConfiguration _configuration;
            private static string _connStr = "";

            public ProviderRepository(IConfiguration configuration)
            {
                _configuration = configuration;
                _connStr = _configuration.GetConnectionString("DefaultConnection");
            }

            //  Add a new Provider
            public async Task AddProviderAsync(CreateProviderDto providerDto)
            {
                using (SqlConnection connection = new SqlConnection(_connStr))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("addProvider", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@FirstName", providerDto.FirstName);
                        command.Parameters.AddWithValue("@LastName", providerDto.LastName);
                        command.Parameters.AddWithValue("@licenseType", providerDto.LicenseType);
                        command.Parameters.AddWithValue("@EmailAddress", providerDto.Email);
                        command.Parameters.AddWithValue("@Specialization", providerDto.Specialization);

                        await command.ExecuteNonQueryAsync();
                    }
                    await connection.CloseAsync();
                }
            }

            //  Update an existing Provider
            public async Task UpdateProviderAsync(UpdateProviderDto providerDto)
            {
                using (SqlConnection connection = new SqlConnection(_connStr))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("updateProvider", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@providerID", providerDto.Id);
                        command.Parameters.AddWithValue("@FirstName", providerDto.FirstName);
                        command.Parameters.AddWithValue("@LastName", providerDto.LastName);
                        command.Parameters.AddWithValue("@licenseType", providerDto.LicenseType);
                        command.Parameters.AddWithValue("@EmailAddress", providerDto.Email);
                        command.Parameters.AddWithValue("@Specialization", providerDto.Specialization);

                        await command.ExecuteNonQueryAsync();
                    }
                    await connection.CloseAsync();
                }
            }

            //  Delete a Provider by ID
            public async Task DeleteProviderAsync(int id)
            {
                using (SqlConnection connection = new SqlConnection(_connStr))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("deleteProvider", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@providerID", id);

                        await command.ExecuteNonQueryAsync();
                    }
                    await connection.CloseAsync();
                }
            }

            //  Get a single Provider by ID
            public async Task<Provider> GetProviderAsync(int id)
            {
                Provider provider = new Provider();

                using (SqlConnection connection = new SqlConnection(_connStr))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("getProviderById", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@providerID", id);

                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            provider.Id = reader.GetInt32(0);
                            provider.FirstName = reader.GetString(1);
                            provider.LastName = reader.GetString(2);
                            provider.LicenseType = reader.GetString(3);
                            provider.Email = reader.GetString(4);
                            provider.Specialization = reader.GetString(5);
                        }
                    }
                    await connection.CloseAsync();
                }
                return provider;
            }
            public async Task<List<Provider>> GetAllProvidersAsync()
            {
                List<Provider> providers = new List<Provider>();

                using (SqlConnection connection = new SqlConnection(_connStr))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("getAllProviders", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            Provider provider = new Provider();

                            provider.Id = reader.GetInt32(0);
                            provider.FirstName = reader.GetString(1);
                            provider.LastName = reader.GetString(2);
                            provider.LicenseType = reader.GetString(3);
                            provider.Email = reader.GetString(4);
                            provider.Specialization = reader.GetString(5);

                            providers.Add(provider);
                        }
                    }
                    await connection.CloseAsync();
                }
                return providers;
            }
        }
    }
