using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Timer : MonoBehaviour
    {
        [SerializeField]
        private float _time;

        [SerializeField]
        private Image _timerImage;

        [SerializeField]
        private Image _timerDownImage;

        private float _timeLeft = 0f;

        public event Action OnTimerEnd;
        public event Action OnTimerStart;

        private void Start()
        {
            _timeLeft = _time;
            StartCoroutine(StartTimer());
        }

        private IEnumerator StartTimer()
        {
            OnTimerStart?.Invoke();
            _timerImage.gameObject.SetActive(true);
            _timerDownImage.gameObject.SetActive(true);

            while (_timeLeft > 0)
            {
                _timeLeft -= Time.deltaTime;
                float normalizedValue = Mathf.Clamp(_timeLeft / _time, 0.0f, 1.0f);
                _timerImage.fillAmount = normalizedValue;
                yield return null;
            }

            OnTimerEnd?.Invoke();
            _timerImage.gameObject.SetActive(false);
            _timerDownImage.gameObject.SetActive(false);
        }
    }
}