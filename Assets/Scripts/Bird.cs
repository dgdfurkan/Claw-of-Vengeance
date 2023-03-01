using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GunduzDev
{
    public class Bird : MonoSingleton<Bird>
    {
        [SerializeField] private Rigidbody2D rigidbody2D;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Animator animator;

        [HideInInspector]
        public Vector2 startPosition;
        private float multipleValue => GameManager.Instance.multipleValue;
        private float maxDragValue;

        public BirdStatus currentBirdStatus;
        public enum BirdStatus { Ready, Flying, Collisioned}

        public void SetupBird(Sprite sprite, RuntimeAnimatorController animatorController, Vector2 vector2)
        {
            //multipleValue = GameManager.Instance.multipleValue;
            maxDragValue = GameManager.Instance.maxDragValue;

            spriteRenderer.sprite = sprite;
            animator.runtimeAnimatorController = animatorController;
            startPosition = vector2;
        }

        void Start()
        {
            ResetBird();
        }

        private void ResetBird()
        {
            currentBirdStatus = BirdStatus.Ready;
            rigidbody2D.position = startPosition;
            rigidbody2D.isKinematic = true;
            rigidbody2D.velocity = Vector2.zero;
        }

        private void OnMouseDown()
        {
            if (currentBirdStatus != BirdStatus.Ready) return;
        }

        private void OnMouseDrag()
        {
            if (currentBirdStatus != BirdStatus.Ready) return;

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 desiredPosition = mousePosition;

            float distance = Vector2.Distance(desiredPosition, startPosition);

            if(distance > maxDragValue)
            {
                Vector2 direction = desiredPosition - startPosition;
                direction.Normalize();
                desiredPosition = startPosition + (direction * maxDragValue);
            }

            if (desiredPosition.x > startPosition.x) desiredPosition.x = startPosition.x;

            rigidbody2D.position = desiredPosition;
        }

        private void OnMouseUp()
        {
            if (currentBirdStatus != BirdStatus.Ready) return;

            currentBirdStatus = BirdStatus.Flying;

            Vector2 currentPosition = rigidbody2D.position;
            Vector2 direction = startPosition - currentPosition;
            direction.Normalize();

            rigidbody2D.isKinematic = false;
            rigidbody2D.AddForce(direction * multipleValue);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (currentBirdStatus == BirdStatus.Collisioned) return;
            currentBirdStatus = BirdStatus.Collisioned;
            StartCoroutine(ResetingBird(3f));
            //Invoke("ResetBird", 3f);
        }

        IEnumerator ResetingBird(float time)
        {
            yield return new WaitForSecondsRealtime(time);
            ResetBird();
            //if (!GameManager.Instance.AllMonstersAreDied())
            //{
            //    Invoke("CheckMonsters", 1f);
            //}

            //else
            //{
            //    Debug.Log("Win!"); // Level 2 ye geç iþte
            //}
        }

        void CheckMonsters()
        {
            if (GameManager.Instance.AllMonstersAreDied()) Debug.Log("Win!"); // Level 2 ye geç iþte
        }
    }
}
