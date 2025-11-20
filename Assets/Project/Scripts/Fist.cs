using System.Collections;
using UnityEngine;

public class Fist : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out KickAndStun kickedPlayer))
        {            
            StartCoroutine(DisableFist(kickedPlayer));
        }
    }
    IEnumerator DisableFist(KickAndStun kickedPlayer)
    {
        kickedPlayer.GetStun();
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);
    }
}
