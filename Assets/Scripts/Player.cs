using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool _laserActive = false;

    public Projectile projectile;
    public GameObject shoot_effect;
    public GameObject hit_effect;

    // Update is called once per frame
    void Update()
    {
        Movement();
        FireProjectile();
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < 6.6) transform.Translate(new Vector3(5 * Time.deltaTime, 0, 0));

        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -6.6) transform.Translate(new Vector3(-5 * Time.deltaTime, 0, 0));

        if (Input.GetKey(KeyCode.UpArrow) && transform.position.y < 6) transform.Translate(new Vector3(0, 5 * Time.deltaTime, 0));

        if (Input.GetKey(KeyCode.DownArrow) && transform.position.y > -6) transform.Translate(new Vector3(0, -5 * Time.deltaTime, 0));

    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void FireProjectile()
    {
        if((Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0) ) && !_laserActive)
        {
            float x = transform.position.x;
            float y = transform.position.y;

            Instantiate(shoot_effect, new Vector3(x, y + .45f, 0), Quaternion.identity);
            Projectile lazer = Instantiate(projectile, new Vector3(x, y + .5f, 0), Quaternion.identity);

            _laserActive = true;
            lazer._playerLaserDestroyed += LazerDestroyed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "missile" || collision.gameObject.tag == "enemy")
        {
            gameObject.GetComponent<AudioSource>().Play();
            Instantiate(hit_effect, gameObject.transform.position, Quaternion.identity);
            ReloadScene();
        }
    }
    void LazerDestroyed()
    {
        _laserActive = false;
    }
}
