using System.Text.Json.Serialization;
public class Book
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("author")]
    public string? Author { get; set; }

    [JsonPropertyName("image")]
    public string? Image { get; set; }
    
    [JsonPropertyName("published")]
    public int Published { get; set; }

    [JsonPropertyName("price")]
    public double Price { get; set; }

    [JsonPropertyName("synopsis")]
    public string? Synopsis { get; set;}
}

[JsonSerializable(typeof(List<Book>))]
public sealed partial class ProductSerializerContext : JsonSerializerContext
{

}