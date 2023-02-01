using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string videoFileName = "video.mp4";
    public Text path;
    private VideoClip video;
    string videoFilePath;
    void Start()
    {




    }

    IEnumerator LoadVideoRoutine()
    {
        if (videoPlayer == null)
            yield return null;


        string root = videoFilePath;

        print("This is video url : " + root);

        if (!File.Exists(root))
        {
            print("no video");
            yield return null;
        }
        else if (File.Exists(root))
        {
            print("Found vid");
            videoPlayer.url = root;

            videoPlayer.Play();
        }

        while (!videoPlayer.isPrepared)
        {
            //print("preparing");
            yield return null;
        }

        print("playing");
        videoPlayer.Play();




        // video.url = root;
        // video.Play();
    }
    public void PlayVideo(int index)
    {

        //videoFilePath = Application.persistentDataPath + "/" + "Video (" + index + ").mp4";
        videoFilePath = "/storage/emulated/0/360Videos/Video (" + index + ").mp4";
        path.text = videoFilePath;
        videoPlayer.url = videoFilePath;
        StartCoroutine("LoadVideoRoutine");

    }
    public void StopVideo()
    {
        videoPlayer.targetTexture.Release();
        videoPlayer.Stop();
    }
}