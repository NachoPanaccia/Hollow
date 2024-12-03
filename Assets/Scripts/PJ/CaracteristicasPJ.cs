using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaracteristicasPJ : MonoBehaviour, IDamageable
{
    public HealthManager healthManager;
    private Animator anim;
    public AudioSource dañoSound;
    public AudioSource audioSource;

    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        healthManager.InitializeHealth(5, anim, audioSource);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CorazonVida"))
        {
            healthManager.AddLife();
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        healthManager.TakeDamage(damage);
    }
}