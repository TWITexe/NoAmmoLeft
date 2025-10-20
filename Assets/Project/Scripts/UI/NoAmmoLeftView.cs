using TMPro;
using UnityEngine;

namespace UI
{
    public class NoAmmoLeftView : MonoBehaviour
    {
        [SerializeField]
        private Gun _gun;

        [SerializeField]
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _gun.NoAmmoLeft += Show;

            _text.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _gun.NoAmmoLeft -= Show;
        }

        private void Show()
        {
            _text.gameObject.SetActive(true);
        }
    }
}