using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour {
    [SerializeField]
    private float max = 4.6f, speed, slowFactor, waitDeath;

    private Rigidbody2D rb;

    /*[SerializeField]
    private KeyCode up, down;*/

    [SerializeField]
    private TextMeshProUGUI scoreText, highScoreText, posDebug;

    [SerializeField]
    private GameObject highScore, pauseButton;

    public bool PressingUp, pressingDown;

    private Vector3 lastPos;

    // Use this for initialization
    void Start () {
        Time.timeScale = 1f;
        rb = GetComponent<Rigidbody2D>();

        PressingUp = false;
        pressingDown = false;
        if (SceneManager.GetActiveScene().buildIndex == 1) // in normal game
        {
            highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScoreGame");
        }
        else
        {
            highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScoreStack");
        }
    }
	
	// Update is called once per frame
	void Update () {
        /*if (Input.GetKey(up) && transform.position.y < max && Time.timeScale != slowFactor && !Input.GetKey(down))
            rb.velocity = Vector2.up * speed;
        else if (Input.GetKey(down) && transform.position.y > -max && Time.timeScale != slowFactor && !Input.GetKey(up))
            rb.velocity = Vector2.up * -speed;
        else
            if (Time.timeScale != slowFactor)
                rb.velocity = Vector2.zero;
        */

        /*if (PressingUp && transform.position.y < max && Time.timeScale != slowFactor && !pressingDown)
            rb.velocity = Vector2.up * speed;
        else if (pressingDown && transform.position.y > -max && Time.timeScale != slowFactor && !PressingUp)
            rb.velocity = Vector2.up * -speed;
        
        if (Time.timeScale != slowFactor && !PressingUp && !pressingDown)
            rb.velocity = Vector2.zero;*/
        //posDebug.text = transform.position.x.ToString() + " | " + transform.position.y.ToString() + " | " + transform.position.z.ToString();
        if (Time.timeScale == 1)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                // Get movement of the finger since last frame
                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

                // Move object across XY plane
                transform.Translate(touchDeltaPosition.x * speed, touchDeltaPosition.y * speed, 0);
            }
            /*lastPos = transform.position;
            Vector3 mousePos = Input.mousePosition;
            Vector3 wantedPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));
            transform.position = wantedPos;
            if(Vector3.Distance(wantedPos, lastPos) > 0.5f)
            {
                Debug.Log(Vector3.Distance(wantedPos, lastPos));
            }*/
        }

        /*if (transform.position.y > max)
        {
            transform.position = new Vector3(transform.position.x, max, transform.position.z);
        }
        else if(transform.position.y < -max)
        {
            transform.position = new Vector3(transform.position.x, -max, transform.position.z);
        }*/
    }

    public void OnPressUp(bool newUp)
    {
        PressingUp = newUp;
        if (PressingUp && transform.position.y < max && Time.timeScale != slowFactor && !pressingDown)
            rb.velocity = Vector2.up * speed;
        else if (Time.timeScale != slowFactor)
            rb.velocity = Vector2.zero;
    }

    public void OnPressDown(bool newDown)
    {
        pressingDown = newDown;
        if (pressingDown && transform.position.y > -max && Time.timeScale != slowFactor && !PressingUp)
            rb.velocity = Vector2.up * -speed;
        else if (Time.timeScale != slowFactor)
            rb.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        ReceivedDamage();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        ReceivedDamage();
    }

    private void ReceivedDamage()
    {
        Time.timeScale = slowFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;

        // set high score if needed
        int scoreInt = System.Convert.ToInt32(scoreText.text.Substring(7, scoreText.text.Length - 7)); // adding score

        if (SceneManager.GetActiveScene().buildIndex == 1) // in normal game
        {
            if (scoreInt > PlayerPrefs.GetInt("HighScoreGame"))
            {
                PlayerPrefs.SetInt("HighScoreGame", scoreInt);
                highScoreText.text = "High Score: " + scoreInt;
            }
        }
        else // in stack game
        {
            if (scoreInt > PlayerPrefs.GetInt("HighScoreStack"))
            {
                PlayerPrefs.SetInt("HighScoreStack", scoreInt);
                highScoreText.text = "High Score: " + scoreInt;
            }
        }

        highScore.SetActive(true);

        pauseButton.SetActive(false);

        StartCoroutine(WaitForSlowMo());
    }

    private IEnumerator WaitForSlowMo()
    {
        yield return new WaitForSeconds(waitDeath * slowFactor);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
