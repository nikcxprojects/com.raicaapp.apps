using UnityEngine;

public class RoadMap : MonoBehaviour
{
    [SerializeField] float speed;

    private void Update()
    {
        foreach(Transform t in transform)
        {
            t.Translate(speed * Time.deltaTime * Vector2.down);
            if(t.position.y < -23.0f)
            {
                var last = transform.GetChild(transform.childCount - 1).position;
                var newPos = new Vector2(0, last.y + 43.9f);

                t.position = newPos;
                t.SetAsLastSibling();
            }
        }
    }
}
