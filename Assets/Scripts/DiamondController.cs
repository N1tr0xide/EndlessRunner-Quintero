using UnityEngine;

public class DiamondController : MonoBehaviour
{
    public float Score;
    [SerializeField] private float _speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * (_speed * Time.deltaTime));
    }
}
