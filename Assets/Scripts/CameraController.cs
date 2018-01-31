using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] GameObject target;
    [SerializeField] float followAhead;
    [SerializeField] float smoothing;

    private Vector3 targetPosition;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        this.targetPosition = new Vector3(this.target.transform.position.x, this.transform.position.y, this.transform.position.z);

        if (this.target.transform.localScale.x > 0f) {
            this.targetPosition = new Vector3(this.targetPosition.x + this.followAhead, this.targetPosition.y, this.targetPosition.z);
        } else {
            this.targetPosition = new Vector3(this.targetPosition.x - this.followAhead, this.targetPosition.y, this.targetPosition.z);
        }

        this.transform.position = Vector3.Lerp(this.transform.position, this.targetPosition, smoothing * Time.deltaTime);
    }
}