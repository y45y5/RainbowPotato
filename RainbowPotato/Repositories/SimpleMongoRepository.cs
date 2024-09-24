using RainbowPotato.Dao;
using RainbowPotato.Model;

namespace RainbowPotato.Repositories
{
    internal class SimpleMongoRepository<T> : ISimpleMongoRepository<T> where T : ISimpleMongoModel
    {
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        private readonly ISimpleDao<T> _mongoDao;

        public SimpleMongoRepository(ISimpleDao<T> mongoDao)
        {
            _mongoDao = mongoDao;
        }

        public async Task<List<T>> GetResults(string cacheKey)
        {
            await _semaphore.WaitAsync();

            try
            {
                List<T> results = _mongoDao.GetResultsFromDatabase("ImgurAlbumTrigger#0");
                results ??= new List<T>();

                return results;
            }
            catch (Exception e)
            {
                // TODO sodkaodksaodkmafm
            }
            finally 
            { 
                _semaphore.Release(); 
            }

            return new List<T>();
        }
    }
}
