﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    [SerializeField]
    private float _speedBoost = 1.5f;
    [SerializeField]
    private float _thrusterBoost = 1.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotLaserPrefab;
    [SerializeField]
    private GameObject _shieldsUpViewer;

    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _shields = 0;

    private SpawnManager _spawnManager;
    private UIManager _uIManager;

    private bool _isTripleShotActive = false;
    private bool _isShieldsUp = false;

    private int _score = 0;

    [SerializeField]
    private GameObject _playerThrusterLeft;
    [SerializeField]
    private GameObject _playerThrusterRight;
    [SerializeField]
    private GameObject _playerThrusterBoost;

    [SerializeField]
    private GameObject _playerDamageLeft;
    [SerializeField]
    private GameObject _playerDamageRight;
    [SerializeField]
    private GameObject _playerDamageMajor;

    [SerializeField]
    private GameObject _explosionPrefab;

    [SerializeField]
    private AudioClip _laserAudioClip;

    private AudioSource _audioSource;



    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, -2.75f, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL!");
        }        

        if (_uIManager == null)
        {
            Debug.LogError("UI Manager is NULL!");
        }

        if (_audioSource == null)
        {
            Debug.LogError("The audiosource on the player is NULL!");
        }
        else
        {
            _audioSource.clip = _laserAudioClip;
        }



    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Thruster();        

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

       


    }

    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 3.0f));

        if (transform.position.x >= 11f)
        {
            transform.position = new Vector3(-11f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11f)
        {
            transform.position = new Vector3(11f, transform.position.y, 0);
        }
    }

    void Thruster()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _playerThrusterBoost.SetActive(true);
            _speed *= _thrusterBoost;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _playerThrusterBoost.SetActive(false);
            _speed /= _thrusterBoost;
        }

    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;

        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotLaserPrefab, transform.position, Quaternion.identity);
        }

        else
        {
            Vector3 laserOffset = new Vector3(0, 0.8f, 0);
            Instantiate(_laserPrefab, transform.position + laserOffset, Quaternion.identity);
        }

        _audioSource.Play();
        
    }

     public void Damage()
    {
        if (_isShieldsUp == true)
        {
            ShieldsDown();
            AddToScore(10);

            return;
        }
        else
        {
            _lives--;
            AddToScore(-10);

           switch (_lives)
           {
                case 0:
                    _playerDamageMajor.SetActive(true);
                    break;
                case 1:
                    _playerThrusterRight.SetActive(false);
                    _playerDamageRight.SetActive(true);
                    break;
                case 2:
                    _playerThrusterLeft.SetActive(false);
                    _playerDamageLeft.SetActive(true);
                    break;
                default:
                    Debug.Log("Default Value");
                    break;

           }

            if (_lives < 0)
            {
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                _spawnManager.OnPlayerDeath();
                _uIManager.UpdateLivesImg(0);
                _uIManager.GameOverSequence();               
                Destroy(this.gameObject);                
            }
            else
            {
                _uIManager.UpdateLivesImg(_lives);
            }

        }

    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowedownRoutine());
    }

    IEnumerator TripleShotPowedownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }


    public void SpeedBoostActive()
    {
        _speed *= _speedBoost;
        StartCoroutine(SpeedBoostOffCourotine());

    }

    IEnumerator SpeedBoostOffCourotine()
    {
        yield return new WaitForSeconds(5.0f);
        _speed /= _speedBoost;
    }    

    public void ShieldsUp()
    {
        if (_isShieldsUp == false)
        {
            _isShieldsUp = true;
            _shieldsUpViewer.SetActive(true);
        }
                
        if (_shields < 3)
        {
            _shields++;
            _uIManager.UpdateShieldsImg(_shields);
        }

    }

    void ShieldsDown()
    {
        _shields--;
        _uIManager.UpdateShieldsImg(_shields);
        
        if (_shields == 0)
        {
            _isShieldsUp = false;
            _shieldsUpViewer.SetActive(false);
        }

    }
    public void AddToScore(int scorePoints)
    {
        _score += scorePoints;
        _uIManager.UpdateScoreText(_score);

    }

}