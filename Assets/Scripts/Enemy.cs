using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GunduzDev
{
    [SelectionBase]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite deadSprite;
        [SerializeField] private ParticleSystem particleSystem;

        private bool _hasDied = false;

        public void SetupEnemy(Sprite sprite, Sprite deadSprite, Vector2 vector2)
        {
            spriteRenderer.sprite = sprite;
            this.deadSprite = deadSprite;
            transform.position = vector2;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (ShouldDie(collision)) StartCoroutine(Die());
        }

        IEnumerator Die()
        {
            _hasDied = true;
            spriteRenderer.sprite = deadSprite;
            particleSystem.Play();

            yield return new WaitForSecondsRealtime(1.2f);
            Destroy(gameObject);
        }

        private bool ShouldDie(Collision2D collision)
        {
            if (_hasDied) return false;

            Bird bird = collision.gameObject.GetComponent<Bird>();
            if (bird != null) return true;

            if(collision.contacts[0].normal.y < .5f)
            {
                return true;
            }

            return false;
        }

        void OnDisable()
        {
            GameManager.Instance.CreatedEnemies.Remove(gameObject);
            UIManager.Instance.UpdateEnemyCount(GameManager.Instance.CreatedEnemies.Count);

            if (GameManager.Instance.AllMonstersAreDied())
            {
                Debug.Log("Win!");
                LevelManager.Instance.NextLevel(); // Level 2 ye geç iþte
            }
        }
    }
}
