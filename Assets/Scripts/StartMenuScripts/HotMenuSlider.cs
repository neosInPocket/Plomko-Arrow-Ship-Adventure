using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Vector2 = UnityEngine.Vector2;

public class HotMenuSlider : MonoBehaviour
{
	[SerializeField] private Transform content;
	[FormerlySerializedAs("pointer")] [SerializeField] private Transform handler;
	[FormerlySerializedAs("fadeTime")] [SerializeField] private float animationTime;

	[SerializeField] private Transform settingsIcon;
	[SerializeField] private Transform startIcon;
	[SerializeField] private Transform storeIcon;
	
	[SerializeField] private float cDistributionConstant;
	[SerializeField] private float maxIconSize;
	private const float SettingsDestination = -1242f;
	private const float MenuDestination = 0;
	private const float StoreDestination = 1242f;
	private float currentSpeed;
	private float destination;
	private int direction;

	private const float PointerSettingsDestination = 414f;
	private const float PointerMenuDestination = 0;
	private const float PointerStoreDestination = -414f;
	private float pointerDestination;
	private float currentPointerVelocity;

	private void Start()
	{
		var scale = new Vector3(maxIconSize, maxIconSize, 1);
		startIcon.localScale = scale;
	}

	private void SetFade(float goal, float pointerGoal)
	{
		StopAllCoroutines();
		destination = goal - content.localPosition.x;
		pointerDestination = pointerGoal - handler.transform.localPosition.x;
		
		currentSpeed = Mathf.Abs(destination) / animationTime;
		currentPointerVelocity = Mathf.Abs(pointerDestination) / animationTime;
		direction = (int)(destination / Mathf.Abs(destination));
		
		StartCoroutine(Animate(goal, pointerGoal));
	}
	
	public void StoreAnimation()
	{
		SetFade(StoreDestination, PointerStoreDestination);
	}
	
	public void MenuAnimation()
	{
		SetFade(MenuDestination, PointerMenuDestination);
	}
	
	public void SettingsAnimation()
	{
		SetFade(SettingsDestination, PointerSettingsDestination);
	}
	
	private IEnumerator Animate(float xDestination, float handlerDestination)
	{
		if (direction == -1)
		{
			while (content.localPosition.x > xDestination)
			{
				var localPosition = content.transform.localPosition;
				float currentDistance = Mathf.Abs(xDestination - localPosition.x);
				var newX = localPosition.x - currentSpeed * (currentDistance + 40) / 1242;
				localPosition = new Vector2(newX, localPosition.y);
				content.transform.localPosition = localPosition;

				var position = handler.transform.localPosition;
				float currentPointerDistance = Mathf.Abs(handlerDestination - position.x);
				var newPointerX = position.x + currentPointerVelocity * (currentPointerDistance + 13.333333f) / 414f;
				position = new Vector2(newPointerX, position.y);
				handler.transform.localPosition = position;
				
				CalculateIconSize(storeIcon, PointerStoreDestination);
				CalculateIconSize(startIcon, PointerMenuDestination);
				CalculateIconSize(settingsIcon, PointerSettingsDestination);
				yield return new WaitForFixedUpdate();
			}
		}
		else
		{
			while (content.transform.localPosition.x < xDestination)
			{
				var transform1 = content.transform;
				var localPosition = transform1.localPosition;
				float currentDistance = xDestination - localPosition.x;
				var newX = localPosition.x + currentSpeed * (currentDistance + 40) / 1242;
				localPosition = new Vector2(newX, localPosition.y);
				transform1.localPosition = localPosition;

				var position = handler.transform.localPosition;
				float currentPointerDistance = Mathf.Abs(handlerDestination - position.x);
				var newPointerX = position.x - currentPointerVelocity * (currentPointerDistance + 13.333333f) / 414f;
				position = new Vector2(newPointerX, position.y);
				handler.transform.localPosition = position;
				
				CalculateIconSize(storeIcon, PointerStoreDestination);
				CalculateIconSize(startIcon, PointerMenuDestination);
				CalculateIconSize(settingsIcon, PointerSettingsDestination);
				yield return new WaitForFixedUpdate();
			}
		}

		var transform2 = content.transform;
		transform2.localPosition = new Vector2(xDestination, transform2.localPosition.y);
	}

	private void CalculateIconSize(Transform icon, float xPos)
	{
		float scale = (maxIconSize - 1) * (float)Math.Exp(-Mathf.Pow(handler.localPosition.x - xPos, 2) / cDistributionConstant) + 1;
		icon.localScale = new Vector3(scale, scale, icon.localScale.z);
	}
}
