using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector2 direction;
    [SerializeField] private Transform enermyDirection;

    private void Start()
    {
        this.enermyDirection = GameObject.Find("EnermyAnimation").GetComponent<Transform>();
        this.direction.x = enermyDirection.localScale.x * -1;
    }

    private void Update()
    {
        transform.Translate(this.direction * Time.deltaTime * this.speed);
    }

    public void DestroyBullet()
    {
        Destroy(gameObject, 3f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject, 0f);
        }
    }
}
