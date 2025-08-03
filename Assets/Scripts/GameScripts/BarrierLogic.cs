using System.Collections;
using UnityEngine;

public class BarrierLogic : MonoBehaviour
{
    [HideInInspector] public bool is_loop_4 = false;



    private void LateUpdate()
    {
        if (GameManager.Instance.IsLoop(4)) GetComponent<BoxCollider2D>().enabled = true;
        else { GetComponent<BoxCollider2D>().enabled = false; }

        Debug.Log(" " + is_loop_4 + " Worked!");

    }
}
