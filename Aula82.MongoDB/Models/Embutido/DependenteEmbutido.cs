using MongoDB.Bson.Serialization.Attributes;

namespace Aula82.MongoDB.Models.Embutido
{
    public class DependenteEmbutido
    {
        [BsonElement("nome")]
        [BsonRequired()]
        public string Nome { get; set; } = string.Empty;

        [BsonElement("datanascimento")]
        [BsonRequired()]
        public DateTime DataNascimento { get; set; }
    }
}
