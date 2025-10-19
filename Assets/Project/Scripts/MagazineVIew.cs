using TMPro;
using UnityEngine;

public class MagazineVIew : MonoBehaviour
{
    [SerializeField]
    private Magazine _magazine;

    [SerializeField]
    private TextMeshProUGUI _text;

    private void Awake()
    {
        this.ValidateSerializedFields();
    }

    private void OnEnable()
    {
        _magazine.AmmoChanged += UpdateView;
    }

    private void OnDisable()
    {
        _magazine.AmmoChanged -= UpdateView;
    }

    private void UpdateView(int ammo)
    {
        _text.text = ammo.ToString();
    }
}