using UnityEngine;

public class StaticTrap : MonoBehaviour
{
    [SerializeField] private Transform PlayerSpawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>()) 
        {
            collision.gameObject.transform.position = PlayerSpawnPoint.position;
            FindFirstObjectByType<GameOverManager>().reduceLives();
        }
    }
}
