using MiniJamGame16.Player;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    MiniJamGame16.Player.PlayerController _pc;

    int _pScore;

    [SerializeField]
    TMP_Text _scoreText;
    [SerializeField]
    TMP_Text _flavourText;

    string score499 = "You're attrocious at this.\nYou're FIRED!\nSECURITY!!!!!!!!!!";
    string score500 = "I've never seen such poor work ethic.\nYou're FIRED!";
    string score1000 = "Not too bad.\nYou can keep your job....for now.";
    string score1500 = "Nice work out there today.\nKeep it up!";
    string score2000 = "WOW! I'm actually impressed!\nSolid work out there today!";
    string score3000 = "Exceptional!\nKeep working like that maybe you'll get a promotion....maybe!";
    string score5000 = "Magnificent!\nYou've been promoted!\nCongratulations!";
    string score5001 = "With work like this, you could be CEO in no time!\nYou're Fired!!!!\nI won't allow someone to take over my cushy job!";

    // Start is called before the first frame update
    void Start()
    {
        _pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>(); //I imagine that that we are overlaying the GameOver scene ontop of the current Main scene.
        _pScore = _pc.GetPlayerScore();

        _scoreText.text = "Score: " + _pScore.ToString();


        if(_pScore >= 500 && _pScore <= 999)
        {
            _flavourText.text = score500;

        } else if(_pScore >= 1000 && _pScore <= 1499)
        {
            _flavourText.text = score1000;

        } else if( _pScore >= 1500 && _pScore <= 1999)
        {
            _flavourText.text = score1500;

        } else if( _pScore >= 2000 && _pScore <= 2999)
        {
            _flavourText.text = score2000; 

        } else if( _pScore >= 3000 && _pScore <= 4999)
        {
            _flavourText.text = score3000;  

        } else if( _pScore == 5000)
        {
            _flavourText.text = score5000;

        }
        else if ( _pScore >= 5001)
        {
            _flavourText.text = score5001;

        }
        else
        {
            _flavourText.text = score499;

        }


    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }




}
