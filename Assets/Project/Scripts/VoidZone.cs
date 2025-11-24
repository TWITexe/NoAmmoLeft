using UnityEngine;

public class VoidZone : MonoBehaviour
{
    [Header("Границы зоны (два угла прямоугольника)")]
    [SerializeField] private Transform _pointA;
    [SerializeField] private Transform _pointB;

    [Header("Урон при выходе за границу")]
    [SerializeField] private int _damageAmount = 1;

    [Tooltip("Раз в сколько секунд наносить урон, пока персонаж вне зоны")]
    [SerializeField] private float _damageInterval = 1f;

    private Health _playerHealth;

    private float _minX, _maxX, _minY, _maxY;

    private float _timer;

    private void Awake()
    {
        _playerHealth = GetComponent<Health>();

        if (_playerHealth == null)
        {
            Debug.LogError($"{nameof(VoidZone)}: на объекте нет компонента PlayerHealth!");
        }

        if (_pointA == null || _pointB == null)
        {
            Debug.LogError($"{nameof(VoidZone)}: не заданы точки _pointA / _pointB!");
            return;
        }

        _minX = Mathf.Min(_pointA.position.x, _pointB.position.x);
        _maxX = Mathf.Max(_pointA.position.x, _pointB.position.x);
        _minY = Mathf.Min(_pointA.position.y, _pointB.position.y);
        _maxY = Mathf.Max(_pointA.position.y, _pointB.position.y);
    }

    private void Update()
    {
        if (_playerHealth == null || _pointA == null || _pointB == null)
            return;

        Vector3 pos = transform.position;

        bool isOutOfBounds =
            pos.x < _minX || pos.x > _maxX ||
            pos.y < _minY || pos.y > _maxY;

        if (!isOutOfBounds)
        {
            _timer = 0f;
            return;
        }

        _timer += Time.deltaTime;

        if (_timer >= _damageInterval)
        {
            _playerHealth.ApplyDamage(_damageAmount);
            _timer = 0f;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_pointA == null || _pointB == null)
            return;

        float minX = Mathf.Min(_pointA.position.x, _pointB.position.x);
        float maxX = Mathf.Max(_pointA.position.x, _pointB.position.x);
        float minY = Mathf.Min(_pointA.position.y, _pointB.position.y);
        float maxY = Mathf.Max(_pointA.position.y, _pointB.position.y);

        Vector3 center = new Vector3((minX + maxX) / 2f, (minY + maxY) / 2f, 0f);
        Vector3 size = new Vector3(maxX - minX, maxY - minY, 0f);

        Gizmos.DrawWireCube(center, size);
    }

}
