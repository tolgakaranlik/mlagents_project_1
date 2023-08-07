using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

// AI ajanlarının scripti
public class CharacterMovement : Agent
{
    public Transform[] SpawningPoints;
    public float Speed = 1f;
    int coins = 0;

    void Start()
    {
        coins = 0;
    }

    void Update()
    {
        transform.localPosition += transform.forward * Speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Coin")
        {
            Destroy(collision.collider.gameObject);
            coins += 1;
            Debug.Log("AI character's ("+ name +") coins are now " + coins);
        }
    }
}
