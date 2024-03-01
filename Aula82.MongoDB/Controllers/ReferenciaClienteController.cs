using Aula82.MongoDB.Models.Referencia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Aula82.MongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReferenciaClienteController : ControllerBase
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<ClienteReferencia> _dbCollection;

        public ReferenciaClienteController()
        {
            _client = new MongoClient("mongodb://root:example@localhost");
            _database = _client.GetDatabase("Dados");
            _dbCollection = _database.GetCollection<ClienteReferencia>("ClienteReferencia");
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ClienteReferencia clienteReferencia)
        {
            await _dbCollection.InsertOneAsync(clienteReferencia);
            return Ok();
        }

        [HttpGet(Name = "TodosReferencia")]
        public async Task<ActionResult<IEnumerable<ClienteReferencia>>> GetTodos()
        {
            return await _dbCollection.Find(_ => true).ToListAsync();
        }

        [HttpGet()]
        [Route("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            var filter = Builders<ClienteReferencia>.Filter;
            var eqFilter = filter.Eq(x => x.Id, id);
            var retorno = await _dbCollection.FindAsync<ClienteReferencia>(eqFilter).ConfigureAwait(false);

            return Ok(retorno.FirstOrDefault());
        }

        [HttpPut(Name = "UpdateClienteReferencia")]
        public async Task<ActionResult> Update([FromQuery] string id, [FromBody] ClienteReferencia clienteReferencia)
        {
            FilterDefinitionBuilder<ClienteReferencia> eqFilter = Builders<ClienteReferencia>.Filter;
            FilterDefinition<ClienteReferencia> eqFilterDefinition = eqFilter.Eq(x => x.Id, id);

            UpdateDefinitionBuilder<ClienteReferencia> updateFilter = Builders<ClienteReferencia>.Update;
            UpdateDefinition<ClienteReferencia> updateFilterDefinition = updateFilter
                .Set(x => x.Nome, clienteReferencia.Nome)
                .Set(x => x.DataNascimento, clienteReferencia.DataNascimento);

            UpdateResult updateResult = await _dbCollection.UpdateOneAsync(eqFilterDefinition, updateFilterDefinition).ConfigureAwait(false);

            if (updateResult.ModifiedCount > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] string id)
        {
            FilterDefinitionBuilder<ClienteReferencia> eqFilter = Builders<ClienteReferencia>.Filter;
            FilterDefinition<ClienteReferencia> eqFilterDefinition = eqFilter.Eq(x => x.Id, id);

            DeleteResult res = await _dbCollection.DeleteOneAsync(eqFilterDefinition).ConfigureAwait(false);

            if(res.DeletedCount > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
