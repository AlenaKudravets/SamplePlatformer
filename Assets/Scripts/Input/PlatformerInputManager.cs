using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformerInputManager : MonoBehaviour
{
    [SerializeField] private KeyCode moveRightButton = KeyCode.RightArrow;
    [SerializeField] private KeyCode moveLeftButton = KeyCode.LeftArrow;
    [SerializeField] private List<KeyCode> jumpButtons = new List<KeyCode> {KeyCode.Space, KeyCode.W};
    [SerializeField] private KeyCode boostButton = KeyCode.LeftShift;

    public delegate void ControlDelegateMove(bool isBoosted);
    public delegate void ControlDelegateJump();

    public static event ControlDelegateMove OnMovingLeft;
    public static event ControlDelegateMove OnMovingRight;
    public static event ControlDelegateJump OnJump;

    void Update()
    {
        ProcessKeyBoardInput();
    }

    void OnDestroy()
    {
        OnMovingLeft = null;
        OnMovingRight = null;
    }

    private void ProcessKeyBoardInput()
    {
        bool isBoosted = Input.GetKey(boostButton);

        if (Input.GetKey(moveLeftButton))
        {
            if (OnMovingLeft != null)
            {
                OnMovingLeft(isBoosted);
            }

        }

        if (Input.GetKey(moveRightButton))
        {
            if (OnMovingRight != null)
            {
                OnMovingRight(isBoosted);
            }
        }

        //foreach (var buttonKey in jumpButtons)
        for (int i = 0; i < jumpButtons.Count; i++)
        {
            if (Input.GetKeyDown(jumpButtons[i]))
            {
                if (OnJump != null)
                {
                    OnJump();
                }
            }
        }
    }
}