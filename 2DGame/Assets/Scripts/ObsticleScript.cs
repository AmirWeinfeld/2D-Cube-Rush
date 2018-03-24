using UnityEngine;
using TMPro;

public class ObsticleScript : MonoBehaviour {
    [SerializeField]
    private float minLeft;

    [HideInInspector]
    public int maxTimePassedEnd;

    private float spawnX;

    private Rigidbody2D rb;

    [HideInInspector]
    public TextMeshProUGUI scoreText;

    public float rotSpeed, speed;

    private int timePassedEnd = 0; // hold the time this obj added score to the player

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spawnX = transform.position.x;
    }

    // Update is called once per frame
    void Update () {
        rb.velocity = Vector2.left * speed;
        transform.Rotate(0, 0, rotSpeed * Time.deltaTime);

        if (transform.position.x < minLeft)
        {
            if (scoreText)
            {
                int scoreInt = System.Convert.ToInt32(scoreText.text.Substring(7, scoreText.text.Length - 7)) + 1; // adding score
                scoreText.text = "Score: " + scoreInt;
            }

            if (timePassedEnd == maxTimePassedEnd) // Destroying the gameobject if the obj added max score
            {
                Destroy(gameObject);
            }

            transform.position = new Vector3(spawnX, transform.position.y, transform.position.z); // reseting obj pos
            timePassedEnd++;
        }
	}
}
