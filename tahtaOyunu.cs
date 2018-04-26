using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tahtaOyunu : MonoBehaviour {

    // oyuncular[0] = x ve x için matrise 0 yazılır
    // oyuncular[1] = o ve o için matrise 1 yazılır.
    // x için kazanç +10, o için kazanç -10 düşünelim
    public Texture[] oyuncular;
    public int[] matrix;
    public int oyuncu = 0;
    public Text mesaj;
	// Use this for initialization
	void Start () {
		for(int i=0; i < matrix.Length; i++)
        {
            matrix[i] = -1;
        }
	}
	
    //sıradaki oyuncuyu 0 , 1 cinsinden döndürür. 
	public int oynayanOyuncu()
    {
        return oyuncu;
    }

    //kullanıcı tarafından girilen pozisyon boşmu kontrol yapıyor
    public bool dolumu(int pozisyon)
    {
        if (matrix[pozisyon] == -1)
            return true;
        else
           return false;
    }
    
    //biz oynadıktan sonra oyun bittimi bakıyor.
    public void oyuncuOynadi(int pozisyon)
    {
        
        matrix[pozisyon] = oyuncu;
        
            oyuncu++;
            if (oyuncu > 1)
                oyuncu = 0;  
    }
    //tahtada boş yer varmı kontrolü yapıyor.
    public bool tahta_kontrol()
    {
        for(int i = 1; i < matrix.Length; i++)
        {
            if (matrix[i] == -1)
                return true;
        }
        return false;
    }

    public int minmax(bool siradaki_oyuncu,int derinlik)
    {
        int kazanan = oyunbitti();
        int skor = degerlendirme(kazanan,derinlik);
        int temp;
        //maximize oyuncusu kazanmıştır yani x
        if (skor == 10 - derinlik)
            return skor;
        //minimize oyuncusu kazanmıştır yani o
        if (skor == derinlik - 10)
            return skor;
        //tahta dolmuş ise oyun berabere
        if (tahta_kontrol() == false)
            return 0;

        //x icin oyun yani maximize için
        if (siradaki_oyuncu)
        {
            int en_max = -1000; // x için max hamleyi seçer.
            for(int i=0; i < matrix.Length; i++)
            {
                if(matrix[i] == -1)
                {
                    matrix[i] = 0;
                    temp = minmax(false,derinlik+1); // bir sonraki hamle için o yu seçer.
                    if (en_max < temp)
                        en_max = temp;
                    matrix[i] = -1;
                }
            }
            return en_max;
        }else
        {
            int en_min = 1000; // o için minimum hamleyi seçer
            for (int i = 0; i < matrix.Length; i++)
            {
                if (matrix[i] == -1)
                {
                    matrix[i] = 1;
                    temp = minmax(true,derinlik+1); // bir sonraki hamle için x yu seçer.
                    if (en_min > temp)
                        en_min = temp;
                    matrix[i] = -1;
                }
            }
            return en_min;

        }

    }

    // o için yani yapayzeka için (min) hareketi bulma
    public int en_iyi_hareket_bulma()
    {
        int en_min = 1000;
        int pozisyon = -1;
        int hareket_degeri = 0;
        for(int i=0; i < matrix.Length; i++)
        {
            if(matrix[i] == -1)
            {
                matrix[i] = oyuncu;
                hareket_degeri = minmax(true,0);
                matrix[i] = -1;
                if (hareket_degeri < en_min)
                {
                    en_min = hareket_degeri;
                    pozisyon = i;
                }
                    
            }
        }
        return pozisyon;
    }

    public int degerlendirme(int kazanan,int derinlik)
    {

        // x kazanmış ise
        if(kazanan == 0)
        {
            
            return 10 - derinlik;
        } // o kazanmış ise
         else if (kazanan == 1)
        {
            
            return derinlik - 10;
        }
        else
        {
            return -1;
        }

    }

    //sıradaki oyuncuyu döndürür.
    public Texture getOyuncuTexture()
    {
       
        return oyuncular[oyuncu]; 
    }

    public int oyunbitti()
    {
        // satır kontrolü
        if (matrix[0] == matrix[1] && matrix[1] == matrix[2] && matrix[0] != -1)
            return matrix[0];
        if (matrix[3] == matrix[4] && matrix[4] == matrix[5] && matrix[3] != -1)
            return matrix[3];
        if (matrix[6] == matrix[7] && matrix[7] == matrix[8] && matrix[6] != -1)
            return matrix[6];

        // sutun kontrolü
        if (matrix[0] == matrix[3] && matrix[3] == matrix[6] && matrix[0] != -1)
            return matrix[0];
        if (matrix[1] == matrix[4] && matrix[4] == matrix[7] && matrix[1] != -1)
            return matrix[1];
        if (matrix[2] == matrix[5] && matrix[5] == matrix[8] && matrix[2] != -1)
            return matrix[2];

        //çapraz kontrolü
        if (matrix[0] == matrix[4] && matrix[4] == matrix[8] && matrix[0] != -1)
            return matrix[0];
        if (matrix[2] == matrix[4] && matrix[4] == matrix[6] && matrix[2] != -1)
            return matrix[2];

        //berabere demek
        return -1;

    }
}
