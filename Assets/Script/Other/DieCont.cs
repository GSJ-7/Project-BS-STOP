using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieCont : MonoBehaviour {

    public int score;

    bool isDie = false;

    public void Die()
    {
        StopCoroutine("ChangetoMovement");
        isDie = true;

        Destroy(gameObject);
    }
}
