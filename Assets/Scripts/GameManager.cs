using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GunduzDev
{
	public class GameManager : MonoSingleton<GameManager>
	{
        public GameStatus currentGameStatus;
        public enum GameStatus { Started, Paused, Finished }

        [HideInInspector]
        public float multipleValue = 750f;
        [HideInInspector]
        public float maxDragValue = 3.5f;

        [HideInInspector]
        public List<GameObject> CreatedBirds = new List<GameObject>();
        [HideInInspector]
        public List<GameObject> CreatedEnemies = new List<GameObject>();
        [HideInInspector]
        public List<GameObject> CreatedObstacles = new List<GameObject>();

        public void SetupGameValues(float multiple, float maxDarg)
        {
            multipleValue = multiple;
            maxDragValue = maxDarg;
        }

        public bool AllMonstersAreDied()
        {
            int value = CreatedEnemies.Count;
            if (value != 0) return false;
            return true;
        }
    }
}
