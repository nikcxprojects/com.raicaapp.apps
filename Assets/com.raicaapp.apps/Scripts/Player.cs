using UnityEngine;

public class Player : MonoBehaviour
{
    private const float distance = 0.5f;
    private const float horizontalSpeed = 15.0f;

    private Vector2 startInput;
    private Vector2 endInput;

    private int min;
    private int max;

    private int targetLine;

    private AudioSource SfxSource { get; set; }

    private void Awake()
    {
        targetLine = 2;

        SfxSource = GameObject.Find("SFXSource").GetComponent<AudioSource>();

        MinMaxTrigger.OnTriggerEnter += (_min, _max) =>
        {
            min = _min;
            max = _max;
        };
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            startInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if(Input.GetMouseButtonUp(0))
        {
            endInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            var lenght = Vector2.Distance(startInput, endInput);
            if(lenght <= distance)
            { 
                return; 
            }

            var direction = (endInput - startInput).normalized;

            var swipeIsLeft = Vector2.Dot(direction, Vector2.left) > 0;
            var swipeIsRight = Vector2.Dot(direction, Vector2.right) > 0;

            if(swipeIsLeft)
            {
                targetLine--;
            }
            else if(swipeIsRight)
            {
                targetLine++;
            }

            if(targetLine <= min)
            {
                targetLine = min;
            }

            if(targetLine >= max)
            {
                targetLine = max;
            }
        }

        var x = GetTargetPositionX();
        var y = transform.position.y;

        transform.position = Vector2.MoveTowards(transform.position, new Vector2(x, y), horizontalSpeed * Time.deltaTime);
    }

    private float GetTargetPositionX()
    {
        return targetLine switch
        {
            0 => -1.598f,
            1 => -0.813f,
            2 => 0.0f,
            3 => 0.813f,
            4 => 1.598f
        };
    }
}
