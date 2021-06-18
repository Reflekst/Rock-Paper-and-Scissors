using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    private enum playerChoice { Rock, Paper, Scissors };

    private bool playersReady = false;
    private bool iWon;

    playerChoice myChoice;
    playerChoice enemyChoice;

    void Start()
    {

    }

    void Update()
    {
        if (playersReady)
        {
            EndTurn();
        }
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
                        iWon = true;
                    }
                    else if (enemyChoice == playerChoice.Paper)
                    {
                        iWon = false;
                    }
                    break;
                case playerChoice.Paper:
                    if (enemyChoice == playerChoice.Rock)
                    {
                        iWon = true;
                    }
                    else if (enemyChoice == playerChoice.Scissors)
                    {
                        iWon = false;
                    }
                    break;
                case playerChoice.Scissors:
                    if (enemyChoice == playerChoice.Paper)
                    {
                        iWon = true;
                    }
                    else if (enemyChoice == playerChoice.Rock)
                    {
                        iWon = false;
                    }
                    break;
            }
        }
        else
        {
            //remis
        }
    }

    public void SelectRock()
    {
        myChoice = playerChoice.Rock;
    }
    public void SelectPaper()
    {
        myChoice = playerChoice.Paper;
    }
    public void SelectScissors()
    {
        myChoice = playerChoice.Scissors;
    }


}
