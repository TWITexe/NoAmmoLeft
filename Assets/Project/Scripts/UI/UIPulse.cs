using UnityEngine;

public class UIPulse : MonoBehaviour
{
    public float pulseAmount = 1.1f;
    public float pulseSpeed = 2f;

    private Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        float scale = 1 + (Mathf.Sin(Time.time * pulseSpeed) * (pulseAmount - 1));
        transform.localScale = initialScale * scale;
    }
}

