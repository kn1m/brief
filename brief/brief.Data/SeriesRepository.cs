namespace brief.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Library.Entities;
    using Library.Repositories;

    public class SeriesRepository : BaseDapperRepository, ISeriesRepository
    {
        public SeriesRepository(string connectionString) : base(connectionString) {}

        public Task AddBookToSeries(Guid bookId, Guid seriesId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CheckSeriesForUniqueness(Series series)
        {
            var existingCount = (await Connection.QueryAsync<int>("select count(*) from dbo.serieses where Name = @name and " +
                                                                  "Description = @description",
                                                                  new
                                                                  {
                                                                      name = series.Name,
                                                                      description = series.Description
                                                                  })).Single();
            if (existingCount != 0)
            {
                return false;
            }

            return true;
        }

        public async Task<Series> GetSeries(Guid id)
        {
            const string sql = "select * from dbo.serieses where Id = @seriesId; " +
                                 "select b.Id, b.Name, b.Description from dbo.books b inner join books_in_series bs on b.Id = bs.BookId and SeriesId = @seriesId";

            Series series;

            using (SqlMapper.GridReader multi = await Connection.QueryMultipleAsync(sql, new { seriesId = id }))
            {
                series = multi.Read<Series>().SingleOrDefault();

                if (series != null)
                {
                    series.BooksInSeries = multi.Read<Book>().ToList();
                }
            }

            return series;
        }           

        public async Task<Guid> CreateSerires(Series serires)
        {
            await Connection.ExecuteAsync("insert into dbo.serieses (Id, Name, Description)" +
                                          " values (@id, @name, @description)",
                                          new
                                          {
                                              id = serires.Id,
                                              name = serires.Name,
                                              description = serires.Description
                                          });
            return serires.Id;
        }

        public async Task<Guid> UpdateSerires(Series serires)
        {
            await Connection.ExecuteAsync("update dbo.serieses set Name = @name," +
                                          " Description = @description where Id = @id",
                                          new
                                          {
                                              id = serires.Id,
                                              name = serires.Name,
                                              description = serires.Description
                                          });
            return serires.Id;
        }

        public async Task RemoveSerires(Series serires)
            => await Connection.ExecuteAsync("delete from dbo.serieses where Id = @id",
                new { id = serires.Id });
    }
}
