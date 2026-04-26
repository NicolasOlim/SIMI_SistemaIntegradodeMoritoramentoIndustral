using ApiProcessamento.Config;
using ApiProcessamento.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared;
using System.Linq;

namespace ApiProcessamento.Controllers
{
    [ApiController]
    [Route("api/v1/sensores")]
    public class SensorController : ControllerBase
    {
        private readonly IOptions<ApiConfig> _config;
        private readonly AppDbContext _context;

        public SensorController(IOptions<ApiConfig> config, AppDbContext context)
        {
            _config = config;
            _context = context;
        }

        /// <summary>
        /// Metodo para adicionar os valores do Sensores.
        /// </summary>
        /// <remarks>
        /// Esse metodo tem como objetivo adicionar os valores dos sensores.
        /// Obs: O Valores são enviados pelos sensores e adicionados automaticamente ao banco, por isso você pode ignorar totalmente ess metodo.
        /// </remarks>
        /// <param name="sensor"></param>
        /// <response code="201">Registro criado com sucesso. Retorna o novo objeto inserido.</response>
        /// <response code="400">Caso os dados enviados na requisição sejam inválidos.</response>
        /// <response code="500">Erro interno do servidor.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Receber(SensorData sensor)
        {
            try
            {
                _context.Add(sensor);
                await _context.SaveChangesAsync();
                return Ok(sensor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao salvar: {ex.Message}");
            }
        }

        ///<summary>
        /// Obtém todos os registros de sensores.
        /// </summary>
        /// <remarks>
        /// Esse metodo tem como objetivo retornar todos os dados que estão registrados no banco.
        /// </remarks>
        /// <response code="200">Retorna a lista de registros com sucesso.</response>
        /// <response code="500">Erro interno do servidor ao tentar buscar os registros.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var dadosDoBanco = await _context.Set<SensorData>().ToListAsync();
                return Ok(dadosDoBanco);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao listar: {ex.Message}");
            }
        }

        /// <summary>
        /// Metodo para obter um Registro especifico pelo seu id.
        /// </summary>
        /// <remarks>
        /// Esse metodo tem como foco buscar um registro especifico baseado no id inserido pelo usuario manualmente, então insira um numero inteiro e positivo para que o sistema possa buscar sem problemas.
        /// </remarks>
        /// <param name="id">O identificador único (ID) do Registro.</param>
        /// <response code="200">Retorna o registro correspondente ao ID informado.</response>
        /// <response code="404">Caso não exista nenhum registro com o ID informado.</response>
        /// <response code="500">Erro interno do servidor.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var registro = await _context.Set<SensorData>().FindAsync(id);

                if (registro == null)
                {
                    return NotFound(new { message = $"Registro com ID {id} não foi encontrado no sistema." });
                }

                return Ok(registro);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = $"Ocorreu um erro interno ao tentar buscar o registro com ID {id}.",
                    detalhe = ex.Message
                });
            }
        }

        /// <summary>
        /// Metodo para atualizar um registro existente.
        /// </summary>
        /// <remarks>
        /// Este metodo permite alterar os valores de temperatura ou umidade de um registro já gravado.
        /// </remarks>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] SensorData sensor)
        {
            if (id != sensor.Id)
                return BadRequest("O ID enviado na URL não coincide com o ID do objeto.");

            var existeNoBanco = await _context.Set<SensorData>().AnyAsync(x => x.Id == id);
            if (!existeNoBanco)
                return NotFound($"Registro com ID {id} não encontrado.");

            try
            {
                _context.Update(sensor);
                await _context.SaveChangesAsync();
                return Ok(sensor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar: {ex.Message}");
            }
        }

        /// <summary>
        /// Metodo para deletar um Registro existente pelo seu id.
        /// </summary>
        /// <remarks>
        /// **Atenção:** Esta metodo removerá os dados permanentemente, sem chances de recuperação.
        /// </remarks>
        /// <param name="id">O identificador único (ID) do Registro que será removido.</param>
        /// <response code="204">Registro deletado com sucesso (Sem conteúdo).</response>
        /// <response code="404">Caso o ID informado não exista no banco de dados.</response>
        /// <response code="500">Erro interno do servidor ao tentar excluir.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var registro = await _context.Set<SensorData>().FindAsync(id);
                if (registro == null)
                    return NotFound($"Registro com ID {id} não encontrado.");

                _context.Remove(registro);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao excluir: {ex.Message}");
            }
        }
    }
}