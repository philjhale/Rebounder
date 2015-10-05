using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public class OutOfBounds : MonoBehaviour 
	{
		void OnCollisionEnter(Collision collision) 
		{
			if(collision.collider.tag == Tags.Ball)
				DestroyObject(collision.gameObject);
		}
	}
}
