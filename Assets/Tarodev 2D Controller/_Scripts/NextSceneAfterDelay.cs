using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NextSceneAfterDelay : MonoBehaviour
{
    [SerializeField] private float delay = 3f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Optional: only trigger for player
        if (other.CompareTag("Player"))
        {
            StartCoroutine(LoadNextScene());
        }
    }

    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(delay);

        // Loads the next scene in Build Settings
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}