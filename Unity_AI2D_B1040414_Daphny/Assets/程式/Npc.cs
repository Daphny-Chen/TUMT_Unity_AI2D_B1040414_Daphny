using UnityEngine;
using UnityEngine.UI;   // 引用 介面 API
using System.Collections;

public class Npc : MonoBehaviour
{


    #region 欄位
    // 定義列舉
    // 修飾詞 列舉 列舉名稱 { 列舉內容, .... }
    public enum state
    {
        // 一般、尚未完成、完成
        start, notComplete, complete
    }
    // 使用列舉
    // 修飾詞 類型 名稱
    public state _state;

    [Header("對話")]
    public string sayStart = "年輕的冒險者,你為何踏入這迷幻洞穴?不過也罷,那就幫幫我吧!請幫我找到散落在洞穴中的10個小星星吧!";
    public string sayNotComplete = "還沒找10個小星星哦?!別慌，血快沒了就吃點胡蘿蔔補血吧?";
    public string sayComplete = "小星星夠了!!!";
    [Header("對話速度")]
    public float speed = 500f;
    [Header("任務相關")]
    public bool complete;
    public int countPlayer;
    public int countFinish = 10;
    [Header("介面")]
    public GameObject objCanvas;
    public Text textSay;
    #endregion
    public AudioClip soundSay;

    private AudioSource aud;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    // 2D 觸發事件
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 如果碰到物件為"主角"
        if (collision.name == "主角")
            Say();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "主角")
            SayClose();
    }

    /// <summary>
    /// 對話：打字效果
    /// </summary>
    private void Say()
    {
        objCanvas.SetActive(true);
        StopAllCoroutines();
 
        if (countPlayer >= countFinish) _state = state.complete;

        // 判斷式
        switch (_state)
        {
            case state.start:
                StartCoroutine(ShowDialog(sayStart));           // 開始對話
                _state = state.notComplete;
                break;
            case state.notComplete:
                StartCoroutine(ShowDialog(sayNotComplete));     // 開始對話未完成
                break;
            case state.complete:
                StartCoroutine(ShowDialog(sayComplete));        // 開始對話完成
                break;
        }
    }

    private IEnumerator ShowDialog(string say)
    {
        textSay.text = "";                              // 清空文字

        for (int i = 0; i < say.Length; i++)       // 迴圈跑對話.長度
        {
            textSay.text += say[i].ToString();     // 累加每個文字
            aud.PlayOneShot(soundSay, 0.6f);
            yield return new WaitForSeconds(speed);     // 等待
        }
    }



    /// <summary>
    /// 關閉對話
    /// </summary>
    private void SayClose()
    {
        StopAllCoroutines();
        objCanvas.SetActive(false);
    }
    /// <summary>
    /// 玩家取得道具
    /// </summary>
    public void PlayerGet()
    {
        countPlayer++;
    }
}
