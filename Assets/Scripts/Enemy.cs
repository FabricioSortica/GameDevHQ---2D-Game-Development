using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3.5f;

    private Player _player;

    private Animator _animator;

    [SerializeField]
    private AudioClip _explosionSound;

    private AudioSource _audiosource;

   


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _animator = GetComponent<Animator>();
        _audiosource = GetComponent<AudioSource>();

        if (_player == null)
        {
            Debug.LogError("The Player is NULL!");
        }        

        if (_animator == null)
        {
            Debug.LogError("The animator is NULL!");
        }

        if (_audiosource == null)
        {
            Debug.LogError("the audiosource on the enemy is NULL!");
        }
        else
        {
            _audiosource.clip = _explosionSound;
        }

       

        

    }

    // Update is called once per frame
    void Update()
    {        
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -7.0f)
        {
            float randomX = Random.Range(-9.5f, 9.5f);
            transform.position = new Vector3(randomX, 8.0f, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();

            }

            EnemyDeath();
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.AddToScore(10);
            }

            EnemyDeath();
        }

                      
    }

    void EnemyDeath()
    {
        _speed = 0f;
        _audiosource.Play();
        _animator.SetTrigger("OnEnemyDeath");        
        Destroy(this.gameObject, 2.0f);
    }


}
