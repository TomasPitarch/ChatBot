using Cysharp.Threading.Tasks;

public interface ILlmService
{
    
     public UniTask<Result<string>> SendMessageAsync(string message);
}