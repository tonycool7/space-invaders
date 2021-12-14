using UnityEngine;

public class Projectile : MonoBehaviour {
    public bool isEnemy;
    public float speed;
    public System.Action _playerLaserDestroyed;

    // in the update func, we use the isEnemy boolean
    // variable to change the direction of the projectile 
    void Update()
    {
        float direction = isEnemy ? -1f : 1f;

        transform.Translate(new Vector3(0, speed * direction * Time.deltaTime, 0));
    }

    //we destroy the projectile when it collides with any onbject,
    //there are exceptions.. depending on the layer the obj is on
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(_playerLaserDestroyed != null)
        {
            this._playerLaserDestroyed.Invoke();
        }
        Destroy(gameObject);
    }
}

