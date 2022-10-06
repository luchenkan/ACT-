using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingManager : MonoBehaviour
{
    public static PostProcessingManager Instance;
    private PostProcessVolume volume;
    private ChromaticAberration chromaticAberration;

    private void Awake()
    {
        Instance = this;
        volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings<ChromaticAberration>(out chromaticAberration);
    }

    // 色差效果
    public void ChromaticAberrationEF()
    {
        // 这里属于正常操作，协程相关的调用之前都要确认一次
        StopCoroutine("StartChromaticAberrationEF");
        StartCoroutine("StartChromaticAberrationEF");
    }

    IEnumerator StartChromaticAberrationEF()
    {
        while(chromaticAberration.intensity < 0.25f)
        {
            yield return new WaitForSeconds(0.01f);
            chromaticAberration.intensity.value += 0.02f;
        }
        yield return StopChromaticAberrationEF();
    }

    IEnumerator StopChromaticAberrationEF()
    {
        while (chromaticAberration.intensity > 0)
        {
            yield return new WaitForSeconds(0.01f);
            chromaticAberration.intensity.value -= 0.02f;
        }
    }
}
