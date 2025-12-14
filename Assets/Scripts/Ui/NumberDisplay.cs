using UnityEngine;
public abstract class NumberDisplay : MonoBehaviour {
    protected abstract string BaseText { get; }
    protected abstract int GetNumber();
    [SerializeField] TMPro.TMP_Text numberText;
    protected void UpdateDisplay() {
        numberText.SetText($"{BaseText}: {GetNumber()}");
    }
}