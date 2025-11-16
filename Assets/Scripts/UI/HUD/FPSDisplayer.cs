using UnityEngine;
using UnityEngine.UI;

public class FPSDisplayer : MonoBehaviour {
    private const string fpsDisplay = "{0} FPS";
    private const float fpsMeasurePeriod = 0.5f;

    private int m_FpsAccumulator = 0;
    private float m_FpsNextPeriod = 0;
    private int m_CurrentFps;

    private Text fpsText;

    void Awake() {
        fpsText = GetComponent<Text>();
    }

    void Start() {
        m_FpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;
    }

    void Update() {
        m_FpsAccumulator++;
        if (Time.realtimeSinceStartup > m_FpsNextPeriod) {
            m_CurrentFps = (int)(m_FpsAccumulator / fpsMeasurePeriod);
            m_FpsAccumulator = 0;
            m_FpsNextPeriod += fpsMeasurePeriod;
            fpsText.text = string.Format(fpsDisplay, m_CurrentFps);
            fpsText.text = fpsText.text + " / Ping: " + PhotonNetwork.GetPing();
        }

    }
}
