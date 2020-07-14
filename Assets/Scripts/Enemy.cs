﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3.5f;

    private Player _player;


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        if (_player == null)
        {
            Debug.LogError("The Player is NULL!");
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
            
            Destroy(this.gameObject);
        }

        if (other.tag == "Laser")
        {
            
            if (_player != null)
            {
                _player.AddToScore(10);
            }            
          
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

        
    }


}
