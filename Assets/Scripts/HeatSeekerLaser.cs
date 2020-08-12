using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatSeekerLaser : MonoBehaviour
{

    private Transform _target;
    [SerializeField]
    private float _speed = 8f;
    [SerializeField]
    private float _rotateSpeed = 500f;
    private Rigidbody2D _rb;
    private GameObject _enemySearch;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _enemySearch = FindClosestEnemy();

        if (_enemySearch != null)
        {
            _target = _enemySearch.transform;
        }


    }

    void FixedUpdate()
    {
        Movement();
        SelfDestruct();
    }

    void Movement()
    {
        if (_target)
        {
            Vector2 direction = (Vector2)_target.position - _rb.position;

            direction.Normalize();

            Vector3.Cross(direction, transform.up);

            float rotateAmount = Vector3.Cross(direction, transform.up).z;

            _rb.angularVelocity = -rotateAmount * _rotateSpeed;

            _rb.velocity = transform.up * _speed;


        }
        else
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);

            if (transform.position.y > 7.5f)
            {
                if (transform.parent != null)
                {
                    Destroy(transform.parent.gameObject);
                }

                Destroy(this.gameObject);
            }
            
        }
       
    }

    void SelfDestruct()
    {
        if (transform.position.y > 7.5f || transform.position.y < -7.5f)
        {
            Destroy(this.gameObject);
        }
        else if (transform.position.x > 10.8f || transform.position.x < -10.8f)
        {
            Destroy(this.gameObject);
        }
    }

  

    GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject closest = null;
        float distance = Mathf.Infinity;

        Vector3 position = transform.position;

        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }


        return closest;
    }
}
