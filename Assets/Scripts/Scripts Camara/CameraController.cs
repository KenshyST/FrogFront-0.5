using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;

    public Transform ActiveRoom;
    public float damSpeed;

    public static CameraController instance;


    [Range(-5,5)]
    public float minModX = 4.51f, maxModX = -5f, minModY= 5f, maxModY= -5f;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        //player = PlayerController.instance.gameObject.transform;

        ActiveRoom = player;

        transform.position = new Vector3(player.position.x, player.position.y, -1);
    }
    // Update is called once per frame
    void Update()
    {
        var minPosY = ActiveRoom.GetComponent<BoxCollider2D>().bounds.min.y+minModY;
        var maxPosY = ActiveRoom.GetComponent<BoxCollider2D>().bounds.max.y+maxModY;
        var minPosX = ActiveRoom.GetComponent<BoxCollider2D>().bounds.min.x+minModX;
        var maxPosX = ActiveRoom.GetComponent<BoxCollider2D>().bounds.max.x+maxModX;


        Vector3 clampedPos = new Vector3(
            Mathf.Clamp(player.position.x, minPosX, maxPosX),
            Mathf.Clamp(player.position.y, minPosY, maxPosY),
            Mathf.Clamp(player.position.z, -10f, -10f)
            );

        Vector3 smoothPosition =  Vector3.Lerp(transform.position, clampedPos, damSpeed *Time.deltaTime);
        transform.position = smoothPosition;
    }
}
