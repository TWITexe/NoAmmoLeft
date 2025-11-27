using TMPro;
using UnityEngine;

public class RainbowText : MonoBehaviour
{
    [Header("Rainbow Settings")]
    public float speed = 1f;
    public float saturation = 0.8f;
    public float brightness = 1f;
    public bool useUnscaledTime = false;

    [Header("Individual Character Colors")]
    public bool perCharacterRainbow = false;
    public float characterOffset = 0.1f;

    private TextMeshProUGUI textMeshPro;
    private float baseHue;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        float deltaTime = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
        baseHue += deltaTime * speed;
        if (baseHue > 1f) baseHue -= 1f;

        if (perCharacterRainbow && textMeshPro.textInfo != null)
        {
            UpdatePerCharacterColors();
        }
        else
        {
            UpdateSingleColor();
        }
    }

    void UpdateSingleColor()
    {
        Color rainbowColor = Color.HSVToRGB(baseHue, saturation, brightness);
        textMeshPro.color = rainbowColor;
    }

    void UpdatePerCharacterColors()
    {
        TMP_TextInfo textInfo = textMeshPro.textInfo;
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];
            if (!charInfo.isVisible) continue;

            int materialIndex = charInfo.materialReferenceIndex;
            int vertexIndex = charInfo.vertexIndex;

            Color32[] vertexColors = textInfo.meshInfo[materialIndex].colors32;
            float charHue = (baseHue + i * characterOffset) % 1f;
            Color32 charColor = Color.HSVToRGB(charHue, saturation, brightness);
            for (int j = 0; j < 4; j++)
            {
                vertexColors[vertexIndex + j] = charColor;
            }
        }
        textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }
}
