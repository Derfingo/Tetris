using TMPro;
using UnityEngine;

[RequireComponent(typeof(FPSCounter))]
public class FPSDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _averageLabel;
    [SerializeField] private TextMeshProUGUI _highestLabel;
    [SerializeField] private TextMeshProUGUI _lowestLabel;
    [SerializeField] private TextMeshProUGUI _delayCPULabel;

    private FPSCounter _fpsCounter;
    private string[] _frameRate;
    private const int MAX_FRAME_RATE = 240;

    private void Awake()
    {
        _fpsCounter = GetComponent<FPSCounter>();
        _frameRate = new string[241];

        for (int i = 0; i <= MAX_FRAME_RATE; i++)
        {
            _frameRate[i] = i.ToString();
        }
    }

    private void Update()
    {
        Display(_averageLabel, _fpsCounter.AverageFPS, "A");
        Display(_highestLabel, _fpsCounter.HighestPFS, "H");
        Display(_lowestLabel, _fpsCounter.LowersFPS, "L");
        DisplayDelay(_delayCPULabel);
    }

    private void Display(TextMeshProUGUI valueText, int fps, string label)
    {
        valueText.text = $"{label}: {_frameRate[Mathf.Clamp(fps, 0, MAX_FRAME_RATE)]}";
    }

    private void DisplayDelay(TextMeshProUGUI label)
    {
        var msec = _fpsCounter.DeltaTime * 1000.0f;
        var text = $"{msec:0.0} ms";
        label.text = text;
    }
}
