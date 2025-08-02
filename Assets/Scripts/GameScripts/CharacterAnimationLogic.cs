using System.Collections;
using UnityEngine;

public class CharacterAnimationLogic : MonoBehaviour
{
    [SerializeField] private Sprite[] characterSprites;

    private bool is_animating = false;

    public void StartAnimation()
    {
        if (!is_animating)
            StartCoroutine(AnimationStart());
        is_animating = true;
    }

    public void StopAnimation()
    {
        if (!is_animating) return;
        StopAllCoroutines();
        GetComponent<SpriteRenderer>().sprite = characterSprites[0];
        is_animating = false;
    }

    public void SetCharacterSprites(Sprite[] sprites)
    {
        characterSprites = sprites;
    }

    private IEnumerator AnimationStart()
    {
        while (true)
        {
            GetComponent<SpriteRenderer>().sprite = characterSprites[1];
            yield return new WaitForSeconds(0.5f);
            GetComponent<SpriteRenderer>().sprite = characterSprites[2];
            yield return new WaitForSeconds(0.5f);
        }
    }
}
