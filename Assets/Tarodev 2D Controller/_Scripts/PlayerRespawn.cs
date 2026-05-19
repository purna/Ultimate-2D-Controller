using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private GameObject deathParticles;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            PlayDeathEffects();

            GameManager.Instance.RespawnPlayer(gameObject);
        }
    }

    private void PlayDeathEffects()
    {
        // particles (safe)
        if (deathParticles != null)
        {
            Instantiate(deathParticles, transform.position, Quaternion.identity);
        }

        // sound (FIXED)
        if (deathSound != null)
        {
            GameObject tempAudio = new GameObject("DeathSound");
            AudioSource source = tempAudio.AddComponent<AudioSource>();

            source.clip = deathSound;
            source.volume = 1f;
            source.Play();

            Destroy(tempAudio, deathSound.length);
        }
    }
}