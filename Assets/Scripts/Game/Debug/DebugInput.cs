using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System;

namespace Ph.Bouncer
{
	public class DebugInput : MonoBehaviour 
	{
		public UITextList TextList;
		private UIInput input;
		public GUIText DebugText;

		void Start() 
		{
			input = GetComponent<UIInput>();
		}
		
		public void OnSubmit()
		{
			if (TextList == null)
				return;

			// It's a good idea to strip out all symbols as we don't want user input to alter colors, add new lines, etc
			string text = NGUIText.StripSymbols(input.value);
			
			if (!string.IsNullOrEmpty(text))
			{
				DebugText.text = "";
				Debug.Log(text);
				TextList.Add(text);
				RunCommand(text);
				input.value = "";
				input.isSelected = false;
			}
		}

		// f, forward
		// b, back
		// l[0-9], load[0-9]
		private void RunCommand(string text)
		{
			if(string.IsNullOrEmpty(text))
				return;

	
			text = text.Trim().ToLower();
			
			Regex commandNumberPattern = new Regex("([a-z]+)([0-9]*)"); 
			
			if(commandNumberPattern.IsMatch(text))
			{
				Match match = commandNumberPattern.Match(text);
				string command = match.Groups[1].Value;
				int commandNumber = 0;
				
				if(match.Groups.Count > 2) 
					int.TryParse(match.Groups[2].Value, out commandNumber);
				
				if(command == "f" || command == "foward")
				{
					if(commandNumber == 0) 
					   LevelLoadHelper.NextLevel();
					else
						LevelLoadHelper.Load(CurrentLevel.GetNumber() + commandNumber);
				}
				else if(command == "b" || command == "back")
				{
					if(commandNumber == 0) 
						LevelLoadHelper.Load(CurrentLevel.GetNumber() - 1);
					else
						LevelLoadHelper.Load(CurrentLevel.GetNumber() - commandNumber);
				}
				else if((command == "l" || command == "load") && commandNumber > 0)
				{
					LevelLoadHelper.Load(commandNumber);
				}
				else if(command == "bk" && commandNumber > 0 && commandNumber < 5)
				{
					var changeBackground = (ChangeBackground)FindObjectOfType(typeof(ChangeBackground));
					changeBackground.SetBackground(commandNumber);
				}
				else if(command == "c")
				{
					Messenger.Broadcast(Events.CloseDebugMenu);
				}
				else if(command == "restest")
				{
					LevelLoadHelper.LoadResolutionTest();
				}
				else if(command == "res")
				{
					string resolutionText = string.Format("Width: {0}, height: {1}, DPI: {2}", Screen.width, Screen.height, Screen.dpi);
					Debug.Log(resolutionText);
					DebugText.text = resolutionText;
				}
			}

		}
	}
}