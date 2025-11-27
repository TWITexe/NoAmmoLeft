using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class KickAndStun : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject fist;
    [SerializeField] float stunTime;
    private PlayerAnimation _animation;
    private bool _isStun = false;
    public bool IsStun => _isStun;

    void Awake()
    {
        _animation = GetComponent<PlayerAnimation>();
    }
    public void Kick()
    {
        _animation.KickAnimation();
        fist.SetActive(true);
    }
    public void GetStun()
    {
        if (gameObject.GetComponent<Health>().IsAlive == false)
            return;

        Debug.Log(gameObject.name + " stunned");
        _isStun = true;
        _animation.StunAnimation(true);
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
        StartCoroutine(WakeUp());
    }
    IEnumerator WakeUp()
    {
        yield return new WaitForSeconds(stunTime-0.01f);
        _animation.StunAnimation(false);
        yield return new WaitForSeconds(stunTime);
        _isStun = false;
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }


}
