using UnityEngine;
using TMPro;

public class ObsticleScript : MonoBehaviour {
    [SerializeField]
    private float minLeft;
    
    public int maxTimePassedEnd;

    private float spawnX;

    private Rigidbody2D rb;
    
    public TextMeshProUGUI scoreText;

    public float rotSpeed, speed;

    private int timePassedEnd = 0; // hold the time this obj added score to the player

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spawnX = transform.position.x;
        rb.velocity = speed * Vector2.left;
    }

    public void Spawn()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
        rb.velocity = speed * Vector2.left;
    }

    // Update is called once per frame
    void Update () {
        // rb.velocity = Vector2.left * speed;
        transform.Rotate(0, 0, rotSpeed * Time.deltaTime);

        if (transform.position.x < minLeft && Time.timeScale == 1)
        {
            if (scoreText)
            {
                int scoreInt = System.Convert.ToInt32(scoreText.text.Substring(7, scoreText.text.Length - 7)) + 1; // adding score
                scoreText.text = "Score: " + scoreInt;
            }

            if (timePassedEnd == maxTimePassedEnd) // Destroying the gameobject if the obj added max score
            {
                PassedMaxTimes();
            }

            transform.position = new Vector3(spawnX, transform.position.y, transform.position.z); // reseting obj pos
            timePassedEnd++;
        }
	}

    private void PassedMaxTimes()
    {
        transform.position = new Vector3(spawnX, 0, 0);
        timePassedEnd = 0;
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}
