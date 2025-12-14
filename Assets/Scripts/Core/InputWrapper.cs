/// <summary>
/// Wrapper for the input system to allow easy access to controls.
/// Designed as a static class to provide global access.
/// </summary>
public static class InputWrapper {
    public static Control control { get; private set; }
    [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize(){
        control = new();
        control.Enable();
    }
}