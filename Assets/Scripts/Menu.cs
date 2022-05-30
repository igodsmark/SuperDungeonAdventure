using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class Menu : MonoBehaviour
{
    [SerializeField] List<UnityEngine.UI.Button> buttons;
    [SerializeField] float menuButtonUpdateTime = 0.5f;
    Button selectedButton;
    [SerializeField]float menuButtonChangeTime;

    int buttonIndex = 0;

    private void Awake()
    {
        menuButtonChangeTime = Time.time;
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {

        if (Input.GetAxis("Vertical") != 0)
        {
            if (Time.time > menuButtonChangeTime + 1f)
            {
                menuButtonChangeTime = Time.time;
                ChangeMenuItem();

            }
        }
    }

    private void ChangeMenuItem()
    {
        buttonIndex++;
        if (buttonIndex >= buttons.Count)
        {
            buttonIndex = 0;
        }
        Button button = buttons[buttonIndex];
        selectedButton = button;
        button.Select();

    }
}
