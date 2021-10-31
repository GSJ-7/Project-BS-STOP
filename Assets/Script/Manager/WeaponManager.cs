using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    public static bool LeftMouseOn;

    void Update()
    {
        FaceWeapomMouse();
    }

    void FaceWeapomMouse()
    {
        if (BtnManager.PauseCheck == true)
        {
            return;
        }
        else
        {
            Vector3 playerObj = GameObject.Find("Player").transform.position;

            if (LeftMouseOn == false)
            {
                gameObject.transform.position = new Vector3(playerObj.x - 0.1f, playerObj.y - 0.12f, 0);
            }
            else
            {
                gameObject.transform.position = new Vector3(playerObj.x + 0.1f, playerObj.y - 0.12f, 0);
            }

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

            if (direction.x > 0)
            {
                LeftMouseOn = true;
            }
            else
            {
                LeftMouseOn = false;
            }

            transform.up = direction;
        }
    }
}
