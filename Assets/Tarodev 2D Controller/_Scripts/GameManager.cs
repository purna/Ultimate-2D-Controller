using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private float respawnDelay = 2f;
    [SerializeField] private ResetCinemachineTargetGroup cameraTargetReset;
    [SerializeField] private List<TriggerMoveSprite> itemsToReset;

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
        if (movement != null) movement.enabled = false;

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
        }

        yield return new WaitForSeconds(respawnDelay);

        GameObject newPlayer = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
        cameraTargetReset.OnPlayerRespawned(newPlayer.transform);
        ResetItems();

        Destroy(player);

        isRespawning = false;
    }

    private void ResetItems()
    {
        foreach (TriggerMoveSprite item in itemsToReset)
        {
            if (item != null)
                item.ResetObject();
        }
    }
}