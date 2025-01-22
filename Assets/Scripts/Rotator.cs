using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 100f;

    // Update is called once per frame

    void Update()
    {
        if(GameManager.Instance.IsGameOver) return;
        transform.Rotate(0,0,_rotationSpeed * Time.deltaTime);
    }
}
