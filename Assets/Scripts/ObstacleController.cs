using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public float Score;
    [SerializeField] private float _initialSpeed;
    [SerializeField] private float _maxSpeed = 15;
    
    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.IsGameOver) return;
        float speed = Mathf.Clamp(GameManager.Instance.WorldSpeed + _initialSpeed, 1, _maxSpeed);
        transform.Translate(Vector3.left * (speed * Time.deltaTime));
    }
}
