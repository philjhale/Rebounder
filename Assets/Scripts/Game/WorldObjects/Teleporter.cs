using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public class Teleporter : MonoBehaviour 
	{
		public Teleporter OtherTeleporter;

		void OnTriggerEnter(Collider other) 
		{
			var ballScript = other.collider.GetComponent<Ball>();
			
			if(!ballScript)
				return;

			// TODO This is a bit rough. Could do with improving
			var newPosition = new Vector3(OtherTeleporter.transform.position.x, OtherTeleporter.transform.position.y, 0);
			newPosition += new Vector3(ballScript.rigidbody.velocity.normalized.x * 1.8f, ballScript.rigidbody.velocity.normalized.y * 1.8f, 0);
			ballScript.gameObject.transform.position = newPosition;
		}
	}
}