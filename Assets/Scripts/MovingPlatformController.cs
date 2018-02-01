using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour {

    [SerializeField] Vector3 startPoint;
    [SerializeField] Vector3 endPoint;
    [SerializeField] float moveSpeed;

    private bool movingForward;

	// Use this for initialization
	void Start () {
        this.transform.position = startPoint;
        this.movingForward = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (this.movingForward) {
            this.transform.position = Vector3.MoveTowards(this.transform.position, endPoint, moveSpeed * Time.deltaTime);
        } else {
            this.transform.position = Vector3.MoveTowards(this.transform.position, startPoint, moveSpeed * Time.deltaTime);
        }

        if (this.transform.position == endPoint) {
            this.movingForward = false;
        } else if (this.transform.position == startPoint) {
            this.movingForward = true;
        }
	}
}
