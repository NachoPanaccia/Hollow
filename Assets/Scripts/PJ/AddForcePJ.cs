using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForcePJ : MonoBehaviour
{
    public float forceMagnitude = 2f; 

    void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D otherRb = collision.collider.GetComponent<Rigidbody2D>();
        if (otherRb != null)
        {
            Vector2 forceDirection = collision.contacts[0].point - (Vector2)transform.position;
            forceDirection.Normalize();

            otherRb.AddForce(forceDirection * forceMagnitude);
        }
    }
}
