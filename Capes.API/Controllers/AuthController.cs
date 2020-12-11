using Capes.API.Models;
using Capes.Helpers;
using MySql.Data.MySqlClient;
using System;
using Microsoft.AspNetCore.Mvc;
using Capes.Domain;
using Microsoft.AspNetCore.Cors;

namespace Capes.API.Controllers
{
    [DisableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        [ActionName("SignIn")]
        public IActionResult SignIn([FromBody]SignInRequest credentials)
        {
            IActionResult result = null;
            try
            {
                if (credentials != null)
                {
                    if (string.IsNullOrWhiteSpace(credentials.Username))
                        throw new Exception($"Email is required.");

                    if (string.IsNullOrWhiteSpace(credentials.Password))
                        throw new Exception($"Password is required.");

                    using (MySqlConnection ctx = new MySqlConnection(ConnectionString.GetConnectionString()))
                    {
                        ctx.Open();

                        string query = $@"
                            SELECT {Domain.User.ColumnNames.Id},{Domain.User.ColumnNames.Email}
                            FROM users
                            WHERE LOWER({Domain.User.ColumnNames.Email}) = LOWER(""{credentials.Username}"")
                            AND LOWER({Domain.User.ColumnNames.Password}) = LOWER(""{credentials.Password}"")
                        ";

                        MySqlCommand command = new MySqlCommand(query, ctx);
                        MySqlDataReader rdr = command.ExecuteReader();

                        Domain.User user = null;
                        while (rdr.Read())
                        {
                            user = new Domain.User()
                            {
                                Id = Convert.ToInt32(rdr[Domain.User.ColumnNames.Id]),
                                Email = rdr[Domain.User.ColumnNames.Email]?.ToString()
                            };

                            // to take the first coincidence
                            break;
                        }

                        if (user == null)
                            throw new Exception($"Wrong credentials.");

                        result = Ok(user);

                        ctx.Close();
                    }
                }
                else
                {
                    result = BadRequest($"Credentials are required");
                }
            }
            catch (Exception ex)
            {
                result = BadRequest(new { message = ex.Message });
            }

            return result;
        }
    }
}
