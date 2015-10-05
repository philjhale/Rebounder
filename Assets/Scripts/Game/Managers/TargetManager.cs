using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Ph.Bouncer
{
	public class TargetManager : MonoBehaviour 
	{		
		private const int GOAL_TARGET_HITS = 5;
		private TargetHits targetHits;
		
		void Start () {
			var targets = (Target[])FindObjectsOfType(typeof(Target));
			
			targetHits = new TargetHits(targets);
		}	
		
		void OnTargetHit(Colour targetColour, int hits)
	    {
	        //Debug.Log("Hit event! " + targetColour + "hits " + hits);
			
			targetHits.UpdateHits(targetColour, hits);
			if(targetHits.AllTargetsHaveHitsGreaterOrEqualTo(GOAL_TARGET_HITS))
				Messenger.Broadcast(Events.LevelComplete);
	    }
		
		void OnTargetDrain(Colour targetColour, int hits)
	    {
	        //Debug.Log("Drain event! " + targetColour + "hits " + hits);
			targetHits.UpdateHits(targetColour, hits);
	    }
		
		void OnEnable()
	    {
	        Messenger<Colour, int>.AddListener(Events.TargetHit, OnTargetHit);
			Messenger<Colour, int>.AddListener(Events.TargetDrain, OnTargetDrain);
	    }
		
		void OnDisable()
	    {
	        Messenger<Colour, int>.RemoveListener(Events.TargetHit, OnTargetHit);
			Messenger<Colour, int>.RemoveListener(Events.TargetDrain, OnTargetDrain);
	    }
	}
}