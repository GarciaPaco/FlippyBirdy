using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class LogicScript : MonoBehaviour
{

    public int playerScore;
    public Text scoreText;
    public GameObject gameOverScreen;
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip scoreSound;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private Button tryAgainButton;
    private PlayerInput playerInput;

/*     private void OnEnable()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInput.actions["Submit"].performed += OnSubmit;
    }

    private void OnDisable()
    {
        if (playerInput != null)
            playerInput.actions["Submit"].performed -= OnSubmit;
    } */

    public void addScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();

        if (audioSource && scoreSound)
        {
            audioSource.PlayOneShot(scoreSound);
        }
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void gameOver()
    {
        if (audioSource && gameOverSound)
        {
            audioSource.PlayOneShot(gameOverSound);
        }
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        if (context.performed && gameOverScreen.activeInHierarchy)
        {
            tryAgainButton.onClick.Invoke();
        }
    }

    public void quitGame()
{
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Pour arrêter le jeu dans l'éditeur
    #else
        Application.Quit(); // Pour fermer le jeu en build
    #endif
}

}