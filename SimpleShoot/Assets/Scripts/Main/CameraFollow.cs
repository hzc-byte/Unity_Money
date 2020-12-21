using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float deltaX = 0;
    Vector3 targetPos;
    Vector3 deltaPos;
    private CompatController player;
    public Vector2 startPos;
    public Vector2 endPos;
    void Start()
    {
        //deltaX = target.position.x - transform.position.x;
        // deltaPos = new Vector3(deltaX, 0, 0);
        player = target.GetComponent<CompatController>();
        //if (PlayerPrefs.GetInt("Load") == 1)
        //{
        //    transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //if (player.MoveDown)
        {
            targetPos = new Vector3(Mathf.Clamp(target.position.x - deltaX, 25, 75.6f), transform.position.y, transform.position.z);
            {
                transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 3);
            }
        }

        if (Input.GetKeyDown(KeyCode.Delete))
            PlayerPrefs.SetInt("Load", 0);
    }
}
