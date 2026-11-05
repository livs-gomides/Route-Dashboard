
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RouteDashboard.Api.Model;

public class Preference
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("userId")]
    public string UserId { get; set; } = string.Empty;

    [BsonElement("age")]
    public int Age { get; set; }

    [BsonElement("visualImpairmentType")]
    public string VisualImpairmentType { get; set; } = string.Empty;

    [BsonElement("visualImpairmentLevel")]
    public string VisualImpairmentLevel { get; set; } = string.Empty;

    [BsonElement("distanceUnit")]
    public string DistanceUnit { get; set; } = "Meters";

    [BsonElement("orientationInDegrees")]
    public bool OrientationInDegrees { get; set; }

    [BsonElement("preferredContrast")]
    public string PreferredContrast { get; set; } = string.Empty;

    [BsonElement("preferredColor")]
    public string PreferredColor { get; set; } = string.Empty;

    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
