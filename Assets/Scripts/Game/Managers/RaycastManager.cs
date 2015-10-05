using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public class RaycastManager : MonoBehaviour 
	{
		private Line line;
		private bool isMouseDown = false;
		private FlickTracker flickTracker;
		private string lastHitTag;
		private Launcher tappedShooter;
		
		private LineCountManager lineCountManager;
		private bool isPausedOrComplete = false;
		
		void Start()
		{
			lineCountManager = (LineCountManager)FindObjectOfType(typeof(LineCountManager));	
		}
		
		void Update() 
		{		
			if(isPausedOrComplete) return;

			if(InputHelper.IsFirstTouch()) 
			{
				AttemptToGrab();
			}
			else if(InputHelper.IsTouchEnded())
			{
				TouchEnded();
			}
			else if(isMouseDown)
			{
				UpdateLine();
			}
		}

		public void OnPauseOrComplete()
		{
			isPausedOrComplete = true;
		}
		
		public void OnResume()
		{
			isPausedOrComplete = false;
		}
		
		void OnEnable()
		{
			Messenger.AddListener(Events.GamePaused, OnPauseOrComplete);
			Messenger.AddListener(Events.GameResumed, OnResume);
			Messenger.AddListener(Events.LevelComplete, OnPauseOrComplete);
		}
		
		void OnDisable()
		{
			Messenger.RemoveListener(Events.GamePaused, OnPauseOrComplete);
			Messenger.RemoveListener(Events.GameResumed, OnResume);
			Messenger.RemoveListener(Events.LevelComplete, OnPauseOrComplete);
		}

		private void AttemptToGrab()
		{
			// Bit of a hack. The the UI raycast hit anything then return
			var hit = UICamera.lastHit;
			if(hit.collider != null)
				return;

			RaycastHit raycastHit;

			if(RaycastHelper.HasHitObjectWithTags(out raycastHit, Tags.Background, Tags.Handle, Tags.Line, Tags.Shooter, Tags.Obstacle))
			{
				lastHitTag = raycastHit.collider.gameObject.tag;

				if(lineCountManager.CanDrawAnotherLine() && (lastHitTag == Tags.Background || lastHitTag == Tags.Obstacle))
					StartDrawingLine(raycastHit.point);
				else if(lastHitTag == Tags.Handle)
					GrabHandle(raycastHit.collider.gameObject);
				else if(lastHitTag == Tags.Line)
					StartDraggingLine(raycastHit.collider.gameObject);
				else if(lastHitTag == Tags.Shooter)
					StartShooterTap(raycastHit.collider.gameObject);
			}
		}
		
		private void GrabHandle(GameObject grabbedHandle)
		{
			// TODO Null check?
			//Handle handle = (Handle)grabbedHandle.GetComponent<HandleParent>().Handle;
			
			//line = handle.Line;
			//line.GrabHandle(handle.Name);
			LineHandle handle = (LineHandle)grabbedHandle.transform.parent.GetComponent<LineHandle>();
			line = handle.Grab();

			isMouseDown = true;
			Messenger.Broadcast(Events.LineManipulationStarted);
		}
		
		private void StartDraggingLine(GameObject grabbedObject)
		{
			// This is the touch collider
			line = grabbedObject.transform.parent.parent.GetComponent<Line>();
			line.StartDragging();

			isMouseDown = true;
			flickTracker = new FlickTracker();
			Messenger.Broadcast(Events.LineManipulationStarted);
		}
		
		private void StartDrawingLine(Vector2 startPoint)
		{
			line = LineHelper.Create(startPoint, lineCountManager.GetNextColour());
			isMouseDown = true;
			Messenger.Broadcast(Events.LineManipulationStarted);
		}

		private void StartShooterTap(GameObject shooter)
		{
			tappedShooter = shooter.GetComponent<Launcher>();
			tappedShooter.StartTap();
		}

		private void TouchEnded()
		{
			if(lastHitTag == Tags.Shooter)
				EndShooterTap();
			else
				FinishDrawingLine();

			lastHitTag = null;
		}

		private void FinishDrawingLine()
		{
			isMouseDown = false;
			
			if(line == null)
				return;
			
			if(line.IsLongEnough() && !IsFlick())
			{
				if(line.IsCreatingLine())
					lineCountManager.LineDrawn(line.Colour);	
				
				line.StopDragging();	
			}
			else
			{
				if(!line.IsCreatingLine()) 
					lineCountManager.LineDeleted(line.Colour);
				
				if(IsFlick())
					line.Flick(flickTracker.GetFlickVector());
				else
					GameObject.Destroy(line.gameObject);
			}
			
			line = null;
			flickTracker = null;
			Messenger.Broadcast(Events.LineManipulationFinished);
		}

		private void EndShooterTap()
		{
			tappedShooter.EndTap();
			tappedShooter = null;
		}
		
		private void UpdateLine()
		{
			RaycastHit raycastHit;
			
			if(RaycastHelper.HasHitObjectWithLayerMaskAndTags(out raycastHit, LayerMaskHelper.FromInt((int)Layers.Background), Tags.Background))
			{
				line.UpdatePosition(raycastHit.point.x, raycastHit.point.y);
				//Debug.Log(string.Format("Updatng position {0}, {1}", raycastHit.point.x, raycastHit.point.y));
				if(line.IsBeingDragged())
					flickTracker.UpdatePosition(raycastHit.point.x, raycastHit.point.y, raycastHit.point.z);
			}
		}
		
		private bool IsFlick()
		{
			return flickTracker != null && flickTracker.IsFlick();	
		}
	}
}
