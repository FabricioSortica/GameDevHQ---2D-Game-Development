using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    [SerializeField]
    private float _speed = 8.0f;
    
    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (gameObject.tag == "Laser")
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);

            if (transform.position.y > 8.0f)
            {
                if (transform.parent != null)
                {
                    Destroy(transform.parent.gameObject);
                }

                Destroy(this.gameObject);
            }
        }
        else if (gameObject.tag == "Enemy_Laser")
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);

            if (transform.position.y < -8.0f)
            {
                if (transform.parent != null)
                {
                    Destroy(transform.parent.gameObject);
                }

                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && gameObject.tag == "Enemy_Laser")
        {
            Player player = other.GetComponent<Player>();

            if (player!= null)
            {
                player.Damage();
            }

            Destroy(this.gameObject);
        }
        
    }

}
