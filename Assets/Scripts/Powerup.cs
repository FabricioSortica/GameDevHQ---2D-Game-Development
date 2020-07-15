using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField] // 0 = triple shot; 1 = speed up; 2 = shields up
    private int _powerupID;

    [SerializeField]
    private AudioClip _powerupSound;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        
        if(transform.position.y < -6.0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {

               switch (_powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldsUpActive();
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;

                }
                
            }

            AudioSource.PlayClipAtPoint(_powerupSound, transform.position);

            Destroy(this.gameObject);
        }
    }
}
