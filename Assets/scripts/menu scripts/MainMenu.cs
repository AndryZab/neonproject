using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public Image fadePanel;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject backgame;
    [SerializeField] public GameObject pausebutton;
    public GameObject canvasanim;
    private bool hasRestarted = false;
    private bool hasMenu = false;
    public Collider2D colider;

    public ParticleTrail scripteffects;
    public Player body;

    Audiomanager audiomanager;
    private void Awake()
    {
        if (body != null)
        {

          body = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
        audiomanager = GameObject.FindGameObjectWithTag("audio").GetComponent<Audiomanager>();
       
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        if (!hasRestarted && body != null)
        {
            if (colider != null)
            {
                colider.enabled = false;
            }
            body.rb.bodyType = RigidbodyType2D.Static; 
            StartCoroutine(FadeAndRestart());
            hasRestarted = true;
        }
    }

    private IEnumerator FadeAndRestart()
    {
        float fadeDuration = 1.5f; 

        
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            fadePanel.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        hasRestarted = false;
    }

    public void Pause()
    {
        audiomanager.musicsource.Pause();
        audiomanager.musicsource.clip = audiomanager.background;
        pausebutton.SetActive(false);
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }


   
    public void Back()
    {
        Time.timeScale = 1;
        audiomanager.musicsource.clip = audiomanager.background;
        audiomanager.musicsource.UnPause();
        backgame.SetActive(false);
    }
    public void Mainmenu()
    {
        Time.timeScale = 1;
        if (!hasMenu)
        {
            StartCoroutine(menu());
            hasMenu = true;
        }
    }
    private IEnumerator menu()
    {
        hasMenu = false;
        body.rb.bodyType = RigidbodyType2D.Static;
        if (colider != null)
        {
            colider.enabled = false; 
        }
        canvasanim.SetActive(true);
        yield return new WaitForSecondsRealtime(1f); 
        SceneManager.LoadSceneAsync("main menu");
    }

    public void QuitGame()
    {
        
        Application.Quit();
    }

    

}   

