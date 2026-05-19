using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private ParticleSystem deathParticles;

    private bool isDead = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        if (collision.gameObject.CompareTag("Hazard"))
        {
            isDead = true;

            PlayDeathEffects();

            GameManager.Instance.RespawnPlayer(gameObject);
        }
    }

    private void PlayDeathEffects()
    {
        // SOUND
        if (deathSound != null)
        {
            GameObject tempAudio = new GameObject("DeathSound");
            AudioSource source = tempAudio.AddComponent<AudioSource>();

            source.clip = deathSound;
            source.volume = 1f;
            source.Play();

            Destroy(tempAudio, deathSound.length);
        }

        // PARTICLES (JUST PLAY)
        if (deathParticles != null)
        {
            deathParticles.Play();
        }
    }
}