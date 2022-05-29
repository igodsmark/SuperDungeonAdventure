using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] GameObject dialogueHolder;
    [SerializeField] TMPro.TMP_Text text;
    [SerializeField] EventManager eventManager;

    float nextDialogueTime = 0.5f;
    float lastKeyPress = 0f;

    public event EventHandler FinishedText;

    bool displayText = true;
    List<string> messages = new List<string>();
    int messageIndex = 0;

    private void Awake()
    {
        //dialogueHolder.SetActive(true);
        //text.text = "";
    }

    private void Update()
    {
        if (displayText)
        {
            if (Input.anyKey)
            {
                if(Time.time > lastKeyPress + nextDialogueTime)
                {
                    DisplayNextLine();
                }

                
            }
        }
    }

    protected virtual void OnFinishedText(EventArgs e)
    {
        EventHandler handler = FinishedText;
        handler?.Invoke(this, e);
    }

    public void DisplayDialogue(List<string> text)
    {
        messages = text;
        DisplayNextLine();
    }

    void DisplayNextLine()
    {
        if(messageIndex < messages.Count)
        {
            lastKeyPress = Time.time;
            dialogueHolder.SetActive(true);
            text.text = messages[messageIndex];
            messageIndex++;
        }
        else
        {
            dialogueHolder.SetActive(false);
            messageIndex = 0;
            messages.Clear();
            eventManager.FinishText();
        }
    }
}
