using UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject bluePlayer;
    [SerializeField] GameObject purplePlayer;
    [SerializeField] private Timer timer;
    [SerializeField] private BaseSpawner spawner;
    [SerializeField] private TMP_Text winnerText;

    private Health bluePlayerHealth;
    private Health purplePlayerHealth;
    private Gun bluePlayerGun;
    private Gun purplePlayerGun;
    private bool blueNoAmmoLeft = false;
    private bool purpleNoAmmoLeft = false;
    private bool endGame = false;
    private string winText = "NO AMMO LEFT!";

    private void Awake()
    {
        bluePlayerGun.NoAmmoLeft += BlueNoAmmo;
        purplePlayerGun.NoAmmoLeft += PurpleNoAmmo;
    }
    private void Start()
    {
        spawner.StartSpawn();
        timer.OnTimerEnd += spawner.StopSpawn;
    }
    private void Update()
    {
        if ((endGame || ( blueNoAmmoLeft && purpleNoAmmoLeft) || bluePlayerHealth.CurrentHealth <= 0 || purplePlayerHealth.CurrentHealth <= 0))
        {
            GameOver();

            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

    }

    private void BlueNoAmmo() => blueNoAmmoLeft = true;
    private void PurpleNoAmmo() => purpleNoAmmoLeft = true;
    public void GameOver()
    {
        winnerText.gameObject.SetActive(true);

        if (bluePlayerHealth.CurrentHealth <= 0)
            winnerText.text = "Purple player Winner!";
        else if (purplePlayerHealth.CurrentHealth <= 0)
            winnerText.text = "Blue player Winner!";
    }
    private void OnDestroy()
    {
        timer.OnTimerEnd -= spawner.StopSpawn;
    }
}