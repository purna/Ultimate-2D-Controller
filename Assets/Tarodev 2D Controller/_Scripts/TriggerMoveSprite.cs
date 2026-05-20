using UnityEngine;

public class TriggerMoveSprite : MonoBehaviour
{
    public Transform spriteToMove;

    public enum MoveDirection { Up, Down, Left, Right }
    public MoveDirection direction = MoveDirection.Up;

    public float moveDistance = 3f;
    public float moveSpeed = 2f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool shouldMove = false;

    void Start()
    {
        if (spriteToMove != null)
        {
            startPosition = spriteToMove.position;

            Vector3 moveDir = direction switch
            {
                MoveDirection.Up    => Vector3.up,
                MoveDirection.Down  => Vector3.down,
                MoveDirection.Left  => Vector3.left,
                MoveDirection.Right => Vector3.right,
                _                   => Vector3.zero
            };

            targetPosition = startPosition + moveDir * moveDistance;
        }
    }

    void Update()
    {
        if (shouldMove && spriteToMove != null)
        {
            spriteToMove.position = Vector3.MoveTowards(
                spriteToMove.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            shouldMove = true;
        }
    }

    public void ResetObject()
    {
        shouldMove = false;
        if (spriteToMove != null)
            spriteToMove.position = startPosition;
    }
}