using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animals : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 0)
        {
            Destroy(gameObject);
        }


        if (GameObject.Find("Level Manager") != null && GameManager.Instance.isGameActive == false)
        {
            Destroy(gameObject);
        }
    }
}
