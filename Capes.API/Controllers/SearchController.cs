using System;
using System.Collections.Generic;
using Capes.API.Models;
using Capes.Domain;
using Capes.Helpers;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace Capes.API.Controllers
{
    [DisableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        [HttpPost]
        [ActionName("Filter")]
        public IActionResult Filter([FromBody]BookFilters filters)
        {
            IActionResult result = null;
            try
            {
                using (MySqlConnection ctx = new MySqlConnection(ConnectionString.GetConnectionString()))
                {
                    ctx.Open();

                    string filterBooksQuery = $@"
                        SELECT 
                            {Book.ColumnNames.Id},
                            {Book.ColumnNames.WorkType},
                            {Book.ColumnNames.BookAuthors},
                            {Book.ColumnNames.ChapAuthors},
                            {Book.ColumnNames.Editors},
                            {Book.ColumnNames.BookTitle},
                            {Book.ColumnNames.ChapTitle},
                            {Book.ColumnNames.FirstPage},
                            {Book.ColumnNames.LastPage},
                            {Book.ColumnNames.Editorial},
                            {Book.ColumnNames.City},
                            {Book.ColumnNames.Country},
                            {Book.ColumnNames.Year},
                            {Book.ColumnNames.Isbn},
                            {Book.ColumnNames.Report}
                        FROM books
                        WHERE 
                            MATCH(
                                {Book.ColumnNames.WorkType},
                                {Book.ColumnNames.BookAuthors},
                                {Book.ColumnNames.ChapAuthors},
                                {Book.ColumnNames.Editors},
                                {Book.ColumnNames.BookTitle},
                                {Book.ColumnNames.ChapTitle},
                                {Book.ColumnNames.Editorial},
                                {Book.ColumnNames.City},
                                {Book.ColumnNames.Country},
                                {Book.ColumnNames.Year},
                                {Book.ColumnNames.Isbn},
                                {Book.ColumnNames.Report}
                            )
                            AGAINST(""{filters.Text}*"" IN BOOLEAN MODE)
                    ";
                    List<Book> books = FilterBooks(filterBooksQuery, ctx);

                    string filterAwardsQuery = $@"
                        SELECT 
                            {Award.ColumnNames.Id},
                            {Award.ColumnNames.Name},
                            {Award.ColumnNames.Awardee},
                            {Award.ColumnNames.Contribution},
                            {Award.ColumnNames.Institution},
                            {Award.ColumnNames.Country},
                            {Award.ColumnNames.Year},
                            {Award.ColumnNames.Report}
                        FROM awards
                        WHERE 
                            MATCH(
                                {Award.ColumnNames.Name},
                                {Award.ColumnNames.Awardee},
                                {Award.ColumnNames.Contribution},
                                {Award.ColumnNames.Institution},
                                {Award.ColumnNames.Country},
                                {Award.ColumnNames.Year},
                                {Award.ColumnNames.Report}
                            )
                            AGAINST(""{filters.Text}*"" IN BOOLEAN MODE)
                    ";
                    List<Award> awards = FilterAwards(filterAwardsQuery, ctx);

                    string filterParticipationsQuery = $@"
                        SELECT 
                            {Participation.ColumnNames.Id},
                            {Participation.ColumnNames.Type},
                            {Participation.ColumnNames.NameEvent},
                            {Participation.ColumnNames.Country},
                            {Participation.ColumnNames.City},
                            {Participation.ColumnNames.Modality},
                            {Participation.ColumnNames.Title},
                            {Participation.ColumnNames.CapesPersons},
                            {Participation.ColumnNames.Report}
                        FROM participation
                        WHERE 
                            MATCH(
                                {Participation.ColumnNames.Type},
                                {Participation.ColumnNames.NameEvent},
                                {Participation.ColumnNames.Country},
                                {Participation.ColumnNames.City},
                                {Participation.ColumnNames.Modality},
                                {Participation.ColumnNames.Title},
                                {Participation.ColumnNames.CapesPersons},
                                {Participation.ColumnNames.Report}
                            )
                            AGAINST(""{filters.Text}*"" IN BOOLEAN MODE)
                    ";
                    List<Participation> participations = FilterParticipations(filterParticipationsQuery, ctx);

                    string filterPublicationsQuery = $@"
                        SELECT 
                            {Publication.ColumnNames.Id},
                            {Publication.ColumnNames.Doi},
                            {Publication.ColumnNames.Authors},
                            {Publication.ColumnNames.ArticleTitle},
                            {Publication.ColumnNames.JournalName},
                            {Publication.ColumnNames.Volume},
                            {Publication.ColumnNames.Year},
                            {Publication.ColumnNames.FirstPage},
                            {Publication.ColumnNames.LastPage},
                            {Publication.ColumnNames.Notes},
                            {Publication.ColumnNames.Report}
                        FROM public
                        WHERE 
                            MATCH(
                                {Publication.ColumnNames.Doi},
                                {Publication.ColumnNames.Authors},
                                {Publication.ColumnNames.ArticleTitle},
                                {Publication.ColumnNames.JournalName},
                                {Publication.ColumnNames.Volume},
                                {Publication.ColumnNames.Year},
                                {Publication.ColumnNames.FirstPage},
                                {Publication.ColumnNames.LastPage},
                                {Publication.ColumnNames.Notes},
                                {Publication.ColumnNames.Report}
                            )
                            AGAINST(""{filters.Text}*"" IN BOOLEAN MODE)
                    ";
                    List<Publication> publications = FilterPublications(filterPublicationsQuery, ctx);
                    
                    FilterResult search = new FilterResult() { 
                        Books = books,
                        Awards = awards,
                        Participations = participations, 
                        Publications = publications
                    };

                    result = Ok(search);
                    ctx.Close();
                }
            }
            catch (Exception ex)
            {
                result = BadRequest(new { message = ex.Message });
            }

            return result;
        }

        public List<Book> FilterBooks(string query, MySqlConnection ctx)
        {
            List<Book> result = null;

            MySqlCommand command = new MySqlCommand(query, ctx);
            MySqlDataReader rdr = command.ExecuteReader();
            
            while (rdr.Read())
            {
                // initialize list only if the query returns any books
                if (result == null)
                    result = new List<Book>();

                Book book = new Book
                {
                    Id = Convert.ToInt32(rdr[Book.ColumnNames.Id]),
                    WorkType = rdr[Book.ColumnNames.WorkType]?.ToString(),
                    BookAuthors = rdr[Book.ColumnNames.BookAuthors]?.ToString(),
                    ChapAuthors = rdr[Book.ColumnNames.ChapAuthors]?.ToString(),
                    Editors = rdr[Book.ColumnNames.Editors]?.ToString(),
                    BookTitle = rdr[Book.ColumnNames.BookTitle]?.ToString(),
                    ChapTitle = rdr[Book.ColumnNames.ChapTitle]?.ToString(),
                    FirstPage = rdr[Book.ColumnNames.FirstPage]?.ToString(),
                    LastPage = rdr[Book.ColumnNames.LastPage]?.ToString(),
                    Editorial = rdr[Book.ColumnNames.Editorial]?.ToString(),
                    City = rdr[Book.ColumnNames.City]?.ToString(),
                    Country = rdr[Book.ColumnNames.Country]?.ToString(),
                    Year = rdr[Book.ColumnNames.Year]?.ToString(),
                    Isbn = rdr[Book.ColumnNames.Isbn]?.ToString(),
                    Report = rdr[Book.ColumnNames.Report]?.ToString()
                };

                result.Add(book);
            }

            command.Dispose();
            rdr.Dispose();

            return result;
        }

        public List<Award> FilterAwards(string query, MySqlConnection ctx)
        {
            List<Award> result = null;

            MySqlCommand command = new MySqlCommand(query, ctx);
            MySqlDataReader rdr = command.ExecuteReader();

            while (rdr.Read())
            {
                // initialize list only if the query returns any books
                if (result == null)
                    result = new List<Award>();

                Award award = new Award
                {
                    Id = Convert.ToInt32(rdr[Award.ColumnNames.Id]),
                    Name = rdr[Award.ColumnNames.Name]?.ToString(),
                    Awardee = rdr[Award.ColumnNames.Awardee]?.ToString(),
                    Contribution = rdr[Award.ColumnNames.Contribution]?.ToString(),
                    Institution = rdr[Award.ColumnNames.Institution]?.ToString(),
                    Country = rdr[Award.ColumnNames.Country]?.ToString(),
                    Year = rdr[Award.ColumnNames.Year]?.ToString(),
                    Report = rdr[Award.ColumnNames.Report]?.ToString()
                };

                result.Add(award);
            }

            command.Dispose();
            rdr.Dispose();

            return result;
        }

        public List<Participation> FilterParticipations(string query, MySqlConnection ctx)
        {
            List<Participation> result = null;

            MySqlCommand command = new MySqlCommand(query, ctx);
            MySqlDataReader rdr = command.ExecuteReader();

            while (rdr.Read())
            {
                // initialize list only if the query returns any books
                if (result == null)
                    result = new List<Participation>();

                Participation participation = new Participation
                {
                    Id = Convert.ToInt32(rdr[Participation.ColumnNames.Id]),
                    Type = rdr[Participation.ColumnNames.Type]?.ToString(),
                    NameEvent = rdr[Participation.ColumnNames.NameEvent]?.ToString(),
                    Country = rdr[Participation.ColumnNames.Country]?.ToString(),
                    City = rdr[Participation.ColumnNames.City]?.ToString(),
                    Modality = rdr[Participation.ColumnNames.Modality]?.ToString(),
                    Title = rdr[Participation.ColumnNames.Title]?.ToString(),
                    CapesPersons = rdr[Participation.ColumnNames.CapesPersons]?.ToString(),
                    Report = rdr[Participation.ColumnNames.Report]?.ToString()
                };

                result.Add(participation);
            }

            command.Dispose();
            rdr.Dispose();

            return result;
        }

        public List<Publication> FilterPublications(string query, MySqlConnection ctx)
        {
            List<Publication> result = null;

            MySqlCommand command = new MySqlCommand(query, ctx);
            MySqlDataReader rdr = command.ExecuteReader();

            while (rdr.Read())
            {
                // initialize list only if the query returns any books
                if (result == null)
                    result = new List<Publication>();

                Publication publication = new Publication
                {
                    Id = Convert.ToInt32(rdr[Publication.ColumnNames.Id]),
                    Doi = rdr[Publication.ColumnNames.Doi]?.ToString(),
                    Authors = rdr[Publication.ColumnNames.Authors]?.ToString(),
                    ArticleTitle = rdr[Publication.ColumnNames.ArticleTitle]?.ToString(),
                    JournalName = rdr[Publication.ColumnNames.JournalName]?.ToString(),
                    Volume = rdr[Publication.ColumnNames.Volume]?.ToString(),
                    Year = rdr[Publication.ColumnNames.Year]?.ToString(),
                    FirstPage = rdr[Publication.ColumnNames.FirstPage]?.ToString(),
                    LastPage = rdr[Publication.ColumnNames.LastPage]?.ToString(),
                    Notes = rdr[Publication.ColumnNames.Notes]?.ToString(),
                    Report = rdr[Publication.ColumnNames.Report]?.ToString()
                };

                result.Add(publication);
            }

            command.Dispose();
            rdr.Dispose();

            return result;
        }
    }
}
