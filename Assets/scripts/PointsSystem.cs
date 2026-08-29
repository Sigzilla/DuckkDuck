using UnityEngine;
using TMPro;

public class PointsSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text PointsText;

    public static PointsSystem Instance;

    private int Points = 0;

    private void Awake()
    {
        //if pointsSystem doesnt exist
        if (Instance == null)
        {
            //make this the pointsSystem
            Instance = this;
        }
        else //if pointsSystem does exist
        {
            //destroy the new pointsSystem
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PointsText.text = "points: " + Points;
    }

    public void IncreasePoints(int amount) 
    {
        Points += amount;
        PointsText.text = "points: " + Points;
    }
}
