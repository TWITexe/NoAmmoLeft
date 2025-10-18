using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Timer : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private Image timerImage;

    private float _timeLeft = 0f;
    public event Action OnTimerEnd;
    private IEnumerator StartTimer()
    {
        while (_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
            var normalizedValue = Mathf.Clamp(_timeLeft / time, 0.0f, 1.0f);
            timerImage.fillAmount = normalizedValue;
            yield return null;
        }
        OnTimerEnd?.Invoke();
    }

    private void Start()
    {
        _timeLeft = time;
        StartCoroutine(StartTimer());
    }
}
