using Aula82.MongoDB.Models.Referencia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Aula82.MongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReferenciaDependenteController : ControllerBase
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<DependenteReferencia> _dbCollection;

        public ReferenciaDependenteController()
        {
            _client = new MongoClient("mongodb://root:example@localhost");
            _database = _client.GetDatabase("Dados");
            _dbCollection = _database.GetCollection<DependenteReferencia>("DependenteReferencia");
        }


        [HttpPost]
        public async Task<ActionResult> Add([FromBody] DependenteReferencia dependenteReferencia)
        {
            await _dbCollection.InsertOneAsync(dependenteReferencia);
            return Ok();
        }

        [HttpGet(Name = "TodosDependenteReferencia")]
        public async Task<ActionResult<IEnumerable<DependenteReferencia>>> GetTodos()
        {
            return await _dbCollection.Find(_ => true).ToListAsync();
        }

        [HttpGet()]
        [Route("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            var filter = Builders<DependenteReferencia>.Filter;
            var eqFilter = filter.Eq(x => x.Id, id);
            var retorno = await _dbCollection.FindAsync<DependenteReferencia>(eqFilter).ConfigureAwait(false);

            return Ok(retorno.FirstOrDefault());
        }

        
    }
}
