using System.Text.Json.Serialization;

namespace Movies.Contracts.Responses;

public abstract class HalResponse
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<Link>? Links { get; set; }
}