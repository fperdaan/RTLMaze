using System.Text.Json.Serialization;

namespace RTLMaze.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum JobStatus
{
	New,
	Queueued,	
	Running,
	Failed,
	Aborted,
	Processed
}