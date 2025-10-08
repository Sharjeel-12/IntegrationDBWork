using System.Data;
using System.Data.SqlClient;
using APIProject.Dtos;
using APIProject.Models;
using APIProject.Data.Interfaces;

namespace APIProject.Data.Repositories
    {
        public class ResourceRepository : IResourceRepository
        {
            private readonly IConfiguration _configuration;
            private static string _connStr = "";

            public ResourceRepository(IConfiguration configuration)
            {
                _configuration = configuration;
                _connStr = _configuration.GetConnectionString("DefaultConnection");
            }

            public async Task AddResourceAsync(CreateResourceDto resource)
            {
                using (SqlConnection connection = new SqlConnection(_connStr))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("addResource", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@resourceName", resource.ResourceName);

                        await command.ExecuteNonQueryAsync();
                    }

                    await connection.CloseAsync();
                }
            }

            public async Task DeleteResourceAsync(int ResourceId)
            {
                using (SqlConnection connection = new SqlConnection(_connStr))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("deleteResource", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ResourceID", ResourceId);

                        await command.ExecuteNonQueryAsync();
                    }

                    await connection.CloseAsync();
                }
            }
        public async Task<List<Resource>> GetAllResources()
        {
            List<Resource> resources = new List<Resource>();

            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("getAllResources", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        Resource resource = new Resource
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };
                        resources.Add(resource);
                    }
                }

                await connection.CloseAsync();
            }

            return resources;
        }
    }
    }
