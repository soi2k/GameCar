using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class FadeinAlphabetController : MonoBehaviour, IFadeinAlphabet
{
    public void FadeinAlphabet()
    {
        StartCoroutine(Fadein());
    }
    private IEnumerator Fadein()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < transform.childCount; i++)
        {
            SpriteRenderer sprite = transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>();
            Color coler = sprite.color;
            coler.b = 0;
            sprite.color = coler;
            if (i == 0) SoundManager.Instance.PlaySound(SoundType.m);
            else if (i == 1) SoundManager.Instance.PlaySound(SoundType.a);
            else if (i == 2) SoundManager.Instance.PlaySound(SoundType.t);
            yield return new WaitForSeconds(1f);
        }
        SoundManager.Instance.PlaySound(SoundType.mat);
    }
}

