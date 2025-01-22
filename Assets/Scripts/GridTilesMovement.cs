using UnityEngine;

public class GridTilesMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _foreground;
    [SerializeField] private GameObject _background;
    private Vector3 _foregroundStartPos;
    private float _foregroundWidth;
    private Vector3 _backgroundStartPos;
    private float _backgroundWidth;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _foregroundStartPos = _foreground.transform.position;
        _foregroundWidth = _foreground.GetComponent<BoxCollider2D>().size.x / 2;
        
        _backgroundStartPos = _background.transform.position;
        _backgroundWidth = _background.GetComponent<BoxCollider2D>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.IsGameOver) return;
        
        float speed = _speed + GameManager.Instance.WorldSpeed;
        
        _foreground.transform.Translate(Vector3.left * (speed * Time.deltaTime));
        if (_foreground.transform.position.x < _foregroundStartPos.x - _foregroundWidth)
        { 
            _foreground.transform.position = _foregroundStartPos;
        }
        
        _background.transform.Translate(Vector3.left * ((speed / 2) * Time.deltaTime));
        if (_background.transform.position.x < _backgroundStartPos.x - _backgroundWidth)
        { 
            _background.transform.position = _backgroundStartPos;
        }
    }
}
