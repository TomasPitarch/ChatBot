using UnityEngine;

[CreateAssetMenu(fileName = "LlmConfig", menuName = "ScriptableObjects/LlmConfig", order = 1)]
public class LlmConfig : ScriptableObject
{
    [SerializeField] 
    private LLMProvider llmProvider;
    
    [SerializeField]
    [TextArea(3,10)]
    private string systemPromptContext = "You are a helpful assistant.";
    
    
    
    public string GetSystemPromptContext() => systemPromptContext;
    
}

public enum LLMProvider
{
    OpenAI,
    AzureOpenAI,
    LocalModel,
    Gemini
}
