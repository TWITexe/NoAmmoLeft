using UI;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject bluePlaye;
    [SerializeField] private GameObject purplePlayer;
    [SerializeField] private Timer timer;
    [SerializeField] private BaseSpawner spawner;
    [SerializeField] private TMP_Text winnerText;
    [SerializeField] private Gun bluePlayerGun;
    [SerializeField] private Gun purplePlayerGun;
    private Health bluePlayerHealth;
    private Health purplePlayerHealth;
    private bool blueNoAmmoLeft = false;
    private bool purpleNoAmmoLeft = false;
    private bool endGame = false;
    private bool noAmmoLeftAll = false;
    private string winText = "NO AMMO LEFT!";
    public event Action NoAmmoLeftAll;

    private void Awake()
    {
        bluePlayerHealth = bluePlaye.GetComponent<Health>();
        purplePlayerHealth = purplePlayer.GetComponent<Health>();

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
        if (blueNoAmmoLeft && purpleNoAmmoLeft)
        {
            noAmmoLeftAll = true;
            NoAmmoLeftAll?.Invoke();
        }
        if ((endGame == false && (noAmmoLeftAll || bluePlayerHealth.CurrentHealth <= 0 || purplePlayerHealth.CurrentHealth <= 0)))
        {           
            GameOver();           
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
    private void BlueNoAmmo() => blueNoAmmoLeft = true;
    private void PurpleNoAmmo() => purpleNoAmmoLeft = true;
    public void GameOver()
    {
        endGame = true;
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