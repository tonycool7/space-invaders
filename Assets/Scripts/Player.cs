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
    // in the update func, we constantly want to listen to execute player movement or
    // projectile fire when certain keys are pressed
    void Update()
    {
        Movement();
        FireProjectile();
    }

    // handles player movement in 4 directions
    void Movement()
    {
        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < 6.6) transform.Translate(new Vector3(5 * Time.deltaTime, 0, 0));

        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -6.6) transform.Translate(new Vector3(-5 * Time.deltaTime, 0, 0));

        if (Input.GetKey(KeyCode.UpArrow) && transform.position.y < 6) transform.Translate(new Vector3(0, 5 * Time.deltaTime, 0));

        if (Input.GetKey(KeyCode.DownArrow) && transform.position.y > -6) transform.Translate(new Vector3(0, -5 * Time.deltaTime, 0));

    }

    // reload game scene
    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    // fire projectile when space bar is pressed or on the left mouse click
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

    //destroy player ship when a missile or enemy ship collides with it
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "missile" || collision.gameObject.tag == "enemy")
        {
            gameObject.GetComponent<AudioSource>().Play();
            Instantiate(hit_effect, gameObject.transform.position, Quaternion.identity);
            ReloadScene();
        }
    }


    // we call this function everytime we are notified a lazer has been destroyed,
    // this is to ensure the player can shoot only when the previous lazer has been destroyed, 
    // by using the flag variable _laserActive
    void LazerDestroyed()
    {
        _laserActive = false;
    }
}
