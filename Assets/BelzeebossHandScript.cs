using System.Collections;
using UnityEngine;

public class BelzeebossHandScript : MonoBehaviour
{
    private Vector3 handPosition;
    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    public void MoveHandToSmash()
    {
        var pos = transform.position;
        handPosition = new Vector3(pos.x, pos.y, pos.z);
        transform.position = new Vector3(pos.x, 11.25f, pos.z);
    }

    public void ResetHandPosition()
    {
        transform.position = handPosition;
    }

    public void MoveHandToRest()
    {
        transform.position = new Vector3(transform.position.x, 12, transform.position.z);
    }

    public void CutsceneCancel()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        MoveHandToRest();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("AllyProjectile"))
            StartCoroutine(StrobeColorHelper(0, 5, sprite, Color.white, new Color(1, 1, 1, 0.5f)));
    }

    private IEnumerator StrobeColorHelper(int _i, int _stopAt, SpriteRenderer _mySprite, Color _color, Color _toStrobe)
    {
        if (_i <= _stopAt)
        {
            if (_i % 2 == 0)
                _mySprite.color = _toStrobe;
            else
                _mySprite.color = _color;

            yield return new WaitForSeconds(.1f);
            StartCoroutine(StrobeColorHelper((_i + 1), _stopAt, _mySprite, _color, _toStrobe));
        }
    }
}
