using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GunduzDev
{
	public class Obstacle : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer spriteRenderer;
		public void SetupObstacle(Sprite sprite, Vector2 vector2)
		{
			spriteRenderer.sprite = sprite;
			transform.position = vector2; 
		}
	}
}
