using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No se encontró un componente AudioSource en este objeto.");
            return;
        }

        
        audioSource.loop = true;

      
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
