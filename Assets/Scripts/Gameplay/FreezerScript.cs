using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezerScript : MonoBehaviour
{
    public delegate void SpeedChangeHandler (float factor, float sec);
    public static event SpeedChangeHandler ChangeSpeedByFactor;

    [SerializeField] private LayerMask CollectByLayers;
    [SerializeField] private string HideAnimationName;

    private bool m_isCollected = false;
    private Timer m_timer;
    private static float m_freezeSpeedFactor = 0.5f;
    private Animation m_animation;

    private void Awake()
    {
        m_animation = GetComponent<Animation>();
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (m_isCollected)
        {
            return;
        }

        if (((1 << collider2D.gameObject.layer) & CollectByLayers.value) != 0)
        {
            m_isCollected = true;
            ChangeSpeedByFactor(m_freezeSpeedFactor, 2f);
            m_animation.Play(HideAnimationName);
        }

        if (GetComponent<AudioSource>() != null)
        {
            GetComponent<AudioSource>().Play();
        }
    }

    public void DestroySun()
    {
        Destroy(gameObject);
    }
}
