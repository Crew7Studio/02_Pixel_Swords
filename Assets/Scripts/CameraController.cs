using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    /* PlayerController player;
     bool isFollowing;

     public float xOffset;
     public float yOffset;

     void Start () {
         player = FindObjectOfType<PlayerController>();
         isFollowing = true;
     }

     void FixedUpdate () {
         if(isFollowing)
             transform.position = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + yOffset, transform.position.z);
     }
     */

    public Transform target;
    public float smoothing;

     Vector3 offset;

     float lowestPointY;

    void Start() {
        offset = transform.position - target.position;
        lowestPointY = transform.position.y;
    }

    void FixedUpdate() {
        Vector3 targetCamPos = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

        if (transform.position.y < lowestPointY)
            transform.position = new Vector3(transform.position.x, lowestPointY, transform.position.z);
    }


}
