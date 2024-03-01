using Aula82.MongoDB.Models.Embutido;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Aula82.MongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmbutidoClienteController : Controller
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<ClienteEmbutido> _dbCollection;

        public EmbutidoClienteController()
        {
            _client = new MongoClient("mongodb://root:example@localhost");
            _database = _client.GetDatabase("Dados");
            _dbCollection = _database.GetCollection<ClienteEmbutido>("Embutido");
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ClienteEmbutido clienteEmbutido)
        {
            await _dbCollection.InsertOneAsync(clienteEmbutido);
            return Ok();
        }

        [HttpGet(Name = "TodosEmbutidos")]
        public async Task<ActionResult<IEnumerable<ClienteEmbutido>>> GetTodos()
        {
            return await _dbCollection.Find(_ => true).ToListAsync();
        }

        [HttpGet()]
        [Route("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            var filter = Builders<ClienteEmbutido>.Filter;
            var eqFilter = filter.Eq(x => x.Id, id);
            var retorno = await _dbCollection.FindAsync<ClienteEmbutido>(eqFilter).ConfigureAwait(false);

            return Ok(retorno.FirstOrDefault());
        }
    }
}
