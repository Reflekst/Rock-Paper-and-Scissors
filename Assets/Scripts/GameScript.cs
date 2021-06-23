using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    public static GameScript Instance { get; set; }

    public Text scoreText, roundText, resultText, summaryText;

    public Image  myChoiceImage, enemyChoiceImage;

    public Sprite rockSprite, paperSprite, scissorsSprite;

    public GameObject controllPanel, resultPanel, summaryPanel;

    private int score = 0, round = 1, maxRoundValue = 4;

    private enum playerChoice { Rock, Paper, Scissors };
    private playerChoice myChoice, enemyChoice;

    private bool meReady, enemyReady = false;
    
    private ClientScript client;


    void Start()
    {
        Instance = this;
        client = FindObjectOfType<ClientScript>();
        resultPanel.SetActive(false);
        summaryPanel.SetActive(false);
    }

    void Update()
    {
        if (meReady&&enemyReady)
        {
            EndTurn();
        }
        scoreText.text = "Moje punkty: " + score.ToString();
        roundText.text = "Runda: " + round.ToString();
    }

    public void EndTurn()
    {
        if (myChoice != enemyChoice)
        {
            switch (myChoice)
            {
                case playerChoice.Rock:
                    if (enemyChoice == playerChoice.Scissors)
                    {
                        enemyChoiceImage.sprite = scissorsSprite;
                        score++;
                        resultText.text = "Wygrywasz!";
                    }
                    else
                    {
                        enemyChoiceImage.sprite = paperSprite;
                        resultText.text = "Przegrywasz!";
                    }
                    break;
                case playerChoice.Paper:
                    if (enemyChoice == playerChoice.Rock)
                    {
                        enemyChoiceImage.sprite = rockSprite;
                        score++;
                        resultText.text = "Wygrywasz!";
                    }
                    else
                    {
                        enemyChoiceImage.sprite = scissorsSprite;
                        resultText.text = "Przegrywasz!";
                    }
                    break;
                case playerChoice.Scissors:
                    if (enemyChoice == playerChoice.Paper)
                    {
                        enemyChoiceImage.sprite = paperSprite;
                        score++;
                        resultText.text = "Wygrywasz!";
                    }
                    else
                    {
                        enemyChoiceImage.sprite = rockSprite;
                        resultText.text = "Przegrywasz!";
                    }
                    break;
            }
        }
        else
        {
            enemyChoiceImage.sprite = myChoiceImage.sprite;
            resultText.text = "Remis!";
            maxRoundValue++;
        }
        if (++round >= maxRoundValue)
        {
            EndGame();
        }
        meReady = false;
        enemyReady = false;
        StartCoroutine(ResultTIme());
    }

    public void SelectRock()
    {
        myChoice = playerChoice.Rock;
        myChoiceImage.sprite = rockSprite;
        SaveChoice();
    }
    public void SelectPaper()
    {
        myChoice = playerChoice.Paper;
        myChoiceImage.sprite = paperSprite;
        SaveChoice();
    }
    public void SelectScissors()
    {
        myChoice = playerChoice.Scissors;
        myChoiceImage.sprite = scissorsSprite;
        SaveChoice();
    }
    public void StringToEnum(string s)
    {
        enemyChoice = (playerChoice)Enum.Parse(typeof(playerChoice),s);
        enemyReady = true;
    }
    private void SaveChoice()
    {
        client.Send(myChoice.ToString() + "|");
        controllPanel.SetActive(false);
        meReady = true;
    }

    IEnumerator ResultTIme()
    {
        resultPanel.SetActive(true);
        yield return new WaitForSeconds(3);
        resultPanel.SetActive(false);
        controllPanel.SetActive(true);
    }

    private void EndGame()
    {
        if (score >= 2)
            summaryText.text = "Wygrywasz!";
        else
            summaryText.text = "Przegrywasz!";
        summaryPanel.SetActive(true);
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    public void RematchButton()
    {
        score = 0;
        round = 1;
        maxRoundValue = 4;
        summaryPanel.SetActive(false);
    }
}
