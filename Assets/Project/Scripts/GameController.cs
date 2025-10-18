using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private BaseSpawner spawner;

    private void Start()
    {
        spawner.StartSpawn();
        timer.OnTimerEnd += spawner.StopSpawn;
    }

    private void OnDestroy()
    {
        timer.OnTimerEnd -= spawner.StopSpawn;
    }
}
