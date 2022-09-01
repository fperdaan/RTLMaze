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


	public virtual bool Is( JobStatus status ) => Status == status;

	# region Fluid interface ( proxy for start/end )

	public Job End()
	{
		DateEnd = DateTime.UtcNow;

		return this;
	}

	public virtual Job Start()
	{
		DateStart = DateTime.UtcNow;

		return this;
	}
	# endregion
}