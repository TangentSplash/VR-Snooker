using UnityEngine;

public class Ball : MonoBehaviour
{
    SoundManager AudioManager;

    public Vector3 StartPos;
    public string Name;
    public int Score;
    public int no;

    private Rigidbody Rigidbody;
    public bool Stopped = false;
    private Vector2 Velocity;

    void Start()
    {
        Rigidbody=GetComponent<Rigidbody>();
        AudioManager = GameObject.Find("Audio Manager").GetComponent<SoundManager>();
        /*StartPos = transform.position;
        Name = gameObject.name.Split(' ')[0];
        switch (Name)
        {
            case "Red":
                Score = 1;
                break;
            case "Yellow":
                Score = 2;
                break;
            case "Green":
                Score = 3;
                break;
            case "Brown":
                Score = 4;
                break;
            case "Blue":
                Score = 5;
                break;
            case "Pink":
                Score = 6;
                break;
            case "Black":
                Score = 7;
                break;
            default:
                break;
        }*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 15 || collision.gameObject.name=="Cue")     //In balls Layer
        {
            Vector3 location;
            float speed = collision.relativeVelocity.magnitude;
            float num;
            string name;

            if (collision.gameObject.layer == 15)
            {
                if (speed <= 1)
                {
                    name = "Small";
                }
                else if (speed <= 5)
                {
                    name = "Mid";
                }
                else
                {
                    name = "Large";
                }
            }
            else
            {
                name = "Cue";
                AudioManager.PlayHaptic(0, 0.4f, 0.25f);
            }
            location = collision.GetContact(0).point;
            num = (Mathf.Round(Random.Range(1, 3)));
            name += num.ToString();
            AudioManager.PlaySound(name, location);
            }
    }

    public void Reset()
    {
        Debug.Log("Reset");
        Rigidbody.Sleep();
        transform.position = StartPos;
    }

    private void Update()
    {
        Velocity = new Vector2(Rigidbody.velocity.x, Rigidbody.velocity.z);
        Stopped = (Velocity.magnitude <= 0.01);
    }
}
