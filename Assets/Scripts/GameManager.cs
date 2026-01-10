using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject gameOverMenu;
    private bool gameOver = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
    }
    
    void Start()
    {
        gameOverMenu.SetActive(false);
    }

    void Update()
    {
    }

    public void GameOver()
    {
        if (gameOver) 
            return;
        gameOver = true;
        if (gameOverMenu != null)
        {
            Time.timeScale = 0;
            gameOverMenu.SetActive(true);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }
}
