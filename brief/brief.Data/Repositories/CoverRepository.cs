namespace brief.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Library.Entities;
    using Library.Repositories;

    public class CoverRepository : BaseDapperRepository,  ICoverRepository
    {
        public CoverRepository(string connectionString) : base(connectionString) {}

        public async Task<Guid> SaveCover(Cover cover)
        {
            await Connection.ExecuteAsync("insert into dbo.covers (Id, LinkTo, EditionId)" +
                                          " values (@id, @linkTo, @editionId)",
                                          new
                                          {
                                              id = cover.Id,
                                              linkTo = cover.LinkTo,
                                              editionId = cover.EditionId
                                          });
            return cover.Id;
        }

        public async Task<List<Cover>> GetCoversByEdition(Guid id)
            => (List<Cover>) await Connection.QueryAsync<Cover>("select * from dbo.covers where EditionId = @editionId", new { editionId = id});

        public async Task<Cover> GetCover(Guid id)
            => await Connection.QueryFirstAsync<Cover>("select * from dbo.covers where Id = @coverId", new { coverId = id });

        public async Task<bool> CheckCoverForUniqueness(Cover cover)
        {
            var existingCount = (await Connection.QueryAsync<int>("select count(*) from dbo.covers where LinkTo = @path and ",
                                                                  new
                                                                  {
                                                                      path = cover.LinkTo
                                                                  })).Single();
            if (existingCount != 0)
            {
                return false;
            }

            return true;
        }

        public async Task RemoveCovers(IEnumerable<Cover> covers)
        {
            var columnName = "Id";
            var coversList = covers.ToList();

            await Connection.ExecuteAsync(string.Format("CREATE TABLE #{0}s({0} UNIQUEIDENTIFIER PRIMARY KEY)", columnName));

            using (var bulkCopy = new SqlBulkCopy(Connection))
            {
                bulkCopy.BatchSize = coversList.Count;
                bulkCopy.DestinationTableName = $"#{columnName}s";

                var table = new DataTable();
                table.Columns.Add(columnName, typeof(Guid));
                bulkCopy.ColumnMappings.Add(columnName, columnName);

                foreach (var cover in coversList)
                {
                    table.Rows.Add(cover.Id);
                }

                await bulkCopy.WriteToServerAsync(table);
            }

            await Connection.ExecuteAsync(string.Format(@"DELETE FROM dbo.covers where Id IN 
                                   (SELECT {0} FROM #{0}s", columnName));

            await Connection.ExecuteAsync($"DROP TABLE #{columnName}s");
        }

        public async Task RemoveCover(Cover cover)
            => await Connection.ExecuteAsync("delete from dbo.covers where Id = @coverId",
                new { coverId = cover.Id });
    }
}
