using ApiProcessamento.Data;
using ApiProcessamento.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace ApiProcessamento.Repositories
{
    public class SensorRepository : ISensorRepository
    {

        private readonly AppDbContext _context;

        public SensorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SensorData>>GetAll() 
            => await _context.SensorData.ToListAsync();

        public async Task<SensorData> GetById(int id)
            => await _context.SensorData.FindAsync(id);

        public async Task Add(SensorData data)
        {
            _context.SensorData.Add(data);
            await _context.SaveChangesAsync();
        }

        public async Task Update(SensorData data) 
        {
            var dadosexistente = await _context.SensorData.FindAsync(data.Id);

            if(dadosexistente != null)
            {
                _context.Entry(dadosexistente).CurrentValues.SetValues(data);

                await _context.SaveChangesAsync();
            }
            else
            {

                throw new KeyNotFoundException($"Dado não registrado.");
            }
        
        
        }

        public async Task Delete(int id)
        {
            var dado = await _context.SensorData.FindAsync(id);
            if (dado == null)
            {

                throw new KeyNotFoundException($"Sala com ID {id} não encontrada.");
            }

            _context.SensorData.Remove(dado);
            await _context.SaveChangesAsync();
        }


    }
}
