using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _speed = 20f;
    [SerializeField]
    private GameObject _explosionPrefab;

    private SpawnManager _spawnManager;

    private UIManager _uIManager;

    

   

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("Tha spawn manager is NULL!");
        }

        if (_uIManager == null)
        {
            Debug.LogError("the UI Manager is NULL!");
        }

       
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.back * _speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser" || other.tag == "Heat_Seeker_Laser")
        {
            _uIManager.StartGame();
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spawnManager.StartSpawning();            
            Destroy(this.gameObject, 0.5f);
        }
    }

}
