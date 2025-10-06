using Cysharp.Threading.Tasks;
using UnityEngine;

public class ChatController : MonoBehaviour
{
    [SerializeField]
    private ChatView chatView;
    
    [SerializeField]
    private LlmGeminiService service;
    
    
    private ILlmService _llmService;


    private void Start()
    {
        chatView.OnSendButtonClicked += HandleSendButtonClicked;
        _llmService = service;
    }

    private void HandleSendButtonClicked()
    {
        chatView.DisableInput();
        string text = chatView.GetInputFieldText();
        _llmService.SendMessageAsync(text).ContinueWith(OnServerAnswerReceived).Forget();
    }

    private UniTask OnServerAnswerReceived(Result<string> result)
    {
        if (result.IsSuccess)
        {
            string text = chatView.GetInputFieldText();
            chatView.ClearInputField();
            chatView.AddMessageToChat(text);
            chatView.AddMessageToChat(result.Value, "Bot");
        }
        else
        {
            chatView.AddMessageToChat("Error: " + result.ErrorMessage, "Bot");
        }
        chatView.EnableInput();
        return UniTask.CompletedTask;
    }
}