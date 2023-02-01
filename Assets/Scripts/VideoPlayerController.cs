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
    string videoFilePath;
    public GameObject[] button;
    public Transform VideoListMenu;
    public RectTransform rect;

    public GameObject selectionMenu;
    public GameObject controlsPanel;

    void Start()
    {


        GetVideoList();

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
        selectionMenu.SetActive(false);
        controlsPanel.SetActive(true);



        // video.url = root;
        // video.Play();
    }
    public void PlayVideo(FileDetails file)
    {

        videoFilePath = Application.persistentDataPath + "/" + "Video/" + file.fileName;
        //videoFilePath = "/storage/emulated/0/360Videos/Video (" + file.id + ").mp4";
        path.text = videoFilePath;
        videoPlayer.url = videoFilePath;
        StartCoroutine("LoadVideoRoutine");

    }
    public void StopVideo()
    {
        videoPlayer.targetTexture.Release();
        videoPlayer.Stop();
        selectionMenu.SetActive(true);
    }

    public void GetVideoList() {
        string path;
        path = Application.persistentDataPath + "/Video";
        //path = "/storage/emulated/0/360Videos/Video";
        string[] fileEntries = Directory.GetFiles(path, "*.mp4");

        for (int i = 0; i < fileEntries.Length; i++)
        {
            Debug.Log(fileEntries[i]);
           // button = Instantiate(buttonPrefab, VideoListMenu);
            string fileName = Path.GetFileName(fileEntries[i]);
            button[i].GetComponentInChildren<TMPro.TMP_Text>().text = fileName;
      
            button[i].GetComponent<FileDetails>().id = i;
            button[i].GetComponent<FileDetails>().fileName = fileName;
        }
        for (int i = fileEntries.Length; i < button.Length; i++)
        {
            button[i].SetActive(false);
        }

       
        rect.sizeDelta = new Vector2(0, ((fileEntries.Length / 4)+1) * 300);

    }
}