using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class oyuncuTiklama : MonoBehaviour
{
    public GameObject gameo;
    public tahtaOyunu oyunum;
    public int pozisyon = 0, kazanan, temp, ai_pozisyon;
    public bool kontrol;
    public static bool bitti_mi = false;
    public Text siradaki, kazanan_x, kazanan_o;
    public int derinlik;
    Renderer rend;
    public int[] tahtalar;
    void Start()
    {
        oyunum = GameObject.FindObjectOfType<tahtaOyunu>();
        
        for(int i = 0; i < oyunum.matrix.Length; i++)
        {
            tahtalar[i] = i;
        }
    }


    private void OnMouseDown()
    {
        if (!bitti_mi) { 
        var olusturucum = this.gameObject.GetComponent<Renderer>() as Renderer;

        kontrol = oyunum.dolumu(pozisyon);
        // seçilen kordinat boş ise
        if (kontrol)
        {
            olusturucum.material.SetTexture("_MainTex", oyunum.getOyuncuTexture());
            oyunum.oyuncuOynadi(pozisyon);
                Debug.Log("ben"+derinlik);
            temp = oyunum.oyunbitti();
            kazanan = oyunum.degerlendirme(temp, derinlik);
            if (kazanan == 10 - derinlik)
            {
                oyunum.mesaj.text = "kazanan x";
                bitti_mi = true;
            }
            else if (kazanan == -1 && !oyunum.tahta_kontrol())
            {
                oyunum.mesaj.text = "berabere";
            }
            else
            {
                derinlik=derinlik+1;
                ai_pozisyon = oyunum.en_iyi_hareket_bulma();
                oyunum.mesaj.text = ai_pozisyon.ToString();
                gameo = GameObject.Find("P" + tahtalar[ai_pozisyon]);

                if (gameo != null)
                {
                    rend = gameo.GetComponent<Renderer>();
                    rend.material.SetTexture("_MainTex", oyunum.getOyuncuTexture());
                }
                oyunum.oyuncuOynadi(ai_pozisyon);




                    Debug.Log("pc" + derinlik);
                    
                temp = oyunum.oyunbitti();
                kazanan = oyunum.degerlendirme(temp, derinlik);
                if (kazanan == derinlik - 10)
                {
                    oyunum.mesaj.text = "kazanan o";
                    bitti_mi = true;
                }
                else if (kazanan == -1 && !oyunum.tahta_kontrol())
                {
                    oyunum.mesaj.text = "berabere";
                }
            }

        }
        else
        {
            oyunum.mesaj.text = "kordinant dolu";
        }



    }


    }


}
