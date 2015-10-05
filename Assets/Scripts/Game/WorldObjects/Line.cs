using UnityEngine;
using System.Collections;
using System;

namespace Ph.Bouncer
{
	public class Line : MonoBehaviour 
	{
		public Colour Colour = Colour.Orange;

		private const float MIN_LINE_LENGTH = 2f;
		private const float POST_MOVE_CANNOT_COLLIDE_TIME = 0.2f;

		private LineHandle[] handles;
		private int grabbedHandle;

		private bool isDraggingLine = false;
		private bool isCreatingLine = false;

		private LineMiddle lineMiddle;
		private LineTouch lineTouch;

		private Vector3 previousDragPosition;
		private Vector3 previousDragAmount;
		private float previousRotationAmount;
		private float nextStopTime = 0;

		public void Start()
		{
			handles = (LineHandle[])GetComponentsInChildren<LineHandle>();
			lineTouch = (LineTouch)GetComponentInChildren<LineTouch>();
			lineMiddle = (LineMiddle)GetComponentInChildren<LineMiddle>();

			isCreatingLine = true;

			grabbedHandle = 1;
		}

		public void Update()
		{
			SetBallCollisions();
		}

		public void UpdatePosition(float x, float y)
		{			
			if(HasGrabbedHandle())
			{
				RotateLine(x, y);
			}
			else if(isDraggingLine)
			{
				DragLine(x, y);
			}
		}

		private void SetBallCollisions()
		{
			if(CannotCollide())
				SetBallsWillNotCollide();
			else
				SetBallsCanCollide();
		}

		public LineHandle GetGrabbedHandle()
		{
			if(grabbedHandle < 0)
				throw new InvalidOperationException("Attempting to get handle which isn't set");

			return handles[grabbedHandle];
		}

		public LineHandle GetRotatingHandle()
		{
			if(grabbedHandle < 0)
				throw new InvalidOperationException("Attempting to get handle which isn't set");

			int nonGrabbedHandleIndex = grabbedHandle == 0 ? 1 : 0;

			return handles[nonGrabbedHandleIndex];
		}

		public void Flick(Vector3 velocity)
		{
			lineTouch.Flicked();
			
			iTween.ValueTo(gameObject, iTween.Hash("from", 1f, "to", 0f, "time", 0.3f, "onupdate", "AnimateAlpha", "easetype", "easeOutExpo", "oncomplete", "Kill"));
			iTween.ValueTo(gameObject, iTween.Hash("from", velocity, "to", Vector3.zero, "time", 0.3f, "onupdate", "AnimateLine"));
		}

		public void Kill()
		{
			GameObject.Destroy(this.gameObject);	
		}
		
		public void AnimateAlpha(float alpha)
		{
			SetAlpha(alpha);
		}

		private void SetAlpha(float alpha)
		{
			if(lineMiddle != null && handles != null)
			{
				lineMiddle.SetAlpha(alpha);
				handles[0].SetAlpha(alpha);
				handles[1].SetAlpha(alpha);
			}
		}

		public void AnimateAllColours(float value)
		{
			SetAllColours(value);
		}
		
		private void SetAllColours(float value)
		{
			if(lineMiddle != null && handles != null)
			{
				lineMiddle.SetAllColours(value);
				handles[0].SetAllColours(value);
				handles[1].SetAllColours(value);
			}
		}

		
		public void AnimateLine(Vector3 v)
		{
			transform.position += (Vector3)v;
		}

		public void GrabHandle(string handleName)
		{
			if(handles[0].Name == handleName)
				grabbedHandle = 0;
			else
				grabbedHandle = 1;	
		}

		public void StartDragging()
		{
			isDraggingLine = true;
			grabbedHandle = -1;
		}

		public void StopDragging()
		{
			isDraggingLine = false;
			grabbedHandle = -1;
			isCreatingLine = false;
			previousDragPosition = Vector3.zero;
			previousDragAmount = Vector3.zero;
			previousRotationAmount = 0;
		}

		public bool IsLongEnough()
		{
			return Vector3.Distance(handles[0].Position, handles[1].Position) > MIN_LINE_LENGTH;	
		}

		public bool IsCreatingLine()
		{
			return isCreatingLine;
		}

		public bool IsBeingDragged()
		{
			return isDraggingLine;	
		}

		public void OnLineManipulationStarted()
		{
			// This is suppose to give the user some visual feedback that they have tapped a line (it makes the line lighter briefly)
			if(!isCreatingLine && (IsBeingDragged() || HasGrabbedHandle()))
				iTween.ValueTo(gameObject, iTween.Hash("from", 0.5f, "to", 0f, "time", 0.8f, "onupdate", "AnimateAllColours", "easetype", "easeOutExpo"));
		}

		public void LineManipulationFinished()
		{
			SetBallsCanCollide();
		}

		void OnEnable()
		{
			Messenger.AddListener(Events.LineManipulationStarted, OnLineManipulationStarted);
			Messenger.AddListener(Events.LineManipulationFinished, LineManipulationFinished);
		}
		
		void OnDisable()
		{
			Messenger.RemoveListener(Events.LineManipulationStarted, OnLineManipulationStarted);
			Messenger.RemoveListener(Events.LineManipulationFinished, LineManipulationFinished);
		}




		private bool HasGrabbedHandle()
		{
			return grabbedHandle == 0 || grabbedHandle == 1;
		}

		void RotateLine (float x, float y)
		{
			var handleTapPosition = new Vector3 (x, y, transform.position.z);
			var mouseDirection = handleTapPosition - GetRotatingHandle().Position;
			var lineDirection = GetGrabbedHandle().Position - GetRotatingHandle().Position;
			float currentAngle = AngleHelper.AngleFromVertical(lineDirection);
			float desiredAngle = AngleHelper.AngleFromVertical(mouseDirection);
			// Values minused must stay in that order otherwise shit goes crazy yo
			float rotateAmount = currentAngle - desiredAngle;

			// Positive angle is anti-clockwise			
			if (rotateAmount != 0)
				transform.RotateAround(GetRotatingHandle().Position, Vector3.forward, rotateAmount);

			previousRotationAmount = rotateAmount;

			// Update scale
			if (lineMiddle.Stretch(GetRotatingHandle().Position, handleTapPosition))
				GetGrabbedHandle().Position = handleTapPosition;
		}

		private void DragLine(float x, float y)
		{
			// You can't set the line position to the hit point because the
			// line will jump. Instead apply the mouse movement to the line
			if(previousDragPosition == Vector3.zero)
			{
				previousDragPosition = new Vector3(x, y, transform.position.z);	
				return;
			}

			Vector3 dragPosition = new Vector3(x, y, transform.position.z);
			Vector3 difference = dragPosition - previousDragPosition;
			previousDragPosition = dragPosition;
			
			// Apply the same transformation to the parent so the collider moves
			transform.position = transform.position + difference;
			previousDragAmount = difference;
		}

		private const float ROTATION_MOVE_TOLERANCE_DEGREES = 1f;
		private const float DRAG_MOVE_TOLERANCE_GAME_UNITS = 0.1f;
		private bool CannotCollide()
		{
			bool isMoving = Math.Abs(previousRotationAmount) > ROTATION_MOVE_TOLERANCE_DEGREES
				|| Math.Abs(previousDragAmount.magnitude) > DRAG_MOVE_TOLERANCE_GAME_UNITS;

			// This ensures there's a delay between line movement stopping and
			// the line between collideable/visible again. Without this there's a horrible flicker
			if(nextStopTime == 0 || isMoving)
				nextStopTime = Time.time + POST_MOVE_CANNOT_COLLIDE_TIME;

			return isMoving || Time.time < nextStopTime || AreAnyObjectsCollidingWithTrigger();
		}

		private bool AreAnyObjectsCollidingWithTrigger()
		{
			if(lineMiddle != null && handles != null)
			{
				return lineMiddle.AreAnyObjectsCollidingWithTrigger()
					|| handles[0].AreAnyObjectsCollidingWithTrigger()
					|| handles[1].AreAnyObjectsCollidingWithTrigger();
			}

			return false;
		}

		private void SetBallsCanCollide()
		{
			if(lineMiddle != null && handles != null)
			{
				lineMiddle.SetBallsCanCollide();
				handles[0].SetBallsCanCollide();
				handles[1].SetBallsCanCollide();
				SetAlpha(1f);
			}
		}
		
		private void SetBallsWillNotCollide()
		{
			if(lineMiddle != null && handles != null)
			{
				lineMiddle.SetBallsWillNotCollide();
				handles[0].SetBallsWillNotCollide();
				handles[1].SetBallsWillNotCollide();
				SetAlpha(0.5f);
			}
		}
	}
}