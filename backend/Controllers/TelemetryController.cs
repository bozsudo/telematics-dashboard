using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

[ApiController]
[Route("api/[controller]")]
public class TelemetryController : ControllerBase
{
    // private readonly TelemetryDbContext _context;
    private static readonly List<TelemetryData> TelemetryData = new List<TelemetryData>();

    // public TelemetryController(TelemetryDbContext context)
    // {
    //     _context = context;
    // }

    [HttpGet]
    public IActionResult GetTelemetry()
    {
        return Ok(TelemetryData);
    }

    [HttpPost]
    public IActionResult AddTelemetry([FromBody] TelemetryData telemetry)
    {
        TelemetryData.Add(telemetry);
        return CreatedAtAction(nameof(GetTelemetry), new { id = telemetry.MachineId }, telemetry);
    }

    [HttpPost("send-data")]
    public IActionResult SendTelemetryData([FromBody] object telemetryPayload)
    {
        var publisher = new RabbitMQPublisher();
        var message = System.Text.Json.JsonSerializer.Serialize(telemetryPayload);
        publisher.PublishMessage("telemetry_queue", message);

        return Ok(new { Message = "Telemetry data sent to RabbitMQ", Payload = telemetryPayload });
    }
    
    // [HttpPost("save-data")]
    // public async Task<IActionResult> SaveTelemetryData([FromBody] TelemetryData data)
    // {
    //     data.Timestamp = DateTime.UtcNow; // Add timestamp
    //     _context.TelemetryRecords.Add(data);
    //     await _context.SaveChangesAsync();

    //     return Ok(new { Message = "Telemetry data saved successfully", Data = data });
    // }

    // [HttpPost("save-data-mongo")]
    // public IActionResult SaveTelemetryDataMongo([FromBody] object telemetryPayload)
    // {
    //     var service = new MongoDbService();
    //     var bsonData = telemetryPayload.ToBsonDocument();
    //     service.InsertTelemetryData(bsonData);

    //     return Ok(new { Message = "Telemetry data saved to MongoDB", Data = telemetryPayload });
    // }

}
