public static class InputWrapper {
    public static Control control { get; private set; }
    [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize(){
        control = new();
        control.Enable();
    }
}