using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.down * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit");
        if (collision.collider.tag == "Player")
        {
            Player.Instance.TakeDamage();
        }
        Destroy(gameObject);
    }
}
