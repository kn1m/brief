namespace brief.Data
{
    using Library.Repositories;
    class PublisherRepostitory : BaseRepository, IPublisherRepository
    {
        public PublisherRepostitory(IApplicationDbContext appContext) : base(appContext)
        {
        }
    }
}
