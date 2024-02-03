using UnityEngine;

public class FireflyMovement : MonoBehaviour
{
    [SerializeField] private float circleRadius = 2f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float fastMoveSpeed = 4f;
    [SerializeField] private float playerResponseDistance = 3.5f;

    private float _angle;
    private Vector2 _center;
    private Vector2 _movePosition;
    private float _moveSpeedNow;

    private void Start()
    {
        _moveSpeedNow = moveSpeed;
        _movePosition = transform.localPosition;
        _center = transform.localPosition;
    }

    private void Update()
    {
        if (_movePosition == (Vector2)transform.localPosition)
        {
            float randomAngle = Random.Range(0f, Mathf.PI * 2);
            float randomRadius = Random.Range(0f, circleRadius);
            
            float x = _center.x + randomRadius * Mathf.Cos(randomAngle);
            float y = _center.y + randomRadius * Mathf.Sin(randomAngle);
            
            _movePosition = new Vector2(x, y);
        }

        _moveSpeedNow = Vector2.Distance(PlayerControl.Instance.transform.position,
            transform.position) < playerResponseDistance ? fastMoveSpeed : moveSpeed;
        
        transform.localPosition = Vector2.MoveTowards(transform.localPosition, 
            _movePosition, _moveSpeedNow * Time.deltaTime);
    }
}