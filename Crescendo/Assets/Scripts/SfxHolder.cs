using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SfxHolder : MonoBehaviour, IPointerEnterHandler
{
    public AudioSource sfx;
    public AudioClip myClip;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (sfx != null)
        {
            if (myClip != null)
            {

                if (!sfx.isPlaying)
                {
                    sfx.Stop();
                    sfx.time = 0;
                    sfx.clip = myClip;
                    sfx.Play();
                }
                else
                {
                    StartCoroutine(PlayScheduled());
                }
            }
        }
    }

    IEnumerator PlayScheduled()
    {
        float delay = (sfx.clip.length - sfx.time) * 0.5f; ;
        while (delay > 0)
        {
            if (sfx.isPlaying == false)
            {
                break;
            }
            sfx.time = Mathf.Lerp(sfx.time, sfx.clip.length, delay);
            yield return null;
        }
        sfx.Stop();
        sfx.time = 0;
        sfx.clip = myClip;
        sfx.Play();

    }


}
