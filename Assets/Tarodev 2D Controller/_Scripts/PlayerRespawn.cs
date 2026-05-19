using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private float soundVolume = 1f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            PlayDeathSound();

            GameManager.Instance.RespawnPlayer();
            Destroy(gameObject);
        }
    }

    private void PlayDeathSound()
    {
        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position, soundVolume);
        }
    }
}