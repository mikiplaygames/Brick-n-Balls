using UnityEngine;

public class AddLoadScene : MonoBehaviour
{
    [SerializeField] private string sceneName;
    private void Awake() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }
}
