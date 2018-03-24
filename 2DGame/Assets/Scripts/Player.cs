using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour {
    [SerializeField]
    private float max = 4.6f, speed, slowFactor, waitDeath;

    private Rigidbody2D rb;

    [SerializeField]
    private KeyCode up, down;

    [SerializeField]
    private TextMeshProUGUI scoreText, highScoreText;

    [SerializeField]
    private GameObject highScore;

    public bool PressingUp, pressingDown;

    public AudioClip ac;

    // Use this for initialization
    void Start () {
        Time.timeScale = 1f;
        rb = GetComponent<Rigidbody2D>();

        PressingUp = false;
        pressingDown = false;

        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
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

        if (PressingUp && transform.position.y < max && Time.timeScale != slowFactor && !pressingDown)
            rb.velocity = Vector2.up * speed;
        else if (pressingDown && transform.position.y > -max && Time.timeScale != slowFactor && !PressingUp)
            rb.velocity = Vector2.up * -speed;
        else
            if (Time.timeScale != slowFactor)
            rb.velocity = Vector2.zero;

        if (transform.position.y > max)
        {
            transform.position = new Vector3(transform.position.x, max, transform.position.z);
        }
        else if(transform.position.y < -max)
        {
            transform.position = new Vector3(transform.position.x, -max, transform.position.z);
        }
    }

    public void OnPressUp()
    {
        PressingUp = !PressingUp;
    }

    public void OnPressDown()
    {
        pressingDown = !pressingDown;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Time.timeScale = slowFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
        // set high score if needed
        int scoreInt = System.Convert.ToInt32(scoreText.text.Substring(7, scoreText.text.Length - 7)); // adding score
        if (scoreInt > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", scoreInt);
            highScoreText.text = "High Score: " + scoreInt;
        }

        highScore.SetActive(true);

        StartCoroutine(WaitForSlowMo());
    }
    
    private IEnumerator WaitForSlowMo()
    {
        yield return new WaitForSeconds(waitDeath * slowFactor);
        SceneManager.LoadScene(1);
    }
}
