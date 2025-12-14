using UnityEngine;
/// <summary>
/// Helper scripts enabling the GameObject when the game starts
/// </summary>
public class EnableOnGameStart : MonoBehaviour
{
    void Awake()
    {
        GameManager.OnGameStart.AddListener(Show);
    }
    void Show()
    {
        gameObject.SetActive(true);
    }
    void OnDestroy()
    {
        GameManager.OnGameStart.RemoveListener(Show);
    }
}
