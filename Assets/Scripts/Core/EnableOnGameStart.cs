using UnityEngine;

public class EnableOnGameStart : MonoBehaviour
{
    void Awake()
    {
        GameManager.OnGameStart.AddListener(() => gameObject.SetActive(true));
    }
}
