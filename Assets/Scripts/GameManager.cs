using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float _score = 0;

    [SerializeField] private Text _scoreText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IncreaseScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseScore(float byAmount)
    {
        _score += byAmount;
        _scoreText.text = "Score: " + _score;
    }
}
