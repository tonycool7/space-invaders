using UnityEngine;

public class Projectile : MonoBehaviour {
    public bool isEnemy;
    public float speed;
    public System.Action _playerLaserDestroyed;

    void Update()
    {
        float direction = isEnemy ? -1f : 1f;

        transform.Translate(new Vector3(0, speed * direction * Time.deltaTime, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this._playerLaserDestroyed.Invoke();
        Destroy(gameObject);
    }
}

