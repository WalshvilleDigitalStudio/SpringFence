using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(AudioSource))]
public class Launch : MonoBehaviour {

	const int MaxForce = 12 * 1000;
	const float ForceAcceleration = 3;

	DateTime clickStart;
	Boolean pullingBack = false;

	public GameObject launchingBall;

	
	// Use this for initialization
	void Start () {
	}
	
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			PullBack ();		
		} 

		if(Input.GetKeyUp (KeyCode.Space))
		{ 
			Release();
		}
	}


	void PullBack () {
		clickStart = DateTime.Now;
		pullingBack = true;
	}

	void Release() {
		int forceCoeff = currentForceCoeff();
		//var facing = transform.rotation;
		SpawnMyBall ().rigidbody.AddForce (transform.forward * forceCoeff);
		//LaunchingBall.rigidbody.AddForce (new Vector3(0,1,1) * forceCoeff);
		audio.volume = (float) forceCoeff / (float) MaxForce;
		audio.Play ();
		pullingBack = false;
	}

	void OnGUI() {
		if (pullingBack) {
			string forceString = String.Format("Force: {0}%", ((float) currentForceCoeff() / MaxForce) * 100);
			GUI.Label (new Rect (0, 0, Screen.width, Screen.height), forceString);
		}
	}

 	private int currentForceCoeff() {
		TimeSpan clickLength = DateTime.Now - clickStart;
		return (int)Math.Min(MaxForce, clickLength.TotalMilliseconds * ForceAcceleration);
	}

	GameObject SpawnMyBall() {
//		Debug.Log ("SpawnMyAmmo");
		Vector3 spawnBallPosition = new Vector3 (transform.position.x, transform.position.y + 3, transform.position.z);
		//Instantiate(launchingBall);
		return PhotonNetwork.Instantiate("LaunchingBall", spawnBallPosition, transform.rotation, 0);
		//PhotonNetwork.Instantiate("LaunchingBall", transform.position, transform.rotation, 0);

//		if (spawnAmmoSpots == null) {
//			Debug.Log(":Issues With SpawnAmmoSpots");
//			return;
//		}
		
//		SpawnAmmoSpot myAmmoSpawnSpot = spawnAmmoSpots [0];
//		launchingBall = GameObject.Find("LaunchingBall");
//		Instantiate(launchingBall, myAmmoSpawnSpot.transform.position, myAmmoSpawnSpot.transform.rotation);

		//SpawnAmmoSpot myAmmoSpawnSpot = spawnAmmoSpots [UnityEngine.Random.Range (0, spawnAmmoSpots.Length)];
		//PhotonNetwork.Instantiate ("PlayerController", new Vector3(300f, 14f, 0f) ,Quaternion.identity, 0);
		//GameObject myAmmo = (GameObject)PhotonNetwork.Instantiate ("LaunchingBall", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);
		//PhotonNetwork.Instantiate ("LaunchingBall", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);
	}
}
