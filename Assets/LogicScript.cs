using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;


public class LogicScript : MonoBehaviour
{
    public static LogicScript Instance { get; private set; }
    public int playerScore;
    public Text scoreText;
    public GameObject gameOverScreen;
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip scoreSound;
    [SerializeField] private AudioClip gameOverSound;

    public Text finalScoreText;
    public TMP_Text highScoreText;
    [SerializeField] private Button tryAgainButton;
    private PlayerInput playerInput;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple LogicScript instances found, destroying duplicate.");
            Destroy(gameObject);
        }
    }


    public void addScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();

        if (audioSource && scoreSound)
        {
            audioSource.PlayOneShot(scoreSound);
        }
    }

    public void HighScoreUpdate()
    {
        //Is there already a highscore?
        if (PlayerPrefs.HasKey("SavedHighScore"))
        {
            //Is the new score higher than the saved one?
            if (playerScore > PlayerPrefs.GetInt("SavedHighScore"))
            {
                //Set a new high score
                PlayerPrefs.SetInt("SavedHighScore", playerScore);

            }
        }
        else
        {
            //If there is no highscore...set it
            PlayerPrefs.SetInt("SavedHighScore", playerScore);
        }
        //Update our TMP
        finalScoreText.text = playerScore.ToString();
        highScoreText.text = PlayerPrefs.GetInt("SavedHighScore").ToString();
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
        HighScoreUpdate();
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