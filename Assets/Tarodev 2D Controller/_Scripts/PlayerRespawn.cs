using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            GameManager.Instance.RespawnPlayer();
            Destroy(gameObject);
        }
    }
}