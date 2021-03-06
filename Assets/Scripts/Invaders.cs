using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Invaders : MonoBehaviour
{
    private Vector3 _direction = Vector3.right;

    public Text killCountText;
    public Projectile missile;
    public AnimationCurve speed;
    public Invader[] invaders;
    public int columns = 10;
    public float padding = 1f;
    public float missileAttackRate = 1f;

    public int rows => this.invaders.Length;
    public int numberKilled { get; private set; }
    public int totatInvaders => rows * this.columns;
    public int numberAlive => this.totatInvaders - this.numberKilled;

    public float percentKilled => (float)this.numberKilled / (float)this.totatInvaders;

    //this function creates enemy ships on in the scene
    private void Awake()
    {
        for(int i = 0; i < rows; i++)
        {
            float width = padding * (columns - 1);
            float heigth = padding * (rows - 1);
            Vector2 centering = new Vector2(-width / 1.3f, heigth / 1.5f);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + (i * padding), 0);

            for(int j = 0; j < columns; j++)
            {
                Invader invader = Instantiate(this.invaders[i], transform);
                invader.killed += InvaderKilled;
                invader.enemyHitBoundary += ReloadScene;
                rowPosition.x += padding;
                invader.transform.position = rowPosition;
            }
        }
    }

    // Start is called before the first frame update
    // we loop the shooting action of the enemy
    void Start()
    {
        InvokeRepeating(nameof(MissileAttack), missileAttackRate, missileAttackRate);
    }

    // Update is called once per frame
    // on every new frame, we move the enemy fleet from left to right, and right to left
    // and also increase the speed of the enemy with respect to the number of enemy ships available
    void Update()
    {
        transform.position += _direction * speed.Evaluate(this.percentKilled) * Time.deltaTime;

        Vector3 screenLeftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 screenRightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform invader in transform)
        {
            if (!invader.gameObject.activeInHierarchy) continue;

            if (_direction == Vector3.right && invader.position.x >= (screenRightEdge.x - 0.4f)) MoveToNextRow();

            else if (_direction == Vector3.left && invader.position.x <= (screenLeftEdge.x + 0.4f)) MoveToNextRow();
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void InvaderKilled()
    {
        numberKilled++;
        killCountText.text = " " + numberKilled;
        if (numberKilled >= totatInvaders) ReloadScene();
    }

    void MoveToNextRow()
    {
        _direction.x *= -1.0f;
        Vector3 position = transform.position;
        position.y -= 0.3f;
        this.transform.position = position;
    }

    void MissileAttack()
    {
        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy) continue;

            float rnNum = Random.value;

            if (rnNum < (1.0f / (float)this.numberAlive))
            {
                Instantiate(missile, invader.gameObject.transform.position, Quaternion.identity);

                break;
            }
        }
    }
}
