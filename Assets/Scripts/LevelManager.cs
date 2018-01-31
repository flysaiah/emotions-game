using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    [SerializeField] Vector3 playerStartPosition;
    //[SerializeField] Text deathMessage;
    [SerializeField] PlayerController playerCtl;
    [SerializeField] CameraController cameraCtl;
    [SerializeField] ParticleSystem streamOfCons;

	// Use this for initialization

	// Update is called once per frame
	void Update () {

        Vector3 t = this.streamOfCons.transform.position;
        this.streamOfCons.transform.position = new Vector3(this.cameraCtl.gameObject.transform.position.x + 15f, t.y, t.z);
	}

    public void handlePlayerDeath() {
        //this.deathMessage.text = "U dead son";
        this.StartCoroutine("Respawn");
    }

    private IEnumerator Respawn() {
        this.playerCtl.gameObject.SetActive(false);
        yield return new WaitForSeconds(3f);
        this.playerCtl.transform.position = playerStartPosition;
        this.playerCtl.gameObject.SetActive(true);
        //this.deathMessage.text = "";
    }
}
