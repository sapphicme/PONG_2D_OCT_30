using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ScoreManager : MonoBehaviour {

    public Text playerScoreText, computerScoreText;
    [HideInInspector] public int playerScore, computerScore;
    public int scoreToBeat = 10;


 public void IncreasePlayerScore()
    {
        playerScore++;
        playerScoreText.text = playerScore.ToString();

        if (playerScore == scoreToBeat)
        {// load win
            SceneManager.LoadScene("Win");
        }
    }
   
    public void IncreaseComputerScore()
    {
        computerScore++;
        computerScoreText.text = computerScore.ToString();

        if (computerScore == scoreToBeat)
        { //load lose
            SceneManager.LoadScene("Lose");
        }
    }

}
