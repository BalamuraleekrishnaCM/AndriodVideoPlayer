using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class VideoPlayerController1 : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string videoFileName = "video.mp4";
    public Text path;
    string videoFilePath;

    public GameObject selectionMenu;
    public GameObject controlsPanel;

    private void Start()
    {
        GetVideoList();
    }

    private IEnumerator LoadVideo()
    {
        if (videoPlayer == null)
            yield return null;

        videoFilePath = Application.persistentDataPath + "/Video/" + videoFileName;
        path.text = videoFilePath;

        if (File.Exists(videoFilePath))
        {
            videoPlayer.url = videoFilePath;
            videoPlayer.Prepare();
            while (!videoPlayer.isPrepared)
                yield return null;

            videoPlayer.Play();
            selectionMenu.SetActive(false);
            controlsPanel.SetActive(true);
        }
    }

    public void PlayVideo()
    {
        StartCoroutine(LoadVideo());
    }

    public void StopVideo()
    {
        videoPlayer.targetTexture.Release();
        videoPlayer.Stop();
        selectionMenu.SetActive(true);
    }

    private void GetVideoList()
    {
        string videoDirectory = Application.persistentDataPath + "/Video/";
        string[] videoFiles = Directory.GetFiles(videoDirectory, "*.mp4");

        // code to display video files goes here
    }
}
