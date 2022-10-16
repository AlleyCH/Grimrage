using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public AudioSource keys;
    // Start is called before the first frame update
    void Start()
    {
        keys = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            keys.Play();
            Destroy(gameObject, 0.4f);
           
        }
    }
}
