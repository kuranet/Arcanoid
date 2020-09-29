using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            Vector2 touchPosition = touch.position;
            touchPosition = Camera.main.ScreenToWorldPoint(touchPosition);

            Vector2 target = new Vector2(touchPosition.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }        
    }
}
