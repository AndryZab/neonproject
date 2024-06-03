using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Canvas canvas;
    public Button playButton;
    public Button continueb;
    public GameObject panel;  


    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;

        playButton.gameObject.SetActive(false);
        panel.SetActive(false);
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        playButton.gameObject.SetActive(true);
        panel.SetActive(true);
    }

    public void OnPlayButtonClicked()
    {
        videoPlayer.Play();
        playButton.gameObject.SetActive(false);
        panel.SetActive(false);
    }

    public void contuniuebutton()
    {
       continueb.interactable = false;
    }
}
