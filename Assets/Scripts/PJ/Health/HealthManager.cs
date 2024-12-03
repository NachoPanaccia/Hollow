using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public AudioClip damageSound;
    public AudioSource audioSource;
    public AudioClip deadSound;
    private Animator anim;

    [SerializeField] private GameObject m_corazonPrefab;
    [SerializeField] private RectTransform m_corazonContainer;
    [SerializeField] private int maxLifesCorazones = 5;
    private int currentHealth;
    private List<GameObject> m_intancedCorazones = new List<GameObject>();

    private List<IHealthObserver> observers = new List<IHealthObserver>();

    public void InitializeHealth(int initialLifes, Animator animator, AudioSource audioSrc)
    {
        anim = animator;
        audioSource = audioSrc;
        currentHealth = initialLifes;
        SetCorazones(maxLifesCorazones);
        NotifyObservers();
    }

    private void Update()
    {
        if (currentHealth == 0)
        {
            anim.SetInteger("State", 2);
            Die();
        }
    }

    private void SetCorazones(int p_corazones)
    {
        for (int i = 0; i < p_corazones; i++)
        {
            var l_corazon = Instantiate(m_corazonPrefab, m_corazonContainer);
            m_intancedCorazones.Add(l_corazon);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        for (int i = m_intancedCorazones.Count - 1; i >= 0; i--)
        {
            var l_corazon = m_intancedCorazones[i];

            if (!l_corazon.activeSelf)
            {
                continue;
            }

            l_corazon.SetActive(false);
            break;
        }

        NotifyObservers();
    }

    public void AddLife()
    {
        if (currentHealth < maxLifesCorazones)
        {
            currentHealth += 1;

            var l_corazon = Instantiate(m_corazonPrefab, m_corazonContainer);
            m_intancedCorazones.Add(l_corazon);
        }
        NotifyObservers();
    }

    public void RegisterObserver(IHealthObserver observer)
    {
        observers.Add(observer);
    }

    public void DeregisterObserver(IHealthObserver observer)
    {
        observers.Remove(observer);
    }

    private void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.OnHealthChanged(currentHealth, maxLifesCorazones);
        }
    }

    private void Die()
    {
        if (anim.GetInteger("State") == 2)
        {
            GameManager.Instance.PlayerDied();
        }
    }
}