using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuMinoController : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject startWall;
    [SerializeField] GameObject brokeWall;
    [SerializeField] Image fade;
    [SerializeField] AudioSource sfx;
    [SerializeField] float magnitude;

    bool isFading;
    float fadeRatio;
    private void Start()
    {
        isFading = false;
    }
    public void triggerAnim()
    {
        anim.SetTrigger("PlayButton");
        Transform t = startWall.transform;
        Destroy(startWall);
       GameObject wall = Instantiate(brokeWall, t.position, t.rotation);
       Rigidbody[] wallChildren = wall.GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < wallChildren.Length; i++)
        {
            wallChildren[i].AddForce(transform.forward*magnitude, ForceMode.Impulse);
        }
    }
    public void LoadGame()
    {
        isFading = true;
        fadeRatio = 0;
    }

    public void PlayRoarAudio()
    {
        sfx.Play();
    }

    private void Update()
    {
       if (isFading)
        {
            fadeRatio += Time.deltaTime;
            fade.color = new Color32(1, 2, 3, (byte)(fadeRatio * 255));
            if(fadeRatio >= 1)
            {
                //TODO: Check for correct Scene Load
                SceneManager.LoadScene(1);
            }
        }
    }
}
