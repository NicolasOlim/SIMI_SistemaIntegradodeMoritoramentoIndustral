using Shared;

namespace ApiProcessamento.Repositories.Interfaces
{
    public interface ISensorRepository
    {

        Task<List<SensorData>> GetAll();
        Task<SensorData> GetById(int id);
        Task Add(SensorData data);
        Task Update(SensorData data);
        Task Delete(int id);

    }
}
