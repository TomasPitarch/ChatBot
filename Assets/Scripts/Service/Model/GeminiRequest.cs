//##Gemini Request Format##//

using System;
using System.Collections.Generic;

[Serializable]
public class GeminiRequest
{
    public List<Content> contents;

    public GeminiRequest(string context)
    {
        contents = new();
        AddContent(new Content("user",new List<Part>{new Part{ text = context}}));
    }
    public void AddContent(Content content)
    {
        contents.Add(content);
    }
}

[Serializable]
public class Part
{
    public string text;
}

[Serializable]
public class Content
{
    public string role;
    public List<Part> parts;
    
    public Content(string role, List<Part> parts)
    {
        this.role = role;
        this.parts = parts;
    }
   
}




//##Gemini Response Format##//

[Serializable]
public class GeminiResponse
{
    public List<Candidate> candidates;
}

[Serializable]
public class Candidate
{
    public Content content;
    public string finishReason;
}



