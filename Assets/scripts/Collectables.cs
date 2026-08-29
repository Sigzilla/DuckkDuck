using UnityEngine;

public class Collectables : MonoBehaviour
{
    [SerializeField] private int AmountToIncrease;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>()) 
        {
            PointsSystem.Instance.IncreasePoints(AmountToIncrease);
            Destroy(gameObject);
        }
    }

}
