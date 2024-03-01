using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Aula82.MongoDB.Models.Referencia
{
    public class ClienteReferencia
    {
        [BsonElement("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("cpf")]
        [BsonRequired()]
        public string CPF { get; set; } = string.Empty;

        [BsonElement("nome")]
        [BsonRequired()]
        public string Nome { get; set; } = string.Empty;

        [BsonElement("datanascimento")]
        [BsonRequired()]
        public DateTime DataNascimento { get; set; }

    }
}
