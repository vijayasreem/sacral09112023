using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;

namespace sacral09112023
{
    public class UserRepository : IUserRepository
    {
        private readonly string connectionString;

        public UserRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<int> CreateAsync(UserModel user)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new NpgsqlCommand("INSERT INTO users (name, email, phone_number, address, annual_income, credit_score, disbursed_amount, vehicle_assessment_value, payment_amount, vendor_name, funds_availability, payment_approval) " +
                                                "VALUES (@Name, @Email, @PhoneNumber, @Address, @AnnualIncome, @CreditScore, @DisbursedAmount, @VehicleAssessmentValue, @PaymentAmount, @VendorName, @FundsAvailability, @PaymentApproval) " +
                                                "RETURNING id", connection);
                command.Parameters.AddWithValue("Name", user.Name);
                command.Parameters.AddWithValue("Email", user.Email);
                command.Parameters.AddWithValue("PhoneNumber", user.PhoneNumber);
                command.Parameters.AddWithValue("Address", user.Address);
                command.Parameters.AddWithValue("AnnualIncome", user.AnnualIncome);
                command.Parameters.AddWithValue("CreditScore", user.CreditScore);
                command.Parameters.AddWithValue("DisbursedAmount", user.DisbursedAmount);
                command.Parameters.AddWithValue("VehicleAssessmentValue", user.VehicleAssessmentValue);
                command.Parameters.AddWithValue("PaymentAmount", user.PaymentAmount);
                command.Parameters.AddWithValue("VendorName", user.VendorName);
                command.Parameters.AddWithValue("FundsAvailability", user.FundsAvailability);
                command.Parameters.AddWithValue("PaymentApproval", user.PaymentApproval);
                return Convert.ToInt32(await command.ExecuteScalarAsync());
            }
        }

        public async Task<UserModel> GetByIdAsync(int id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new NpgsqlCommand("SELECT * FROM users WHERE id = @Id", connection);
                command.Parameters.AddWithValue("Id", id);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new UserModel
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = reader["name"].ToString(),
                            Email = reader["email"].ToString(),
                            PhoneNumber = reader["phone_number"].ToString(),
                            Address = reader["address"].ToString(),
                            AnnualIncome = Convert.ToDecimal(reader["annual_income"]),
                            CreditScore = Convert.ToInt32(reader["credit_score"]),
                            DisbursedAmount = Convert.ToDecimal(reader["disbursed_amount"]),
                            VehicleAssessmentValue = Convert.ToDecimal(reader["vehicle_assessment_value"]),
                            PaymentAmount = Convert.ToDecimal(reader["payment_amount"]),
                            VendorName = reader["vendor_name"].ToString(),
                            FundsAvailability = Convert.ToBoolean(reader["funds_availability"]),
                            PaymentApproval = Convert.ToBoolean(reader["payment_approval"])
                        };
                    }
                    return null;
                }
            }
        }

        public async Task<List<UserModel>> GetAllAsync()
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new NpgsqlCommand("SELECT * FROM users", connection);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    var users = new List<UserModel>();
                    while (await reader.ReadAsync())
                    {
                        users.Add(new UserModel
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = reader["name"].ToString(),
                            Email = reader["email"].ToString(),
                            PhoneNumber = reader["phone_number"].ToString(),
                            Address = reader["address"].ToString(),
                            AnnualIncome = Convert.ToDecimal(reader["annual_income"]),
                            CreditScore = Convert.ToInt32(reader["credit_score"]),
                            DisbursedAmount = Convert.ToDecimal(reader["disbursed_amount"]),
                            VehicleAssessmentValue = Convert.ToDecimal(reader["vehicle_assessment_value"]),
                            PaymentAmount = Convert.ToDecimal(reader["payment_amount"]),
                            VendorName = reader["vendor_name"].ToString(),
                            FundsAvailability = Convert.ToBoolean(reader["funds_availability"]),
                            PaymentApproval = Convert.ToBoolean(reader["payment_approval"])
                        });
                    }
                    return users;
                }
            }
        }

        public async Task<int> UpdateAsync(UserModel user)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new NpgsqlCommand("UPDATE users SET name = @Name, email = @Email, phone_number = @PhoneNumber, address = @Address, " +
                                                "annual_income = @AnnualIncome, credit_score = @CreditScore, disbursed_amount = @DisbursedAmount, " +
                                                "vehicle_assessment_value = @VehicleAssessmentValue, payment_amount = @PaymentAmount, " +
                                                "vendor_name = @VendorName, funds_availability = @FundsAvailability, payment_approval = @PaymentApproval " +
                                                "WHERE id = @Id", connection);
                command.Parameters.AddWithValue("Name", user.Name);
                command.Parameters.AddWithValue("Email", user.Email);
                command.Parameters.AddWithValue("PhoneNumber", user.PhoneNumber);
                command.Parameters.AddWithValue("Address", user.Address);
                command.Parameters.AddWithValue("AnnualIncome", user.AnnualIncome);
                command.Parameters.AddWithValue("CreditScore", user.CreditScore);
                command.Parameters.AddWithValue("DisbursedAmount", user.DisbursedAmount);
                command.Parameters.AddWithValue("VehicleAssessmentValue", user.VehicleAssessmentValue);
                command.Parameters.AddWithValue("PaymentAmount", user.PaymentAmount);
                command.Parameters.AddWithValue("VendorName", user.VendorName);
                command.Parameters.AddWithValue("FundsAvailability", user.FundsAvailability);
                command.Parameters.AddWithValue("PaymentApproval", user.PaymentApproval);
                command.Parameters.AddWithValue("Id", user.Id);
                return await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new NpgsqlCommand("DELETE FROM users WHERE id = @Id", connection);
                command.Parameters.AddWithValue("Id", id);
                return await command.ExecuteNonQueryAsync();
            }
        }
    }
}