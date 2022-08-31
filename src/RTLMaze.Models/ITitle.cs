using System.Reflection.Metadata;

namespace RTLMaze.Models;

public interface ITitle : IStorableEntity
{
	public string Name { get; set; }
}