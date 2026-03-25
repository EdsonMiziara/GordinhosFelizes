namespace GordinhosFelizes.Domain.Exceptions;

public class TaskCancelException : Exception
{
    public TaskCancelException(string message) : base(message) { }
}
