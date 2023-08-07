using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// oyuncunun scripti
public class PlayerMovement : MonoBehaviour
{
    public float Speed = 1f;

    int coins = 0;
    AudioSource audioCoin;

    // Start is called before the first frame update
    void Start()
    {
        audioCoin = GetComponent<AudioSource>();
        coins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += transform.forward * Speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localPosition += -transform.right * Speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.localPosition += transform.right * Speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Coin")
        {
            Destroy(collision.collider.gameObject);
            coins += 1;
            Debug.Log("Player's coins are now " + coins);

            audioCoin.Play();
        }
    }
}
