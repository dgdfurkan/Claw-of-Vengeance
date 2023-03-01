using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GunduzDev
{
	[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Objects/Level")]
	public class Level : ScriptableObject
	{
		[SerializeField]
		public List<GameValue> GameValues;
	}

	[Serializable]
	public class GameValue
    {
		[Header("Game Informations")]
		public string LevelName;
		[SerializeField]
		public int LevelID;
		[SerializeField]
		public float MultipleValue;
		[SerializeField]
		public float MaxDragValue;

		[Space(10)]
		public List<BirdValue> BirdValuesList = new List<BirdValue>();
		[Space(10)]
		public List<EnemyValue> EnemyValuesList = new List<EnemyValue>();
		[Space(10)]
		public List<ObstacleValues> ObstacleValuesList = new List<ObstacleValues>();

		//[Header("Game Informations")]
		//[SerializeField] public string LevelName;
		//[SerializeField] public float MultipleValue;
		//[SerializeField] public float MaxDragValue;
	}

	[Serializable]
	public class BirdValue
    {
		[Header("Bird Informations")]
		public Sprite BirdSprite;
		public RuntimeAnimatorController BirdAnimator;
		public Vector2 StartPosition;
	}

	[Serializable]
	public class EnemyValue
	{
		[Header("Enemy Informations")]
		public Sprite EnemySprite;
		public Sprite EnemyDeadSprite;
		//public RuntimeAnimatorController EnemyAnimator;
		public Vector2 EnemyPositions;
	}

	[Serializable]
	public class ObstacleValues
	{
		[Header("Obstacle Informations")]
		public Sprite ObstacleSprite;
		public Vector2 ObstaclePositions;
	}
}
