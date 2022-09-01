namespace RTLMaze.Models;

public class Job : StorableEntity
{
	public string Type { get; set; }
	public JobStatus Status { get; set; }

	public DateTime? DateStart { get; private set; }
	public DateTime? DateEnd { get; private set; }

	public Job( string type )
	{
		Type = type;
		Status = JobStatus.New;
	}

	# region Fluid interface

	public Job End()
	{
		DateEnd = DateTime.UtcNow;

		return this;
	}

	public Job End( JobStatus status ) => SetStatus( status ).End();

	public Job SetStatus( JobStatus status )
	{
		Status = status;

		return this;
	}

	public Job Start()
	{
		DateStart = DateTime.UtcNow;

		return this;
	}
	# endregion
}