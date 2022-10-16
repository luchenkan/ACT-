using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2_Controller : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hitAudioClip;
    public AudioClip efAudioClip;

    // 怪物-上一次经历的攻击波数
    private Dictionary<GameObject, int> monsters = new Dictionary<GameObject, int>();

    private int currNum = 0;

    void Start()
    {
        // 协程进行波数的计算
        StartCoroutine("CalNum");

        Invoke("Destroy", 3);
    }

    IEnumerator CalNum()
    {
        audioSource.PlayOneShot(efAudioClip);
        while(currNum < 3)
        {
            yield return new WaitForSeconds(0.1f);
            currNum++;
            audioSource.PlayOneShot(efAudioClip);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Monster")
        {
            if(monsters.ContainsKey(other.gameObject) == false)
            {
                // 收到攻击时添加对应的波数
                monsters.Add(other.gameObject, currNum);
                other.GetComponent<Monster_Controller>().Hurt(0.3f, transform, new Vector3(0, 0.3f, 1), 0.2f, 10);
                audioSource.PlayOneShot(hitAudioClip);
            }
            else
            {
                if (currNum == monsters[other.gameObject])
                {
                    return;
                }
                else
                {
                    // 更新
                    monsters[other.gameObject] = currNum;
                    other.GetComponent<Monster_Controller>().Hurt(0.3f, transform, new Vector3(0, 0.3f, 1), 0.2f, 10);
                    audioSource.PlayOneShot(hitAudioClip);
                }
            }
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
        StopAllCoroutines();
    }
}
