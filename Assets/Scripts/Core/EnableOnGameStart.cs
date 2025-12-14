using UnityEngine;
/// <summary>
/// Helper scripts enabling the GameObject when the game starts
/// </summary>
public class EnableOnGameStart : MonoBehaviour
{
    void Awake()
    {
        GameManager.OnGameStart.AddListener(() => gameObject.SetActive(true));
    }
}
