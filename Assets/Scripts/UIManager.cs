using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GunduzDev
{
	public class UIManager : MonoSingleton<UIManager>
	{
		[SerializeField] private TextMeshProUGUI levelID;
		[SerializeField] private TextMeshProUGUI levelName;
		[SerializeField] private TextMeshProUGUI enemyCount;

		public void SetupTexts(int id, string name, int enemy)
        {
			levelID.text = "Level: " + id;
			levelName.text = name;
			enemyCount.text = ": " + enemy;
        }

		public void UpdateEnemyCount(int value)
        {
			enemyCount.text = ": " + value;
        }
	}
}
