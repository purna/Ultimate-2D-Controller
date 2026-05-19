using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private float respawnDelay = 2f;

    private bool isRespawning = false;

    private void Awake()
    {
        Instance = this;
    }

    public void RespawnPlayer(GameObject player)
    {
        if (isRespawning) return;

        StartCoroutine(RespawnCoroutine(player));
    }

    private IEnumerator RespawnCoroutine(GameObject player)
    {
        isRespawning = true;

        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        if (movement != null)
        {
            movement.enabled = false;
        }

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
        }

        yield return new WaitForSeconds(respawnDelay);

        Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);

        Destroy(player);

        isRespawning = false;
    }
}