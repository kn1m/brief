namespace brief.Data.Repositories
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

        public async Task<(Guid bookId, Guid seriesId)> AddBookToSeries(Guid bookId, Guid seriesId)
        {
            await Connection.ExecuteAsync("insert into dbo.books_in_series (BookId, SeriesId)" +
                                          " values (@book, @series)",
                                          new
                                          {
                                              book = bookId,
                                              series = seriesId
                                          });
            return (bookId, seriesId);
        }

        public async Task<int> RemoveBookFromSeries(Guid bookId, Guid seriesId)
            => await Connection.ExecuteAsync("delete from dbo.books_in_series where BookId = @book and SeriesId = @series",
                new { book = bookId,
                      series = seriesId });

        public async Task<bool> CheckAvailabilityAddingBookToSeries(Guid bookId, Guid seriesId)
        {
            var sql = "select count(*) from dbo.serieses where Id = @series; " +
                      "select count(*) from dbo.books where Id = @book;" +
                      "select count(*) from dbo.books_in_series where SeriesId = @series and BookId = @book;";

            int seriesAmount;
            int booksAmount;
            int booksInSeriesAmount;

            using (SqlMapper.GridReader multi = await Connection.QueryMultipleAsync(sql, new { series = seriesId, book = bookId }))
            {
                seriesAmount = multi.Read<int>().Single();
                booksAmount = multi.Read<int>().Single();
                booksInSeriesAmount = multi.Read<int>().Single();
            }

            if (booksAmount == 1 && seriesAmount == 1 && booksInSeriesAmount == 0)
            {
                return true;
            }

            return false;
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
            var sql = "select * from dbo.serieses where Id = @seriesId; " +
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
