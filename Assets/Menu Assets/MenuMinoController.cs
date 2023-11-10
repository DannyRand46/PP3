using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMinoController : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject startWall;
    [SerializeField] GameObject brokeWall;
    // Start is called before the first frame update
public void triggerAnim()
    {
        anim.SetTrigger("PlayButton");
        Transform t = startWall.transform;
        Destroy(startWall);
        Instantiate(brokeWall, t.position, t.rotation);
    }
}
