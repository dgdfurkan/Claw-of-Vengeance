using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace GunduzDev
{
	public class LevelManager : MonoSingleton<LevelManager>
	{
		private Level level => Resources.Load<Level>("Level/Level");

		private int levelCount => level.GameValues.Count;

		private int currentLevel => PlayerPrefs.GetInt("CurrentLevel", 0);
		public GameValue CurrentLevelData => level.GameValues[currentLevel];


		[Header("Objects")]
		[SerializeField] public GameObject BirdObject;
		[SerializeField] public GameObject EnemyObject;
		[SerializeField] public GameObject ObstacleObject;

		[Space(5)]
		[Header("Objects Parents")]
		[SerializeField] private CinemachineTargetGroup targetGroup;
		[SerializeField] private Transform enemiesParent;
		[SerializeField] private Transform obstaclesParent;

        void OnEnable()  
        {
			//Debug.Log(CurrentLevelData.LevelID + "- " + CurrentLevelData.LevelName);
			LoadCurrentLevelValues();
		}

		public void NextLevel()
        {
			StartCoroutine(NextLevelSlowly());
        }

		IEnumerator NextLevelSlowly()
        {
			PlayerPrefs.SetInt("CurrentLevel", currentLevel + 1);
			DestroyOldObjects();
			yield return new WaitForSecondsRealtime(.4f);
			LoadCurrentLevelValues();
		}

		private void DestroyOldObjects()
        {
			foreach (var item in GameManager.Instance.CreatedBirds)	Destroy(item);
			foreach (var item in GameManager.Instance.CreatedEnemies) Destroy(item);
			foreach (var item in GameManager.Instance.CreatedObstacles)	Destroy(item);
		}

		void LoadCurrentLevelValues()
        {
			CreateGameValues();
			CreateBird();
			CreateEnemy();
			CreateObstacle();
		}

		void CreateGameValues()
        {
			GameManager.Instance.SetupGameValues(CurrentLevelData.MultipleValue, CurrentLevelData.MaxDragValue);
			UIManager.Instance.SetupTexts(CurrentLevelData.LevelID, CurrentLevelData.LevelName, CurrentLevelData.EnemyValuesList.Count);
        }

		void CreateBird()
        {
            foreach (var bird in CurrentLevelData.BirdValuesList)
            {
                GameObject CreatedBird = Instantiate(BirdObject, new Vector3(bird.StartPosition.x, bird.StartPosition.y, 0), Quaternion.identity);
				CreatedBird.GetComponent<Bird>().SetupBird(bird.BirdSprite, bird.BirdAnimator,bird.StartPosition);
				targetGroup.AddMember(CreatedBird.transform, 1, 2);
				GameManager.Instance.CreatedBirds.Add(CreatedBird);
			}
        }

		void CreateEnemy()
		{
			foreach (var enemy in CurrentLevelData.EnemyValuesList)
			{
				GameObject CreatedEnemy = Instantiate(EnemyObject, new Vector3(enemy.EnemyPositions.x, enemy.EnemyPositions.y, 0), Quaternion.identity);
				CreatedEnemy.transform.SetParent(enemiesParent);
				CreatedEnemy.GetComponent<Enemy>().SetupEnemy(enemy.EnemySprite, enemy.EnemyDeadSprite, enemy.EnemyPositions);
				targetGroup.AddMember(CreatedEnemy.transform, 1, 5);
				GameManager.Instance.CreatedEnemies.Add(CreatedEnemy);
			}
		}

		void CreateObstacle()
		{
			foreach (var obstacle in CurrentLevelData.ObstacleValuesList)
			{
				GameObject CreatedObstacle = Instantiate(ObstacleObject, new Vector3(obstacle.ObstaclePositions.x, obstacle.ObstaclePositions.y, 0), Quaternion.identity);
				CreatedObstacle.transform.SetParent(obstaclesParent);
				CreatedObstacle.GetComponent<Obstacle>().SetupObstacle(obstacle.ObstacleSprite, obstacle.ObstaclePositions);
				GameManager.Instance.CreatedObstacles.Add(CreatedObstacle);
			}
		}
	}
}
