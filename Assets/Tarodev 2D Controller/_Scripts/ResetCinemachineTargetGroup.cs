using UnityEngine;
using Unity.Cinemachine;

public class ResetCinemachineTargetGroup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CinemachineTargetGroup targetGroup;
    [SerializeField] private string playerTag = "Player";

    [Header("Target Settings")]
    [SerializeField] private float targetWeight = 1f;
    [SerializeField] private float targetRadius = 2f;

    private Transform _playerController;

    /// <summary>
    /// Call this from your respawn manager after the new player instance exists.
    /// Pass in the fresh Transform directly, or leave null to find by tag.
    /// </summary>
    public void OnPlayerRespawned(Transform newPlayer = null)
    {
        // Use the passed reference, or find the new instance by tag
        if (newPlayer != null)
        {
            _playerController = newPlayer;
        }
        else
        {
            GameObject found = GameObject.FindWithTag(playerTag);
            if (found == null)
            {
                Debug.LogWarning("ResetCinemachineTargetGroup: Could not find player by tag.");
                return;
            }
            _playerController = found.transform;
        }

        if (targetGroup == null)
        {
            Debug.LogWarning("ResetCinemachineTargetGroup: No TargetGroup assigned.");
            return;
        }

        RemoveStaleEntries();
        AddPlayer();

        Debug.Log($"Target Group reset — tracking '{_playerController.name}'.");
    }

    /// <summary>
    /// Removes any null (destroyed) entries and any existing player entry.
    /// </summary>
    private void RemoveStaleEntries()
    {
        for (int i = targetGroup.Targets.Count - 1; i >= 0; i--)
        {
            if (targetGroup.Targets[i].Object == null ||
                targetGroup.Targets[i].Object == _playerController)
            {
                targetGroup.Targets.RemoveAt(i);
            }
        }
    }

    private void AddPlayer()
    {
        targetGroup.Targets.Add(new CinemachineTargetGroup.Target
        {
            Object = _playerController,
            Weight = targetWeight,
            Radius = targetRadius
        });
    }
}