using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField][Tooltip("gamemanager slot")]
    private GameManager manager;

    [SerializeField] 
    private TMP_Text timeText;


    private void Update()
    {
        timeText.text = $"Gametime: {manager.GameTime.ToString("N2")}";
        //timeText.text = $"Gametime: {manager.GameTime}";
        //timeText.text = "Gametime: " + manager.GameTime.ToString();
    }
}
