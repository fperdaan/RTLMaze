namespace RTLMaze.Core;

public class JsonFormatException : Exception
{
	public JsonFormatException() { }

    public JsonFormatException( string message ) : base( message ) { }

    public JsonFormatException( string message, Exception inner ) : base(message, inner) { }
}