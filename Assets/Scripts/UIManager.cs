using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Text _tutorialText;
    [SerializeField]
    private Text _ammoCounterText;
    [SerializeField]
    private Text _ammoOutText;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Image _ShieldsImg;
    [SerializeField]
    private Sprite[] _liveSprites;    
    [SerializeField]
    private Sprite[] _shieldSprites;
    

    private GameManager _gameManager;
    

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 00;
        _ammoCounterText.text = "Ammo: " + 15;
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _tutorialText.gameObject.SetActive(true);

        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("The Game Manager is NULL!");
        }

    }

        
    public void UpdateScoreText(int scoreUpdate)
    {
        _scoreText.text = "Score: " + scoreUpdate;
    }

    public void UpdateAmmoScoreText(int ammoUpdate)
    {
        _ammoCounterText.text = "Ammo: " + ammoUpdate;

        if (ammoUpdate == 0)
        {
            StartCoroutine(AmmoOutMessageRoutine());
        }
    }

    public void UpdateLivesImg(int currentLives)
    {
        _livesImg.sprite = _liveSprites[currentLives];
    }

    public void UpdateShieldsImg(int currentShield)
    {
        _ShieldsImg.sprite = _shieldSprites[currentShield];               
    }

    public void GameOverSequence()
    {        
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
        _gameManager.GameOver();
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while(true)
        {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.25f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.25f);
        }        
       
    }

    public IEnumerator AmmoOutMessageRoutine()
    {
        _ammoOutText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        _ammoOutText.gameObject.SetActive(false);
       
    }

    public void StartGame()
    {
        _tutorialText.gameObject.SetActive(false);
    }


}
