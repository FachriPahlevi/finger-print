using DPUruNet;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FingerPrint4
{
    internal class UserRepository
    {
        public bool ExistsByName(string name)
        {
            using (SqlConnection connection = DbConnectionFactory.CreateConnection())
            {
                connection.Open();

                const string sql = "SELECT 1 FROM Users WHERE Name=@Name";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        return reader.Read();
                    }
                }
            }
        }

        public void Add(UserFingerprint user)
        {
            using (SqlConnection connection = DbConnectionFactory.CreateConnection())
            {
                connection.Open();

                const string sql =
                    @"INSERT INTO Users
                        (Name, FingerPrint, Password)
                        VALUES
                        (@Name, @FingerPrint, @Password)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@FingerPrint", Fmd.SerializeXml(user.Fmd));
                    command.Parameters.AddWithValue("@Password", PasswordProtector.Encrypt(user.Password));
                    command.ExecuteNonQuery();
                }
            }
        }

        public bool IsValidCredential(string name, string password)
        {
            using (SqlConnection connection = DbConnectionFactory.CreateConnection())
            {
                connection.Open();

                const string sql =
                    @"SELECT 1 FROM Users
                    WHERE Name=@Name
                    AND Password=@Password";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Password", PasswordProtector.Encrypt(password));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        return reader.Read();
                    }
                }
            }
        }

        public List<UserFingerprint> GetUsersWithFingerprints()
        {
            List<UserFingerprint> users = new List<UserFingerprint>();

            using (SqlConnection connection = DbConnectionFactory.CreateConnection())
            {
                connection.Open();

                const string sql = "SELECT Name, Password, FingerPrint FROM Users";
                using (SqlCommand command = new SqlCommand(sql, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string fingerprintXml = reader["FingerPrint"].ToString();

                        if (string.IsNullOrWhiteSpace(fingerprintXml))
                        {
                            continue;
                        }

                        try
                        {
                            users.Add(new UserFingerprint
                            {
                                Name = reader["Name"].ToString(),
                                Password = reader["Password"].ToString(),
                                Fmd = Fmd.DeserializeXml(fingerprintXml)
                            });
                        }
                        catch
                        {
                            // Skip invalid fingerprint rows so one bad record does not block login.
                        }
                    }
                }
            }

            return users;
        }
    }
}
