using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;

    PlayerInput playerInput;
    bool isEscPressed;


    
    private void Awake() {
        playerInput = new PlayerInput();

        playerInput.CharacterControls.PauseMenu.started += OnEsc;
        playerInput.CharacterControls.PauseMenu.canceled += OnEsc;
    }

    void OnEsc (InputAction.CallbackContext context){
        isEscPressed = context.ReadValueAsButton();
        if (isEscPressed){
            if (gameIsPaused){
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void PlayGame () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame () {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void GoMenu () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
    public void GoPlayGame () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    void Pause () {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Resume () {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    
    void OnEnable() {
        playerInput.CharacterControls.Enable();
    }

    void OnDisable() {
        playerInput.CharacterControls.Disable();
    }
}
