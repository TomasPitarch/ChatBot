public class Result<T>
{
    public bool IsSuccess { get; }
    public T Value { get; }
    public string ErrorMessage { get; }

     
    public Result(T value, bool isSuccess = true)
    {
        IsSuccess = isSuccess;
        Value = value;
    }
    
}