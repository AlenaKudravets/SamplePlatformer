using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.GetComponent<PlatformerCharacterController>() != null)
        {
            PlatformerGameManager.Instance.RespawnCharacter();
        }
    }
}
