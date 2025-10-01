using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ChatView : MonoBehaviour
{
    [Header("Button to send the message")]
    [SerializeField]
    private Button sendButton;
    [Header("Input field for user to type messages")]
    [SerializeField]
    private TMP_InputField inputField;
    
    [Header("Text area to display chat messages")]
    [SerializeField]
    private TextMeshProUGUI  textArea;
    
    [Header("Color for user messages")]
    [SerializeField]
    private Color userColor=Color.black;
    [Header("Color for bot messages")]
    [SerializeField]
    private Color botColor=Color.blue;
    
    
    #region Events

    public event Action OnSendButtonClicked;
    
    #endregion
   
    
   private InputSystem_Actions _inputActions;

   private void Awake()
    {
        _inputActions=new (); 
    }

   private void Start()
    {
        Subscriptions();
        CheckInputField();
    }
   private void Subscriptions()
    {
        sendButton.onClick.AddListener(SendButtonHandler);
        
        inputField.onValueChanged.AddListener(delegate { CheckInputField(); });
        
        _inputActions.Enable();
        _inputActions.UI.Submit.performed += SendButtonKeyboardHandler;
    } 
   private void CheckInputField()
    {
        sendButton.interactable = !string.IsNullOrWhiteSpace(inputField.text);
    }
   private void SendButtonKeyboardHandler(InputAction.CallbackContext callbackContext)
    {
        OnSendButtonClicked?.Invoke();
    }
   private void SendButtonHandler()
   {
       OnSendButtonClicked?.Invoke();
   }
   public string GetInputFieldText()
   {
       return inputField.text;
   }
   public void ClearInputField()
   {
       inputField.text = string.Empty;
       inputField.ActivateInputField();
   }
   public void AddMessageToChat(string message,string sender="User")
   {
       if (string.IsNullOrWhiteSpace(message)) return;

       string colorHex = ColorUtility.ToHtmlStringRGB(sender == "User" ? userColor : botColor);
      
       string formattedMessage = $"<color=#{colorHex}>{sender}: {message}</color>";

       
       if (string.IsNullOrEmpty(textArea.text))
       {
           textArea.text = formattedMessage;
       }
       else
       {
           textArea.text += "\n" + formattedMessage;
       }
       
   }

   public void EnableInput()
   {
         inputField.interactable = true;
         _inputActions.UI.Submit.performed += SendButtonKeyboardHandler;
         CheckInputField();
   }
   public void DisableInput()
   {
         inputField.interactable = false;
         sendButton.interactable = false;
         _inputActions.UI.Submit.performed -= SendButtonKeyboardHandler;
   }
   
}