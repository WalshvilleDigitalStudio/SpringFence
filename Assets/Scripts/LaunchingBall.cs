using UnityEngine;
using System.Collections;

public class LaunchingBall : Photon.MonoBehaviour {
	
	public int teamId=0;
	Vector3 realPosition = Vector3.zero;
	Quaternion realRotation = Quaternion.identity;
	

	void Start () 
	{
		transform.position = Random.insideUnitSphere * 5;
		realPosition = Vector3.zero;
		realRotation = Quaternion.identity;
	}

	// Update is called once per frame
	void Update () {


//		if (photonView.isMine) {
//			//Do Nothing - The Character motor/input/etc is moving us
//		} else {
//			transform.position = Vector3.Lerp (transform.position, realPosition, 0.1f);
//			transform.rotation = Quaternion.Lerp (transform.rotation, realRotation, 0.1f);
//		}
		
		
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
				if (stream.isWriting) {
						//This is our ball. We need to send its actual position to the network
						stream.SendNext (transform.position);
						stream.SendNext (transform.rotation);		
						if ((transform.position.x > 200f) || (transform.position.x  < 0f)
						    || (transform.position.z  > 200f) || (transform.position.z  < 0f)) {
							Debug.Log ("Should Destroy me!");
							PhotonNetwork.Destroy (gameObject);
						}
		} else {
						//This is someone else's ball.  We need to recieve its position (as of a few 
						//milliseconds ago) and update our version of that ball.
						realPosition = (Vector3)stream.ReceiveNext ();
						realRotation = (Quaternion)stream.ReceiveNext ();
						if ((realPosition.x > 200f) || (realPosition.x  < 0f)
						    || (realPosition.z  > 200f) || (realPosition.z  < 0f)) {
						Debug.Log ("Should Destroy Them!");
					PhotonNetwork.Destroy (gameObject);
						}
				}
		}
/*
*	public Transform explosionPrefab;
*	private Transform explosion;
*	public GameObject bullet;
*	
*	void OnCollisionEnter(Collision col) {
*		if(col.gameObject.name == "Player"){
*			PhotonNetwork.Destroy (col.gameObject); 
*			Instantiate(explosionPrefab, col.transform.position, col.transform.rotation);
*		}
*		if (col.gameObject.name == "SUV") {
*			PhotonNetwork.Destroy (col.gameObject); 
*			Instantiate(explosionPrefab, col.transform.position, col.transform.rotation);
*		}
*		PhotonNetwork.Destroy (bullet); 
*		Instantiate(explosionPrefab, bullet.transform.position, bullet.transform.rotation);
*	}
*/

}
