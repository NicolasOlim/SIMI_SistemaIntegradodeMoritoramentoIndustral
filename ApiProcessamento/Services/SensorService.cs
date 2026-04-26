using ApiProcessamento.Repositories.Interfaces;
using Shared;

namespace ApiProcessamento.Services
{
    public class SensorService
    {

        public readonly ISensorRepository _repository;

        public SensorService(ISensorRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SensorData>> GetAll()
            => await _repository.GetAll();

        public async Task<SensorData> GetById(int id)
            => await _repository.GetById(id);


        public async Task Add(SensorData data)
            => await _repository.Add(data);
        public async Task Update(SensorData data)
            => await _repository.Update(data);
        public async Task Delete(int id)
            => await _repository.Delete(id);


    }
}
