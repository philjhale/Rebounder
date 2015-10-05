using UnityEngine;
using System.Collections;
using Ph.Bouncer;

namespace Ph.Bouncer.Tests
{
	public class AspectRatioHelperTests : MonoBehaviour {

		void Start () 
		{
			Debug.Log(AspectRatioHelper.GetAspectRatioString() + " - " + AspectRatioHelper.GetAspectRatio());
		}
	}
}