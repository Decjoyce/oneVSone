using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject startButton;
    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startButton);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("DecoScene");
    }
}
