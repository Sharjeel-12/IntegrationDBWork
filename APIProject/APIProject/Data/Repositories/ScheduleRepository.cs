
using APIProject.Data.Interfaces;
using APIProject.Dtos;
using APIProject.Models;
using System.Data;
using System.Data.SqlClient;

namespace APIProject.Data.Repositories
    {
        public class ScheduleRepository : IScheduleRepository
        {
            private readonly IConfiguration _configuration;
            private static string _connStr = "";

            public ScheduleRepository(IConfiguration configuration)
            {
                _configuration = configuration;
                _connStr = _configuration.GetConnectionString("DefaultConnection");
            }

            // Add a new schedule
            public async Task AddScheduleAsync(CreateProviderScheduleDto schedule)
            {
                using (SqlConnection connection = new SqlConnection(_connStr))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("CreateSchedule", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@ScheduleDate", schedule.ScheduleDate.ToDateTime(TimeOnly.MinValue));
                        command.Parameters.AddWithValue("@ScheduleTime", schedule.ScheduleTime.ToTimeSpan());
                        command.Parameters.AddWithValue("@Duration", schedule.Duration);
                        command.Parameters.AddWithValue("@ScheduleStatus", schedule.Status);
                        command.Parameters.AddWithValue("@PatientID", schedule.PatientId);
                        command.Parameters.AddWithValue("@ProviderID", schedule.ProviderId);
                        command.Parameters.AddWithValue("@ResourceID", schedule.ResourceId);
                        command.Parameters.AddWithValue("@LocationID", schedule.LocationId);

                        await command.ExecuteNonQueryAsync();
                    }

                    await connection.CloseAsync();
                }
            }

            // Update an existing schedule
            public async Task UpdateScheduleAsync(UpdateProviderScheduleDto schedule)
            {
                using (SqlConnection connection = new SqlConnection(_connStr))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("UpsertSchedule", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@ProviderScheduleID", schedule.ScheduleId);
                        command.Parameters.AddWithValue("@ScheduleDate", schedule.ScheduleDate.ToDateTime(TimeOnly.MinValue));
                        command.Parameters.AddWithValue("@ScheduleTime", schedule.ScheduleTime.ToTimeSpan());
                        command.Parameters.AddWithValue("@Duration", schedule.Duration);
                        command.Parameters.AddWithValue("@ScheduleStatus", schedule.Status);
                        command.Parameters.AddWithValue("@PatientID", schedule.PatientId);
                        command.Parameters.AddWithValue("@ProviderID", schedule.ProviderId);
                        command.Parameters.AddWithValue("@ResourceID", schedule.ResourceId);
                        command.Parameters.AddWithValue("@LocationID", schedule.LocationId);

                        await command.ExecuteNonQueryAsync();
                    }

                    await connection.CloseAsync();
                }
            }

            // Delete a schedule
            public async Task DeleteScheduleAsync(int id)
            {
                using (SqlConnection connection = new SqlConnection(_connStr))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("deleteSchedule", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ProviderScheduleID", id);

                        await command.ExecuteNonQueryAsync();
                    }

                    await connection.CloseAsync();
                }
            }

            // Get all schedules ----- raw form
            public async Task<List<ProviderSchedule>> GetAllSchedulesAsync()
            {
                List<ProviderSchedule> schedules = new List<ProviderSchedule>();

                using (SqlConnection connection = new SqlConnection(_connStr))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("getAllSchedules", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            ProviderSchedule schedule = new ProviderSchedule();

                            schedule.ScheduleId = reader.GetInt32(0);
                            schedule.ScheduleDate = DateOnly.FromDateTime(reader.GetDateTime(1));
                            schedule.ScheduleTime = TimeOnly.FromTimeSpan(reader.GetTimeSpan(2));
                            schedule.Duration = reader.GetInt32(3);
                            schedule.Status = reader.GetBoolean(4);
                            schedule.PatientId = reader.GetInt32(5);
                            schedule.ProviderId = reader.GetInt32(6);
                            schedule.ResourceId = reader.GetInt32(7);
                            schedule.LocationId = reader.GetInt32(8);

                            schedules.Add(schedule);
                        }
                    }

                    await connection.CloseAsync();
                }

                return schedules;
            }
        // Get all schedules ----- pure form



        // Get a single schedule by ID
        public async Task<ProviderSchedule> GetScheduleByIdAsync(int id)
            {
                ProviderSchedule schedule = new ProviderSchedule();

                using (SqlConnection connection = new SqlConnection(_connStr))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("getScheduleById", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("ProviderScheduleID", id);

                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            schedule.ScheduleId = reader.GetInt32(0);
                            schedule.ScheduleDate = DateOnly.FromDateTime(reader.GetDateTime(1));
                            schedule.ScheduleTime = TimeOnly.FromTimeSpan(reader.GetTimeSpan(2));
                            schedule.Duration = reader.GetInt32(3);
                            schedule.Status = reader.GetBoolean(4);
                            schedule.PatientId = reader.GetInt32(5);
                            schedule.ProviderId = reader.GetInt32(6);
                            schedule.ResourceId = reader.GetInt32(7);
                            schedule.LocationId = reader.GetInt32(8);
                        }
                    }

                    await connection.CloseAsync();
                }

                return schedule;
            }
        }
    }


