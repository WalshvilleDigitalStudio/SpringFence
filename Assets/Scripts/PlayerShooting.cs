using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {

	public float damage = 25;
	public float fireRate = 0.5f;
	float coolDown = 0;

	// 
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		coolDown -= Time.deltaTime;

		if (Input.GetButton("Fire1")) {
			//Player wants to shoot so shoot!
			Fire();
		}

	}
	
	// 
	void Fire () {
		if (coolDown > 0) {
			return;
		}
		Debug.Log ("Firing Gun");

		Ray ray = new Ray (Camera.main.transform.position, Camera.main.transform.forward);
		Transform hitTransform;
		Vector3 hitPoint;

		hitTransform = findClosestHitObject (ray, out hitPoint);

		if (hitTransform != null) {  //null fires into nothing
			Debug.Log ("We hit: " + hitTransform.name);
			//We could do a special effect at the hit location
			//like do smoke or spark effect at hitInfo.point

			Health h = hitTransform.GetComponent<Health>();

			while (h == null && hitTransform.parent) {
				hitTransform = hitTransform.parent;
				h = hitTransform.GetComponent<Health>();
			}

			if (h != null) {
				h.TakeDamage(damage);
			}
		}


		//Physics.Raycast (ray, out hitInfo);
		//if (hitInfo.collider.name == "Graphics") {
		//	Debug.Log ("We hit: " + hitInfo.collider.name);
		//}



		coolDown = fireRate;
	}


	Transform findClosestHitObject(Ray ray, out Vector3 hitPoint) {
		RaycastHit[] hits = Physics.RaycastAll(ray);

		Transform closestHit = null;
		float distance = 0;
		hitPoint = Vector3.zero;

		foreach (RaycastHit hit in hits) {
			if (hit.transform != this.transform && (closestHit == null || hit.distance < distance)) {
				//hit something that is not us
				//hit something
				//hit the first thing that is not us
				//is at least closer than the previous closes thing

				closestHit = hit.transform;
				distance = hit.distance;
				hitPoint = hit.point;

			}
		}
		//closestHit is either null because we did not hit anything (other than maybe ourselves) or it is the closest thing that we hit.
		return closestHit;

	
	}



}
