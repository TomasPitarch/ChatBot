using System.Collections.Generic;
using CandyCoded.env;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class LlmGeminiService : MonoBehaviour,ILlmService
{
    [SerializeField]
    private LlmConfig llmConfig;
    
    private const string APIKeyEnvVar = "GEMINI_API_KEY";
    private const string API_URL = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent";

    
    private string _apiKey;

    private GeminiRequest _requestData;

    private void Start()
    {
      _apiKey = env.variables[APIKeyEnvVar];
      _requestData=new GeminiRequest(llmConfig.GetSystemPromptContext());
    }

    public async UniTask<Result<string>> SendMessageAsync(string message)
    {
        _requestData.AddContent(new Content("user",new List<Part>{new Part{ text = message}}));
        
        string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(_requestData);
        
        using (UnityWebRequest webRequest = new UnityWebRequest(API_URL, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonPayload);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            
           
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("X-goog-api-key", _apiKey);

          
            await webRequest.SendWebRequest();

           
            if (webRequest.result is UnityWebRequest.Result.Success)
            {
                string responseContent = ExtractResponseText(webRequest.downloadHandler.text);
                _requestData.AddContent(new Content("model",new List<Part>{new Part{ text = responseContent}}));

                return new Result<string>(responseContent);
            }
            else
            {
               
                return new Result<string>(webRequest.error,false);
            }
        }

    }
    
    private string ExtractResponseText(string jsonResponse)
    {
        GeminiResponse response = Newtonsoft.Json.JsonConvert.DeserializeObject<GeminiResponse>(jsonResponse);
        if (response != null && response.candidates != null && response.candidates.Count > 0)
        {
            return response.candidates[0].content.parts[0].text;
        }
        return "No response";
    }
}


