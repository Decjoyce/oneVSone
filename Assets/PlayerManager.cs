using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject connectText_p1, connectText_p2, readyText_p1, readyText_p2;

    private PlayerInputManager playerInputManager;
    private void Awake()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
    }

    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += PlayerConnected;
    }

    void PlayerConnected(PlayerInput player)
    {
        if (playerInputManager.playerCount > 2)
            playerInputManager.EnableJoining();
        if (playerInputManager.playerCount == 2)
            playerInputManager.DisableJoining();

        if (playerInputManager.playerCount == 1)
        {
            connectText_p1.SetActive(false);
            readyText_p1.SetActive(true);
        }
        
        if(playerInputManager.playerCount == 2)
        {
            connectText_p2.SetActive(false);
            readyText_p2.SetActive(true);
        }

    }
    
}
