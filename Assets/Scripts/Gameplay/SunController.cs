using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{
    [SerializeField] private LayerMask CollectByLayers;
    [SerializeField] private string HideAnimationName;
    [SerializeField] private int ScoreSun = 100;

    private bool _isCollected = false;
    private Animation _animation;

    void Awake()
    {
        _animation = GetComponent<Animation>();
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (_isCollected)
        {
            return;
        }

        PlatformerStats.AddScore(ScoreSun);

        if (((1 << collider2D.gameObject.layer) & CollectByLayers.value) != 0)
        {
            _isCollected = true;
            _animation.Play(HideAnimationName);

        }

        GetComponent<AudioSource>().Play();
    }

    public void DestroySun()
    {

        Destroy(gameObject);
    }
}
